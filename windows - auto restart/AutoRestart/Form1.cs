using System.Diagnostics;

namespace AutoRestart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Process.Start("shutdown", "/r /t 0 /f");
        }
    }
}
