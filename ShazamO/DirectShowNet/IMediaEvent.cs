using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

namespace DirectShowNet
{
    public enum EventCode : uint
    {
        NotCompleted = 0x0,
        Success = 0x01,
        UserAbort = 0x02,
        ErrorAbort = 0x03,
        ExpTime = 0x04
    }

    [ComImport, 
     System.Security.SuppressUnmanagedCodeSecurity,
     Guid("56A868B6-0AD4-11CE-B03A-0020AF0BA770"),
     InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IMediaEvent
    {
        [PreserveSig]
        int CancelDefaultHandling([In] uint lEvCode);
        
        [PreserveSig]
        int FreeEventParams(
            [In] EventCode lEventCode,
            [In] uint lParam1,
            [In] uint lParam2
        );

        [PreserveSig]
        int GetEventHandle([Out] IntPtr hEvent);

        [PreserveSig]
        int GetEvent(
                        [Out] out EventCode lEventCode, 
                        [Out] out uint lParam1, 
                        [Out] out uint lParam2, 
                        [In] uint msTimeout);

        [PreserveSig]
        int WaitForCompletion([In] int msTimeout, [Out] out EventCode pEvCode);
        
        [PreserveSig]
        int RestoreDefaultHandling([In] uint lEvCode);
    }

    [ComImport, 
     System.Security.SuppressUnmanagedCodeSecurity,
     Guid("56a868c0-0ad4-11ce-b03a-0020af0ba770"),
     InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IMediaEventEx
    {
        [PreserveSig]
        int GetNotifyFlags([Out] out uint lplNoNotifyFlags);

        [PreserveSig]
        int SetNotifyFlags([In] uint lNoNotifyFlags);

        [PreserveSig]
        int SetNotifyWindow(
                            IntPtr hwnd,
                            int lMsg,
                            IntPtr lInstanceData);

    }
    
}
