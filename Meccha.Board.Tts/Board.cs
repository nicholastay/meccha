using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Meccha.Board.Tts
{
    public class Board : BaseBoard
    {
        public new static string BoardName => "System Default TTS";

        public Board() : base() { }

        // store vk against sound
        Dictionary<int, byte[]> SpeechCache = new Dictionary<int, byte[]>();

        protected override void Init()
        {
            cacheSpeech();
        }

        private void cacheSpeech()
        {
            //65-90 = A-Z
            for (int i = 65; i <= 90; i++)
                generateSpeechCache(i);
        }

        private void generateSpeechCache(int vk)
        {
            if (SpeechCache.Keys.Contains(vk))
                return;

            // https://stackoverflow.com/questions/773303/splitting-camelcase more friendly name
            Keys k = (Keys)vk;
            string s = Regex.Replace(k.ToString("g"), "(?<=[a-z])([A-Z])", " $1", RegexOptions.Compiled).Trim();

            using (var synth = new SpeechSynthesizer())
            using (var ms = new MemoryStream())
            {
                synth.SetOutputToWaveStream(ms);
                synth.Speak(s);
                SpeechCache.Add(vk, ms.GetBuffer());
            }
        }

        public override byte[] OnKeyDown(int vk)
        {
            if (!SpeechCache.Keys.Contains(vk))
            {
                Thread t = new Thread(() => generateSpeechCache(vk));
                t.Start();
                t.Join();
            }

            return SpeechCache[vk];
        }
    }
}
