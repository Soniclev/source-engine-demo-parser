using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SourceEngineDemoParser.Frames;

namespace SourceEngineDemoParser
{
    public class DemoReader : IDisposable
    {
        private const string SOURCE_ENGINE_DEMO_HEADER = "HL2DEMO";
        private const int SOURCE_ENGINE_DEMO_PROTOCOL = 3;
        private readonly BinaryReader _binaryReader;
        public DemoHeader Header;

        public DemoReader(BinaryReader binaryReader)
        {
            _binaryReader = binaryReader;
        }

        public DemoReader(string path)
        {
            var input = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            _binaryReader = new BinaryReader(input);
        }

        public void ReadHeader()
        {
            Header = new DemoHeader();
            Header.Header = StringUtils.ReadNullTerminatedString(_binaryReader);
            if (Header.Header != SOURCE_ENGINE_DEMO_HEADER)
            {
                throw new FileLoadException($"Demo header '{Header.Header}' is not equal to '{SOURCE_ENGINE_DEMO_HEADER}'!");
            }

            Header.DemoProtocol = _binaryReader.ReadInt32();
            if (Header.DemoProtocol != SOURCE_ENGINE_DEMO_PROTOCOL)
            {
                throw new FileLoadException($"Demo protocol '{Header.DemoProtocol}' is not equal to '{SOURCE_ENGINE_DEMO_PROTOCOL}'!");
            }

            Header.NetworkProtocol = _binaryReader.ReadInt32();
            Header.ServerName = StringUtils.ReadSizedNullString(_binaryReader, 260);
            Header.ClientName = StringUtils.ReadSizedNullString(_binaryReader, 260);
            Header.MapName = StringUtils.ReadSizedNullString(_binaryReader, 260);
            Header.GameDirectory = StringUtils.ReadSizedNullString(_binaryReader, 260);
            Header.PlaybackTime = _binaryReader.ReadSingle();
            Header.Ticks = _binaryReader.ReadInt32();
            Header.Frames = _binaryReader.ReadInt32();
            Header.SignonDataLength = _binaryReader.ReadInt32();
        }

        public Frame ReadFrame()
        {
            if (Header == null)
                ReadHeader();
            FrameType type = (FrameType)_binaryReader.ReadByte();
            if (type == FrameType.Stop)
                return new StopFrame();
            int tick = _binaryReader.ReadInt32();
            int length;
            switch (type)
            {
                case FrameType.Signon:
                    return new SignonFrame(tick, _binaryReader.ReadBytes(Header.SignonDataLength));
                case FrameType.Packet:
                    _binaryReader.ReadBytes(4);

                    var x = _binaryReader.ReadSingle();
                    var y = _binaryReader.ReadSingle();
                    var z = _binaryReader.ReadSingle();

                    _binaryReader.ReadBytes(0x44);
                    length = _binaryReader.ReadInt32();
                    _binaryReader.ReadBytes(length);
                    return new PacketFrame(tick, x, y, z);
                case FrameType.SyncTick:
                    return new SyncTickFrame(tick);
                case FrameType.ConsoleCmd:
                    length = _binaryReader.ReadInt32();
                    string command = StringUtils.ReadSizedNullString(_binaryReader, length);
                    return new ConsoleCmdFrame(tick, command);
                case FrameType.UserCmd:
                    _binaryReader.ReadInt32(); // skip sequence
                    length = _binaryReader.ReadInt32();
                    _binaryReader.ReadBytes(length);
                    return new UserCmdFrame(tick);
                case FrameType.Datatables:
                    length = _binaryReader.ReadInt32();
                    _binaryReader.ReadBytes(length);
                    return new DatatablesFrame(tick);
                case FrameType.StringTables:
                    length = _binaryReader.ReadInt32();
                    _binaryReader.ReadBytes(length);
                    return new StringTablesFrame(tick);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Dispose()
        {
           _binaryReader.Dispose();
        }

        public class DemoHeader
        {
            public string Header { get; set; }
            public int DemoProtocol { get; set; }
            public int NetworkProtocol { get; set; }
            public string ServerName { get; set; }
            public string ClientName { get; set; }
            public string MapName { get; set; }
            public string GameDirectory { get; set; }
            public float PlaybackTime { get; set; }
            public int Ticks { get; set; }
            public int Frames { get; set; }
            public int SignonDataLength { get; set; }
            public int TickRate => (int)Math.Floor(Ticks / PlaybackTime);
        }
    }
}
