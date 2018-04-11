using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OutOfBounds.SteamVR;

public class VRInput : MonoBehaviour
{
    
	// Update is called once per frame
	void Update ()
    {
	    if(TrackedController.left != null && TrackedController.left.GetTouchPressDown())
        {
            Vector2 touchPos = TrackedController.left.GetTouchPosition();

            if (touchPos.x > 0)
                GoForward();
            else
                GoBack();
        }

        if (TrackedController.right != null && TrackedController.right.GetTouchPressDown())
        {
            Vector2 touchPos = TrackedController.right.GetTouchPosition();

            if (touchPos.x > 0)
                GoForward();
            else
                GoBack();
        }
    }

    void GoBack()
    {
        print("go back");
    }

    void GoForward()
    {
        print("go forward");
    }
}
