using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Timers;

public class SceneManagement : MonoBehaviour {
    private Timer projectTimer;


    // Use this for initialization
    void Start () {
        Scene currentScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(currentScene);
    }
	
	// Update is called once per frame
	void Update () {
      if (Input.GetKeyDown("space"))
      {
            StartCoroutine(loadTitle());
      }
      else if(Input.GetKeyDown("s"))
      {
         
      }
    }

    public IEnumerator LoadScene(string sceneName)
    {
        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        Scene currentScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(currentScene);
    }

  

 

    private IEnumerator loadTitle()
    {
        StartCoroutine(LoadScene("titleScreen"));
        yield return new WaitForSeconds(7.0f);
       StartCoroutine(LoadScene("room"));
    }

    

}
