using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OscCursorController : MonoBehaviour {


    public OSC osc;
    public float pointerX, pointerY;
    public RectTransform cursorTransform;
    public RectTransform canvasTransform;

    // Use this for initialization
    void Start () {
        pointerX = 0;
        pointerY = 0;

        osc.SetAddressHandler("/pointerX", OnReceivePointerX);
        osc.SetAddressHandler("/pointerY", OnReceivePointerY);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnReceivePointerX(OscMessage message) {
        pointerX = message.GetFloat(0);
        SetPointerPosition();
    }

    void OnReceivePointerY(OscMessage message)
    {
        pointerY = message.GetFloat(0);
        SetPointerPosition();
    }

    void SetPointerPosition()
    {
        cursorTransform.position = new Vector3(
            canvasTransform.rect.width * pointerX,
            canvasTransform.rect.height * pointerY
            );

        if(GetComponent<ObjectGrabberController>() != null)
        {
            GetComponent<ObjectGrabberController>().updateGrabberWithPosition(cursorTransform.position);
        }
        
    }
}


