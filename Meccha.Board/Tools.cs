using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Meccha.Board
{
    public static class Tools
    {
        public static string ProgramPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static Random RNG = new Random();

        public static T PickRandomFromList<T>(List<T> ss)
        {
            if (ss.Count <= 1)
                return default(T);

            return ss[RNG.Next(0, ss.Count)];
        }
    }
}
