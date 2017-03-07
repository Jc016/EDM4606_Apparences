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
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
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
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }



    private void LoadRoom()
    {
        LoadScene("room");
    }

    

}
