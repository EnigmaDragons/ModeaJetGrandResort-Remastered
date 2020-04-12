using System.Collections.Generic;
using Assets.Scripts.DataStructures.Commands;
using EnigmaDragons.NodeSystem;

public class PresentOptions : INodeCommand
{
    public List<ChooseOption> Options { get; set; }
}
