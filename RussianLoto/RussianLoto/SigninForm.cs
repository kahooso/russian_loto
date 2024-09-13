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
        }

        private void InitializeFont()
        {
            this.Font = new Font("Robot", 14, FontStyle.Regular);
        }
}
}
