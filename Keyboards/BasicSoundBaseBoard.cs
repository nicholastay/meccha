using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meccha.Keyboards
{
    public class BasicSoundBaseBoard : BaseBoard
    {
        public BasicSoundBaseBoard(Form1 f, Stream keyup, Stream keydown) : base(f)
        {
            KeyDownSound = stream2bytearr(keydown);
            KeyUpSound = stream2bytearr(keyup);
        }

        private byte[] KeyDownSound;
        private byte[] KeyUpSound;

        public override void OnKeyDown(KeyboardHook.KeyInputEventArgs e)
        {
            if (KeyDownSound != null)
                playSound(KeyDownSound);
        }

        public override void OnKeyUp(KeyboardHook.KeyInputEventArgs e)
        {
            if (KeyUpSound != null)
                playSound(KeyUpSound);
        }
    }
}
