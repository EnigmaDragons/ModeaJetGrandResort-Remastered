using EnigmaDragons.NodeSystem;

namespace Assets.Scripts.DataStructures.Commands
{
    public class Option : INodeCommand
    {
        public string Text { get; set; }
        //Special Named Value
        public string[] NodeTreeIds { get; set; }
    }
}
