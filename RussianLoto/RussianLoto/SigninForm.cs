using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace RussianLoto
{
    public partial class SigninForm : Form
    {

        private Database database = new Database();

        public SigninForm()
        {
            InitializeComponent();
            InitializeFont();
            InitializeColors();
            InitializeStyles();
            InitializeComponents();
        }

        private void InitializeStyles()
        {

            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.LightCyan;

            loginTextBox.BackColor = Color.White;
            loginTextBox.ForeColor = Color.DarkBlue;
            loginTextBox.BorderStyle = BorderStyle.FixedSingle;

            passwordTextBox.BackColor = Color.White;
            passwordTextBox.ForeColor = Color.DarkBlue;
            passwordTextBox.BorderStyle = BorderStyle.FixedSingle;
            passwordTextBox.UseSystemPasswordChar = true;

            signInButton.BackColor = Color.DarkBlue;
            signInButton.ForeColor = Color.White;
            signInButton.FlatStyle = FlatStyle.Flat;
        }
        private void SigninForm_Load(object sender, EventArgs e) { passwordTextBox.UseSystemPasswordChar = true; }
        private void InitializeComponents() { StartPosition = FormStartPosition.CenterScreen; }
        private void InitializeFont() { Font = new Font("Roboto", 12, FontStyle.Regular); }
        private void InitializeColors() { BackColor = Color.LightCyan; }

        private void signInButton_Click(object sender, EventArgs e)
        {
            try
            {
                    if (CheckUser()) return;
                    string query_string = $"INSERT INTO Players([login], [password], win_amount, balance) VALUES('{loginTextBox.Text}', '{passwordTextBox.Text}', 250, 250)";
                    SqlCommand sql_command = new SqlCommand(query_string, database.get_connection());
                    database.open_connection();
                    try
                    {
                        if (Convert.ToBoolean(sql_command.ExecuteNonQuery() == 1))
                        {
                            MessageBox.Show("Вы успешно зарегистрированы!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoginForm login_form = new LoginForm();
                            this.Hide();
                            login_form.ShowDialog();
                        }
                        else MessageBox.Show("Вы не зарегистрованы! Повторите попытку.", "Регистрация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message, "Регистрация", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    database.close_connection();
            }
            catch (Exception)
            {
                MessageBox.Show("Что-то пошло не так! Повторите попытку.", "Регистрация", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CheckUser()
        {
            SqlDataAdapter sql_adapter = new SqlDataAdapter();
            DataTable data_table = new DataTable();
            string query_string = $"SELECT ID, [login], [password] FROM Players WHERE [login] = '{loginTextBox.Text.Trim().ToString()}' and [password] = '{passwordTextBox.Text.Trim().ToString()}'";
            SqlCommand sql_command = new SqlCommand(query_string, database.get_connection());
            sql_adapter.SelectCommand = sql_command;
            sql_adapter.Fill(data_table);
            if (data_table.Rows.Count > 0)
            {
                MessageBox.Show("Пользователь уже существует!", "Регистрация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            else return false;
        }

        private void SigninForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoginForm login_form = new LoginForm();
            this.Hide();
            login_form.ShowDialog();
        }
    }
}