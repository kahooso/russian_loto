using System;
using System.Drawing;
using System.Windows.Forms;

namespace RussianLoto
{
    public partial class SettingsForm : Form
    {

        private Color main_color;

        private Color second_color;

        public Color MainColor { get { return main_color; } }

        public Color SecondColor { get { return second_color; } }

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            colorComboBox.SelectedIndex = colorComboBox.FindString(Properties.Settings.Default.lastTheme);
            colorComboBox_SelectedIndexChanged(sender, e);
            InitializeStyles();
        }

        private void InitializeStyles()
        {
            Font = new Font("Roboto", 14, FontStyle.Regular);

            this.BackColor = main_color;

            this.panel1.BackColor = second_color;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void colorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (colorComboBox.SelectedItem.ToString()) {
                case "Арктика":
                    main_color = Color.SkyBlue;
                    second_color = Color.AliceBlue;
                    Properties.Settings.Default.lastTheme = "Арктика";
                    break;
                case "Пустыня":
                    main_color = Color.LightGoldenrodYellow;
                    second_color = Color.LightYellow;
                    Properties.Settings.Default.lastTheme = "Пустыня";
                    break;
                case "Обычная":
                    main_color = Color.FromArgb(222, 183, 179);
                    second_color = Color.FromArgb(137, 170, 167);
                    Properties.Settings.Default.lastTheme = "Обычная";
                    break;
            }

            Properties.Settings.Default.Save();
        }
    }
}