using System;
using System.Collections.Generic;
using System.Text;

namespace SourceEngineDemoParser.Frames
{
    public class SyncTickFrame : Frame
    {
        public SyncTickFrame(int tick) : base(FrameType.SyncTick, tick)
        {
            
        }
    }
}
