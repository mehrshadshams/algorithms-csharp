using System;
using Optional;

namespace AIMA.Agent
{
    public class SimpleAgent<TPercept, TAction> : IAgent<TPercept, TAction>
    {
        private readonly AgentProgram<TPercept, TAction> program;
        
        public SimpleAgent() : this(default)
        {
            Alive = true;
        }

        public SimpleAgent(AgentProgram<TPercept, TAction> program)
        {
            this.program = program;
        }
        
        public virtual Option<TAction> Act(TPercept percept)
        {
            throw new NotImplementedException();
        }

        public bool Alive { get; set; }
    }
}