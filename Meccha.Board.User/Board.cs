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

        static string basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        static string userDir = Path.Combine(basePath, "user");

        static string upPath = Path.Combine(userDir, "up");
        static string downPath = Path.Combine(userDir, "down");

        public Board() : base(tryLoadDir(upPath), tryLoadDir(downPath)) { }

        static List<Stream> tryLoadDir(string dir)
        {
            if (!Directory.Exists(dir))
                return null;

            List<Stream> s = Directory.GetFiles(dir)
                .Where(f => Path.GetExtension(f) == ".wav")
                .Select(f => (Stream)File.Open(f, FileMode.Open))
                .ToList();

            if (s.Count == 0)
                return null;
            return s;
        }
    }
}
