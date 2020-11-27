using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public int damage = 50;
    public GameObject impactEffect1;
    public GameObject impactEffect2;
    public GameObject muzzleFlash;
    public Animator anim;
    public CharacterController2D player;
    private GameObject playerobj;
    private SpriteRenderer hitAnim;
    private List<GameObject> impacteffects = new List<GameObject>();
    

    private void Start()
    {     
        impacteffects.Add(impactEffect1); //adiciona os prefabs à lista de hit effects (Especifico para a arma)
        impacteffects.Add(impactEffect2);
        playerobj = GameObject.FindGameObjectWithTag("Player1");
        hitAnim = null;
    }

    private Animator flipper;
    public bool flip;


    void Update()
    {
        anim.SetBool("IsShooting", false);

        if (GetComponent<CharacterController2D>().m_wasCrouching == false)
        {
            if (Input.GetKeyDown("e"))
            {
                Shoot();
                anim.SetBool("IsShooting", true);
            }
        }
        
    }

    void Shoot()
    {
        playerobj.GetComponent<PlayerMovement>().runSpeed = 0; // Zera o movimento do jogador no momento que ele pressiona o botão de tiro, só não vai dar bom se forem usadas outras armas que talvez pudessem permitir movimento...
        Invoke("Recoil", 0.35f);
        Invoke("Flash", 0.15f);

        FindObjectOfType<AudioManager>().Play("TiroPlayer");
        Debug.Log("Som de tiro");

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
            BossBehaviour boss = hitInfo.transform.GetComponent<BossBehaviour>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Instantiate(impactEffect2, hitInfo.point, Quaternion.identity);
            }
            else if (boss != null)
            {
                boss.TakeDamage(damage);
                Instantiate(impactEffect2, hitInfo.point, Quaternion.identity);
            }
            else
            {
                Instantiate(impactEffect1, hitInfo.point, Quaternion.identity);
                
            }
        }
    }

    void Flash()
    {
        Instantiate(muzzleFlash, firePoint.position, firePoint.rotation);
    }
 
    void Recoil() //Só serve pra ser Invokado depois de zerar o movimento do personagem
    {
        playerobj.GetComponent<PlayerMovement>().runSpeed = 15;
    }
}
