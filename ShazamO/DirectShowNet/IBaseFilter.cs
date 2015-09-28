using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace DirectShowNet
{
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)]
    public class FilterInfo
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst=128)]
        public string achName;

        public object pUnk;
    }

    [ComImport]
    [Guid("56A86897-0AD4-11CE-B03A-0020AF0BA770")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IReferenceClock
    {
        ulong GetTime();

        int AdviseTime([In] ulong rtBaseTime, [In] ulong rtStreamTime, [In] IntPtr hEvent);

        int AdvisePeriodic([In] ulong rtStartTime, [In] ulong rtPeriodTime, [In] IntPtr hSemaphore);

        void Unadvise([In] uint dwAdviseCookie);
    }

    [ComImport]
    [Guid("56A86893-0AD4-11CE-B03A-0020AF0BA770")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEnumFilters
    {
        void Next([In] uint cFilters, [Out] out IBaseFilter ppFilter, [Out] out uint pcFetched);

        void Skip([In] uint cFilter);

        void Reset();

        void Clone([Out] out IEnumFilters ppEnum);
    }


    [ComImport]
    [Guid("56A86895-0AD4-11CE-B03A-0020AF0BA770")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBaseFilter
    {
        void GetClassID([Out] out Guid pClsID);

        void Stop();

        void Pause();

        void Run([In] ulong tStart);

        int GetState([In] uint dwMilliSecsTimeout);

        void SetSyncSource([In] IReferenceClock pClock);

        IReferenceClock GetSyncSource();

        [PreserveSig]
        int EnumPins(
            [Out, MarshalAs(UnmanagedType.Interface)] out IEnumPins ppEnum
        );

        [PreserveSig]
        int FindPin(
            [In, MarshalAs(UnmanagedType.LPWStr)]  string Id,
            [Out, MarshalAs(UnmanagedType.Interface)] out IPin ppPin
        );

        [PreserveSig]
        int JoinFilterGraph(
            [In, MarshalAs(UnmanagedType.Interface)] IGraphBuilder pGraph,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pName
        );

        [PreserveSig]
        int QueryFilterInfo(
            [Out, MarshalAs(UnmanagedType.Struct)] FilterInfo pInfo
        );

        [PreserveSig]
        int QueryVendorInfo(
            [Out, MarshalAs(UnmanagedType.LPWStr)] out string pVendorInfo
        );
    }

    public static class IBaseFilterExt
    {
        static int S_OK = 0;

        public static IPin FindPin(this IBaseFilter pFilter, PinDirection PinDir)
        {
            IEnumPins pEnum;
            IPin pPin;
            uint fetched = 0;

            int hresult = pFilter.EnumPins(out pEnum);
            Marshal.ThrowExceptionForHR(hresult);

            while (S_OK == pEnum.Next(1, out pPin, out fetched))
            {
                PinDirection dir;
                pPin.QueryDirection(out dir);
                if (dir == PinDir) break;
                pPin.Dispose();
            }
            pEnum.Dispose();
            return pPin;
        }



        /// <summary>
        /// Search and return first unconnected pin 
        /// </summary>
        /// <param name="pFilter"></param>
        /// <param name="PinDir"></param>
        /// <param name="ppPin"></param>
        /// <returns></returns>
        public static IPin FindUnconnectedPin(this IBaseFilter pFilter, PinDirection PinDir)
        {
            IEnumPins pEnum;
            IPin pPin;
            uint fetched = 0;

            int hresult = pFilter.EnumPins(out pEnum);
            Marshal.ThrowExceptionForHR(hresult);

            while (S_OK == pEnum.Next(1, out pPin, out fetched))
            {
                if (pPin.Match(PinDir, false)) break;
                pPin.Dispose();
            }
            pEnum.Dispose();
            return pPin;
        }
    }
}
