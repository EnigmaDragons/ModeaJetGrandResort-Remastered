using EnigmaDragons.NodeSystem;

public class ChangeExpression : INodeCommand
{
    public Character Character { get; set; }
    public Expression Expression { get; set; }
}