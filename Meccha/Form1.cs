using Meccha.Board;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Speech.Synthesis;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Meccha
{
    public partial class Form1 : Form
    {
        KeyboardHook Hook;
        BaseBoard board;

        public float Volume => volumeBar.Value / 100f;

        string programPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public Form1()
        {
            InitializeComponent();

            populateBoards();
            comboBox1.SelectedIndex = 0;
            //setBoard(boardTypes[0]);

            Hook = new KeyboardHook();
            Hook.Hook();

            Hook.KeyDown += (s, e) => playSound(board.OnKeyDown(e.KeyCode));
            Hook.KeyUp += (s, e) => playSound(board.OnKeyUp(e.KeyCode));
        }

        private void playSound(byte[] s)
        {
            if (s == null)
                return;

            MemoryStream ms = new MemoryStream(s);
            WaveFileReader r = new WaveFileReader(ms);
            WaveOut player = new WaveOut();

            player.Volume = Volume;
            player.Init(r);
            player.Play();

            player.PlaybackStopped += (o, ex) =>
            {
                ms.Dispose();
                r.Dispose();
                player.Dispose();
            };
        }

        private void populateBoards()
        {
            IEnumerable<string> dlls = Directory.GetFiles(programPath)
                .Where(f => f.EndsWith(".dll") && Path.GetFileNameWithoutExtension(f).StartsWith("Meccha.Board."));
            foreach (string dll in dlls)
            {
                Assembly asm = Assembly.LoadFile(dll);
                Type t = asm.GetExportedTypes().First(a => typeof(BaseBoard).IsAssignableFrom(a));

                comboBox1.Items.Add(new DisplayBoard(this, t));
            }

            if (comboBox1.Items.Count == 0)
            {
                MessageBox.Show("No board dlls found. Please ensure you have some boards installed before using Meccha.");
                Environment.Exit(1);
                return;
            }

            if (comboBox1.Items.Count < 2)
                comboBox1.Enabled = false;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Hook.Dispose();

            base.OnClosing(e);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            setBoard((DisplayBoard)comboBox1.SelectedItem);
            GC.Collect();
        }

        private void setBoard(DisplayBoard bt)
        {
            if (bt != null)
                board = bt.CreateInstance();
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
