using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public int atkDamage; //sugiro também colocar já editável no inspetor e alterar no prefab os danos que cada inimigo deve causar. Seria interessante setar 2 valores diferentes como "Range" de dano, eventualmente 
    public float atkCD;   //Tempo de "recarga" (em segundos) entre ataques de cada inimigo
    public GameObject deathEffect;
    public GameObject impactEffect2; //por enquanto usando o mesmo que o da classe Weapon
    public Transform shootPoint;
    public Animator enemyAnim; //Tentar utilizar este animador para movimentos comuns entre inimigos, como idle ou ataques
    
   
    
    public GameObject mFlash;

    private bool emRecarga = false;  //Booleana a ser utilizada em conjuto com o atkCD para dividir ataques


    public void TakeDamage (int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Attack() //chamar este método a partir do evento que causar o ataque, provavelmente vindo do chase ou patrol
    {
        if(!emRecarga)
        { 
          if(shootPoint!=null && this.gameObject.name == "EnemyRail")
          {
              //código para atiradores, ativa a animação de tiro e checa se acerta um characterController2D com player
              enemyAnim.SetTrigger("Shoot");
                Invoke("EnemyFlash", 0.15f);

                FindObjectOfType<AudioManager>().Play("TiroCult");

              RaycastHit2D hitInfo = Physics2D.Raycast(shootPoint.position, shootPoint.right);
                      
              if (hitInfo)
              {
         
                  CharacterController2D player = hitInfo.transform.GetComponent<CharacterController2D>();
                  if (player != null)
                  {
                      player.TakeDamage(atkDamage);
                      Instantiate(impactEffect2, hitInfo.point, Quaternion.identity);
                  }
                 
              }         
          }
          else
          {
            //código de atacantes de perto

            if(this.gameObject.name == "EnemyChase")
                {
                   enemyAnim.SetTrigger("IsStabbing");
                   Invoke("DelayedDamage", 0.2f);
                   
                   Debug.Log("Hit Player");
                }
          }

            emRecarga = true;
            Invoke("Recarregar", atkCD);

        }
    }

    private void Recarregar() // método a ser invocado para recarregar o ataque;
    {
        emRecarga = false;
    }

    public void Die()
    {
        Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    void EnemyFlash()
    {
        Instantiate(mFlash, shootPoint.position, shootPoint.rotation);
        
    }

    void DelayedDamage()
    {
        GameObject.FindGameObjectWithTag("Player1").GetComponent<CharacterController2D>().TakeDamage(atkDamage);
    }
    


}
