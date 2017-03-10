using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAvatarController : MonoBehaviour {

    Random randomGenerator;
    public float maxIntensity = 6;
    private Light light;

	// Use this for initialization
	void Start () {
        randomGenerator = new Random();
        light = GetComponent<Light>();
    }
	
	// Update is called once per frame
	void Update() {
        
	}

    public void UpdateLightAmplitude(float amplitude)
    {
        light.range = Mathf.Abs(amplitude) * maxIntensity;
    }
}
