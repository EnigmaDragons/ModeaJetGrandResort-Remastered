using EnigmaDragons.NodeSystem;

public class ShowStatement : INodeCommand
{
    public Character Character { get; set; }
    public string Statement { get; set; }
}