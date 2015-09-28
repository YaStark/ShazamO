using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DirectShowNet
{
    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
     Guid("C6E13340-30AC-11d0-A18C-00A0C9118956"),
     InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAMStreamConfig
    {
        [PreserveSig]
        int GetFormat(
            [Out] IntPtr pmt
        );

        [PreserveSig]
        int GetNumberOfCapabilities(
           [Out] out int piCount,
           [Out] out int piSize
        );

        [PreserveSig]
        int GetStreamCaps(
           [In]  int iIndex,
           [Out, MarshalAs(UnmanagedType.LPStruct)] out AmMediaType pmt,
           [Out] IntPtr pSCC
        );

        [PreserveSig]
        int SetFormat(
           [In] IntPtr fmt
        );
    }


    public static class IAMStreamConfigEx
    {
        public static AmMediaType GetFormat(this IAMStreamConfig cfg)
        {
            AmMediaType mtype = new AmMediaType();
            IntPtr ptr = Marshal.AllocCoTaskMem(Marshal.SizeOf(mtype));
            uint hresult = (uint)cfg.GetFormat(ptr);
            if (0x8007000E == hresult) 
                throw new OutOfMemoryException();
            else if (0x80040209 == hresult) 
                throw new Exception("The input pin is not connected.");
            Marshal.PtrToStructure(ptr, mtype);
            Marshal.FreeCoTaskMem(ptr);
            return mtype;
        }
        
        public static void SetFormat(this IAMStreamConfig cfg, AmMediaType media)
        {
            int size = Marshal.SizeOf(media);
            IntPtr ptr = Marshal.AllocCoTaskMem(Marshal.SizeOf(media.GetType()));
            Marshal.StructureToPtr(media, ptr, false);
            uint hresult = (uint)cfg.SetFormat(ptr);
            if (0x8007000E == hresult)
                throw new OutOfMemoryException();
            else if (0x80040209 == hresult)
                throw new Exception("The input pin is not connected.");
            Marshal.FreeCoTaskMem(ptr);
        }
         
    }
}
