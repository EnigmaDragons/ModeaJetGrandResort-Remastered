using EnigmaDragons.NodeSystem;

public class DoesRememberDialogue : INodeCondition
{
    public string Dialogue { get; set; }
    public bool Inverse { get; set; }
}