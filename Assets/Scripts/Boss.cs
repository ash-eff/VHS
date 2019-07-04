using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
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

    public Transform weaponOne;
    public Transform weaponTwo;
    public Transform weaponThree;

    public GameObject bulletPrefab;


    private void Start()
    {
        StartCoroutine(Wait(AttackOne()));
    }

    IEnumerator Wait(IEnumerator i)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        StartCoroutine(i);
    }

    IEnumerator AttackOne()
    {
        int bullets = attackOneBullets;
        int waves = attackOneWaves;
        int i = 0;
        while (bullets > 0)
        {
            bullets--;
            weaponOne.rotation = Quaternion.Euler(0, 0, i * (360 / (attackOneBullets / waves)));
            Instantiate(bulletPrefab, weaponOne.transform.position, weaponOne.rotation);
            i++;
            yield return new WaitForSecondsRealtime(attackOneWaitTime);
        }

        StartCoroutine(Wait(AttackTwo()));
    }

    IEnumerator AttackTwo()
    {
        int bullets = attackTwoBullets;
        int waves = attackTwoWaves;
        int offset = 0;
        int bulletsPerWave = bullets / waves;
        float weaponRot = 0f;
        float rotAmount = 360 / bulletsPerWave;
        while (waves > 0)
        {
            for (int i = 0; i < bulletsPerWave; i++)
            {
                weaponRot = i * rotAmount;
                weaponTwo.rotation = Quaternion.Euler(0, 0, weaponRot + offset);
                GameObject obj = Instantiate(bulletPrefab, weaponTwo.transform.position, weaponTwo.rotation);
            }
            offset += 9;
            waves--;
            yield return new WaitForSecondsRealtime(attackTwoWaitTime);
        }

        StartCoroutine(Wait(AttackThree()));
    }

    IEnumerator AttackThree()
    {
        int bullets = attackThreeBullets;
        int waves = attackThreeWaves;
        int bulletsPerWave = bullets / waves;
        float weaponRot = 0f;
        float rotOffset = 0;
        float rotAmount = 360 / bulletsPerWave;
        while (waves > 0)
        {
            for (int i = 0; i < bulletsPerWave / 4; i++)
            {
                weaponRot = i * rotAmount;
                weaponTwo.rotation = Quaternion.Euler(0, 0, weaponRot + rotOffset);
                GameObject obj = Instantiate(bulletPrefab, weaponTwo.transform.position, weaponTwo.rotation);
            }

            yield return new WaitForSecondsRealtime(attackTwoWaitTime / 2);

            for (int i = 0; i < bulletsPerWave / 4; i++)
            {
                weaponRot = i * rotAmount;
                weaponTwo.rotation = Quaternion.Euler(0, 0, weaponRot + rotOffset);
                GameObject obj = Instantiate(bulletPrefab, weaponTwo.transform.position, weaponTwo.rotation);
            }
            waves--;
            rotOffset += 90f;
            yield return new WaitForSecondsRealtime(attackTwoWaitTime);
        }

        waves = attackThreeWaves * 2;
        weaponRot = 0;
        rotOffset = 0;

        while (waves > 0)
        {
            for (int i = 0; i < bulletsPerWave / 4; i++)
            {
                weaponRot = i * rotAmount;
                weaponTwo.rotation = Quaternion.Euler(0, 0, weaponRot + rotOffset);
                GameObject obj = Instantiate(bulletPrefab, weaponTwo.transform.position, weaponTwo.rotation);
            }

            waves--;
            rotOffset += 90f;
            yield return new WaitForSecondsRealtime(attackTwoWaitTime / 4);
        }

        StartCoroutine(Wait(AttackOne()));
    }
}
