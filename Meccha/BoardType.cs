using Meccha.Keyboards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meccha
{
    public class BoardType
    {
        private Form1 _f;
        private Type type;
        public BoardType(Form1 f, Type t)
        {
            _f = f;
            type = t;
        }

        public BaseBoard CreateInstance()
        {
            return (BaseBoard)Activator.CreateInstance(type, _f);
        }

        public override string ToString()
        {
            return type
                .GetProperty("BoardName")
                .GetGetMethod()
                .Invoke(null, null)
                .ToString();
        }
    }
}
