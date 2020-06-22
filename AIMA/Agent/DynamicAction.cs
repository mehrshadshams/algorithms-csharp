namespace AIMA.Agent
{
    public class DynamicAction : DynamicObject, IAction
    {
        public string Name { get; }

        public DynamicAction(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}