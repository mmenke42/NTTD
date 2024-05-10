using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuStartGame : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ToControls()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void ToGame()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }

    public void ToTitle()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }


    public void RestartGame()
    {
        GameObject f = GameObject.Find("GameManager");
        Destroy(f);
        GameObject a = GameObject.Find("Evac_Exit");
        Destroy(a);
        GameObject b = GameObject.Find("Player");
        Destroy(b);
        GameObject v = GameObject.Find("Main Camera");
        Destroy(v);
        GameObject w = GameObject.Find("Follow Camera");
        Destroy(w); 
        GameObject e = GameObject.Find("SceneTransition");
        Destroy(e);


        Scene curerentScene = SceneManager.GetActiveScene();
        SceneManager.UnloadSceneAsync(curerentScene);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
