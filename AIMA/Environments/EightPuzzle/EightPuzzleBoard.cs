using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIMA.Agent;

namespace AIMA
{
    public class EightPuzzleBoard : IEquatable<EightPuzzleBoard>, ICloneable
    {
        private static readonly (int dx, int dy)[] Directions = {(-1, 0), (1, 0), (0, -1), (0, 1)};

        public static readonly DynamicAction Left = new DynamicAction("Left");
        public static readonly DynamicAction Right = new DynamicAction("Right");
        public static readonly DynamicAction Down = new DynamicAction("Down");
        public static readonly DynamicAction Up = new DynamicAction("Up");

        private static readonly List<string> ActionNames = new List<string> {Left.Name, Right.Name, Up.Name, Down.Name};

        private static readonly DynamicAction[] Actions = {Left, Right, Up, Down};

        private readonly int[] state;

        public int Length { get; }

        private readonly int zeroIndex;
        private readonly int hashCode;

        public EightPuzzleBoard(int[] state)
        {
            this.state = state;
            this.Length = state.Length;

            for (int i = 0; i < state.Length; i++)
            {
                if (state[i] == 0)
                {
                    zeroIndex = i;
                    break;
                }
            }

            hashCode = ArrayUtils.Hash(state);
        }

        public bool Equals(EightPuzzleBoard other)
        {
            if (other == null) return false;

            return state.SequenceEqual(other.state);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is EightPuzzleBoard)) return false;
            return Equals((EightPuzzleBoard) obj);
        }

        public override int GetHashCode()
        {
            return hashCode;
        }

        public override string ToString()
        {
            int m = (int) Math.Sqrt(state.Length);
            var sb = new StringBuilder();
            for (int row = 0; row < m; row++)
            {
                for (int col = 0; col < m; col++)
                {
                    int idx = row * m + col;
                    sb.Append(state[idx] + " ");
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }

        public object Clone()
        {
            var boardClone = (int[]) state.Clone();
            return new EightPuzzleBoard(boardClone);
        }

        public static EightPuzzleBoard FromValues(IList<int> values)
        {
            return new EightPuzzleBoard(values.ToArray());
        }

        public bool IsGoal()
        {
            for (int i = 0; i < state.Length; i++)
            {
                if (state[i] != i) return false;
            }

            return true;
        }

        public IEnumerable<DynamicAction> GetActions()
        {
            return Actions.Where(CanApply);
        }

        public EightPuzzleBoard ApplyAction(DynamicAction action)
        {
            var index = ActionNames.IndexOf(action.Name);
            if (index < 0) throw new ArgumentOutOfRangeException($"Invalid action {action.Name}");

            EightPuzzleBoard clone = (EightPuzzleBoard) Clone();
            var direction = Directions[index];
            
            clone.MoveGap(direction);

            return clone;
        }

        private bool CanApply(DynamicAction action)
        {
            int m = (int) Math.Sqrt(state.Length);
            int row = zeroIndex / m, col = zeroIndex % m;
            bool result = true;
            if (action == Left)
            {
                result = col != 0;
            }
            else if (action == Right)
            {
                result = col != m - 1;
            }
            else if (action == Up)
            {
                result = row != 0;
            }
            else if (action == Down)
            {
                result = row != m - 1;
            }
            else
            {
                throw new ArgumentOutOfRangeException($"Invalid action {action.Name}");
            }

            return result;
        }

        private void MoveGap((int dx, int dy) dir)
        {
            int m = (int) Math.Sqrt(state.Length);

            int row = zeroIndex / m, col = zeroIndex % m;
            int r = row + dir.dy;
            int c = col + dir.dx;
            if (r < 0 || r >= m || c < 0 || c >= m)
            {
                return;
            }

            int j = r * m + c;
            int val = state[zeroIndex];
            state[zeroIndex] = state[j];
            state[j] = val;
        }

        private Position IndexToPosition(int index)
        {
            int m = (int) Math.Sqrt(state.Length);

            int row = index / m, col = index % m;

            return new Position(row, col);
        }
        
        public static double GetManhattanDistance(StateNode<EightPuzzleBoard, DynamicAction> node)
        {
            var board = node.State;
            double result = 0.0;
            for (int i = 0; i < board.Length; i++)
            {
                var value = board.state[i];
                if (value != 0)
                {
                    Position pos = board.IndexToPosition(i);
                    Position goal = board.IndexToPosition(value);
                    result += Math.Abs(pos.X - goal.X) + Math.Abs(pos.Y - goal.Y);
                }
            }

            return result;
        }
    }
}