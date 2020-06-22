using AIMA.Agent;

namespace AIMA
{
    public enum NQueenActionType
    {
        PlaceQueen,
        RemoveQueen,
        MoveQueen
    }

    public struct Position
    {
        public int X { get; }
        public int Y { get; }

        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
    
    public class NQueenAction : IAction
    {
        public Position Position { get; private set; }
        public NQueenActionType Type { get; private set; }

        public NQueenAction(NQueenActionType type, Position position)
        {
            Position = position;
            Type = type;
        }

        public override string ToString()
        {
            return $"Action[name={Type}, location={Position}]";
        }
    }
}