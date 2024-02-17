using System.Runtime.InteropServices;

namespace RemoteCamShot
{
    public partial class Form1 : Form
    {
        private Camera camera;
        private PictureBox pictureBox;

        public Form1()
        {
            InitializeComponent();

            InitializeCameraCaptureForm();
            camera = new Camera();
        }

        private void InitializeCameraCaptureForm()
        {
            pictureBox = new PictureBox();
            pictureBox.Size = new System.Drawing.Size(640, 480);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            Controls.Add(pictureBox);

            var captureButton = new Button();
            captureButton.Text = "Capture";
            captureButton.Click += CaptureButton_Click;
            captureButton.Dock = DockStyle.Bottom;
            Controls.Add(captureButton);
        }

        private void CaptureButton_Click(object sender, EventArgs e)
        {
            string fileName = camera.NewFileNAME();

            camera.startCAM();

            camera.captureCAM(fileName);

            camera.stopCAM();

            pictureBox.ImageLocation = fileName;
        }
    }
    public class Camera
    {
        [DllImport("kernel32.dll")]
        private static extern void Sleep(int dwMilliseconds);

        [DllImport("kernel32.dll")]
        private static extern int Beep(int dwFreq, int dwDuration);

        [DllImport("avicap32.dll", EntryPoint = "capCreateCaptureWindowW")]
        private static extern int capCreateCaptureWindow(string lpszWindowName, int dwStyle, int x, int y, int nWidth, int nHeight, int hWndParent, int nID);
        private const int WS_VISIBLE = 0x10000000;
        private const int WS_CHILD = 0x40000000;

        [DllImport("user32.dll", EntryPoint = "SendMessageW")]
        private static extern int SendMessage(int hwnd, int wMsg, int wParam, int lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessageW")]
        private static extern int SendMessageFL(int hwnd, int wMsg, int wParam, string lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessageW")]
        private static extern int SendMessageSD(int hwnd, int wMsg, string wParam, int lParam);

        private const int WM_USER = 0x400;
        private const int WM_CAP_DRIVER_CONNECT = (WM_CAP_START + 10);
        private const int WM_CAP_START = WM_USER;
        private const int WM_CAP_FILE_SAVEDIBA = (WM_CAP_START + 25);
        private const int WM_CAP_SET_SCALE = WM_USER + 53;
        private const int WM_CAP_SET_PREVIEW = WM_USER + 50;
        private const int WM_CAP_SET_PREVIEWRATE = WM_USER + 52;
        private const int WM_CAP_FILE_SAVEDIB = WM_USER + 25;
        private const int WM_CAP_DRIVER_DISCONNECT = WM_USER + 11;

        private string _camtitle;   // a pointer to HDCAM
        private int hWebcam;
        private const int nDevice = 0;
        private const int nFPS = 50;
        private string _filename;   // image.bmp filename



        public int getCAM(string cam_title, int cam_x, int cam_y, int cam_width, int cam_height, IntPtr HWNDparent, int cam_ID)
        {
            _camtitle = cam_title;
            hWebcam = capCreateCaptureWindow(cam_title, WS_VISIBLE + WS_CHILD, cam_x, cam_y, cam_width, cam_height, HWNDparent.ToInt32(), cam_ID);
            return hWebcam;
        }

        public string NewFileNAME()
        {
            DateTime DT = new DateTime();
            DT.Date.ToString();
            return "file-" + DT.Date.ToString();

        }
        public void startCAM()
        {
            SendMessage(hWebcam, WM_CAP_DRIVER_CONNECT, nDevice, 0);
            SendMessage(hWebcam, WM_CAP_SET_SCALE, 1, 0);
            SendMessage(hWebcam, WM_CAP_SET_PREVIEWRATE, nFPS, 0);
            SendMessage(hWebcam, WM_CAP_SET_PREVIEW, 1, 0);
        }

        public void captureCAM(string BMPfilename)
        {
            _filename = BMPfilename;
            SendMessageFL(hWebcam, WM_CAP_FILE_SAVEDIBA, 0, _filename);
        }

        public void stopCAM()
        {
            SendMessageSD(hWebcam, WM_CAP_DRIVER_DISCONNECT, _camtitle, 0);
        }
    }
}
