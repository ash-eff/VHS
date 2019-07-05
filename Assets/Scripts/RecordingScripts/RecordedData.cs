using UnityEngine;

public struct RecordedData
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
    public Vector3 destination;
    public int state;

    public RecordedData(Vector3 _position, Quaternion _rotation, Vector3 _scale, Vector3 _destination, int _state)
    {
        position = _position;
        rotation = _rotation;
        scale = _scale;
        destination = _destination;
        state = _state;
    }
}
