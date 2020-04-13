
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class ConversationOptionButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private Button button;

    private Action _onSelect = () => { };

    private void Awake() => button.onClick.AddListener(() => _onSelect());
    
    public void Show(string text, Action onSelect)
    {
        label.text = text;
        _onSelect = onSelect;
        gameObject.SetActive(true);
    }

    public void Activate() => button.onClick.Invoke();
}
