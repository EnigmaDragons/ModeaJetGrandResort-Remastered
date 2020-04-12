using System.Collections.Generic;
using Assets.Scripts.DataStructures.Commands;
using EnigmaDragons.NodeSystem;

public class ShowOptions : INodeCommand
{
    public List<Option> Options { get; set; }
}
