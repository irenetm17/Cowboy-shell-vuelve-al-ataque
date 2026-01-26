using UnityEngine;
using UnityEngine.SceneManagement;

public class Save : MonoBehaviour
{
    public static int _Progress {  get; private set; }

    public static void Complete(int progress, string scene) { _Progress = progress; SceneManager.LoadScene(scene); }
}
