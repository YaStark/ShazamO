using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DirectShowNet
{
    [Guid("56A868B1-0AD4-11CE-B03A-0020AF0BA770"),
            InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IMediaControl 
    {
        [PreserveSig]
        int Run();

        [PreserveSig]
        int Pause();

        [PreserveSig]
        int Stop();

        [PreserveSig]
        int GetState([In] int msTimeout, [Out] out int pfs);

        [PreserveSig]
        int RenderFile([In, MarshalAs(UnmanagedType.BStr)] string strFilename);

        [PreserveSig]
        int AddSourceFilter(
            [In, MarshalAs(UnmanagedType.BStr)] string strFilename,
            [Out, MarshalAs(UnmanagedType.Interface)] out object ppUnk);

        [PreserveSig]
        int get_FilterCollection([Out, MarshalAs(UnmanagedType.Interface)] out object ppUnk);

        [PreserveSig]
        int get_RegFilterCollection([Out, MarshalAs(UnmanagedType.Interface)] out object ppUnk);

        [PreserveSig]
        int StopWhenReady();
    }
}
