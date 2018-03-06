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

        BoardType[] boardTypes;
        BaseBoard board;

        public Form1()
        {
            InitializeComponent();

            populateBoards();
            comboBox1.SelectedIndex = 0;
            //setBoard(boardTypes[0]);

            Hook = new KeyboardHook();
            Hook.Hook();

            Hook.KeyDown += (s, e) => board.OnKeyDown(e);
            Hook.KeyUp += (s, e) => board.OnKeyUp(e);
        }

        private void populateBoards()
        {
            boardTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.Namespace == "Meccha.Keyboards.Boards"
                    && !t.IsDefined(typeof(CompilerGeneratedAttribute), false)) // filter out the compilergenerated stuff (<>DisplayClass, etc. -- linq shit) ~ https://stackoverflow.com/questions/6513648/how-do-i-filter-out-c-displayclass-types-when-going-through-types-via-reflecti
                .Select(t => new BoardType(this, t))
                .ToArray();

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

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
