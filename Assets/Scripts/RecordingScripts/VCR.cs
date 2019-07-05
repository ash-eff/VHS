using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// processes all recorded data that it recieves from the vhs

[RequireComponent(typeof(VHS))]
public class VCR : MonoBehaviour
{
    private bool isRewinding = false;
    private bool isPaused = false;

    private float recordTime = 5f;
    private float timeRewound = 0f;

    private VHS vhs;

    private LinkedList<RecordedData> recordedData;

    public bool IsRewinding
    {
        get { return isRewinding; }
    }

    public bool IsPaused
    {
        get { return isPaused; }
    }

    public float TimeRewound
    {
        get { return timeRewound; }
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
        recordedData = new LinkedList<RecordedData>();
        vhs = GetComponent<VHS>();
    }

    private void FixedUpdate()
    {
        // if rewinding
        if (isRewinding)
        {
            // record time length of rewinding
            timeRewound += Time.fixedDeltaTime;
            // rewind
            Rewind();
        }
        else
        {
            timeRewound = 0f;
            Record();
        }
    }

    void Rewind()
    {

        /// currentlt there is an issue with rewinding too early before there is a full buffer of data
        /// apparently rewinding back to a start point works fine, but if it rewinds further, things like position
        /// and scale will be set to 0 or null

        // cannot be paused while rewinding
        isPaused = false;

        // if there is data in our linked list
        if(recordedData.Count > 0)
        {
            // assign values from the first index of our list, to a new RecordedData called recordedDatum
            RecordedData recordedDatum = recordedData.First.Value;
            // set values in our VHS to the values in the recordedDatum
            vhs.Position = recordedDatum.position;
            vhs.Rotation = recordedDatum.rotation;
            vhs.Scale = recordedDatum.scale;
            vhs.Destination = recordedDatum.destination;
            vhs.State = recordedDatum.state;
            // remove the RecordedData at the first index, so the next can be processed
            recordedData.RemoveFirst();
        }
        else
        {
            StopRewind();
        }
    }

    void Record()
    {
        // do not record while paused
        if (!isPaused)
        {
            // drop recordedData at the end of our linked list if it's outside of our record timeframe
            if (recordedData.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
            {
                recordedData.RemoveLast();
            }
            // add RecordedData to our linked list from the VHS
            recordedData.AddFirst(new RecordedData(vhs.Position, vhs.Rotation, vhs.Scale, vhs.Destination, vhs.State));
        }
    }

    void Paused()
    {
        isPaused = !isPaused;
    }

    void StartRewind()
    {
        if (!isPaused)
        {
            isRewinding = true;
        }
    }

    void StopRewind()
    {
        isRewinding = false;
    }
}
