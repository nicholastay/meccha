using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meccha.Board
{
    public static class Tools
    {
        public static Random RNG = new Random();

        public static T PickRandomFromList<T>(List<T> ss)
        {
            if (ss.Count <= 1)
                return default(T);

            return ss[RNG.Next(0, ss.Count)];
        }
    }
}
