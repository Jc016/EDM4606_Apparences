using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class OscCursorController : MonoBehaviour
{


    public OSC osc;
    public float pointerXLeft, pointerYLeft, speedXLeft, speedYLeft;
    public float pointerXRight, pointerYRight, speedXRight, speedYRight;
    public RectTransform canvasTransformLeft, canvasTransformRight;
    public RectTransform cursorTransformLeft;
    public RectTransform cursorTransformRight;

    // Use this for initialization
    void Start()
    {
        pointerXLeft = 0;
        pointerYLeft = 0;

        pointerXRight = 0;
        pointerYRight = 0;

        osc.SetAddressHandler("/pointerX:left", OnReceivepointerXLeft);
        osc.SetAddressHandler("/pointerY:left", OnReceivepointerYLeft);
        osc.SetAddressHandler("/speedX:left", OnReceivespeedXLeft);
        osc.SetAddressHandler("/speedY:left", OnReceivespeedYLeft);
        osc.SetAddressHandler("/pointerY:left", OnReceivepointerYLeft);
        osc.SetAddressHandler("/hand_closed:left", OnReceiveHandStatusLeft);

        osc.SetAddressHandler("/pointerX:right", OnReceivepointerXRight);
        osc.SetAddressHandler("/pointerY:right", OnReceivepointerYRight);
        osc.SetAddressHandler("/speedX:right", OnReceivespeedXRight);
        osc.SetAddressHandler("/speedY:right", OnReceivespeedYRight);
        osc.SetAddressHandler("/pointerY:right", OnReceivepointerYRight);
        osc.SetAddressHandler("/hand_closed:right", OnReceiveHandStatusRight);

        osc.SetAddressHandler("/stand_sit", OnBodyStanceChange);

    }

    // Update is called once per frame
    void Update()
    {
        SetPointerPositionLeft();
        SetPointerPositionRight();
    }

    void OnBodyStanceChange(OscMessage message)
    {
        float bodyYSpeed = message.GetFloat(0);
        if (bodyYSpeed == 0)
        {
            Physics.gravity = new Vector3(0, 0.4f, 0);
            Camera.main.GetComponent<MotionBlur>().blurAmount = 1.0f;

        }
        else
        {
            Physics.gravity = new Vector3(0, -200.0f, 0);
            Camera.main.GetComponent<MotionBlur>().blurAmount = 0f;

        }
    }

    void OnReceiveHandStatusLeft(OscMessage message)
    {
        ObjectGrabberControllerLeft ogc;
        float handStatus = message.GetFloat(0);
        if (IsObjectGrabberLeftSet(out ogc))
        {
            ogc.setGrabState(handStatus == 1);
            cursorTransformLeft.gameObject.GetComponent<Image>().enabled = handStatus == 0 || ogc.currentSelectedObject == null;
        }
    }

    bool IsObjectGrabberLeftSet(out ObjectGrabberControllerLeft refOGC)
    {
        refOGC = GetComponent<ObjectGrabberControllerLeft>();
        return refOGC != null;
    }

    void OnReceivepointerXLeft(OscMessage message)
    {
        pointerXLeft = message.GetFloat(0);
    }

    void OnReceivepointerYLeft(OscMessage message)
    {
        pointerYLeft = message.GetFloat(0);

    }

    void OnReceivespeedXLeft(OscMessage message)
    {
        speedXLeft = message.GetFloat(0);
    }

    void OnReceivespeedYLeft(OscMessage message)
    {
        speedYLeft = message.GetFloat(0);
    }

    void SetPointerPositionLeft()
    {
        ObjectGrabberControllerLeft ogc;
        cursorTransformLeft.position = new Vector3(
           canvasTransformLeft.rect.width * pointerXLeft,
           canvasTransformLeft.rect.height * pointerYLeft
            );



        if (IsObjectGrabberLeftSet(out ogc))
            ogc.updateGrabberWithPosition(cursorTransformLeft.position, new Vector3(speedXLeft,speedYLeft));


    }

    void OnReceiveHandStatusRight(OscMessage message)
    {
        ObjectGrabberControllerRight ogc;
        float handStatus = message.GetFloat(0);
        if (IsObjectGrabberRightSet(out ogc))
        {
            ogc.setGrabState(handStatus == 1);
            cursorTransformRight.gameObject.GetComponent<Image>().enabled = handStatus == 0 || ogc.currentSelectedObject == null;
        }
    }

    bool IsObjectGrabberRightSet(out ObjectGrabberControllerRight refOGC)
    {
        refOGC = GetComponent<ObjectGrabberControllerRight>();
        return refOGC != null;
    }

    void OnReceivepointerXRight(OscMessage message)
    {
        pointerXRight = message.GetFloat(0);
        Debug.Log(pointerXRight);
    }

    void OnReceivepointerYRight(OscMessage message)
    {
        pointerYRight = message.GetFloat(0);
        Debug.Log(pointerYRight);

    }

    void OnReceivespeedXRight(OscMessage message)
    {
        speedXRight = message.GetFloat(0);
    }

    void OnReceivespeedYRight(OscMessage message)
    {
        speedYRight = message.GetFloat(0);
    }

    void SetPointerPositionRight()
    {
        ObjectGrabberControllerRight ogc;
        cursorTransformRight.position = new Vector3(
           canvasTransformRight.rect.width * pointerXRight,
           canvasTransformRight.rect.height * pointerYRight
            );



        if (IsObjectGrabberRightSet(out ogc))
            ogc.updateGrabberWithPosition(cursorTransformRight.position, new Vector3(speedXRight, speedYRight));


    }
}


