namespace SourceEngineDemoParser.Frames
{
    public abstract class Frame
    {
        public FrameType Type { get; private set; }
        public int Tick { get; private set; }

        protected Frame(FrameType type, int tick)
        {
            Type = type;
            Tick = tick;
        }
    }
}
