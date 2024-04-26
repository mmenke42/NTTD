using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;
    [SerializeField] Animator transitionScene;
    [SerializeField] private GameObject ImageObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void OnSceneChanged(Scene arg0, Scene arg1)
    {
        //ToggleImage(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextScene()
    {
        StartCoroutine(LoadScene());
    }
    IEnumerator LoadScene()
    {
        ToggleImage(true);
        transitionScene.SetTrigger("SceneEnd"); 
        yield return new WaitForSeconds(1);
        
        var asyncLoadLevel = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        asyncLoadLevel.allowSceneActivation = true;

        while (!asyncLoadLevel.isDone)
        {
            yield return null;
        }
        asyncLoadLevel.allowSceneActivation = true;

        yield return new WaitForSeconds(1.0f);
        transitionScene.SetTrigger("SceneBegin");
        yield return new WaitForSeconds(1.5f);

        ToggleImage(false);
    }

    private void ToggleImage(bool toggle)
    {
        ImageObject.SetActive(toggle);
    }
}
