using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace DirectShowNet
{
    [ComImport]
    [Guid("A2104830-7C70-11CF-8BCE-00AA00A3F1A6")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IFileSinkFilter
    {
        void SetFileName(
                [In, MarshalAs(UnmanagedType.LPWStr)] string pszFileName,
                [In, MarshalAs(UnmanagedType.LPStruct)] AmMediaType pmt);

        void GetCurFile(
                [Out, MarshalAs(UnmanagedType.LPWStr)] out string ppszFileName,
                [Out, MarshalAs(UnmanagedType.LPStruct)] AmMediaType pmt);
    }
}
