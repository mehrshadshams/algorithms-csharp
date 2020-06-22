using AIMA.Agent;

namespace AIMA.Environments.Map
{
    public class MoveToAction : DynamicAction
    {
        private static readonly string Location = "location";
        private static readonly string MoveTo = "moveTo";
        
        public MoveToAction(string location) : base(MoveTo)
        {
            this[Location] = location;
        }

        public string To => this[Location] as string;
    }
}