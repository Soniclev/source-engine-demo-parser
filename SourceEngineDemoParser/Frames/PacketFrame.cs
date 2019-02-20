using System;
using System.Collections.Generic;
using System.Text;

namespace SourceEngineDemoParser.Frames
{
    public class PacketFrame : Frame
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public PacketFrame(int tick) : base(FrameType.Packet, tick)
        {
        }

        public PacketFrame(int tick, float x, float y, float z) : base(FrameType.Packet, tick)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
