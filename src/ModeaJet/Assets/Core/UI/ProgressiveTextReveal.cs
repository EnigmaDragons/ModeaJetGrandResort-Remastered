using System;
using System.Collections;
using TMPro;
using UnityEngine;

public sealed class ProgressiveTextReveal : MonoBehaviour
{
    [SerializeField] private GameObject chatBox;
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private FloatReference secondsPerCharacter;
    [SerializeField, ReadOnly] private bool isRevealing;
    
    private int _cursor;
    private string _fullText = "" ;
    private Action _onFinished = () => { };

    public void Hide() => chatBox.SetActive(false);

    public void Display(string text) => Display(text, () => { });
    public void Display(string text, Action onFinished)
    {
        if (isRevealing)
            return;
        
        _fullText = text;
        _onFinished = onFinished;
        StartCoroutine(BeginReveal());
    }

    public void Proceed()
    {
        if (isRevealing)
            ShowCompletely();
        else
            _onFinished();
    }
    
    public void ShowCompletely()
    {
        textBox.text = _fullText;
        isRevealing = false;
    }

    private IEnumerator BeginReveal()
    {
        isRevealing = true;
        chatBox.SetActive(true);
        _cursor = 1;
        while (isRevealing && _cursor < _fullText.Length)
        {
            var shownText = _fullText.Substring(0, _cursor);
            Debug.Log(shownText);
            textBox.text = shownText;
            _cursor++;
            yield return new WaitForSeconds(secondsPerCharacter);
        }
        ShowCompletely();
    }
}
