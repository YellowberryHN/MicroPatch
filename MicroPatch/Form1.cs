using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MicroPatch
{
    public partial class MicroPatch : Form
    {
        public MicroPatch()
        {
            InitializeComponent();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_NCLBUTTONDBLCLK)
            {
                m.Result = IntPtr.Zero;
                return;
            }
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);
        }

        private const int WM_NCLBUTTONDBLCLK = 0x00A3; //double click on a title bar a.k.a. non-client area of the form
        private const int WM_NCHITTEST = 0x84;
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;

        public void Form1_Load(object sender, EventArgs e)
        {
            Music.BASSMOD_Init(-1, 44100, BASSMOD_BASSInit.BASS_DEVICE_DEFAULT);
            Music.BASSMOD_MusicLoad(true, Properties.Resources.freedom,0,0, BASSMOD_BASSMusic.BASS_MUSIC_LOOP | BASSMOD_BASSMusic.BASS_MUSIC_RAMP | BASSMOD_BASSMusic.BASS_MUSIC_SURROUND2);
            Music.BASSMOD_MusicPlay();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Music.BASSMOD_Free();
        }

        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            Close();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 100;
        }

        private void windowTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
