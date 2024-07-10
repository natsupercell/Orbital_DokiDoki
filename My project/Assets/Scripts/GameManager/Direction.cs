using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction 
{
    LEFT,
    RIGHT, 
    UP, 
    DOWN
}

public static class DirectionExtensions
{
    public static Quaternion toQuaternion(this Direction direction)
    {
        switch (direction)
        {
            case Direction.LEFT:
                return Quaternion.Euler(0, 0, 180f);
            case Direction.RIGHT:
                return Quaternion.Euler(0, 0, 0f);
            case Direction.UP:
                return Quaternion.Euler(0, 0, 90f);
            case Direction.DOWN:
                return Quaternion.Euler(0, 0, 270f);
            default:
                return Quaternion.Euler(0, 0, 0f);
        }
    }

    public static Vector2 toVector2(this Direction direction)
    {
        switch (direction)
        {
            case Direction.LEFT:
                return Vector2.left;
            case Direction.RIGHT:
                return Vector2.right;
            case Direction.UP:
                return Vector2.up;
            case Direction.DOWN:
                return Vector2.down;
            default:
                return Vector2.zero;
        }
    }

    public static Vector3 toVector3(this Direction direction)
    {
        switch (direction)
        {
            case Direction.LEFT:
                return Vector3.left;
            case Direction.RIGHT:
                return Vector3.right;
            case Direction.UP:
                return Vector3.up;
            case Direction.DOWN:
                return Vector3.down;
            default:
                return Vector3.zero;
        }
    }
}