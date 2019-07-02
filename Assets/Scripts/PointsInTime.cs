using UnityEngine;

public struct PointsInTime
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 destination;

    public PointsInTime (Vector3 _pos, Quaternion _rot, Vector3 _dest)
    {
        position = _pos;
        rotation = _rot;
        destination = _dest;
    }
}
