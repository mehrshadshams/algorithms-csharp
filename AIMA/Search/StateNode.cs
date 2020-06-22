using System;
using System.Collections.Generic;
using System.Linq;

namespace AIMA
{
    public class StateNode<TState, TAction>
    {
        public static Comparison<StateNode<TState, TAction>> IncreasingPathCost =
            (node1, node2) => node1.PathCost.CompareTo(node2.PathCost);

        public TState State { get; }
        public int Depth { get; }
        public TAction Action { get; }
        public StateNode<TState, TAction> Parent { get; }

        public double PathCost { get; }

        public bool IsRoot => Parent == null;

        public StateNode(TState state) : this(state,  default, default, 0, 0.0)
        {
        }

        public StateNode(TState state, TAction action = default, StateNode<TState, TAction> parent = default, int depth = 0, double cost = 0.0)
        {
            this.State = state;
            this.Action = action;
            this.Parent = parent;
            this.Depth = depth;
            this.PathCost = cost;
        }

        public IList<StateNode<TState, TAction>> Path
        {
            get
            {
                StateNode<TState, TAction> state = this;
                var path = new List<StateNode<TState, TAction>>();
                while (state != null)
                {
                    path.Add(state);
                    state = state.Parent;
                }

                path.Reverse();

                return path;
            }
        }

        public IList<TAction> Actions => Path.Skip(1).Select(x => x.Action).ToList();
    }
}