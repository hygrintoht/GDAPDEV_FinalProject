using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGestureReciver : MonoBehaviour
{
    [SerializeField]ShipControls ship;

    void Start()
    {
        GestureManager.Instance.OnSwipe += OnSwipe;
        GestureManager.Instance.OnSpread += OnSpread;
    }

    void OnDisable()
    {
        GestureManager.Instance.OnSwipe -= OnSwipe;
        GestureManager.Instance.OnSpread -= OnSpread;
    }
    
    void Update()
    {
        
    }

    private void OnSwipe(object sender, SwipeEventArgs args)
    {
        if(args.SwipeDirection == SwipeEventArgs.SwipeDirections.UP)
        {
            ship.ChangeBulletType(true);
        }
        if(args.SwipeDirection == SwipeEventArgs.SwipeDirections.DOWN)
        {
            ship.ChangeBulletType(false);
        }
        if(args.SwipeDirection == SwipeEventArgs.SwipeDirections.RIGHT)
        {
            ship.DodgeRoll(true);
        }
        if(args.SwipeDirection == SwipeEventArgs.SwipeDirections.LEFT)
        {
            ship.DodgeRoll(false);   
        }
    }

    private void OnSpread(object sender, SpreadEventArgs args)
    {
        if(args.SpreadableType == SpreadType.Spread)
        {
            
        }
        else
        {
            ship.ActivateShield();
        }
    }
}
