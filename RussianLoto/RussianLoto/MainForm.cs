using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace RussianLoto
{
    public partial class MainForm : Form
    {

        private Database database = new Database();

        private Card[] cards = new Card[] { new Card() };

        private User current_player;

        private Random random = new Random();

        private HashSet<Int32> drawnNumbers = new HashSet<Int32>();

        private static int current_barrel_number = 0;

        private Difficulty difficulty;

        private int win_bonus_pay;

        private SettingsForm settingsForm;

        private List<Barrel> barrels = new List<Barrel>();
        public MainForm(User current_user)
        {
            InitializeComponent();
            InitializeFont();
            InitializeDifficulty();
            InitializeColors();
            InitializePlayer(current_user);
            InitializeBarrels();
            InitializeCards();
            InitializeDataGridView();
            InitializeOtherComponents();
        }

        // load 

        private void MainForm_Load(object sender, EventArgs e) { }

        // initializations

        private void InitializeDifficulty() { this.difficulty = Difficulty.Easy; }

        private void InitializeCards() { cards[0] = new Card(); }

        private void InitializeOtherComponents()
        {
            balanceToolStripLabel.Text = ("Баланс: " + current_player.getBalance() + " €").Trim().ToString();
            nameToolStripLabel.Text = ("Логин: " + current_player.getLogin()).Trim().ToString();
        }

        private void InitializeBarrels()
        {
            for (Int32 i = 1; i <= 90; ++i)
            {
                barrels.Add(new Barrel(i));
            }
            ShakeTheBagOfBarrels();
        }

        private void ShakeTheBagOfBarrels()
        {
            this.barrels = barrels.OrderBy(a => random.Next()).ToList();
        }

        private void InitializeTimer()
        {
            switch (difficulty)
            {
                case Difficulty.Easy:
                    timer.Interval = 100;
                    win_bonus_pay = 300;
                    break;
                case Difficulty.Medium:
                    timer.Interval = 4000;
                    win_bonus_pay = 400;
                    break;
                case Difficulty.Hard:
                    timer.Interval = 3500;
                    win_bonus_pay = 500;
                    break;
            }
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        private void InitializeDataGridView()
        {
            cardsFlowLayoutPanel.Controls.Clear();

            for (int i = 0; i < cards.Length; ++i)
            {
                DataGridView dataGridView = new DataGridView();

                dataGridViewSettings(dataGridView);

                fillDataGridView(dataGridView, cards[i]);

                cardsFlowLayoutPanel.Controls.Add(dataGridView);
            }

            fieldVisible(false);
        }

        private void InitializePlayer(User current_user) { this.current_player = current_user; }

        private void InitializeColors()
        {
            string lastTheme = Properties.Settings.Default.lastTheme;

            Color main_color = Color.White;
            Color second_color = Color.LightGray;

            switch (lastTheme)
            {
                case "Арктика":
                    main_color = Color.SkyBlue;
                    second_color = Color.AliceBlue;
                    break;
                case "Пустыня":
                    main_color = Color.LightGoldenrodYellow;
                    second_color = Color.LightYellow;
                    break;
                case "Обычная":
                    main_color = Color.FromArgb(222, 183, 179);
                    second_color = Color.FromArgb(137, 170, 167);
                    break;
            }

            this.BackColor = main_color;
            cardsFlowLayoutPanel.BackColor = second_color;
            gameSettingsPanel.BackColor = second_color;
            toolStrip1.BackColor = second_color;
            menuStrip1.BackColor = second_color;
        }

        private void InitializeFont() { Font = new Font("Roboto", 10, FontStyle.Regular); }

        // datagridview settings

        private void fillDataGridView(DataGridView dataGridView, Card card)
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (card.numbers[row, col] == 0)
                    {
                        dataGridView.Rows[row].Cells[col].Value = "";
                    }
                    else
                    {
                        dataGridView.Rows[row].Cells[col].Value = card.numbers[row, col];
                    }
                }
            }
        }

        private void dataGridViewSettings(DataGridView dataGridView)
        {
            dataGridView.CellClick += DataGridView_CellClick;

            dataGridView.ColumnCount = 9;
            dataGridView.RowCount = 3;

            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dataGridView.RowHeadersVisible = false;
            dataGridView.ColumnHeadersVisible = false;

            dataGridView.ReadOnly = true;
            dataGridView.AllowUserToResizeColumns
                = dataGridView.AllowUserToResizeRows = false;

            dataGridView.Width = 315;
        }

        private int full_row_marked_award = 0;
        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridView dataGridView = (DataGridView)sender;

                int rowIndex = e.RowIndex;

                int columnIndex = e.ColumnIndex;

                int cardIndex = cardsFlowLayoutPanel.Controls.IndexOf(dataGridView);

                if (dataGridView.Rows[rowIndex].Cells[columnIndex].Value != null && drawnNumbers.Contains((Int32)dataGridView.Rows[rowIndex].Cells[columnIndex].Value))
                {
                    dataGridView.Rows[rowIndex].Cells[columnIndex].Style.BackColor = Color.LightGreen;

                    dataGridView.Rows[rowIndex].Cells[columnIndex].Style.ForeColor = Color.DarkGreen;

                    cards[cardIndex].marked[rowIndex, columnIndex] = true;

                    if (cards[cardIndex].IsRowComplete(rowIndex))
                    {
                        switch (rowIndex)
                        {
                            case 0:
                                full_row_marked_award = 300;
                                break;
                            case 1:
                                full_row_marked_award = 600;
                                break;
                            case 2:
                                full_row_marked_award = 1200;
                                break;
                        }
                        MessageBox.Show($"Строка {rowIndex + 1} на карточке {cardIndex + 1} заполнена!", "Поздравляем!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.current_player.setBalance(this.current_player.getBalance() + full_row_marked_award + win_bonus_pay);
                        this.current_player.setWinAmount(this.current_player.getWinAmount() + full_row_marked_award + win_bonus_pay);
                    }

                    checkIfAnyCardComplete();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Это не правильное число!", "=(", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void checkIfAnyCardComplete()
        {
            foreach (Card card in this.cards)
            {
                if (isCardComplete(card))
                {
                    this.current_player.setBalance(this.current_player.getBalance() + full_row_marked_award + win_bonus_pay);
                    this.current_player.setWinAmount(this.current_player.getWinAmount() + full_row_marked_award + win_bonus_pay);
                    TheEnd();
                }
            }
        }

        private bool isCardComplete(Card card)
        {
            for (int row = 0; row < 3; ++row)
            {
                for (int col = 0; col < 3; ++col)
                {
                    if (card.numbers[row, col] != 0 && !card.marked[row, col])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        // click events

        private void fieldVisible(bool isVisible)
        {
            foreach (Control control in cardsFlowLayoutPanel.Controls)
            {
                control.Visible = isVisible;
            }
        }

        private void nextRoundButton_Click(object sender, EventArgs e)
        {
            drawnNumbers.Clear();
            if (!this.current_player.isPaid(cards_count))
            {
                MessageBox.Show("К сожалению у вас не хватает средств для покупки билета!", "=(", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                this.current_player.setBalance(this.current_player.getBalance() - 50 * cards_count);
                InitializeOtherComponents();
                gameSettingsPanel.Enabled = false;
                InitializeTimer();
                fieldVisible(true);
                Play();
            }
        }

        private void Play()
        {
            int drawnNumber;
            do
            {
                drawnNumber = random.Next(1, 91);
            } while (drawnNumbers.Contains(drawnNumber));

            drawnNumbers.Add(drawnNumber);

            drawnNumberLabel.Text = "Бочонки -> " + string.Join(", ", drawnNumbers.Reverse());
            current_barrel_number = drawnNumber;
        }

        private void TheEnd()
        {
            timer.Stop();
            MessageBox.Show("Игра закончена!", "Игра", MessageBoxButtons.OK, MessageBoxIcon.Information);
            gameSettingsPanel.Enabled = true;
            fieldVisible(false);
            CalculateResults();
        }

        private void CalculateResults()
        {
        }

        // tick event

        private void timer_Tick(object sender, EventArgs e)
        {
            if (drawnNumbers.Count < 90)
                Play();
            else TheEnd();
        }

        private static int cards_count = 0;

        private void allChanges_RadioButton(object sender, EventArgs e)
        {

            RadioButton current_radiobutton = (RadioButton)sender;


            switch (current_radiobutton.Name)
            {
                case "oneCardRadioButton":
                    cards_count = 1;
                    break;
                case "twoCardRadioButton":
                    cards_count = 2;
                    break;
                case "threeCardRadioButton":
                    cards_count = 3;
                    break;
                case "fourthCardRadioButton":
                    cards_count = 4;
                    break;
            }

            Array.Resize(ref cards, cards_count);

            for (int i = 0; i < cards_count; ++i)
            {
                cards[i] = new Card();
            }

            InitializeDataGridView();

        }

        private void changeDifficulty_RadioButtons(object sender, EventArgs e)
        {
            RadioButton current_radiobutton = (RadioButton)sender;

            switch (current_radiobutton.Name)
            {
                case "easyRadioButton":
                    this.difficulty = Difficulty.Easy;
                    MessageBox.Show("Вы выбрали легкую сложность", "Игра", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
                case "mediumRadioButton":
                    this.difficulty = Difficulty.Medium;
                    MessageBox.Show("Вы выбрали среднюю сложность", "Игра", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
                case "hardRadioButton":
                    this.difficulty = Difficulty.Hard;
                    MessageBox.Show("Вы выбрали тяжёлую сложность", "Игра", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
            }
        }

        private void appartmentButton_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            int userId = GetUserIdByLogin(this.current_player.getLogin());

            if (userId != -1) // Если удалось получить ID
            {
                string change_query = $"UPDATE Players SET balance = {this.current_player.getBalance()}, win_amount = {this.current_player.getWinAmount()} WHERE ID = {userId}";

                using (SqlCommand command = new SqlCommand(change_query, database.get_connection()))
                {
                    try
                    {
                        database.open_connection();
                        command.ExecuteNonQuery(); // Выполняем запрос на обновление
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка при обновлении данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        database.close_connection();
                    }
                }
            }
            else
            {
                MessageBox.Show("Ошибка: ID пользователя не найден!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetUserIdByLogin(string login)
        {
            int userId = -1;
            string query = "SELECT ID FROM Players WHERE login = @login";

            using (SqlCommand command = new SqlCommand(query, database.get_connection()))
            {
                command.Parameters.AddWithValue("@login", login);

                try
                {
                    database.open_connection();
                    // Используем ExecuteScalar для получения одного значения
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value) // Проверяем, есть ли результат
                    {
                        userId = Convert.ToInt32(result); // Преобразуем результат в int
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка при получении ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    database.close_connection();
                }
            }

            return userId;
        }


        private void exitToolStripButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены что хотите выйти? Процесс будет не сохранен!", "Игра", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
               this.Close();
        }

        private void informationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Традиционная российская игра, о существовании которой знает каждый. Благодаря простым правилам и азарту эта игра завоевала огромную популярность и встала в один ряд с такими развлечениями для большой весёлой компании, как домино и карты.\n\n\nКосовский Семён Андреевич 419/7", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settingsForm = new SettingsForm();

            this.Hide();

            settingsForm.ShowDialog();

            this.Show();

            Color main_color = settingsForm.MainColor;

            Color second_color = settingsForm.SecondColor;

            this.BackColor = main_color;

            cardsFlowLayoutPanel.BackColor = second_color;
            gameSettingsPanel.BackColor = second_color;
            toolStrip1.BackColor = second_color;
            menuStrip1.BackColor = second_color;
        }

        private void сохранитьКартыToolStripMenuItem_Click(object sender, EventArgs e)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            saveFileDialog.Title = "Сохранить карты";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {

                string filePath = saveFileDialog.FileName;

                try
                {

                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        for (int i = 0; i < cards.Length; i++)
                        {
                            writer.WriteLine($"Карта {i + 1}:");

                            for (int row = 0; row < 3; row++)
                            {
                                string rowData = "";

                                for (int col = 0; col < 9; col++)
                                {
                                    int number = cards[i].numbers[row, col];

                                    rowData += (number == 0 ? "-" : number.ToString()) + " ";
                                }

                                writer.WriteLine(rowData.TrimEnd());
                            }

                            writer.WriteLine();
                        }
                    }

                    MessageBox.Show("Карты успешно сохранены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении карт: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void freeButton_Click(object sender, EventArgs e)
        {
            this.current_player.setBalance(this.current_player.getBalance() + 1500);
            this.current_player.setWinAmount(this.current_player.getWinAmount() + 1500);
            InitializeOtherComponents();
        }
    }
}