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

namespace Meccha.Keyboards
{
    public class TtsBoard : BaseBoard
    {
        public new static string BoardName => "System Default TTS";

        public TtsBoard(Form1 f) : base(f) { }

        Dictionary<Keys, byte[]> SpeechCache = new Dictionary<Keys, byte[]>();

        protected override void Init()
        {
            cacheSpeech();
        }

        private void cacheSpeech()
        {
            //65-90 = A-Z
            for (int i = 65; i <= 90; i++)
                generateSpeechCache((Keys)i);
        }

        private void generateSpeechCache(Keys k)
        {
            if (SpeechCache.Keys.Contains(k))
                return;

            // https://stackoverflow.com/questions/773303/splitting-camelcase more friendly name
            string s = Regex.Replace(k.ToString("g"), "(?<=[a-z])([A-Z])", " $1", RegexOptions.Compiled).Trim();

            using (var synth = new SpeechSynthesizer())
            using (var ms = new MemoryStream())
            {
                synth.SetOutputToWaveStream(ms);
                synth.Speak(s);
                SpeechCache.Add(k, ms.GetBuffer());
            }
        }

        public override void OnKeyDown(KeyboardHook.KeyInputEventArgs e)
        {
            Thread t = new Thread(() => generateSpeechCache(e.Key));
            t.Start();
            t.Join();

            playSound(SpeechCache[e.Key]);
        }
    }
}
