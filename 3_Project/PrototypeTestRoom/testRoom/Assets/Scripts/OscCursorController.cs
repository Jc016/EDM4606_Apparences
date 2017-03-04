using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OscCursorController : MonoBehaviour
{


    public OSC osc;
    public float pointerX, pointerY, speedX, speedY;
    public RectTransform cursorTransform;
    public RectTransform canvasTransform;

    // Use this for initialization
    void Start()
    {
        pointerX = 0;
        pointerY = 0;

        osc.SetAddressHandler("/pointerX", OnReceivePointerX);
        osc.SetAddressHandler("/pointerY", OnReceivePointerY);
        osc.SetAddressHandler("/speedX", OnReceiveSpeedX);
        osc.SetAddressHandler("/speedY", OnReceiveSpeedY);
        osc.SetAddressHandler("/pointerY", OnReceivePointerY);
        osc.SetAddressHandler("/hand_closed", OnReceiveHandStatus);
        osc.SetAddressHandler("/stand_sit", OnBodyStanceChange);

    }

    // Update is called once per frame
    void Update()
    {
        SetPointerPosition();
    }

    void OnBodyStanceChange(OscMessage message)
    {
        float bodyYSpeed = message.GetFloat(0);
        if (bodyYSpeed == 0)
        {
            Physics.gravity = new Vector3(0, 0.5f, 0);

        }
        else
        {
            Physics.gravity = new Vector3(0, -160.0f, 0);

        }
    }

    void OnReceiveHandStatus(OscMessage message)
    {
        ObjectGrabberController ogc;
        float handStatus = message.GetFloat(0);
        if (IsObjectGrabberSet(out ogc))
        {
            ogc.setGrabState(handStatus == 1);
            cursorTransform.gameObject.GetComponent<Image>().enabled = handStatus == 0 || ogc.currentSelectedObject == null;
        }




    }

    bool IsObjectGrabberSet(out ObjectGrabberController refOGC)
    {
        refOGC = GetComponent<ObjectGrabberController>();
        return refOGC != null;
    }

    void OnReceivePointerX(OscMessage message)
    {
        pointerX = message.GetFloat(0);
    }

    void OnReceivePointerY(OscMessage message)
    {
        pointerY = message.GetFloat(0);

    }

    void OnReceiveSpeedX(OscMessage message)
    {
        speedX = message.GetFloat(0);
    }

    void OnReceiveSpeedY(OscMessage message)
    {
        speedY = message.GetFloat(0);
    }

    void SetPointerPosition()
    {
        ObjectGrabberController ogc;
        cursorTransform.position = new Vector3(
           canvasTransform.rect.width * pointerX,
           canvasTransform.rect.height * pointerY
            );



        if (IsObjectGrabberSet(out ogc))
            ogc.updateGrabberWithPosition(cursorTransform.position, new Vector3(speedX,speedY));


    }
}


