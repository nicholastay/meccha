using Meccha.Keyboards;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
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

        BoardType[] boardTypes;
        BaseBoard board;

        public Form1()
        {
            InitializeComponent();

            Hook = new KeyboardHook();
            Hook.Hook();

            populateBoards();
            comboBox1.SelectedIndex = 0;
            //setBoard(boardTypes[0]);

            Hook.KeyDown += (s, e) => board.OnKeyDown(e);
            Hook.KeyUp += (s, e) => board.OnKeyUp(e);
        }

        private void populateBoards()
        {
            boardTypes = new BoardType[]
            {
                new BoardType(this, typeof(MxBlueBoard)),
                new BoardType(this, typeof(TtsBoard))
            };

            comboBox1.Items.AddRange(boardTypes);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Hook.Dispose();

            base.OnClosing(e);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            setBoard((BoardType)comboBox1.SelectedItem);
            GC.Collect();
        }

        private void setBoard(BoardType bt)
        {
            if (bt != null)
                board = bt.CreateInstance();
        }
    }
}
