using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseObjectController : MonoBehaviour {

    private Renderer rend;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            GameObject gameObject = hit.transform.gameObject;
            if (gameObject.tag == "Interactive")
            {
                float emission = Mathf.PingPong(Time.time, 1.0f);
                Color baseColor = Color.yellow; //Replace this with whatever you want for your base color at emission level '1'

                Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);

                mat.SetColor("_EmissionColor", finalColor);
            }
           


        }
    }

}
