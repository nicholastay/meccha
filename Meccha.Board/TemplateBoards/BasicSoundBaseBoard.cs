using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Meccha.Board.TemplateBoards
{
    public class BasicSoundBaseBoard : BaseBoard
    {
        public BasicSoundBaseBoard(Stream keyup, Stream keydown) : base()
        {
            KeyDownSound = (keyup == null) ? null : stream2bytearr(keydown);
            KeyUpSound = (keyup == null) ? null : stream2bytearr(keyup);
        }

        private byte[] KeyDownSound;
        private byte[] KeyUpSound;

        public override byte[] OnKeyDown(int vk)
        {
            return KeyDownSound;
        }

        public override byte[] OnKeyUp(int vk)
        {
            return KeyUpSound;
        }
    }
}
