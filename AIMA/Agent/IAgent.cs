using Optional;

namespace AIMA.Agent
{
    public delegate Option<TAction> AgentProgram<TPercept, TAction>(TPercept percept);
    
    public interface IAgent<TPercept, TAction> : IEnvironmentObject
    {
        Option<TAction> Act(TPercept percept);

        bool Alive { get; set; }
    }

}