using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "GameTemplate/OnlyOnce/Navigator")]
public sealed class Navigator : ScriptableObject
{
    public void NavigateToMainMenu() => NavigateTo("MainMenu");
    public void NavigateToGame() => NavigateTo("GameScene");

    public void NavigateTo(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
