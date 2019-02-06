using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Meccha.Board
{
    public abstract class BaseBoard
    {
        public static string BoardName => MethodBase.GetCurrentMethod().DeclaringType.Name; // gets the class name by reflection

        public BaseBoard()
        {
            Init();
        }

        protected virtual void Init() { }

        public virtual byte[] OnKeyDown(int vk) { return null; }
        public virtual byte[] OnKeyUp(int vk) { return null; }

        private float volume = 1f;

        protected byte[] stream2bytearr(Stream s)
        {
            using (var ms = new MemoryStream())
            {
                s.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return BoardName;
        }
    }
}
