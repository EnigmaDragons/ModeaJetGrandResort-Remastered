using EnigmaDragons.NodeSystem;

public class Option : INodeObject
{
    public string Text { get; set; }
    //Special Named Value
    public string[] NodeTreeIds { get; set; }
}