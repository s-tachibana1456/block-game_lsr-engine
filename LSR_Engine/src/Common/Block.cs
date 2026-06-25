namespace LSR_Engine.src.Common
{
    public class Block
    {
        public readonly string Shape;
        public readonly byte[,] Data;
        public readonly int Angle;

        public Block(string shape, byte[,] data, int angle)
        {
            Shape = shape;
            Data = data;
            Angle = angle;
        }
    }
}
