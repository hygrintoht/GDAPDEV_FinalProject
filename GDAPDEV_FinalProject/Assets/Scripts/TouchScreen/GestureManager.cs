using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GestureManager : MonoBehaviour
{
    //singleton
    public static GestureManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    //gesture propeties
    //public TapProperty tapProperty;
    //public EventHandler<TapEventArgs> OnTap;
    public SwipeProperty swipeProperty;
    public EventHandler<SwipeEventArgs> OnSwipe;
    //public DragProperty dragProperty;
    //public EventHandler<DragEventArgs> OnDrag;
    public SpreadProperty spreadProperty;
    public EventHandler<SpreadEventArgs> OnSpread;
    public RotateProperty rotateProperty;
    public EventHandler<RotateEventArgs> OnRotate;

    //finger tracking variables
    Touch trackedFinger1;
    Touch trackedFinger2;

    private Vector2 startPoint = Vector2.zero;
    private Vector2 endPoint = Vector2.zero;
    private float gestureTime = 0;

    private void Update()
    {
        if (Input.touchCount > 0) 
        {
            if (Input.touchCount == 1)
            {
                trackedFinger1 = Input.GetTouch(0);

                if (trackedFinger1.phase == TouchPhase.Began)
                {
                    startPoint = trackedFinger1.position;
                    gestureTime = 0;
                }
                /*else if (gestureTime >= dragProperty.bufferTime)
                {
                    FireDragEvent(trackedFinger1);
                }*/
                else if (trackedFinger1.phase == TouchPhase.Ended /*&& gestureTime < dragProperty.bufferTime*/)
                {
                    endPoint = trackedFinger1.position;

                    /*if (gestureTime <= tapProperty.tapTime && Vector2.Distance(startPoint, endPoint) < (Screen.dpi * tapProperty.tapMaxDistance))
                    {
                        FireTapEvent(trackedFinger1.position);
                    }*/

                    if (gestureTime <= swipeProperty.swipeTime && Vector2.Distance(startPoint, endPoint) >= (swipeProperty.minSwipeDistance * Screen.dpi))
                    {
                        //Debug.Log("Swipe");
                        FireSwipeEvent();
                    }
                }
                else
                    gestureTime += Time.deltaTime;
            }
            else if (Input.touchCount > 1)
            {
                trackedFinger1 = Input.GetTouch(0);
                trackedFinger2 = Input.GetTouch(1);
                
                if(trackedFinger1.phase == TouchPhase.Moved || trackedFinger2.phase == TouchPhase.Moved)
                {
                    Vector2 prevPoint1 = GetPreviousPoint(trackedFinger1);
                    Vector2 prevPoint2 = GetPreviousPoint(trackedFinger2);

                    float currDistance = Vector2.Distance(trackedFinger1.position, trackedFinger2.position);
                    float prevDistance = Vector2.Distance(prevPoint2, prevPoint1);

                    if(Math.Abs(currDistance - prevDistance) >= spreadProperty.MinDistanceChange * Screen.dpi)
                    {
                        FireSpreadEvent(currDistance - prevDistance);
                    }
                }

                if((trackedFinger1.phase == TouchPhase.Moved || trackedFinger2.phase == TouchPhase.Moved) && Vector2.Distance(trackedFinger1.position, trackedFinger2.position) >= rotateProperty.minDistance * Screen.dpi)//long boi
                {
                    Vector2 prevPoint1 = GetPreviousPoint(trackedFinger1);
                    Vector2 prevPoint2 = GetPreviousPoint(trackedFinger2);

                    Vector2 diffVector = trackedFinger1.position - trackedFinger2.position;
                    Vector2 prevDiffVector = prevPoint1 - prevPoint2;

                    float angle = Vector2.Angle(prevDiffVector, diffVector);

                    if (angle >= rotateProperty.minChange)
                    {
                        //Debug.Log("Rotated")
                        FireRotateEvent(diffVector, prevDiffVector, angle);
                    }
                }
            }
        }
    }

    /*private void OnDrawGizmos()
    {
        if (Input.touchCount > 0)
        {
            Ray r = Camera.main.ScreenPointToRay(trackedFinger1.position);
            Gizmos.DrawIcon(r.GetPoint(10), "TestIcon");
        }
    }*/

    /*private void FireTapEvent(Vector2 pos)
    {
        Debug.Log("Tap");
        if(OnTap != null)
        {
            GameObject hitObj = null;
            Ray r = Camera.main.ScreenPointToRay(pos);
            RaycastHit hit = new RaycastHit();

            if(Physics.Raycast(r, out hit, Mathf.Infinity))
            {
                hitObj = hit.collider.gameObject;
            }

            TapEventArgs tapArgs = new TapEventArgs(pos, hitObj);
            OnTap(this, tapArgs);

            if(hitObj != null)
            {
                ITap handler = hitObj.GetComponent<ITap>();
                if (handler != null) handler.OnTap();
            }
        }
    }*/

    private void FireSwipeEvent()
    {

        Vector2 dir = endPoint - startPoint;
        SwipeEventArgs.SwipeDirections swipeDir = SwipeEventArgs.SwipeDirections.RIGHT;

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            if(dir.x > 0)
            {
                //Debug.Log("Right");
                swipeDir = SwipeEventArgs.SwipeDirections.RIGHT;
            }
            else
            {
                //Debug.Log("Left");
                swipeDir = SwipeEventArgs.SwipeDirections.LEFT;
            }
        }
        else
        {
            if(dir.y > 0)
            {
                //Debug.Log("Up");
                swipeDir = SwipeEventArgs.SwipeDirections.UP;
            }
            else
            {
                //Debug.Log("Down");
                swipeDir = SwipeEventArgs.SwipeDirections.DOWN;
            }
        }

        Ray r = Camera.main.ScreenPointToRay(startPoint);
        RaycastHit hit = new RaycastHit();
        GameObject hitObj = null;

        if (Physics.Raycast(r, out hit, Mathf.Infinity))
        {
            hitObj = hit.collider.gameObject;
        }

        SwipeEventArgs swipeArgs = new SwipeEventArgs(startPoint, swipeDir, dir, hitObj);
        if (OnSwipe != null)
        {
            OnSwipe(this, swipeArgs);
        }

        if (hitObj != null)
        {
            ISwipeable swipeable = hitObj.GetComponent<ISwipeable>();
            if (swipeable != null)
                swipeable.OnSwipe(swipeArgs);
        }
    }

    /*private void FireDragEvent(Touch trackedFinger)
    {
        Debug.Log("FireDragEvent");

        GameObject hitObj = null;
        Ray r = Camera.main.ScreenPointToRay(startPoint);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(r, out hit, Mathf.Infinity))
        {
            hitObj = hit.collider.gameObject;
        }

        DragEventArgs dragArgs = new DragEventArgs(trackedFinger, hitObj);

        if (OnDrag != null)
        {
            OnDrag(this, dragArgs);
        }
        
        if (hitObj != null)
        {
            IDragable dragable = hitObj.GetComponent<IDragable>();
            if(dragable != null)
            {
                dragable.OnDrag(dragArgs);
            }
        }
    }*/

    private void FireRotateEvent(Vector2 diffVector, Vector2 prevDiffVector, float angle)
    {
        Vector3 cross = Vector3.Cross(prevDiffVector, diffVector);
        RotateDirection dir;
        if(cross.z > 0)
        {
            //Debug.Log($"Rotate CCW {angle}");
            dir = RotateDirection.CCW;
        }
        else
        {
            //Debug.Log($"Rotate CW {angle}");
            dir = RotateDirection.CW;
        }
        GameObject hitObj = null;
        Vector2 mid = GetMidpoint(trackedFinger1.position, trackedFinger2.position);
        Ray r = Camera.main.ScreenPointToRay(mid);
        RaycastHit hit;

        if (Physics.Raycast(r, out hit, Mathf.Infinity))
        {
            hitObj = hit.collider.gameObject;
        }

        RotateEventArgs args = new RotateEventArgs(trackedFinger1, trackedFinger2, angle, dir, hitObj);
        if (OnRotate != null)
        {
            OnRotate(this, args);
        }

        if (hitObj != null)
        {
            IRotateable rotateable = hitObj.GetComponent<IRotateable>();
            if (rotateable != null)
                rotateable.OnRotate(args);
        }
    }

    private Vector2 GetPreviousPoint(Touch finger)
    {
        return finger.position - finger.deltaPosition;
    }

    private void FireSpreadEvent(float distDelta)
    {
        SpreadType type;
        if (distDelta > 0)
        {
            Debug.Log("Spread");
            type = SpreadType.Spread;
        }
        else
        {
            Debug.Log("Pinch");
            type = SpreadType.Pinch;
        }

        Vector2 midpoint = GetMidpoint(trackedFinger1.position, trackedFinger2.position);

        Ray r = Camera.main.ScreenPointToRay(midpoint);
        RaycastHit hit;
        GameObject hitObj = null;
        Debug.DrawRay(r.origin, r.direction * 100f, Color.red, 5f);
        if (Physics.Raycast(r, out hit, Mathf.Infinity))
        {
            hitObj = hit.collider.gameObject;
            Debug.Log("hit object");
        }

        SpreadEventArgs args = new SpreadEventArgs(trackedFinger1, trackedFinger2, distDelta, type, hitObj);

        if (OnSpread != null)
        {
            OnSpread(this, args);
        }

        if (hitObj != null)
        {
            ISpreadable spreadable = hitObj.GetComponent<ISpreadable>();
            if (spreadable != null)
            {
                Debug.Log("Spreadable hit");
                spreadable.OnSpread(args);
            }
        }
    }

    private Vector2 GetMidpoint(Vector2 p1, Vector2 p2)
    {
        return (p1 + p2) / 2;
    }
}
