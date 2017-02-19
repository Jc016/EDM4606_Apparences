using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSLimiter : MonoBehaviour {

    // Use this for initialization
    private void Awake()
    {
        Application.targetFrameRate = 24;
    }
}
