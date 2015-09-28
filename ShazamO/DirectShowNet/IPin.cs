using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace DirectShowNet
{
    public enum PinDirection { Input, Output }

    [ComImport]
    [Guid("56A86891-0AD4-11CE-B03A-0020AF0BA770")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPin
    {
        [PreserveSig]
        uint Connect(
            [In] IPin pReceivePin, 
            [In, MarshalAs(UnmanagedType.LPStruct)] AmMediaType pmt
        );

        void ReceiveConnection([In] IPin pReceivePin, [In, MarshalAs(UnmanagedType.LPStruct)] AmMediaType pmt);

        void Disconnect();

        [PreserveSig]
        uint ConnectedTo(
            [Out, MarshalAs(UnmanagedType.Interface)] out IPin pPin
        );

        [PreserveSig]
        int ConnectionMediaType(
           [Out] IntPtr pmt
        );

        void QueryPinInfo(IntPtr pInfo);
        
        [PreserveSig]
        uint QueryDirection(
            [Out] out PinDirection pPinDir
        );

        string QueryId();

        [PreserveSig]
        uint QueryAccept([In, MarshalAs(UnmanagedType.LPStruct)] AmMediaType pmt);

        [PreserveSig]
        int EnumMediaTypes(
            [Out, MarshalAs(UnmanagedType.IUnknown)] out object enumMediaTypes    
        );

        void QueryInternalConnections([Out] IntPtr apPin, [In, Out] ref uint nPin);

        void EndOfStream();

        void BeginFlush();

        void EndFlush();

        void NewSegment(ulong tStart, ulong tStop, double dRate);
    }

    [ComImport]
    [Guid("56A86892-0AD4-11CE-B03A-0020AF0BA770")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEnumPins
    {
        [PreserveSig]
        uint Next(
            [In]  uint cPins,
            [Out, MarshalAs(UnmanagedType.Interface)] out IPin ppPins,
            [Out] out uint pcFetched
        );

        [PreserveSig]
        uint Skip([In] uint cPins);

        [PreserveSig]
        uint Reset();

        [PreserveSig]
        uint Clone(
            [Out, MarshalAs(UnmanagedType.Interface)] out IEnumPins ppEnum
        );
    }

    public static class PinEx
    {
        static uint VFW_E_NOT_CONNECTED = 0x80040209;

        public static AmMediaType ConnectionMediaType(this IPin pin)
        {
            AmMediaType type = new AmMediaType();
            IntPtr ptr = Marshal.AllocCoTaskMem(Marshal.SizeOf(type.GetType()));
            int hresult = pin.ConnectionMediaType(ptr);
            Marshal.PtrToStructure(ptr, type);
            Marshal.FreeCoTaskMem(ptr);
            return type;
        }

        public static T GetFormat<T>(this IPin pin) where T : struct
        {
            AmMediaType type = pin.ConnectionMediaType();
            return type.GetStruct<T>();
        }

        public static void Dispose(this IPin pin)
        {
            if (pin != null)
            {
                Marshal.ReleaseComObject(pin);
                pin = null;
            }
        }

        public static void Dispose(this IEnumPins pins)
        {
            if (pins != null)
            {
                Marshal.ReleaseComObject(pins);
                pins = null;
            }
        }

        public static bool IsConnected(this IPin pPin)
        {
            IPin pTmp;
            uint hresult = pPin.ConnectedTo(out pTmp);
            pTmp.Dispose();
            if (hresult == VFW_E_NOT_CONNECTED)
            {
                return false;
            }
            else
            {
                Marshal.ThrowExceptionForHR((int)hresult);
                return true;
            }
        }

        public static AmMediaType[] GetAllMediaTypes(this IPin pin)
        {
            object obj = new object();
            pin.EnumMediaTypes(out obj);
            IEnumMediaTypes mediaTypes = (IEnumMediaTypes)obj;
            List<AmMediaType> ret = new List<AmMediaType>();
            int fetched = 0, hr = 0;

            while (hr == 0)
            {
                AmMediaType type = new AmMediaType();
                hr = mediaTypes.Next(1, out type, out fetched);
                ret.Add(type);
                Marshal.ThrowExceptionForHR(hr);
            }
            Marshal.ReleaseComObject(mediaTypes);
            mediaTypes = null;
            return ret.ToArray();
        }

        public static bool Match(this IPin pPin, PinDirection direction, bool bShouldBeConnected)
        {
            if (pPin.IsConnected() == bShouldBeConnected)
            {
                PinDirection pDir;
                uint hresult = pPin.QueryDirection(out pDir);
                Marshal.ThrowExceptionForHR((int)hresult);
                if (pDir == direction) return true;
            }

            return false;
        }

    }

}
