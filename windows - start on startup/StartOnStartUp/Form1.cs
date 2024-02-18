using Microsoft.Win32;

namespace StartOnStartUp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            string filename = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;

            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            rk.SetValue(filename, Application.ExecutablePath);
        }
    }
}
