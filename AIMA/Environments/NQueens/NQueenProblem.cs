using System;
using System.Collections.Generic;
using AIMA.Search;

namespace AIMA
{
    public class NQueenProblem : GeneralProblem<NQueenBoard, NQueenAction>
    {
        public NQueenProblem(NQueenBoard initialState)
         : base(initialState, board => board.GetActions(), (board, action) => board.ApplyAction(action),
             board => board.IsGoal())
        {
        }
    }
}