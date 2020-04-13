using System;
using EnigmaDragons.NodeSystem;

[Serializable]
public class GainDialogueMemory : INodeCommand
{
    public string Dialogue { get; set; }
    public string Location { get; set; }
}