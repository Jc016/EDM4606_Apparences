using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabberController : MonoBehaviour {

    public GameObject currentSelectedObject;
    private bool isGrabbing;
    public float minForceThrow = 1.0f;
    public float maxForceThrow = 4.0f;
    public float currentThrowForce = 1.0f;

    public float forceIncrement = 1.1f;
    private Vector3 prevGrabbedObjectPos;
    private Vector3 cursorLastPosition = new Vector3();
    private Color objectInitialColor = Color.black;

	// Use this for initialization
	void Start () {
        isGrabbing = false;
        prevGrabbedObjectPos = new Vector3(0, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
        incrementForceOnThrow();
    }

    void incrementForceOnThrow()
    {
        if (isGrabbing)
        {
            currentThrowForce +=  forceIncrement *Time.deltaTime;
            Mathf.Clamp(currentThrowForce, minForceThrow, maxForceThrow);
        }
    }

    public void setGrabState(bool isGrabbingState)
    {
       

        if(currentSelectedObject != null)
        {
            if (isGrabbingState != isGrabbing)
            {
                Debug.Log("isGrabbing: " + isGrabbingState);
                isGrabbing = isGrabbingState;
                Rigidbody rigidbody = currentSelectedObject.GetComponent<Rigidbody>();

                if (rigidbody != null)
                {
                    rigidbody.useGravity = !isGrabbingState;
                }

         
            }



        }

     
        currentThrowForce = minForceThrow;
       

    }

    private void updateLastPos(Vector3 position)
    {
        cursorLastPosition = position;
    }



    private Vector3 calculateForceMoveObjectFactor(Vector3 position)
    {
        Vector3 objectMoveForce = position - cursorLastPosition;
        objectMoveForce.Normalize();
        objectMoveForce *= currentThrowForce;
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
        deselectObject();
        if (gameObject.tag == "Interactive")
        {
            
            currentSelectedObject = gameObject;
            objectInitialColor = currentSelectedObject.GetComponent<Renderer>().material.color;
            currentSelectedObject.GetComponent<Renderer>().material.color = Color.red;

        }

    }

    private void deselectObject()
    {
        if(currentSelectedObject != null)
        {
            currentSelectedObject.GetComponent<Renderer>().material.color = objectInitialColor;
            currentSelectedObject = null;
        }
      
    }


}
