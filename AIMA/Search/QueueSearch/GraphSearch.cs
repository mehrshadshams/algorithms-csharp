using System.Collections.Generic;
using AIMA.Agent;
using AlgorithmsLibrary.DataStructures;
using AlgorithmsLibrary.Utils;
using Optional;

namespace AIMA.Search.QueueSearch
{
    public class GraphSearch<TState, TAction> : TreeSearch<TState, TAction>
    {
        private readonly HashSet<TState> explored = new HashSet<TState>();
        
        public GraphSearch() : base()
        {
        }

        public GraphSearch(NodeFactory<TState, TAction> nodeFactory) : base(nodeFactory)
        {
        }

        public override Option<StateNode<TState, TAction>> FindSolution(IQueue<StateNode<TState, TAction>> frontier, IProblem<TState, TAction> problem)
        {
            explored.Clear();
            return base.FindSolution(frontier, problem);
        }

        protected override void AddToFrontier(IQueue<StateNode<TState, TAction>> frontier, StateNode<TState, TAction> node)
        {
            if (!explored.Contains(node.State))
            {
                base.AddToFrontier(frontier, node);
            }
        }

        protected override StateNode<TState, TAction> RemoveFromFrontier(IQueue<StateNode<TState, TAction>> frontier)
        {
            CleanUpFrontier(frontier);
            var stateNode = frontier.Dequeue();
            explored.Add(stateNode.State);
            return stateNode;
        }

        protected override bool IsFrontierEmpty(IQueue<StateNode<TState, TAction>> frontier)
        {
            CleanUpFrontier(frontier);
            return frontier.IsEmpty();
        }

        protected void CleanUpFrontier(IQueue<StateNode<TState, TAction>> frontier) {
            while (!frontier.IsEmpty() && explored.Contains(frontier.Peek().State))
            {
                frontier.Dequeue();
            }
        }
    }


}