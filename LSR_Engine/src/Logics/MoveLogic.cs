using LSR_Engine.src.Common;
using System.Collections.Generic;

namespace LSR_Engine.src.Logics
{
    internal static class MovementMap
    {
        public static readonly IReadOnlyDictionary<Actions, Position> Map =
            new Dictionary<Actions, Position>
            {
                [Actions.MoveLeft] = new Position(-1, 0),
                [Actions.MoveRight] = new Position(1, 0),
                [Actions.MoveUp] = new Position(0, -1),
                [Actions.MoveDown] = new Position(0, 1)
            };
    }

    internal static class MoveLogic
    {
        public static Position Move(Actions action, Position position, Block block, int mapSize)
        {
            var diff = MovementMap.Map[action];

            Position nextPosition = new Position(diff.X, diff.Y);

            // 新たな座標に移動できない場合は、元の座標を返す。
            if (!MovementChecker.CanMoveTo(block, nextPosition.X, nextPosition.Y, mapSize)) return position;

            return nextPosition;
        }
    }
}
