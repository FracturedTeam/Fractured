using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicSceneLoader : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
