using System;
using System.Runtime.InteropServices;


namespace MicroPatch
{
    public enum BASSMOD_BASSMusic
    {
        BASS_MUSIC_RAMP = 1,
        BASS_MUSIC_LOOP = 4,
        BASS_MUSIC_SURROUND2 = 1024
    }

    public enum BASSMOD_BASSInit
    {
        BASS_DEVICE_DEFAULT = 0
    }

    public static class Music
    {
        private const string BASSMODLIB = "BASSMOD.dll";
        [DllImport(BASSMODLIB, EntryPoint = "BASSMOD_Init")]
        public static extern IntPtr BASSMOD_Init(int device, int freq, BASSMOD_BASSInit flags);
        [DllImport(BASSMODLIB, EntryPoint = "BASSMOD_MusicLoad")]
        public static extern IntPtr BASSMOD_MusicLoad(bool mem, byte[] tune, int offset, int len, BASSMOD_BASSMusic flags);
        [DllImport(BASSMODLIB, EntryPoint = "BASSMOD_Free")]
        public static extern IntPtr BASSMOD_Free();
        [DllImport(BASSMODLIB, EntryPoint = "BASSMOD_MusicPlay")]
        public static extern IntPtr BASSMOD_MusicPlay();
    }
}
