using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meccha.Keyboards
{
    public class RandomizedSoundBaseBoard : BaseBoard
    {
        private List<byte[]> KeyDownSounds = null;
        private List<byte[]> KeyUpSounds = null;

        public RandomizedSoundBaseBoard(Form1 f, List<Stream> keyups, List<Stream> keydowns) : base(f)
        {
            if (keyups != null)
                KeyUpSounds = keyups.Select(s => stream2bytearr(s)).ToList();
            if (keydowns != null)
                KeyDownSounds = keydowns.Select(s => stream2bytearr(s)).ToList();
        }

        public override void OnKeyDown(KeyboardHook.KeyInputEventArgs e)
        {
            if (KeyDownSounds != null)
                playSound(Tools.PickRandomFromList(KeyDownSounds));
        }

        public override void OnKeyUp(KeyboardHook.KeyInputEventArgs e)
        {
            if (KeyUpSounds != null)
                playSound(Tools.PickRandomFromList(KeyUpSounds));
        }
    }
}
