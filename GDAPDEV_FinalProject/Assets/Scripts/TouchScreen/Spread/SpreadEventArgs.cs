using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum SpreadType
{
    Spread,
    Pinch
}
public class SpreadEventArgs : EventArgs
{
    
    public Touch Finger1 { get; private set; }
    public Touch Finger2 { get; private set; }

    public float DistanceDelta { get; private set; } = 0;
    
    public SpreadType SpreadableType { get; private set; } = SpreadType.Spread;

    public GameObject HitObject { get; private set; } = null;

    public SpreadEventArgs (Touch finger1, Touch finger2, float distanceDelta, SpreadType spreadType, GameObject hitObject)
    {
        Finger1 = finger1;
        Finger2 = finger2;
        DistanceDelta = distanceDelta;
        SpreadableType = spreadType;
        HitObject = hitObject;
    }
}
