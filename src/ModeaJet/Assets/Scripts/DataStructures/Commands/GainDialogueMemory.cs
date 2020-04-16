using System;
using EnigmaDragons.NodeSystem;

[Serializable]
public class GainDialogueMemory : INodeCommand
{
    public string Dialogue { get; set; }
    public LocationName Location { get; set; }
}