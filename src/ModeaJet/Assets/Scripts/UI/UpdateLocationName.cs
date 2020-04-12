using TMPro;
using UnityEngine;

public sealed class UpdateLocationName : OnMessage<ArrivedAtLocation>
{
    [SerializeField] private TextMeshProUGUI label;

    protected override void Execute(ArrivedAtLocation msg) => label.text = msg.Location.DisplayName;
}
