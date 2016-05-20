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
        private const int WM_NCLBUTTONDBLCLK = 0x00A3; //double click on a title bar a.k.a. non-client area of the form
        private const int WM_NCHITTEST = 0x84;
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;
        protected string filepath = String.Empty;
        public bool patching;
        public string gamedir = 
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "/Steam/steamapps/common/MicroVolts";

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

        public void Message(string msg, string status)
        {
            infoBox.Text = msg+Environment.NewLine+Environment.NewLine+"Status: "+status;
        }

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
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
                openFileDialog1.Filter = "Patch files (*.mvp; *.mpp; *.microvolts)|*.mvp;*.mpp;*.microvolts|All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
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
                PatchInit(filepath);
            }
        }

        public void PatchInit(string patchfile)
        {
            if (Directory.Exists(System.IO.Path.GetTempPath() + "/MVP"))
            {
                Message(TextRes.patching, "Clearing Temporary Files...");
                Directory.Delete(System.IO.Path.GetTempPath() + "/MVP", true);
            }

            ZipFile zip = ZipFile.Read(patchfile);
            using (zip)
            {
                foreach (ZipEntry e in zip)
                {
                    Message(TextRes.patching, "Extracting Patch...");
                    Debug.WriteLine(e.FileName);
                    e.Extract(System.IO.Path.GetTempPath() + "/MVP/PATCH/", ExtractExistingFileAction.OverwriteSilently);
                }
            }
            PatchPrepareFiles(gamedir);
        }

        public void PatchPrepareFiles(string mvdir)
        {
            Message(TextRes.patching, "Preparing Game Files...");
            Array datafolder = Directory.GetFiles(mvdir+"/data");

            //System.IO.File.

            foreach (string e in datafolder)
            {
                if (System.IO.Path.GetExtension(e) == ".dat")
                {
                    Debug.WriteLine("File Select: " + e);
                    if (!System.IO.File.Exists(e + ".bkup"))
                    {
                        System.IO.File.Copy(e, e + ".bkup");
                        Debug.WriteLine(e + ".bkup created");
                    }
                    if (ZipFile.IsZipFile(e))
                    {
                        ZipFile zip = ZipFile.Read(e);
                        foreach (string f in zip.EntryFileNames)
                        {
                            Debug.WriteLine("Jews: " + f);
                            //if (File.ReadAllBytes(f) == File.Rea(System.IO.Path.GetTempPath() + "/MVP/PATCH/"+Path.GetFileNameWithoutExtension(e).ToUpper()))
                            if (File.Exists(System.IO.Path.GetTempPath() + "/MVP/PATCH/" + Path.GetFileNameWithoutExtension(e).ToUpper()+"/"+Path.GetFileName(f)))
                            {
                                Debug.WriteLine("Show me the money");
                                zip.RemoveEntry(Path.GetFileName(f));
                                zip.AddFile(System.IO.Path.GetTempPath() + "/MVP/PATCH/" + Path.GetFileNameWithoutExtension(e).ToUpper() + "/" + Path.GetFileName(f));
                            }
                        }
                        zip.Save(e);
                        Debug.WriteLine("Done");
                    }

                    else
                    {
                        //Figure out how the fuck to rewrite the BMS file.
                    }
                }
            }
        }
    }
}
