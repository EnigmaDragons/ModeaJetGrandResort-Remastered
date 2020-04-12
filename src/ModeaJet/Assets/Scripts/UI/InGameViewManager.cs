using UnityEngine;

public sealed class InGameViewManager : OnMessage<ShowInGameView, DismissCurrentView>
{
    [SerializeField] private GameObject hudView;
    [SerializeField] private GameObject dialogueView;
    [SerializeField] private GameObject dilemmaView;

    private GameObject _currentView;
    private InGameViewId _currentId;

    private void Awake() => ShowHudView();

    protected override void Execute(ShowInGameView msg)
    {
        if (msg.View == InGameViewId.Dilemma)
            ShowView(InGameViewId.Dilemma, dilemmaView);
        else if (msg.View == InGameViewId.Dialogue)
            ShowView(InGameViewId.Dialogue, dialogueView);
        else if (msg.View == InGameViewId.Hud)
            ShowHudView();
        else
            Debug.LogError($"No View for {msg.View} setup in {nameof(InGameViewManager)}");
    }

    protected override void Execute(DismissCurrentView msg) => Dismiss();
    public void Dismiss() => ShowHudView();

    private void ShowHudView() => ShowView(InGameViewId.Hud, hudView);

    private void ShowView(InGameViewId id, GameObject view)
    {
        if (_currentId == id)
            return;
        
        if (_currentView != null)
            _currentView.SetActive(false);
        _currentId = id;
        _currentView = view;
        _currentView.SetActive(true);
    }
}
