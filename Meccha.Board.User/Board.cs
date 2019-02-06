using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Meccha.Board.User
{
    public class Board : TemplateBoards.RandomizedSoundBaseBoard
    {
        public new static string BoardName => "User Folder (\\user)";

        static string userDir = Path.Combine(Tools.ProgramPath, "user");

        static string upPath = Path.Combine(userDir, "up");
        static string downPath = Path.Combine(userDir, "down");

        public Board() : base(tryLoadDir(upPath), tryLoadDir(downPath)) { }

        static List<Stream> tryLoadDir(string dir)
        {
            if (!Directory.Exists(dir))
                return null;

            List<Stream> s = Directory.GetFiles(dir, "*.wav")
                .Select(f => (Stream)File.Open(f, FileMode.Open))
                .ToList();

            if (s.Count == 0)
                return null;
            return s;
        }
    }
}
