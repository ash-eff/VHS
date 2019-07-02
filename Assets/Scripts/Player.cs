using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public delegate void VCREvent();
    public static event VCREvent OnPressedPlay;
    public static event VCREvent OnPressedPause;
    public static event VCREvent OnPressedRewind;

    public float rewindTime;
    public float speed;

    private float currentRewindTime;

    private Rigidbody2D rb2d;
    private Vector3 vel;

    private void Start()
    {
        currentRewindTime = rewindTime;
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        vel = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (Input.GetKey(KeyCode.Return))
        {
            currentRewindTime -= Time.deltaTime;
            if(currentRewindTime < 0)
            {
                OnPressedPlay();
            }
            else
            {
                OnPressedRewind();
            }
        }
        if (Input.GetKeyUp(KeyCode.Return))
        {
            currentRewindTime = rewindTime;
            OnPressedPlay();
        }

        if(currentRewindTime == rewindTime && Input.GetKeyDown(KeyCode.R))
        {
            OnPressedPause();
        }
    }

    private void FixedUpdate()
    {
        Vector3 movePos = transform.position + vel.normalized * speed * Time.deltaTime;

        //movePos.x = Mathf.Clamp(movePos.x, minClamp.x, maxClamp.x);
        //movePos.y = Mathf.Clamp(movePos.y, minClamp.y, maxClamp.y);

        rb2d.MovePosition(movePos);
    }
}
