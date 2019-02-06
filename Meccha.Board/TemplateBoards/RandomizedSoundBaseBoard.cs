using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meccha.Board.TemplateBoards
{
    public class RandomizedSoundBaseBoard : BaseBoard
    {
        private List<byte[]> KeyDownSounds = null;
        private List<byte[]> KeyUpSounds = null;

        public RandomizedSoundBaseBoard(List<Stream> keyups, List<Stream> keydowns) : base()
        {
            if (keyups != null)
                KeyUpSounds = keyups.Select(s => stream2bytearr(s)).ToList();
            if (keydowns != null)
                KeyDownSounds = keydowns.Select(s => stream2bytearr(s)).ToList();
        }

        public override byte[] OnKeyDown(int vk)
        {
            if (KeyDownSounds == null)
                return null;

            return Tools.PickRandomFromList(KeyDownSounds);
        }

        public override byte[] OnKeyUp(int vk)
        {
            if (KeyUpSounds == null)
                return null;

            return Tools.PickRandomFromList(KeyUpSounds);
        }
    }
}
