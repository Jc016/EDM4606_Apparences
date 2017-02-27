using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabberController : MonoBehaviour {

    private GameObject currentSelectedObject;
    private bool isGrabbing;
    public float forceThrow = 1.0f;
    private Vector3 prevGrabbedObjectPos;
    private Vector3 cursorLastPosition = new Vector3();

	// Use this for initialization
	void Start () {
        isGrabbing = false;
	}
	
	// Update is called once per frame
	void Update () {

        prevGrabbedObjectPos = new Vector3(0, 0, 0);
    }

    public void setGrabState(bool isGrabbingState)
    {
        if(currentSelectedObject != null && isGrabbingState != isGrabbing)
        {
            Debug.Log("isGrabbing: " + isGrabbingState);
            isGrabbing = isGrabbingState;
            Rigidbody rigidbody = currentSelectedObject.GetComponent<Rigidbody>();

            if(rigidbody != null)
            {

                rigidbody.useGravity = !isGrabbingState;

                 
            }


        
        }
    }

    private void updateLastPos(Vector3 position)
    {
        cursorLastPosition = position;
    }


    private Vector3 calculateForceMoveObjectFactor(Vector3 position)
    {
        Vector3 objectMoveForce = position - cursorLastPosition;
        objectMoveForce.Normalize();
        objectMoveForce *= forceThrow;
        return objectMoveForce;
    }

    public void updateGrabberWithPosition(Vector3 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hitInfo;

        if (isGrabbing)
        {
            Vector3 moveForce = calculateForceMoveObjectFactor(position);
            currentSelectedObject.GetComponent<Rigidbody>().AddForce(moveForce);
            Debug.Log("Moving  Object: " + moveForce.ToString());

            updateLastPos(position);
                
        }
        else if (Physics.Raycast(ray, out hitInfo))
        {
            GameObject hitGameObject = hitInfo.transform.gameObject;

            if (hitGameObject != null && hitGameObject.tag == "Interactive")
            {
                iTween.ShakePosition(hitGameObject, new Vector3(0.04f, 0, 0), 0.1f);
            }
            if (hitGameObject == null || hitGameObject != currentSelectedObject)
            {
                setActiveGrabbedObject(hitGameObject);

            }
        }    

    }

    private void setActiveGrabbedObject(GameObject gameObject)
    {
  
        if(gameObject.tag == "Interactive")
        {
            deselectObject();
            currentSelectedObject = gameObject;
        }

    }

    private void deselectObject()
    {
        if(currentSelectedObject != null)
        {
            currentSelectedObject = null;
        }
      
    }


}
