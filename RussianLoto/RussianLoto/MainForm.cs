using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RussianLoto
{
    public partial class MainForm : Form
    {

        private Database database = new Database();
        
        private Player current_player;

        public MainForm(Player current_player)
        {
            this.current_player = current_player;

            InitializeComponent();
        }
    }
}
