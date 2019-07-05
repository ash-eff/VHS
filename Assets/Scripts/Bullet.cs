using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private VCR vcr;
    private VHS vhs;
    private float spawnTime;

    private void Start()
    {
        vhs = GetComponent<VHS>();
        vcr = GetComponent<VCR>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(!vcr.IsPaused)
        {
            if (!vcr.IsRewinding)
            {
                Vector3 movePos = transform.position + transform.right * 5 * Time.deltaTime;
                rb2d.MovePosition(movePos);
                vhs.SetInfo(transform.position, transform.rotation, transform.localScale, Vector3.zero, 0);
            }
            else
            {
                transform.position = vhs.Position;
            }
        }
    }

    public float SpawnTime
    {
        get { return spawnTime; }
        set { spawnTime = value; }
    }
}
