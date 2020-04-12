
using TMPro;
using UnityEngine;

public sealed class DisplayTooltip : OnMessage<ShowUiTooltip>
{
    [SerializeField] private TextMeshProUGUI label;

    protected override void Execute(ShowUiTooltip msg) => label.text = msg.Tooltip ?? "";
}
