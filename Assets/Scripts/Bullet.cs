using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private VCR vcr;
    private VHS vhs;
    private float activeTime;
    private float timeToCheck;
    private float rewindTime;

    private void Start()
    {
        vhs = GetComponent<VHS>();
        vcr = GetComponent<VCR>();
        rb2d = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 6f);
    }

    private void FixedUpdate()
    {
        if(!vcr.IsPaused)
        {
            if (!vcr.IsRewinding)
            {
                activeTime += Time.deltaTime;
                Vector3 movePos = transform.position + transform.right * 5 * Time.deltaTime;
                rb2d.MovePosition(movePos);
                vhs.SetInfo(transform.position, transform.rotation, transform.localScale, Vector3.zero, 0);
            }
            else
            {
                timeToCheck = activeTime;
                transform.position = vhs.Position;
                if (vcr.TimeRewound >= timeToCheck)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
