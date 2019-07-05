using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float dist;
    public float rotDir;
    private Rigidbody2D rb2d;
    private Vector3 vel;
    private Vector3 cameFrom;
    private Vector3 dest;
    private VCR vcr;
    private VHS vhs;

    void Start()
    {
        vcr = GetComponent<VCR>();
        vhs = GetComponent<VHS>();
        rb2d = GetComponent<Rigidbody2D>();
        dest = new Vector3(transform.position.x, dist, 0f);
    }

    void Update()
    {
        cameFrom = transform.position - -transform.up;
        // if we aren't paused
        if (!vcr.IsPaused)
        {
            // if we aren't recording operate as normal and record any relevant information to the VHS
            if (!vcr.IsRewinding)
            {
                transform.Rotate(0f, 0f, rotDir * speed * Time.deltaTime);
                transform.position = Vector3.MoveTowards(transform.position, dest, speed * Time.deltaTime);
                vhs.SetInfo(transform.position, transform.rotation, transform.localScale, dest, 0);
            }
            // we we aren't recording, we are rewinding, and need the VHS to tell us what changes we are making
            else
            {
                transform.position = vhs.Position;
                transform.rotation = vhs.Rotation;
                transform.localScale = vhs.Scale;

                dest = vhs.Destination;
            }
        }

        // if we reach our destination, turn around and set a new dest
        if (transform.position == dest)
        {
            dist = -dist;
            dest = new Vector3(transform.position.x, dist, 0f);
        }
    }
}
