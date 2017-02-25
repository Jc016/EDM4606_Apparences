using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabberController : MonoBehaviour {

    private GameObject currentSelectedObject;
    private bool isGrabbing;
    public float forceThrow = 1.0f;
    private Vector3 prevGrabbedObjectPos;

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
                rigidbody.isKinematic = isGrabbingState;
            }

            if (isGrabbing)
            {
                updateLastPos();
            }
            else
            {
                Vector3 releaseForce = currentSelectedObject.transform.position - prevGrabbedObjectPos;
                releaseForce.Normalize();
                releaseForce= releaseForce * forceThrow;
                currentSelectedObject.GetComponent<Rigidbody>().AddForce(releaseForce);
                currentSelectedObject.GetComponent<Renderer>().material.color = Color.black;
                deselectObject();
                
            }
        }
    }

    private void updateLastPos()
    {
        prevGrabbedObjectPos = currentSelectedObject.transform.position;
    }

    public void updateGrabberWithPosition(Vector3 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hitInfo;

        if (isGrabbing)
        {
            Debug.Log("Moving  Object");
            updateLastPos();
            Vector3 movingPosition = ray.GetPoint(Mathf.Abs(currentSelectedObject.transform.position.z));

            currentSelectedObject.transform.position = new Vector3(
                movingPosition.x,
                movingPosition.y,
                currentSelectedObject.transform.position.z
                );
                
        }
        else if (Physics.Raycast(ray, out hitInfo))
        {
            GameObject hitGameObject = hitInfo.transform.gameObject;

            if (hitGameObject != null && hitGameObject.tag == "Interactive")
            {
                iTween.ShakePosition(hitGameObject, new Vector3(0.07f, 0, 0), 0.1f);
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
