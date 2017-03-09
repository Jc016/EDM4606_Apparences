using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Timers;

public class SceneManagement : MonoBehaviour {
    private Timer projectTimer;
    private bool isPlayerPresent;


    // Use this for initialization
    void Start () {
        Scene currentScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(currentScene);

    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public IEnumerator LoadScene(string sceneName)
    {
        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        Scene currentScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(currentScene);
    }


    public void  updatePlayerPresence(bool playerPresenceNewState)
    {
        if(playerPresenceNewState != isPlayerPresent)
        {
            isPlayerPresent = playerPresenceNewState;
            if (isPlayerPresent)
            {
                StartProject();
            }
            else
            {
                StopProject();
            }
        }

    }

    public void StartProject()
    {
        StopAllCoroutines();
        StartCoroutine(loadTitle());
        Debug.Log("Player on");
    }

    public void StopProject()
    {
        StopAllCoroutines();
        StartCoroutine(LoadScene("Black"));
        Debug.Log("Player off");
    }


    private IEnumerator loadTitle()
    {
        StartCoroutine(LoadScene("titleScreen"));
        yield return new WaitForSeconds(7.1f);
        StartCoroutine(LoadScene("room"));
    }

    

}
