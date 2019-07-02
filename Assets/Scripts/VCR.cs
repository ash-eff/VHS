using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VCR : MonoBehaviour
{
    public bool isRewinding = false;
    public bool isPaused = false;

    private float recordTime = 5f;

    public Vector3 destination;

    private LinkedList<PointsInTime> pointsInTime;

    public bool IsRewinding
    {
        get { return isRewinding; }
    }

    public bool IsPaused
    {
        get { return isPaused; }
    }

    private void OnEnable()
    {
        Player.OnPressedRewind += StartRewind;
        Player.OnPressedPlay += StopRewind;
        Player.OnPressedPause += Paused;
    }

    private void OnDisable()
    {
        Player.OnPressedRewind -= StartRewind;
        Player.OnPressedPlay -= StopRewind;
        Player.OnPressedPause -= Paused;
    }

    void Start()
    {
        pointsInTime = new LinkedList<PointsInTime>();
    }

    private void FixedUpdate()
    {
        if (isRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }
    }

    void Rewind()
    {
        isPaused = false;
        if(pointsInTime.Count > 0)
        {
            PointsInTime pointInTime = pointsInTime.First.Value;
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            transform.GetComponent<Enemy>().dest = pointInTime.destination;
            pointsInTime.RemoveFirst();
        }
        else
        {
            StopRewind();
        }
    }

    void Record()
    {
        if (!isPaused)
        {
            if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
            {
                pointsInTime.RemoveLast();
            }

            destination = transform.GetComponent<Enemy>().dest;
            pointsInTime.AddFirst(new PointsInTime(transform.position, transform.rotation, destination));
        }
    }

    void Paused()
    {
        isPaused = !isPaused;
    }

    void StartRewind()
    {
        isRewinding = true;
    }

    void StopRewind()
    {
        isRewinding = false;
    }
}
