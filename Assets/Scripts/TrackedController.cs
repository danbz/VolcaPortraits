using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace OutOfBounds.SteamVR
{
	public class TrackedController : MonoBehaviour
	{
		private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
		private SteamVR_TrackedObject trackedObj;

		public static TrackedController left { get; private set; }
		public static TrackedController right { get; private set; }
		
		// Use this for initialization
		void Start()
		{
			trackedObj = GetComponent<SteamVR_TrackedObject>();

			print(name);

			if (name.Contains("left"))
			{
				left = this;
			}
			if (name.Contains("right"))
			{
				right = this;
			}
		}

		#region touch pad
		public bool GetTouchDown()
		{
			return controller.GetTouchDown(EVRButtonId.k_EButton_SteamVR_Touchpad);
        }

		public bool GetTouch()
		{
			return controller.GetTouch(EVRButtonId.k_EButton_SteamVR_Touchpad);
		}

		public bool GetTouchUp()
		{
			return controller.GetTouchUp(EVRButtonId.k_EButton_SteamVR_Touchpad);
		}

		public Vector2 GetTouchPosition()
		{
			return controller.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
		}

		public bool GetTouchPressDown()
		{
			return controller.GetPressDown(EVRButtonId.k_EButton_SteamVR_Touchpad);
		}

		public bool GetTouchPress()
		{
			return controller.GetPress(EVRButtonId.k_EButton_SteamVR_Touchpad);
		}

		public bool GetTouchPressUp()
		{
			return controller.GetPressUp(EVRButtonId.k_EButton_SteamVR_Touchpad);
		}

		#endregion
		#region trigger
		public float GetTrigger()
		{
			return controller.GetAxis(EVRButtonId.k_EButton_SteamVR_Trigger).y;
		}
		
		public bool GetTriggerDown()
		{
			return controller.GetTouchDown(EVRButtonId.k_EButton_SteamVR_Trigger);
		}
		
		public bool GetTriggerUp()
		{
			return controller.GetTouchUp(EVRButtonId.k_EButton_SteamVR_Trigger);
		}

		#endregion
		#region grip button
		public bool GetGrip()
		{
			return controller.GetPress(EVRButtonId.k_EButton_Grip);
		}

		public bool GetGripDown()
		{
			return controller.GetPressDown(EVRButtonId.k_EButton_Grip);
		}

		public bool GetGripUp()
		{
			return controller.GetPressUp(EVRButtonId.k_EButton_Grip);
		}
		#endregion
		#region application menu button
		public bool GetApplicationButton()
		{
			return controller.GetPress(EVRButtonId.k_EButton_ApplicationMenu);
		}

		public bool GetApplicationButtonDown()
		{
			return controller.GetPressDown(EVRButtonId.k_EButton_ApplicationMenu);
		}

		public bool GetApplicationButtonUp()
		{
			return controller.GetPressUp(EVRButtonId.k_EButton_ApplicationMenu);
		}
		
		#endregion
	}
}