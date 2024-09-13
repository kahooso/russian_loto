using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RussianLoto
{
    public partial class LoginForm : Form
    {

        private Player current_player;
        private Database database = new Database();

        public LoginForm()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            if (loginTextBox.Text.Length != 0 && passwordTextBox.Text.Length != 0)
            {
                current_player = new Player(loginTextBox.Text, passwordTextBox.Text);
                MainForm mainForm = new MainForm(current_player);

                this.Hide();
                mainForm.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Пожалуйста заполните данные правильно!", "Авторизация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
