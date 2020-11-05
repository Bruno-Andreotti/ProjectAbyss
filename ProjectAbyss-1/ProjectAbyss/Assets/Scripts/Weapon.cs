using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public int damage = 50;
    public GameObject impactEffect1;
    public GameObject impactEffect2;

    public CharacterController2D player;
    private SpriteRenderer hitAnim;
    private List<GameObject> impacteffects = new List<GameObject>();

    private void Start()
    {     
        impacteffects.Add(impactEffect1); //adiciona os prefabs à lista de hit effects (Especifico para a arma)
        impacteffects.Add(impactEffect2);
        hitAnim = null;
    }

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

        if(player.m_FacingRight)
        {
            for(int i = 0; i < impacteffects.Count; i++)
            {
                hitAnim = impacteffects[i].GetComponent<SpriteRenderer>();
                hitAnim.flipX = false;
            }
        }
        else
        {
            for (int i = 0; i < impacteffects.Count; i++)
            {
                hitAnim = impacteffects[i].GetComponent<SpriteRenderer>();
                hitAnim.flipX = true;
            }
        }

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
