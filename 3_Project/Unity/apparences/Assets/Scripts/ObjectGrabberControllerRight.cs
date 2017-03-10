using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabberControllerRight : MonoBehaviour
{

    public GameObject currentSelectedObject;
    public bool isGrabbing;
    public float minForceThrow = 1.0f;
    public float maxForceThrow = 4.0f;
    public float currentThrowForce = 1.0f;

    public float forceIncrement = 1.1f;
    private Vector3 prevGrabbedObjectPos;
    private Vector3 cursorLastPosition = new Vector3();
    private Color objectInitialColor = Color.black;
    private bool colorChanged = false;
    public OscCursorController oscCursorController;

    // Use this for initialization
    void Start()
    {
        isGrabbing = false;
        prevGrabbedObjectPos = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void incrementForceOnThrow()
    {
        if (isGrabbing)
        {
            currentThrowForce += forceIncrement * Time.deltaTime;
            Mathf.Clamp(currentThrowForce, minForceThrow, maxForceThrow);
        }
    }

    public void setGrabState(bool isGrabbingState)
    {


        if (currentSelectedObject != null)
        {
            if (isGrabbingState != isGrabbing)
            {

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

    private Vector3 calculateForceMoveObjectFactor(Vector3 speedFactors)
    {
        Vector3 objectMoveForce = speedFactors;
        speedFactors.Normalize();
        objectMoveForce *= currentThrowForce;
        Debug.Log(objectMoveForce);
        return objectMoveForce;
    }

    public void updateGrabberWithPosition(Vector3 position, Vector3 directionnalSpeeds)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hitInfo;

        if (isGrabbing)
        {
            Vector3 moveForce = calculateForceMoveObjectFactor(directionnalSpeeds);
            incrementForceOnThrow();
            currentSelectedObject.GetComponent<Rigidbody>().AddForce(moveForce);
           
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
            if(currentSelectedObject.GetComponent<Renderer>().material.color != Color.red)
            {
                objectInitialColor = currentSelectedObject.GetComponent<Renderer>().material.color;
                currentSelectedObject.GetComponent<Renderer>().material.color = Color.red;
                colorChanged = true;
            }


        }

    }

    private void deselectObject()
    {
        if (currentSelectedObject != null)
        {
            if (colorChanged)
            {
                currentSelectedObject.GetComponent<Renderer>().material.color = objectInitialColor;
                colorChanged = false;
            }
            
            currentSelectedObject = null;
        }

    }


}
