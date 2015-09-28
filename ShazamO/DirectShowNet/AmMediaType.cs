using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace DirectShowNet
{
    [StructLayout(LayoutKind.Sequential)]
    public class AmMediaType
    {
        public Guid majortype;
        
        public Guid subtype;
        
        public bool bFixedSizeSamples;
        
        public bool bTemporalCompression;
        
        public uint lSampleSize;
        
        public Guid formattype;
        
        public IntPtr pUnk;
        
        public uint cbFormat;
        
        public IntPtr pbFormat;
    };


    public static class AmMeiaTypeEx
    {
        public static T GetStruct<T>(this AmMediaType amt) where T : struct
        {
            if (amt == null || amt.pbFormat == IntPtr.Zero) return new T();
            int size = Marshal.SizeOf(typeof(T));
            IntPtr ptr = Marshal.AllocCoTaskMem(size);
            T format = (T)Marshal.PtrToStructure(amt.pbFormat, typeof(T));
            Marshal.FreeCoTaskMem(ptr);
            return format;
        }
    }
    

}
