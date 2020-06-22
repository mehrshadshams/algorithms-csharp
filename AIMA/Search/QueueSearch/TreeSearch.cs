using AIMA.Agent;
using AlgorithmsLibrary.DataStructures;
using AlgorithmsLibrary.Utils;
using Optional;

namespace AIMA.Search.QueueSearch
{
    public class TreeSearch<TState, TAction> : ISearchStrategy<TState, TAction>
    {
        private readonly NodeFactory<TState, TAction> nodeFactory;
        private bool earlyGoalTest;

        public TreeSearch(): this(new NodeFactory<TState, TAction>())
        {
        }
        
        public TreeSearch(NodeFactory<TState, TAction> nodeFactory)
        {
            this.nodeFactory = nodeFactory;
        }

        public virtual Option<StateNode<TState, TAction>> FindSolution(IQueue<StateNode<TState, TAction>> frontier, IProblem<TState, TAction> problem)
        {
            StateNode<TState, TAction> root = nodeFactory.Create(problem.InitialState);
            AddToFrontier(frontier, root);
            if (earlyGoalTest && problem.GoalTest(root.State))
            {
                return Option.Some(root);
            }
            
            while (!IsFrontierEmpty(frontier)) {
                // choose a leaf node and remove it from the frontier
                StateNode<TState, TAction> node = RemoveFromFrontier(frontier);
                
                // if the node contains a goal state then return the corresponding solution
                if (!earlyGoalTest && problem.GoalTest(node.State))
                    return Option.Some(node);

                // expand the chosen node and add the successor nodes to the frontier
                foreach (StateNode<TState, TAction> successor in nodeFactory.GetSuccessors(node, problem)) {
                    AddToFrontier(frontier, successor);
                    if (earlyGoalTest && problem.GoalTest(successor.State))
                        return Option.Some(successor);
                }
            }
            
            return Option.None<StateNode<TState, TAction>>();
        }

        protected virtual StateNode<TState,TAction> RemoveFromFrontier(IQueue<StateNode<TState, TAction>> frontier)
        {
            return frontier.Dequeue();
        }

        protected virtual void AddToFrontier(IQueue<StateNode<TState, TAction>> frontier, StateNode<TState, TAction> node)
        {
            frontier.Add(node);
        }

        protected virtual bool IsFrontierEmpty(IQueue<StateNode<TState, TAction>> frontier)
        {
            return frontier.IsEmpty();
        }
    }

}