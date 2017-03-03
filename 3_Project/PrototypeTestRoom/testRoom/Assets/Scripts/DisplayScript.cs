using UnityEngine;
using System.Collections;

public class DisplayScript : MonoBehaviour
{
    void Awake()
    {
        Screen.SetResolution(3840, 1080, false);
        Display.main.SetRenderingResolution(7680, 2160);
    }
    // Use this for initialization
    void Start()
    {
 


    }
    // Update is called once per frame
    void Update()
    {

    }
}