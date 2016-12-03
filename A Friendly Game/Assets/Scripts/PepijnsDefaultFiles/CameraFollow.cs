using UnityEngine;
using System.Collections;

//by Pepijn Willekens
// https://github.com/peperbol
// https://twitter.com/PepijnWillekens

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    public bool isBounded = false;
    [ConditionalHide("isBounded")]
    public Rect mapBounds;
    
    public float verticalSize;
    private Camera cam;
    [Range(0, 1)]
    public float followUpSpeed;
    public Transform targetToFollow;

    [Range(0, 1)]
    public float horizontalWanderPercent, verticalWanderPercent;
    [Range(-0.5f, 0.5f)]
    public float horizontalCenterPercent, verticalCenterPercent;

    private Rect PosToCamBounds(Vector2 pos)
    {
        return new Rect(pos - CamSize / 2, CamSize);
    }


    private bool IsRectInRect(Rect inner, Rect outer)
    {
        bool toReturn = true;
        toReturn &= inner.yMax <= outer.yMax;
        toReturn &= inner.xMax <= outer.xMax;
        toReturn &= inner.yMin >= outer.yMin;
        toReturn &= inner.xMin >= outer.xMin;
        return toReturn;
    }
    private Rect ClosestContaintingRect(Rect target, Rect container)
    {

        if (target.yMax > container.yMax) target.y -= target.yMax - container.yMax;
        if (target.xMax > container.xMax) target.x -= target.xMax - container.xMax;
        if (target.yMin < container.yMin) target.y += container.yMin - target.yMin;
        if (target.xMin < container.xMin) target.x += container.xMin - target.xMin;
        return target;
    }
    private Vector2 CamSize
    {
        get { return new Vector2(verticalSize * 2 * cam.aspect, verticalSize * 2); }
    }

    private Vector2 MyPosition
    {
        get
        {
            return V2(transform.position);
        }
        set
        {
            if (isBounded)
            {
                Rect r = ClosestContaintingRect(PosToCamBounds(value), mapBounds);
                transform.position = new Vector3(r.center.x, r.center.y, transform.position.z);
            }
            else
            {
                transform.position = ((Vector3)value).SetZ(transform.position.z);
            }

        }
    }
    private Vector2 V2(Vector3 v3)
    {
        return new Vector2(v3.x, v3.y);
    }
    private Rect TargetBounds
    {
        get
        {
            Rect toReturn = PosToCamBounds(MyPosition);
            Vector2 c = MyPosition + new Vector2(toReturn.width * horizontalCenterPercent, toReturn.height * verticalCenterPercent);
            toReturn.width *= horizontalWanderPercent;
            toReturn.height *= horizontalWanderPercent;
            toReturn.center = c;
            return toReturn;
        }
    }
    void Move(Vector2 offset)
    {
        MyPosition += offset;
    }
    // Use this for initialization
    void Start()
    {
        cam = GetComponent<Camera>();

        Vector2 targetPos = ClosestContaintingRect(new Rect(V2(targetToFollow.position), Vector2.zero), TargetBounds).position;
        Move(V2(targetToFollow.position) - targetPos);
        if(!Application.isEditor)
            Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (!TargetBounds.Contains(V2(targetToFollow.position)))
        {
            Vector2 targetPos = ClosestContaintingRect(new Rect(V2(targetToFollow.position), Vector2.zero), TargetBounds).position;

            Move(Vector2.Lerp(targetPos, V2(targetToFollow.position), followUpSpeed) - targetPos);
        }
    }
}
