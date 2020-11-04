using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public int damage = 50;
    public GameObject impactEffect1;
    public GameObject impactEffect2;
    private Animator flipper;
    public bool flip;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right);

        if(hitInfo)
        {
            Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Instantiate(impactEffect2, hitInfo.point, Quaternion.identity);
            }
            else
            {
                Instantiate(impactEffect1, hitInfo.point, Quaternion.identity);
                
            }
        }
    }

    
}
