using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
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
            switch(difficulty)
            {
                case Difficulty.Easy:
                    timer.Interval = 5000;
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
        }
        private void InitializePlayer(User current_user) { this.current_player = current_user; }
        private void InitializeColors()
        {
        }

        private void InitializeFont() { Font = new Font("Roboto", 12, FontStyle.Regular); }

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

                int rowIndex = e.RowIndex,
                    columnIndex = e.ColumnIndex,
                    cardIndex = cardsFlowLayoutPanel.Controls.IndexOf(dataGridView);

                if (dataGridView.Rows[rowIndex].Cells[columnIndex].Value != null &&
                    (Int32)dataGridView.Rows[rowIndex].Cells[columnIndex].Value != current_barrel_number)
                {
                    dataGridView.Rows[rowIndex].Cells[columnIndex].Style.BackColor = Color.LightGreen;
                    dataGridView.Rows[rowIndex].Cells[columnIndex].Style.ForeColor = Color.DarkGreen;

                    cards[cardIndex].marked[rowIndex, columnIndex] = true;

                    if (cards[cardIndex].IsRowComplete(rowIndex))
                    {
                        switch(rowIndex)
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
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Это не правильное число!", "=(", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // click events

        private void nextRoundButton_Click(object sender, EventArgs e)
        {
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
            
            drawnNumberLabel.Text = "Последние бочки -> " + string.Join(", ", drawnNumbers.Reverse());
            current_barrel_number = drawnNumber;
        }

        private void TheEnd()
        {
            MessageBox.Show("Игра закончена!", "Игра", MessageBoxButtons.OK, MessageBoxIcon.Information);
            gameSettingsPanel.Enabled = true;
            timer.Stop();
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

            switch(current_radiobutton.Name)
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
            int userId = GetUserIdByLogin(current_player.getLogin());

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
                    userId = (int)command.ExecuteScalar(); // Получаем ID
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
            if (MessageBox.Show("Вы уверены что хотите выйти? Процесс будет не сохранен!", "Игра", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) this.Close();
        }

        private void informationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("hello");
        }
    }
}
