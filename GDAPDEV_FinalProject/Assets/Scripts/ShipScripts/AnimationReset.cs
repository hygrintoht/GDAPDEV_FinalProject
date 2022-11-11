using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationReset : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void ResetTrigger(string trigger)
    {
        animator.ResetTrigger(trigger);
        Debug.Log(trigger);
    }
}
