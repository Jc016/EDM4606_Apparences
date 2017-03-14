using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAvatarController : MonoBehaviour {

    Random randomGenerator;
    public float maxIntensity = 6;
    public float maxFlare = 0.8f;
    private Light light;
    private LensFlare lensFlare;
    public GameObject parentGameObject;
    public float cameraToggleThreshold = 0.8f;
    public float cameraThreshold = -14f;
    public Camera mainCamera;
    public float soundAmplitude;
    public float positionY;

    public bool boolAmp, boolPos;


	// Use this for initialization
	void Start () {
        randomGenerator = new Random();
        light = GetComponent<Light>();
        lensFlare = GetComponent<LensFlare>();
    }
	
	// Update is called once per frame
	void Update() {
        positionY = parentGameObject.transform.position.y;


        boolAmp = (Mathf.Abs(soundAmplitude) < cameraToggleThreshold && parentGameObject.transform.position.y > cameraThreshold);
        boolPos = parentGameObject.transform.position.y > cameraThreshold;
        mainCamera.enabled = (Mathf.Abs(soundAmplitude) < cameraToggleThreshold && parentGameObject.transform.position.y > cameraThreshold);

    }

    public void UpdateLightAmplitude(float amplitude)
    {
        light.range = Mathf.Abs(amplitude) * maxIntensity;
        lensFlare.brightness = Mathf.Abs(amplitude) * maxFlare;
        soundAmplitude = amplitude;

      
    }
}
