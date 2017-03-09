using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscSceneManagement : MonoBehaviour {
    public OSC oscTouch, oscMax;
    public SceneManagement sceneManagement;

    // Use this for initialization
    void Start()
    {
        oscTouch.SetAddressHandler("/playerPresence", OnReceivePlayerPresence);
        oscTouch.SetAddressHandler("/END", OnReceiveEnd);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnReceiveEnd (OscMessage message)
    {
        float endStatus = message.GetFloat(0);
        if(endStatus == 1)
        {
            sceneManagement.StopProject();
        }
    }

    void OnReceivePlayerPresence(OscMessage message)
    {
        float playerPresence =  message.GetFloat(0);
        sceneManagement.updatePlayerPresence(playerPresence == 1);
    }

}



