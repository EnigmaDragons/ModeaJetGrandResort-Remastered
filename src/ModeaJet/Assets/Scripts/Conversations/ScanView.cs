using System;
using TMPro;
using UnityEngine;

public sealed class ScanView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameLabel;
    [SerializeField] private TextMeshProUGUI scanInfoArea;

    private bool _isDisplaying;
    private Action _onFinished = () => { };
    
    public void Show(Character c, Action onFinished)
    {
        _onFinished = onFinished;
        gameObject.SetActive(true);
        nameLabel.text = c.ScanName;
        scanInfoArea.text = c.ScanInfo;
        _isDisplaying = true;
    }

    public void Dismiss()
    {
        if (!_isDisplaying)
            return;
        
        _onFinished();
        gameObject.SetActive(false);
        _isDisplaying = false;
    }
}
