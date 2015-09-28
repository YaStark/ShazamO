using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DirectShowNet;
using System.Runtime.InteropServices;
using System.Threading;

namespace ShazamO
{
    public class DSConverter
    {
        public static int WM_GRAPHNOTIFY = 0x8001;
        const int S_OK = 0;
        const long VFW_E_WRONG_STATE = 0x80040227;

        public FilterGraphNoThread filterGraph = new FilterGraphNoThread();

        public IGraphBuilder graphBuilder;

        public IMediaControl mediaControl;

        public IMediaEventEx mediaEvent;

        int hresult = 0;

        public void InitInterfaces()
        {
            filterGraph = new FilterGraphNoThread();
            graphBuilder = (IGraphBuilder)filterGraph;
            mediaControl = (IMediaControl)filterGraph;
            mediaEvent = (IMediaEventEx)filterGraph;
        }

        public void CloseInterfaces()
        {
            if (mediaEvent != null)
            {
                hresult = mediaControl.Stop();
                Marshal.ThrowExceptionForHR(hresult);
            }
            mediaControl = null;
            mediaEvent = null;
            graphBuilder = null;
            if (filterGraph != null) Marshal.ReleaseComObject(filterGraph);
            filterGraph = null;
        }

        public void Splitter(string FileName)
        {
                // 0. filename
            string sfname = "Source File";
            graphBuilder.AddSourceFilter(FileName, sfname);
            IBaseFilter file = graphBuilder.FindFilterByName(sfname);

                // 1. mpeg1 stream splitter "336475D0-942A-11CE-A870-00AA002FEAB5"
                // split stream on audio and video
            DSMPEG1StreamSplittter ssf = new DSMPEG1StreamSplittter();
            graphBuilder.AddFilter(ssf.BaseFilter, ssf.Name);

                // 2. ACM Wrapper "6A08CF80-0E18-11CF-A24D-0020AFD79767"
                // decode from mpeg1-layer 3 to pcm
            DSACMWrapper awf = new DSACMWrapper();
            graphBuilder.AddFilter(awf.BaseFilter, awf.Name);

                // 3. sample grabber "C1F400A0-3F08-11D3-9F0B-006008039E37"
            DSSampleGrabber sgf = new DSSampleGrabber();
            graphBuilder.AddFilter(sgf.BaseFilter, sgf.Name);
            ISampleGrabber sgr = (ISampleGrabber)sgf.BaseFilter;

                // 4. null renderer "C1F400A4-3F08-11D3-9F0B-006008039E37"
            DSNullRenderer nrf = new DSNullRenderer();
            graphBuilder.AddFilter(nrf.BaseFilter, nrf.Name);

            graphBuilder.ConnectFilters(file, ssf.BaseFilter);
            graphBuilder.ConnectFilters(ssf.BaseFilter, awf.BaseFilter);
            graphBuilder.ConnectFilters(awf.BaseFilter, sgf.BaseFilter);
            graphBuilder.ConnectFilters(sgf.BaseFilter, nrf.BaseFilter);

            SGCallback sg = new SGCallback();
            sgr.SetBufferSamples(false);
            hresult = sgr.SetOneShot(false);
            ShowMsg(hresult.ToString("x"));
            hresult = sgr.SetCallback(sg, 0);
            ShowMsg(hresult.ToString("x"));

            // Render
            graphBuilder.RenderFile(FileName, null);

            mediaControl.Run();
            Thread.Sleep(2000);
            mediaControl.Stop();

            ssf.Dispose();
            awf.Dispose();
            sgf.Dispose();
            nrf.Dispose();
        }

