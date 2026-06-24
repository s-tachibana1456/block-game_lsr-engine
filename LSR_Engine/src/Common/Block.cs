using System;
using System.Collections.Generic;
using System.Text;

namespace LSR_Engine.src.Common
{
    internal class Block
    {
        public readonly string Shape;
        public readonly IReadOnlyList<IReadOnlyList<int>> Data;
        public readonly int Angle;

        public Block(string shape, IReadOnlyList<IReadOnlyList<int>> data, int angle)
        {
            Shape = shape;
            Data = data;
            Angle = angle;
        }
    }
}
