using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireButton : MonoBehaviour
{
    public void ButtonInteract(bool isPressed)
    {
        Parameters state = new Parameters();
        state.PutExtra(EventNames.ON_FIRE, isPressed);
        EventBroadcaster.Instance.PostEvent(EventNames.ON_FIRE, state);
    }
}
