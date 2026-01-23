using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUtils : MonoBehaviour
{
    public void QuitApplication()
    {
        Application.Quit(0);
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
