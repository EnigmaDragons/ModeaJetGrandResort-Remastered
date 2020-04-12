using UnityEngine;
using UnityEngine.UI;

namespace EnigmaDragons.NodeSystem
{
    public class StartNodeTreeOnClick : MonoBehaviour
    {
        [SerializeField] private TextAsset nodeTree;
        [SerializeField] private CurrentNodeTree currentNodeTree;
        [SerializeField] private Button button;

        public void OnEnable()
        {
            button.onClick.AddListener(() => currentNodeTree.StartNodeTree(nodeTree));
        }
    }
}
