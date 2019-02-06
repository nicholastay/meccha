using Meccha.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meccha
{
    // More of a display class for the dropdown
    public class DisplayBoard
    {
        private Form1 _f;
        private Type type;
        public DisplayBoard(Form1 f, Type t)
        {
            _f = f;
            type = t;
        }

        public BaseBoard CreateInstance()
        {
            return (BaseBoard)Activator.CreateInstance(type);
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
