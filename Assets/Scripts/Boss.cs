using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public enum Phase {  Wait, AttackOne, AttackTwo, AttackThree }
    public Phase phase = Phase.Wait;
    public float waitTime;

    public int attackOneBullets;
    public float attackOneWaitTime;
    public int attackOneWaves;

    public int attackTwoBullets;
    public float attackTwoWaitTime;
    public int attackTwoWaves;

    public int attackThreeBullets;
    public float attackThreeWaitTime;
    public int attackThreeWaves;

    private float timer;

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
        StartCoroutine(Wait(One()));
    }

    IEnumerator Wait(IEnumerator i)
    {
        phase = Phase.Wait;
        timer = 0;
        while(timer < waitTime)
        {
            timer += Time.deltaTime;
            vhs.SetInfo(transform.position, transform.rotation, transform.localScale, Vector3.zero, (int)phase);
            while (vcr.IsRewinding)
            {
                AssignForRewind();
                yield return null;
            }

            while (vcr.IsPaused)
            {
                yield return null;
            }

            yield return null;
        }

        StartCoroutine(i);
    }

    IEnumerator One()
    {
        phase = Phase.AttackOne;
        timer = 0;
        while (timer < waitTime)
        {
            timer += Time.deltaTime;
            vhs.SetInfo(transform.position, transform.rotation, transform.localScale, Vector3.zero, (int)phase);
            while (vcr.IsRewinding)
            {
                AssignForRewind();
                yield return null;
            }

            while (vcr.IsPaused)
            {
                yield return null;
            }

            yield return null;
        }

        StartCoroutine(Wait(Two()));
    }

    IEnumerator Two()
    {
        phase = Phase.AttackTwo;
        timer = 0;
        while (timer < waitTime)
        {
            timer += Time.deltaTime;
            vhs.SetInfo(transform.position, transform.rotation, transform.localScale, Vector3.zero, (int)phase);
            while (vcr.IsRewinding)
            {
                AssignForRewind();
                yield return null;
            }

            while (vcr.IsPaused)
            {
                yield return null;
            }

            yield return null;
        }

        StartCoroutine(Wait(Three()));
    }

    IEnumerator Three()
    {
        phase = Phase.AttackThree;
        timer = 0;
        while (timer < waitTime)
        {
            timer += Time.deltaTime;
            vhs.SetInfo(transform.position, transform.rotation, transform.localScale, Vector3.zero, (int)phase);
            while (vcr.IsRewinding)
            {
                AssignForRewind();
                yield return null;
            }

            while (vcr.IsPaused)
            {
                yield return null;
            }

            yield return null;
        }

        StartCoroutine(Wait(One()));
    }

    //IEnumerator AttackOne()
    //{
    //    phase = Phase.AttackOne;
    //    int bullets = attackOneBullets;
    //    int waves = attackOneWaves;
    //    int i = 0;
    //    bool rewound = false;
    //    float spawnTime = 0;
    //    while (bullets > 0)
    //    {
    //        while (vcr.isRewinding)
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
    //        spawnTime += Time.deltaTime;
    //        GameObject obj = Instantiate(bulletPrefab, weaponOne.transform.position, weaponOne.rotation);
    //        obj.GetComponent<Bullet>().SpawnTime = spawnTime;
    //        i++;
    //        yield return new WaitForSeconds(attackOneWaitTime);
    //    }
    //
    //    StartCoroutine(Wait(AttackOne()));
    //}
    
    //IEnumerator AttackTwo()
    //{
    //    phase = Phase.AttackTwo;
    //    int bullets = attackTwoBullets;
    //    int waves = attackTwoWaves;
    //    int offset = 0;
    //    int bulletsPerWave = bullets / waves;
    //    float weaponRot = 0f;
    //    float rotAmount = 360 / bulletsPerWave;
    //    while (waves > 0)
    //    {
    //        for (int i = 0; i < bulletsPerWave; i++)
    //        {
    //            while (vcr.isRewinding || vcr.isPaused)
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
    //    StartCoroutine(Wait(AttackOne()));
    //}
    
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
