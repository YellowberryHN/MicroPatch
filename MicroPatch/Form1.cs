using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ionic.Zip;

namespace MicroPatch
{
    public partial class MicroPatch : Form
    {
        public MicroPatch()
        {
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                string resourceName = new AssemblyName(args.Name).Name + ".dll";
                string resource = Array.Find(this.GetType().Assembly.GetManifestResourceNames(), element => element.EndsWith(resourceName));

                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
                {
                    Byte[] assemblyData = new Byte[stream.Length];
                    stream.Read(assemblyData, 0, assemblyData.Length);
                    return Assembly.Load(assemblyData);
                }
            };

            InitializeComponent();

            patch.Enabled = false;
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
        protected string filepath = String.Empty;
        public bool patching;

        public void Message(string msg, string status)
        {
            infoBox.Text = msg+Environment.NewLine+Environment.NewLine+"Status: "+status;
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            Music.BASSMOD_Init(-1, 44100, BASSMOD_BASSInit.BASS_DEVICE_DEFAULT);
            Music.BASSMOD_MusicLoad(true, Properties.Resources.freedom,0,0, BASSMOD_BASSMusic.BASS_MUSIC_LOOP | BASSMOD_BASSMusic.BASS_MUSIC_RAMP | BASSMOD_BASSMusic.BASS_MUSIC_SURROUND2);
            Music.BASSMOD_MusicPlay();

            //Debug.WriteLine(System.IO.Path.GetTempPath());

            progressBar1.Value = 50;

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Music.BASSMOD_Free();
        }

        private void closeButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (!patching)
            {
                Close();
            }
            else
            {
                MessageBox.Show("Error 7: Cannot close the program while patching.", "MicroPatch", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (!patching)
            {
                //Stream myStream = null;
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
                openFileDialog1.Filter = "Patch files (*.mvp; *.mpp; *.microvolts)|*.mvp;*.mpp;*.microvolts|All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    /*try
                    {
                        if ((myStream = openFileDialog1.OpenFile()) != null)
                        {
                            using (myStream)
                            {
                                // Insert code to read the stream here.
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error 6: Could not read patch file from disk. Original error: " + ex.Message);
                    }*/
                    filepath = openFileDialog1.FileName;
                    patch.Enabled = true;
                }
            }
        }

        private void patch_Click(object sender, EventArgs e)
        {
            if (!patching)
            {
                patching = true;
                patch.Enabled = false;
                patch.Text = "Patching...";
                Message(TextRes.patching,"Loading...");
                PatcherMain.PatchInit(filepath);
            }
        }
    }
}
