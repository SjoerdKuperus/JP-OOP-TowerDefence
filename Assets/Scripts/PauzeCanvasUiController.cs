using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class PauzeCanvasUiController : MonoBehaviour
{
    public void Continue()
    {
        MainManager.Instance.ContinueGame();
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif        
    }
}
