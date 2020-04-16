using EnigmaDragons.NodeSystem;

public class HasViewedItem : INodeCondition
{
    public string Item { get; set; }
    public bool Inverse { get; set; }
}