using System.Runtime.InteropServices;

namespace MouseJammer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            JammingMouse();
        }

        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);

        void JammingMouse()
        {
            int currentX = Cursor.Position.X;
            int currentY = Cursor.Position.Y;

            DateTime startTime = DateTime.Now;
            // use while true to jamming the mouse forever
            //while (true)
            while (DateTime.Now - startTime < TimeSpan.FromMilliseconds(5000))
            {
                SetCursorPos(currentX, currentY);
                Thread.Sleep(10); 
                Application.DoEvents();
            }
        }
    }
}
