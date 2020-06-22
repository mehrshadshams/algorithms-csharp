using AIMA.Agent;
using AIMA.Search;

namespace AIMA
{
    public class EightPuzzleProblem : GeneralProblem<EightPuzzleBoard, DynamicAction>
    {
        public EightPuzzleProblem(EightPuzzleBoard initialState)
            : base(initialState, board => board.GetActions(),
                (board, action) => board.ApplyAction(action),
                board => board.IsGoal())
        {

        }
    }
}