using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Timers;

public class SceneManagement : MonoBehaviour {
    Scene[] Scenes;
    public enum SceneNames {Intro,Main,Void};
    private Timer projectTimer;


    // Use this for initialization
    void Start () {
        SceneManager.LoadScene("titleScreen", LoadSceneMode.Additive);
        Invoke("LoadRoom", 7);
    }
	
	// Update is called once per frame
	void Update () {
      if (Input.GetKeyDown("space"))
      {
            RestartProject();
      }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(1);
        SceneManager.LoadScene(2,LoadSceneMode.Additive);
    }

    private void RestartProject()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

        }

        SceneManager.LoadScene(0, LoadSceneMode.Additive);
    }



    private void LoadRoom()
    {
        LoadScene("room");
    }

    

}
