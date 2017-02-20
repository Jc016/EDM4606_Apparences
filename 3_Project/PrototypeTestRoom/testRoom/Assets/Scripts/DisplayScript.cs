using UnityEngine;
using System.Collections;

public class DisplayScript : MonoBehaviour
{
    void Awake()
    {
        Screen.SetResolution(3840, 1080, false);
        Display.main.SetRenderingResolution(3840, 1080);
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