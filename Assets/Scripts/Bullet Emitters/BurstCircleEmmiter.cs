using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstCircleEmmiter : EnemyShot {

    public float bulletSpeed;
    public int perCircle;
    public float shotDelay;
    public float offset;
    [Header("Direction is clock(-1) or counterclock(1)")]
    public int direction = 1;

    public int spiraling;
    [Header("the delay is dom%num < comp")]
    public int dom;
    public int num;
    public int comp;

    private int alt = 1;

    public override void Fire()
    {
        Transform[] bullets = new Transform[perCircle];

        for (int i = 0; i < perCircle; i++)
        {
            bullets[i] = Instantiate(bullet);

            float altAngle = (2 * Mathf.PI) / (float)perCircle / (float)spiraling * alt;

            Vector3 bulletVec = AngleMath.AngleToVector3(direction*(((2 * Mathf.PI) / (float)perCircle) * i + altAngle));
            bullets[i].transform.position = transform.position + bulletVec * offset;
            bulletVec = bulletVec * bulletSpeed;

            bullets[i].GetComponent<UpdateBullet>().movement = bulletVec;
        }

        alt++;
        if (alt > spiraling)
        {
            alt = 1;
        }
    }

    public override bool FireRate()
    {
        count += Time.deltaTime;
        if (count >= shotDelay && firing)
        {
            count = 0;
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (FireRate() && Delay())
        {
            Fire();
        }
        dom++;
    }
    public bool Delay()
    {
        if (dom%num <= comp)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
