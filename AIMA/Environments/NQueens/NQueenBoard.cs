using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIMA.Agent;

namespace AIMA
{
    public class NQueenBoard : ICloneable
    {
        private readonly bool[,] board;

        public NQueenBoard(int size)
        {
            board = new bool[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    board[i, j] = false;
                }
            }
        }

        private NQueenBoard(bool[,] board)
        {
            this.board = board;
        }

        public int Size => board.GetLength(0);

        public void Clear()
        {
            int size = this.Size;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    board[i, j] = false;
                }
            }
        }

        public IEnumerable<Position> QueenPositions
        {
            get
            {
                int size = Size;
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (board[i, j]) yield return new Position(i, j);
                    }
                }
            }
        }

        public int NumberOfAttackingQueens
        {
            get
            {
                var positions = QueenPositions.ToList();
                return (from p in positions
                    select GetNumberOfHorizontalAttacks(p.X, p.Y)
                           + GetNumberOfVerticalAttacks(p.X, p.Y)
                           + GetNumberOfDiagonalAttacks(p.X, p.Y)
                           ).Sum() / 2;
            }
        }

        private int GetNumberOfDiagonalAttacks(int row, int col)
        {
            int count = 0;
            int size = Size;
            
            // forward up diagonal
            for (int i = row-1, j = col+1; i >= 0 && j < size; i--, j++)
            {
                if (board[i, j])
                {
                    count++;
                }
            }
            
            // forward down diagonal
            for (int i = row+1, j = col+1; i < size && j < size; i++, j++)
            {
                if (board[i, j])
                {
                    count++;
                }
            }
            
            // backward up diagonal
            for (int i = row-1, j = col-1; i >= 0 && j >= 0; i--, j--)
            {
                if (board[i, j])
                {
                    count++;
                }
            }
            
            // backward down diagonal
            for (int i = row+1, j = col-1; i < size && j >= 0; i++, j--)
            {
                if (board[i, j])
                {
                    count++;
                }
            }

            return count;
        }
        private int GetNumberOfHorizontalAttacks(int row, int col)
        {
            int count = 0;
            for (int j = 0; j < Size; j++)
            {
                if (col != j && board[row, j])
                {
                    count++;
                }
            }

            return count;
        }

        private int GetNumberOfVerticalAttacks(int row, int col)
        {
            int count = 0;
            for (int i = 0; i < Size; i++)
            {
                if (row != i && board[i, col])
                {
                    count++;
                }
            }

            return count;
        }

        public int NumberOfQueens
        {
            get
            {
                int queens = 0;
                int size = Size;
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (board[i, j])
                        {
                            queens++;
                        }
                    }
                }

                return queens;
            }
        }

        public void Set(Position position)
        {
            board[position.X, position.Y] = true;
        }

        public void Remove(Position position)
        {
            board[position.X, position.Y] = false;
        }

        public bool QueenExistsAt(Position position)
        {
            return board[position.X, position.Y];
        }

        public void MoveQueen(Position position)
        {
            for (int row = 0; row < Size; row++)
            {
                board[row, position.Y] = false;
            }

            board[position.X, position.Y] = true;
        }

        public bool IsGoal()
        {
            return NumberOfQueens == Size && NumberOfAttackingQueens == 0;
        }

        public IEnumerable<NQueenAction> GetActions()
        {
            int numberOfQueens = NumberOfQueens;
            
            if (NumberOfQueens == Size)
            {
                yield break;
            }

            for (int col = 0; col < Size; col++)
            {
                if (!IsSquareUnderAttack(numberOfQueens, col))
                {
                    yield return new NQueenAction(NQueenActionType.PlaceQueen, new Position(numberOfQueens, col));
                }
            }
        }

        private bool IsSquareUnderAttack(int row, int col)
        {
            return GetNumberOfHorizontalAttacks(row, col) > 0 ||
                   GetNumberOfVerticalAttacks(row, col) > 0 ||
                   GetNumberOfDiagonalAttacks(row, col) > 0;
        }

        public NQueenBoard ApplyAction(NQueenAction action)
        {
            NQueenBoard result = (NQueenBoard) this.Clone();
            
            switch (action.Type)
            {
                case NQueenActionType.PlaceQueen:
                    result.Set(action.Position);
                    break;
                case NQueenActionType.RemoveQueen:
                    result.Remove(action.Position);
                    break;
                case NQueenActionType.MoveQueen:
                    result.MoveQueen(action.Position);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return result;
        }

        public object Clone()
        {
            var newBoard = (bool[,]) board.Clone();
            return new NQueenBoard(newBoard);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            var size = Size;
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (board[i, j])
                    {
                        sb.Append(" X ");
                    }
                    else
                    {
                        sb.Append(" _ ");
                    }
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}