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
        osc.SetAddressHandler("/hand_closed", OnReceiveHandStatus);
        osc.SetAddressHandler("/stand_sit", OnBodyStanceChange);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnBodyStanceChange(OscMessage  message)
    {
        float bodyYSpeed = message.GetFloat(0);
        if(bodyYSpeed > 0.3f)
        {
            Physics.gravity = new Vector3(0, 0.3f, 0);
            Debug.Log("Standing Up: " + bodyYSpeed);
        }
        else if (bodyYSpeed < -0.3f)
        {
            Physics.gravity = new Vector3(0, -160.0f, 0);
            Debug.Log("Sitting: " + bodyYSpeed);
        }
    }

    void OnReceiveHandStatus(OscMessage message)
    {
        ObjectGrabberController ogc;
        if (IsObjectGrabberSet(out ogc))
            ogc.setGrabState(message.GetFloat(0) == 1);
    }

    bool IsObjectGrabberSet(out ObjectGrabberController refOGC)
    {
        refOGC = GetComponent<ObjectGrabberController>();
        return refOGC  != null;
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
        ObjectGrabberController ogc;
        cursorTransform.position = new Vector3(
           canvasTransform.rect.width * pointerX,
           canvasTransform.rect.height * pointerY
            );

        if (IsObjectGrabberSet(out ogc))
            ogc.updateGrabberWithPosition(cursorTransform.position);
        
        
    }
}


