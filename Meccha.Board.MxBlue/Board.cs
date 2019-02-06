using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meccha.Board.MxBlue
{
    public class Board : TemplateBoards.BasicSoundBaseBoard
    {
        public new static string BoardName => "Cherry MX Blue";

        public Board()
            : base(Properties.Resources.mxblue_up, Properties.Resources.mxblue_down) { }
    }
}
