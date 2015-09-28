using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace DirectShowNet
{
    [ComImport]
    [Guid("56A868A9-0AD4-11CE-B03A-0020AF0BA770")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IGraphBuilder
    {
        [PreserveSig]
        int AddFilter([In] IBaseFilter pFilter, [In, MarshalAs(UnmanagedType.LPWStr)] string pName);

        void RemoveFilter([In] IBaseFilter pFilter);

        IEnumFilters EnumFilters();

        IBaseFilter FindFilterByName([In, MarshalAs(UnmanagedType.LPWStr)] string pName);

        void ConnectDirect([In] IPin ppinOut, [In] IPin ppinIn, [In, MarshalAs(UnmanagedType.LPStruct)] AmMediaType pmt);

        void Reconnect([In] IPin ppin);

        void Disconnect([In] IPin ppin);

        void SetDefaultSyncSource();

        [PreserveSig]
        int Connect([In] IPin ppinOut, [In] IPin ppinIn);

        void Render([In] IPin ppinOut);

        [PreserveSig]
        int RenderFile(
                [In, MarshalAs(UnmanagedType.LPWStr)] string lpwstrFile,
                [In, MarshalAs(UnmanagedType.LPWStr)] string lpwstrPlayList);

        IBaseFilter AddSourceFilter(
                [In, MarshalAs(UnmanagedType.LPWStr)] string lpwstrFileName,
                [In, MarshalAs(UnmanagedType.LPWStr)] string lpwstrFilterName);

        void SetLogFile(IntPtr hFile);

        [PreserveSig]
        int Abort();

        [PreserveSig]
        int ShouldOperationContinue();
    }

    public static class GraphBuilderExt
    {
        /// <summary>
        /// </summary>
        /// <param name="pGraph"></param>
        /// <param name="pSrc"></param>
        /// <param name="pDest"></param>
        /// <returns></returns>
        public static bool ConnectFilters(this IGraphBuilder pGraph, IBaseFilter pSrc, IBaseFilter pDest)
        {
            // Find an output pin on the first filter.
            IPin pIn = pDest.FindUnconnectedPin(PinDirection.Input);
            IPin pOut = pSrc.FindUnconnectedPin(PinDirection.Output);

            if (pIn == null || pOut == null) return false;

            int hresult = pGraph.Connect(pOut, pIn);
            Marshal.ThrowExceptionForHR(hresult);

            pIn.Dispose();
            pOut.Dispose();
            return true;
        }
    }

}
