using UnityEngine;
using System.Collections;

/*
 * Holder for event names
 * Created By: NeilDG
 */ 


public class EventNames {
	//Generic Event will be placed here
	public const string ON_UPDATE_SCORE = "ON_UPDATE_SCORE";
	public const string ON_INCREASE_LEVEL = "ON_INCREASE_LEVEL";

	public const string ON_PICTURE_CLICKED = "ON_PICTURE_CLICKED";

	//Classes will be used to differientiate from one type to another
	public class GLevel1 {
		//Ex. Removed Later
		public const string ON_START_BLUETOOTH_DEMO = "ON_START_BLUETOOTH_DEMO";
		public const string ON_RECEIVED_MESSAGE = "ON_RECEIVED_MESSAGE";
	}

	//For Specific set type of interaction
	public class UIEvents
	{
		public const string ON_PRESS_SWITCH = "ON_PRESS_SWITCH";
		public const string ON_PRESS_QUIT = "ON_PRESS_QUIT";
	}

	public class AnimationTrigger
	{
		public const string ROLL_CW = "Right";
		public const string ROLL_CCW = "Left";
        public const string SHIELD_TOGGLE = "Shield";
    }


}







