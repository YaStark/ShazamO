using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DirectShowNet
{

    [ComImport,
    //Guid("C1F400A0-3F08-11D3-9F0B-006008039E37"),
    Guid("6B652FFF-11FE-4FCE-92AD-0266B5D7C78F"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ISampleGrabber
    {
        [PreserveSig]
        int GetConnectedMediaType(
            [In, MarshalAs(UnmanagedType.Interface)] AmMediaType pType
        );

        [PreserveSig]
        int GetCurrentBuffer(
            [In, Out] ref int pBufferSize,
            [Out, MarshalAs(UnmanagedType.LPArray)]     out int[] pBuffer
        );

        [PreserveSig]
        int GetCurrentSample(
            [Out, MarshalAs(UnmanagedType.Interface)] out IMediaSample ppSample
        );

        void SetBufferSamples(
            [In] bool BufferThem
        );

        [PreserveSig]
        int SetCallback(
            [In, MarshalAs(UnmanagedType.Interface)] ISampleGrabberCB pCallback,
            [In] int WhichMethodToCallback
        );

        [PreserveSig]
        int SetMediaType(
            [In, MarshalAs(UnmanagedType.Interface)] AmMediaType pType
        );

        [PreserveSig]
        int SetOneShot(
            [In] bool OneShot
        );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("0579154A-2B53-4994-B0D0-E773148EFF85"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ISampleGrabberCB
    {
        [PreserveSig]
        int BufferCB(
            [In] double SampleTime,
            [In] IntPtr pBuffer,
            [In] int BufferLen
        );

        [PreserveSig]
        int SampleCB(
            [In] double SampleTime,
            [In, MarshalAs(UnmanagedType.Interface)] IMediaSample pSample
        );
    }
}
