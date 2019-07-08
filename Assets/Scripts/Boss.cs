using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public enum Phase {  Wait, AttackOne, AttackTwo, AttackThree }
    public Phase phase = Phase.Wait;
    public float waitTime;

    public int attackOneBullets;
    public float attackOneTime;

    public int attackTwoBullets;
    public float attackTwoWaitTime;

    public int attackThreeBullets;
    public float attackThreeWaitTime;

    private float timer;
    private float clock;

    public Transform weaponOne;
    public Transform weaponTwo;
    public Transform weaponThree;

    public GameObject bulletPrefab;

    private VCR vcr;
    private VHS vhs;

    private void Start()
    {
        vhs = GetComponent<VHS>();
        vcr = GetComponent<VCR>();
        StartCoroutine(Wait(AttackOne()));
    }

    IEnumerator Wait(IEnumerator i)
    {
        phase = Phase.Wait;
        yield return new WaitForSeconds(waitTime);

        StartCoroutine(i);
    }

    // attack based on time
    IEnumerator AttackOne()
    {
        phase = Phase.AttackOne;
        int bullets = 0;
        int angle = 0;
        float timer = 0;
        int bulletsPerSecond = attackOneBullets / (int)attackOneTime;
        float wTime = 1.0f / bulletsPerSecond;
        while (timer < attackOneTime)
        {
            // adjust firing angle
            weaponOne.rotation = Quaternion.Euler(0, 0, angle * (360 / (attackOneBullets / attackOneTime)));
            angle++;
            // fire and log bullet
            GameObject obj = Instantiate(bulletPrefab, weaponOne.transform.position, weaponOne.rotation);
            bullets++;
            yield return new WaitForSeconds(wTime);
            // adjust timer
            timer = bullets / bulletsPerSecond;
        }

        StartCoroutine(Wait(AttackOne()));
    }

    //IEnumerator AttackOne()
    //{
    //    phase = Phase.AttackOne;
    //    int bullets = attackOneBullets;
    //    int waves = attackOneWaves;
    //    int i = 0;
    //    bool rewound = false;
    //    float spawnTime = 0;
    //    // while there are bullets to fire
    //    while (bullets > 0)
    //    {
    //        while (vcr.IsPaused)
    //        {
    //            yield return null;
    //        }
    //
    //        while (vcr.IsRewinding)
    //        {
    //            rewound = true;
    //            bullets++;
    //            i--;
    //            yield return new WaitForSeconds(attackOneWaitTime);
    //        }
    //
    //        if (rewound)
    //        {
    //            if (i < 0)
    //            {
    //                i = 0;
    //            }
    //            if(bullets > attackOneBullets)
    //            {
    //                bullets = attackOneBullets;
    //            }
    //            rewound = false;
    //        }
    //
    //        bullets--;
    //        weaponOne.rotation = Quaternion.Euler(0, 0, i * (360 / (attackOneBullets / waves)));
    //        i++;
    //        spawnTime += Time.deltaTime;
    //        GameObject obj = Instantiate(bulletPrefab, weaponOne.transform.position, weaponOne.rotation);
    //
    //        yield return new WaitForSeconds(attackOneWaitTime);
    //    }
    //
    //    StartCoroutine(Wait(AttackTwo()));
    //}
    //
    //IEnumerator AttackTwo()
    //{
    //    phase = Phase.AttackTwo;
    //    int bullets = attackTwoBullets;
    //    int waves = attackTwoWaves;
    //    int offset = 0;
    //    int bulletsPerWave = bullets / waves;
    //    float weaponRot = 0f;
    //    float rotAmount = 360 / bulletsPerWave;
    //    bool rewound = false;
    //    while (waves > 0)
    //    {
    //        while (vcr.IsPaused)
    //        {
    //            yield return null;
    //        }
    //
    //        while (vcr.IsRewinding)
    //        {
    //            rewound = true;
    //            bullets++;
    //            i--;
    //            yield return new WaitForSeconds(attackOneWaitTime);
    //        }
    //
    //        if (rewound)
    //        {
    //            if (i < 0)
    //            {
    //                i = 0;
    //            }
    //            if (bullets > attackOneBullets)
    //            {
    //                bullets = attackOneBullets;
    //            }
    //            rewound = false;
    //        }
    //
    //        for (int i = 0; i < bulletsPerWave; i++)
    //        {
    //            while (vcr.IsRewinding || vcr.IsPaused)
    //            {
    //                yield return null;
    //            }
    //            weaponRot = i * rotAmount;
    //            weaponTwo.rotation = Quaternion.Euler(0, 0, weaponRot + offset);
    //            GameObject obj = Instantiate(bulletPrefab, weaponTwo.transform.position, weaponTwo.rotation);
    //        }
    //        offset += 9;
    //        waves--;
    //        yield return new WaitForSeconds(attackTwoWaitTime);
    //    }
    //
    //    StartCoroutine(Wait(AttackThree()));
    //}
    //
    //IEnumerator AttackThree()
    //{
    //    phase = Phase.AttackThree;
    //    int bullets = attackThreeBullets;
    //    int waves = attackThreeWaves;
    //    int bulletsPerWave = bullets / waves;
    //    float weaponRot = 0f;
    //    float rotOffset = 0;
    //    float rotAmount = 360 / bulletsPerWave;
    //    while (waves > 0)
    //    {
    //        for (int i = 0; i < bulletsPerWave / 4; i++)
    //        {
    //            weaponRot = i * rotAmount;
    //            weaponTwo.rotation = Quaternion.Euler(0, 0, weaponRot + rotOffset);
    //            GameObject obj = Instantiate(bulletPrefab, weaponTwo.transform.position, weaponTwo.rotation);
    //        }
    //
    //        yield return new WaitForSeconds(attackTwoWaitTime / 2);
    //
    //        for (int i = 0; i < bulletsPerWave / 4; i++)
    //        {
    //            weaponRot = i * rotAmount;
    //            weaponTwo.rotation = Quaternion.Euler(0, 0, weaponRot + rotOffset);
    //            GameObject obj = Instantiate(bulletPrefab, weaponTwo.transform.position, weaponTwo.rotation);
    //        }
    //        waves--;
    //        rotOffset += 90f;
    //        yield return new WaitForSeconds(attackTwoWaitTime);
    //    }
    //
    //    waves = attackThreeWaves * 2;
    //    weaponRot = 0;
    //    rotOffset = 0;
    //
    //    while (waves > 0)
    //    {
    //        for (int i = 0; i < bulletsPerWave / 4; i++)
    //        {
    //            weaponRot = i * rotAmount;
    //            weaponTwo.rotation = Quaternion.Euler(0, 0, weaponRot + rotOffset);
    //            GameObject obj = Instantiate(bulletPrefab, weaponTwo.transform.position, weaponTwo.rotation);
    //        }
    //
    //        waves--;
    //        rotOffset += 90f;
    //        yield return new WaitForSeconds(attackTwoWaitTime / 4);
    //    }
    //
    //    StartCoroutine(Wait(AttackOne()));
    //}

    void AssignForRewind()
    {
        transform.position = vhs.Position;
        transform.rotation = vhs.Rotation;
        transform.localScale = vhs.Scale;
        if(vcr.TimeRewound > timer)
        {
            Debug.Log("Back To WAIT");
            phase = Phase.Wait;
        }
        else
        {
            Debug.Log("Same State " + phase);
            phase = (Phase)vhs.State;
        }
    }
}