        public AmMediaType GetMinimumFreq(IPin OutputPin)
        {
            AmMediaType[] types = OutputPin.GetAllMediaTypes();
            IAMStreamConfig cfg = (IAMStreamConfig)OutputPin;
            AmMediaType currentType = cfg.GetFormat();
            WaveFormatEx currentWave = currentType.GetStruct<WaveFormatEx>();
            ShowMsg(String.Format(
                "CurrentOut: bps:{0} bit; ch:{1}, freq:{2} Hz",
                currentWave.wBitsPerSample, currentWave.nChannels, currentWave.nSamplesPerSec
                ));
            AmMediaType minimum = currentType;
            foreach (AmMediaType type in types)
            {
                WaveFormatEx wave = type.GetStruct<WaveFormatEx>();
                ShowMsg(String.Format(
                    "bps:{0} bit; ch:{1}, freq:{2} Hz",
                    wave.wBitsPerSample, wave.nChannels, wave.nSamplesPerSec
                    ));
                if (wave.wFormatTag == currentWave.wFormatTag &&
                    wave.nChannels == currentWave.nChannels &&
                    wave.wBitsPerSample == currentWave.wBitsPerSample &&
                    wave.nSamplesPerSec < currentWave.nSamplesPerSec)
                {
                    minimum = type;
                    currentWave = minimum.GetStruct<WaveFormatEx>();
                }
            }
            ShowMsg(String.Format(
                "Selected: bps:{0} bit; ch:{1}, freq:{2} Hz",
                currentWave.wBitsPerSample, currentWave.nChannels, currentWave.nSamplesPerSec
                ));
            return minimum;
        }

        public void Mp3ToWAV(string FileSource, string FileDest)
        {
            // 0. filename
            string sfname = "Source File";
            graphBuilder.AddSourceFilter(FileSource, sfname);
            IBaseFilter file = graphBuilder.FindFilterByName(sfname);

            // 1. mpeg1 stream splitter
            DSMPEG1StreamSplittter msf = new DSMPEG1StreamSplittter();
            hresult = graphBuilder.AddFilter(msf.BaseFilter, msf.Name);
            Marshal.ThrowExceptionForHR(hresult);
            
            // 2. acm wrapper
            DSACMWrapper awf = new DSACMWrapper();
            hresult = graphBuilder.AddFilter(awf.BaseFilter, awf.Name);
            Marshal.ThrowExceptionForHR(hresult);

            // 3. acm wrapper
            DSACMWrapper awf2 = new DSACMWrapper();
            awf2.Name += "2";
            hresult = graphBuilder.AddFilter(awf2.BaseFilter, awf2.Name);
            Marshal.ThrowExceptionForHR(hresult);

            // 4. WAV Dest {3C78B8E2-6C4D-11D1-ADE2-0000F8754B99}
            DSWAVDest wdf = new DSWAVDest();
            hresult = graphBuilder.AddFilter(wdf.BaseFilter, wdf.Name);
            Marshal.ThrowExceptionForHR(hresult);

            // 5. File writer {8596E5F0-0DA5-11D0-BD21-00A0C911CE86}
            DSFileWriter fwf = new DSFileWriter();
            fwf.FileSinkFilter.SetFileName(FileDest, null);
            hresult = graphBuilder.AddFilter(fwf.BaseFilter, fwf.Name);
            Marshal.ThrowExceptionForHR(hresult);

            // 6. Connection
            graphBuilder.ConnectFilters(file, msf.BaseFilter);
            graphBuilder.ConnectFilters(msf.BaseFilter, awf.BaseFilter);
            graphBuilder.ConnectFilters(awf.BaseFilter, awf2.BaseFilter);
            graphBuilder.ConnectFilters(awf2.BaseFilter, wdf.BaseFilter);
            graphBuilder.ConnectFilters(wdf.BaseFilter, fwf.BaseFilter);

            IPin outPin = awf2.BaseFilter.FindPin(PinDirection.Output);
            AmMediaType media = GetMinimumFreq(outPin);
            IAMStreamConfig cfg = (IAMStreamConfig)outPin;
            cfg.SetFormat(media);
            
            outPin.Dispose();

            // 6. Render
            graphBuilder.RenderFile(FileSource, null);

            mediaControl.Run();
            Thread.Sleep(1000);
            mediaControl.Stop();

            msf.Dispose();
            awf.Dispose();
            wdf.Dispose();
            fwf.Dispose();
        }

        public static void ShowMsg(string text)
        {
            Console.WriteLine(text);
        }
    }

    public class SGCallback : ISampleGrabberCB
    {
        public int BufferCB(double SampleTime, IntPtr pBuffer, int BufferLen)
        {
            DSConverter.ShowMsg("BufferCB");
            return 0;
        }

        public int SampleCB(double SampleTime, IMediaSample pSample)
        {
            DSConverter.ShowMsg("SampleCB");
            return 0;
        }
    }

    
}
