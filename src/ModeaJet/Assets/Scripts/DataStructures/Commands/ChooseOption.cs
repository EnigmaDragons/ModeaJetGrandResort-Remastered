using EnigmaDragons.NodeSystem;

namespace Assets.Scripts.DataStructures.Commands
{
    public class ChooseOption : INodeCommand
    {
        public string Option { get; set; }
        //Special Named Value
        public string[] NodeTreeIds { get; set; }
    }
}
