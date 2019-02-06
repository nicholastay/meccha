using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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

        protected byte[] stream2bytearr(Stream s)
        {
            using (var ms = new MemoryStream())
            {
                // reset the streams back to the start if this is called again
                // eg randomized would crash upon making the class again, as its a static stream being reused
                // and cause byte[0] as the stream was at the end.
                // 
                // probably good practice anyway
                if (s.CanSeek)
                    s.Seek(0, SeekOrigin.Begin);

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
