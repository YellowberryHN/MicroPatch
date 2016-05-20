using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroPatch
{
    class PatcherMain
    {
        public static void PatchInit(string patchfile)
        {
            ZipFile zip = ZipFile.Read(patchfile);
            using (zip)
            {
                foreach (ZipEntry e in zip)
                {
                    MicroPatch mp = new MicroPatch();
                    mp.Message(TextRes.patching, "Extracting Patch...");
                    Debug.WriteLine(e.FileName);
                    e.Extract(System.IO.Path.GetTempPath()+"/MVP/PATCH/", ExtractExistingFileAction.OverwriteSilently);
                }
            }
        }
    }
}
