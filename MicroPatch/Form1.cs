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
        public bool patchfail = false;
        public string tempdir = System.IO.Path.GetTempPath() + "/MVP/PATCH/";
        public string gamedir = 
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "/Steam/steamapps/common/MicroVolts";
        private bool music = true;

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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Alt | Keys.H))
            {
                Debug.WriteLine("<CTRL> + Alt + H Captured");
                Form about = new About();
                about.Show();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
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
            infoBox.Cursor = Cursors.Default;
            infoBox.Text = msg+Environment.NewLine+Environment.NewLine+"Status: "+status;
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            if (music)
            {
                Music.BASSMOD_Init(-1, 44100, BASSMOD_BASSInit.BASS_DEVICE_DEFAULT);
                Music.BASSMOD_MusicLoad(true, Properties.Resources.freedom, 0, 0, BASSMOD_BASSMusic.BASS_MUSIC_LOOP | BASSMOD_BASSMusic.BASS_MUSIC_RAMP | BASSMOD_BASSMusic.BASS_MUSIC_SURROUND2);
                Music.BASSMOD_MusicPlay();
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (music)
            {
                Music.BASSMOD_Free();
            }
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
                openFileDialog1.Filter = "Patch files (*.mvp; *.mpp)|*.mvp;*.mpp|All files (*.*)|*.*";
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

        public static void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);
        }

        public void PatchInit(string patchfile)
        {
            if (Directory.Exists(System.IO.Path.GetTempPath() + "/MVP"))
            {
                Message(TextRes.patching, "Clearing Temporary Files...");
                DeleteDirectory(System.IO.Path.GetTempPath() + "/MVP");
            }
            try
            {
                ZipFile zip = ZipFile.Read(patchfile);
                using (zip)
                {
                    foreach (ZipEntry e in zip)
                    {
                        Message(TextRes.patching, "Extracting Patch...");
                        Debug.WriteLine(e.FileName);
                        e.Extract(tempdir, ExtractExistingFileAction.OverwriteSilently);
                    }
                }
                PatchPrepareFiles(gamedir);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                patching = false;
                patchfail = true;
                Message(TextRes.fail, "ERROR: FILE NOT PATCH.");
            }
        }

        public void PatchPrepareFiles(string mvdir)
        {
            Message(TextRes.patching, "Preparing Game Files...");
            Array datafolder = Directory.GetFiles(mvdir+"/data");

            //System.IO.File.

            foreach (string s in datafolder)
            {
                if (System.IO.Path.GetExtension(s) == ".dat")
                {
                    Debug.WriteLine("File Select: " + s);
                    if (!System.IO.File.Exists(s + ".bkup"))
                    {
                        System.IO.File.Copy(s, s + ".bkup");
                        Debug.WriteLine(s + ".bkup created");
                    }
                    if (ZipFile.IsZipFile(s))
                    {
                        //String[] filenames = { };
                        ZipFile zip = ZipFile.Read(s);
                        /*foreach (string f in zip.EntryFileNames)
                        {
                            Debug.WriteLine("Jews: " + f);
                            //if (File.ReadAllBytes(f) == File.Rea(System.IO.Path.GetTempPath() + "/MVP/PATCH/"+Path.GetFileNameWithoutExtension(s).ToUpper()))
                            if (File.Exists(tempdir + Path.GetFileNameWithoutExtension(s).ToUpper() + "/" + Path.GetFileName(f)))
                            {
                                Debug.WriteLine("Show me the money");
                                Array.Resize(ref filenames, filenames.Length + 1);
                                filenames[filenames.Length - 1] = Path.GetFileName(f);
                            }
                        }
                        foreach (string k in filenames)
                        {
                            zip.RemoveEntry(k);
                            zip.AddFile(tempdir + Path.GetFileNameWithoutExtension(s).ToUpper() + "/" + k);
                        }*/


                        BackgroundWorker worker = new BackgroundWorker();
                        worker.WorkerReportsProgress = true;
                        worker.ProgressChanged += (o, e) => { };
                        worker.DoWork += (o, e) =>
                        {
                            using (zip)
                            {
                                int step = (zip.Count / 100);
                                int percentComplete = 0;
                                foreach (ZipEntry fuck in zip)
                                {
                                    Message(TextRes.patching, "Extracting Game Files... " + percentComplete +"%");
                                    Debug.WriteLine(fuck.FileName);
                                    fuck.Extract(tempdir + Path.GetFileNameWithoutExtension(s).ToUpper() + "/", ExtractExistingFileAction.DoNotOverwrite);
                                }
                            }
                        };

                        worker.RunWorkerAsync();

                        ZipFile gameFile = new ZipFile();
                        IEnumerable<String> files = Directory.EnumerateFileSystemEntries(tempdir + Path.GetFileNameWithoutExtension(s).ToUpper() + "/");
                        Debug.WriteLine("Adding Files");
                        gameFile.AddFiles(files);
                        Debug.WriteLine("Saving");
                        gameFile.Save(s);
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
