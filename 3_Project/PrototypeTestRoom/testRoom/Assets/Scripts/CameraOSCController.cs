using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOSCController : MonoBehaviour {

    public OSC osc;
    private Quaternion originRotation;
    private float rx, ry, rz;

    // Use this for initialization
    void Start()
    {
        rx = 0;
        ry = 0;
        rz = 0;
        originRotation = transform.rotation;
        osc.SetAddressHandler("/cameraRx", OnReceiveRx);
        osc.SetAddressHandler("/cameraRy", OnReceiveRy);
        osc.SetAddressHandler("/cameraRz", OnReceiveRz);
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(rx, ry, rz);
    }



    void OnReceiveRx(OscMessage message)
    {
        rx = message.GetFloat(0);
       
    }

    void OnReceiveRy(OscMessage message)
    {
        ry = message.GetFloat(0);
  
    }

    void OnReceiveRz(OscMessage message)
    {
        rz = message.GetFloat(0);
    }

}

