using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabberController : MonoBehaviour {

    private GameObject currentSelectedObject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

       
		
	}

    public void updateGrabberWithPosition(Vector3 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo))
        {
            GameObject hitGameObject = hitInfo.transform.gameObject;
            if(hitGameObject == null  ||  hitGameObject != currentSelectedObject)
            {
                setActiveGrabbedObject(hitGameObject);
            }

        }


    }

    private void setActiveGrabbedObject(GameObject gameObject)
    {
        if(currentSelectedObject != null)
        {
            currentSelectedObject.GetComponent<Renderer>().material.color = Color.white;
        }


        currentSelectedObject = gameObject;

        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }


}
