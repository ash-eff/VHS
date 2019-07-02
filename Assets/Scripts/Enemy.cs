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
    public Vector3 cameFrom;
    public Vector3 dest;
    private VCR vcr;

    void Start()
    {
        vcr = GetComponent<VCR>();
        rb2d = GetComponent<Rigidbody2D>();
        dest = new Vector3(transform.position.x, dist, 0f);
    }

    void Update()
    {
        cameFrom = transform.position - -transform.up;
        if (!vcr.isPaused)
        {
            if (!vcr.IsRewinding)
            {
                transform.Rotate(0f, 0f, rotDir * speed * Time.deltaTime);
                transform.position = Vector3.MoveTowards(transform.position, dest, speed * Time.deltaTime);
            }
        }

        if (transform.position == dest)
        {
            dist = -dist;
            dest = new Vector3(transform.position.x, dist, 0f);
        }
    }
}
