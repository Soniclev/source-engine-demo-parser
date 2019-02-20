using System;
using System.IO;
using System.Linq;
using SourceEngineDemoParser.Frames;

namespace SourceEngineDemoParser
{
    static class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please, specify path to the recorded demo file.");
                Console.ReadKey();
                return;
            }

            foreach (var demoFileName in args)
            {
                if (!File.Exists(demoFileName))
                {
                    Console.WriteLine($"'{demoFileName}' is not exist.");
                    continue;
                }

                try
                {
                    ProccesDemo(demoFileName);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    continue;
                }

                Console.WriteLine();
            }

            Console.ReadKey();
        }

        private static void ProccesDemo(string path)
        {
            var demoReader = new DemoReader(path);
            demoReader.ReadHeader();

            Console.WriteLine("{0} Map: {1}, Ticks: {2}, Playback time: {3} seconds",Path.GetFileName(path), demoReader.Header.MapName, demoReader.Header.Ticks, demoReader.Header.PlaybackTime);
            Console.WriteLine("Press any key to continue parsing the demo file...");
            Console.ReadKey();
            var loop = true;
            while (loop)
            {
                var frame = demoReader.ReadFrame();
                switch (frame.Type)
                {
                    case FrameType.Signon:
                        break;
                    case FrameType.Packet:
                        break;
                    case FrameType.SyncTick:
                        break;
                    case FrameType.ConsoleCmd:
                        var consoleCmdFrame = (ConsoleCmdFrame)frame;
                        Console.WriteLine(consoleCmdFrame.Command);
                        break;
                    case FrameType.UserCmd:
                        break;
                    case FrameType.Datatables:
                        break;
                    case FrameType.Stop:
                        loop = false;
                        break;
                    case FrameType.StringTables:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            demoReader.Dispose();
        }
    }
}
