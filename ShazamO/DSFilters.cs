using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DirectShowNet;
using System.Runtime.InteropServices;

namespace ShazamO
{
    public class DSFilter : IDisposable
    {
        public Guid Guid { get; set; }
        public Type ComType { get; set; }
        public IBaseFilter BaseFilter { get; set; }
        public bool IsDisposed { get; set; }
        public string Name { get; set; }

        protected object _instance;

        public DSFilter()
        {
            IsDisposed = false;
        }

        public void SetGuid(string GuidText)
        {
            Guid = new Guid(GuidText);
            ComType = Type.GetTypeFromCLSID(Guid);
            _instance = Activator.CreateInstance(ComType);
            BaseFilter = (IBaseFilter)_instance;
        }

        public void Dispose()
        {
            if (IsDisposed) return;
            if (BaseFilter != null)
                Marshal.ReleaseComObject(BaseFilter);
            BaseFilter = null;
            IsDisposed = true;
        }
    }

    public class DSMPEG1StreamSplittter : DSFilter
    {
        public DSMPEG1StreamSplittter()
            : base()
        {
            SetGuid("336475D0-942A-11CE-A870-00AA002FEAB5");
            Name = "MPEG-I Stream Splitter";
        }
    }

    public class DSACMWrapper : DSFilter
    {
        public DSACMWrapper()
            : base()
        {
            SetGuid("6A08CF80-0E18-11CF-A24D-0020AFD79767");
            Name = "ACM Wrapper";
        }
    }

    public class DSSampleGrabber : DSFilter
    {
        public ISampleGrabber SampleGrabber { get; set; }

        public DSSampleGrabber()
            : base()
        {
            SetGuid("C1F400A0-3F08-11D3-9F0B-006008039E37");
            Name = "SampleGrabber";
            SampleGrabber = (_instance as ISampleGrabber);
        }
    }

    public class DSNullRenderer : DSFilter
    {
        public DSNullRenderer()
            : base()
        {
            SetGuid("C1F400A4-3F08-11D3-9F0B-006008039E37");
            Name = "Null Renderer";
        }
    }

    public class DSWAVDest : DSFilter
    {
        public DSWAVDest()
            : base()
        {
            SetGuid("3C78B8E2-6C4D-11D1-ADE2-0000F8754B99");
            Name = "WAV Dest";
        }
    }

    public class DSFileWriter : DSFilter
    {
        public IFileSinkFilter FileSinkFilter { get; set; }

        public DSFileWriter()
            : base()
        {
            SetGuid("8596E5F0-0DA5-11D0-BD21-00A0C911CE86");
            Name = "File writer";
            FileSinkFilter = (IFileSinkFilter)_instance;
        }
    }

}
