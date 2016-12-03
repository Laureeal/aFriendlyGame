using UnityEngine;
using System.Collections;
using System;

//by Pepijn Willekens
// https://github.com/peperbol
// https://twitter.com/PepijnWillekens

public static class VectorExtension {

    //Vector3

    public static Vector3 SetX(this Vector3 parent, float newX)
    {
        return new Vector3(newX, parent.y, parent.z);
    }

    public static Vector3 SetY(this Vector3 parent, float newY)
    {
        return new Vector3(parent.y, newY, parent.z);
    }

    public static Vector3 SetZ(this Vector3 parent, float newZ)
    {
        return new Vector3(parent.x, parent.y, newZ);
    }

    //Vector2

    public static Vector2 SetX(this Vector2 parent, float newX)
    {
        return new Vector2(newX, parent.y);
    }

    public static Vector2 SetY(this Vector2 parent, float newY)
    {
        return new Vector2(parent.x, newY);
    }

    public static Vector2 ChangeX(this Vector2 parent, FloatEdit edit)
    {
        return new Vector2(edit(parent.x), parent.y);
    }

    public static Vector2 ChangeY(this Vector2 parent, FloatEdit edit)
    {
        return new Vector2(parent.x, edit(parent.y));
    }
    public delegate float FloatEdit(float input);
}
