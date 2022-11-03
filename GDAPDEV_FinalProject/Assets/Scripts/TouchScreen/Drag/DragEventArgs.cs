using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DragEventArgs : EventArgs
{
    private Touch targetFinger;//Information about the dragging finger
    private GameObject hitObject;//The object where the finger is at the moment the drag event is fired

    public DragEventArgs(Touch _targetFinger, GameObject _hitObject)
    {
        targetFinger = _targetFinger;
        hitObject = _hitObject;
    }

    public Touch TargetFinger { get { return targetFinger; } }
    public GameObject HitObject { get { return hitObject; } }
}
