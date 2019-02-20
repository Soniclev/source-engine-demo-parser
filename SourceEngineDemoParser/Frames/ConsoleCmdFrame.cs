using System;
using System.Collections.Generic;
using System.Text;

namespace SourceEngineDemoParser.Frames
{
    public class ConsoleCmdFrame : Frame
    {
        public string Command { get; set; }

        public ConsoleCmdFrame(int tick) : base(FrameType.ConsoleCmd, tick)
        {
            
        }

        public ConsoleCmdFrame(int tick, string command) : base(FrameType.ConsoleCmd, tick)
        {
            Command = command;
        }
    }
}
