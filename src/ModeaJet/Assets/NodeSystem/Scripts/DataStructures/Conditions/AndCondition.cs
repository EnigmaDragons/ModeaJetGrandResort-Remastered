namespace EnigmaDragons.NodeSystem
{
    public class AndCondition : INodeCondition
    {
        public INodeCondition[] Conditions { get; set; }
    }
}
