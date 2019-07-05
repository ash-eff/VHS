using UnityEngine;

// stores all the recorded data from each class it's attacked to

public class VHS : MonoBehaviour
{
    private Vector3 position;
    private Quaternion rotation;
    private Vector3 scale;
    private Vector3 destination;
    private int state;

    public void SetInfo(Vector3 _pos, Quaternion _rot, Vector3 _scale, Vector3 _dest, int _state)
    {
        position = _pos;
        rotation = _rot;
        scale = _scale;
        destination = _dest;
        state = _state;
    }

    public Vector3 Position
    {
        get { return position; }
        set { position = value; }
    }

    public Quaternion Rotation
    {
        get { return rotation; }
        set { rotation = value; }
    }

    public Vector3 Scale
    {
        get { return scale; }
        set { scale = value; }
    }

    public Vector3 Destination
    {
        get { return destination; }
        set { destination = value; }
    }

    public int State
    {
        get { return state; }
        set { state = value; }
    }
}
