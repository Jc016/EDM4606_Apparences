using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class OscCursorController : MonoBehaviour
{


    public OSC oscTouch, oscMax;
    public float pointerXLeft, pointerYLeft, speedXLeft, speedYLeft;
    public float pointerXRight, pointerYRight, speedXRight, speedYRight;
    public RectTransform canvasTransformLeft, canvasTransformRight;
    public RectTransform cursorTransformLeft;
    public RectTransform cursorTransformRight;
    private bool leftGrab, rightGrab;
    public GUITexture darkOverlay;
    public LightAvatarController lightAvatarController;

    // Use this for initialization
    void Start()
    {
        pointerXLeft = 0;
        pointerYLeft = 0;

        pointerXRight = 0;
        pointerYRight = 0;

        leftGrab = false;
        rightGrab = false;

        oscTouch.SetAddressHandler("/pointerX:left", OnReceivepointerXLeft);
        oscTouch.SetAddressHandler("/pointerY:left", OnReceivepointerYLeft);
        oscTouch.SetAddressHandler("/speedX:left", OnReceivespeedXLeft);
        oscTouch.SetAddressHandler("/speedY:left", OnReceivespeedYLeft);
        oscTouch.SetAddressHandler("/pointerY:left", OnReceivepointerYLeft);
        oscTouch.SetAddressHandler("/hand_closed:left", OnReceiveHandStatusLeft);

        oscTouch.SetAddressHandler("/pointerX:right", OnReceivepointerXRight);
        oscTouch.SetAddressHandler("/pointerY:right", OnReceivepointerYRight);
        oscTouch.SetAddressHandler("/speedX:right", OnReceivespeedXRight);
        oscTouch.SetAddressHandler("/speedY:right", OnReceivespeedYRight);
        oscTouch.SetAddressHandler("/pointerY:right", OnReceivepointerYRight);
        oscTouch.SetAddressHandler("/hand_closed:right", OnReceiveHandStatusRight);

        oscTouch.SetAddressHandler("/stand_sit", OnBodyStanceChange);

        oscMax.SetAddressHandler("/LUMIERE", OnReceiveLumiere);

        oscMax.SetAddressHandler("/PHRASE", OnReceivePhrase);

    }

    // Update is called once per frame
    void Update()
    {
        SetPointerPositionLeft();
        SetPointerPositionRight();

        sendGrabBangtoMax(leftGrab || rightGrab ? 1 : 0);

        GUI.color = new Color(0, 0, 0, 1f);
    }

    void OnReceiveLumiere(OscMessage message)
    {
        darkOverlay.enabled = message.GetFloat(0) == 0;
    }

    void OnReceivePhrase(OscMessage message)
    {
        float amplitude = message.GetFloat(0);

        lightAvatarController.UpdateLightAmplitude(amplitude);
    }
    void OnBodyStanceChange(OscMessage message)
    {
        float bodyYSpeed = message.GetFloat(0);
        if (Camera.main.GetComponent<MotionBlur>() != null)
        {
            if (bodyYSpeed == 0)
            {
                Physics.gravity = new Vector3(0, 0.4f, 0);
                Camera.main.GetComponent<MotionBlur>().blurAmount = 1.0f;

            }
            else
            {
                Physics.gravity = new Vector3(0, -40f, 0);
                Camera.main.GetComponent<MotionBlur>().blurAmount = 0f;

            }
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

    public void sendGrabBangtoMax(int value)
    {
        OscMessage message = new OscMessage();
        message.address = "/Objects";
        message.values.Add(value);
        oscMax.Send(message);
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
        {
            leftGrab = ogc.isGrabbing;
            ogc.updateGrabberWithPosition(cursorTransformLeft.position, new Vector3(speedXLeft, speedYLeft));
        }

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
    }

    void OnReceivepointerYRight(OscMessage message)
    {
        pointerYRight = message.GetFloat(0);

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
        {
            rightGrab = ogc.isGrabbing;
            ogc.updateGrabberWithPosition(cursorTransformRight.position, new Vector3(speedXRight, speedYRight));
        }
            




    }
}


