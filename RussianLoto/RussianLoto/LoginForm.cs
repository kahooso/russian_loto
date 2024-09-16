using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace RussianLoto
{
    public partial class LoginForm : Form
    {

        private Database database = new Database();

        public LoginForm()
        {
            InitializeComponent();
            InitializeFont();
            InitializeStyles();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void InitializeStyles()
        {
            Font = new Font("Roboto", 14, FontStyle.Regular);
            
            BackColor = Color.LightBlue;

            loginTextBox.Font = new Font("Roboto", 12);
            loginTextBox.BackColor = Color.White;
            loginTextBox.ForeColor = Color.DarkBlue;
            loginTextBox.BorderStyle = BorderStyle.FixedSingle;

            passwordTextBox.BackColor = Color.White;
            passwordTextBox.ForeColor = Color.DarkBlue;
            passwordTextBox.BorderStyle = BorderStyle.FixedSingle;
            passwordTextBox.UseSystemPasswordChar = true; 

            // Стили для кнопки входа
            loginButton.Font = new Font("Roboto", 14, FontStyle.Bold);
            loginButton.BackColor = Color.DarkBlue;
            loginButton.ForeColor = Color.White;
            loginButton.FlatStyle = FlatStyle.Flat;

            signInLabel.Font = new Font("Roboto", 10, FontStyle.Underline);
            signInLabel.ForeColor = Color.DarkBlue;
            signInLabel.LinkColor = Color.Blue;
            signInLabel.ActiveLinkColor = Color.Red;
            signInLabel.LinkBehavior = LinkBehavior.HoverUnderline;
        }

        private void InitializeFont() { Font = new Font("Roboto", 14, FontStyle.Regular); }

        private void loginButton_Click(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter data_adapter = new SqlDataAdapter();
                DataTable data_table = new DataTable();
                string query_string = $"SELECT ID, [login], [password], win_amount, balance FROM Players WHERE [login] = '{loginTextBox.Text}' AND [password] = '{passwordTextBox.Text}'";
                SqlCommand sql_command = new SqlCommand(query_string, database.get_connection());
                data_adapter.SelectCommand = sql_command;
                data_adapter.Fill(data_table);
                if (data_table.Rows.Count == 1)
                {
                    var user = new User(data_table.Rows[0].ItemArray[0].ToString(), data_table.Rows[0].ItemArray[1].ToString(), Convert.ToInt32(data_table.Rows[0].ItemArray[2].ToString()), Convert.ToInt32(data_table.Rows[0].ItemArray[3].ToString()));
                    MessageBox.Show("Вы успешно авторизовались!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MainForm main_form = new MainForm(user);
                    this.Hide();
                    main_form.ShowDialog();
                    this.Show();
                }
                else MessageBox.Show("Вы не авторизовались!", "Авторазицаия", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception)
            {
                MessageBox.Show("Что-то пошло не так! Повторите попытку.", "Авторазицаия", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void signInLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SigninForm signinForm = new SigninForm();

            this.Hide();
            signinForm.ShowDialog();
            this.Show();
        }
    }
}
