using System;
using System.Collections.Generic;
using System.Text;

namespace SourceEngineDemoParser
{
    public enum FrameType
    {
        Signon = 1,
        Packet = 2,
        SyncTick = 3,
        ConsoleCmd = 4,
        UserCmd = 5,
        Datatables = 6,
        Stop = 7,
        StringTables = 8,
        Lastcommand = StringTables
    }
}
