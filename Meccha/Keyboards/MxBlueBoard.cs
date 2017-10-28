using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meccha.Keyboards
{
    public class MxBlueBoard : BasicSoundBaseBoard
    {
        public MxBlueBoard(Form1 f)
            : base(f, Properties.Resources.mxblue_up, Properties.Resources.mxblue_down) { }

        public new static string BoardName => "Cherry MX Blue";
    }
}
