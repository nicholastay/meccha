using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Meccha.Keyboards
{
    public abstract class BaseBoard
    {
        public static string BoardName => MethodBase.GetCurrentMethod().DeclaringType.Name; // gets the class name by reflection

        Form1 _f;
        public BaseBoard(Form1 f)
        {
            _f = f;
            Init();
        }

        protected virtual void Init() { }

        protected float UserVolume => (float)(_f.volumeBar.Value) / 100f;

        public virtual void OnKeyDown(KeyboardHook.KeyInputEventArgs e) { }
        public virtual void OnKeyUp(KeyboardHook.KeyInputEventArgs e) { }

        protected void playSound(byte[] s, float? f = null)
        {
            MemoryStream ms = new MemoryStream(s);
            WaveFileReader r = new WaveFileReader(ms);
            WaveOut player = new WaveOut();

            player.Volume = (f == null) ? UserVolume : (float)f;
            player.Init(r);
            player.Play();

            player.PlaybackStopped += (o, ex) =>
            {
                ms.Dispose();
                r.Dispose();
                player.Dispose();
            };
        }

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
