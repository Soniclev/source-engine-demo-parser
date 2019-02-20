using System;
using System.Collections.Generic;
using System.Text;

namespace SourceEngineDemoParser.Frames
{
    public class SignonFrame : Frame
    {
        public byte[] Data { get; set; }

        public SignonFrame(int tick) : base(FrameType.Signon, tick)
        {
        }

        public SignonFrame(int tick, byte[] data) : base(FrameType.Signon, tick)
        {
            Data = data;
        }
    }
}
