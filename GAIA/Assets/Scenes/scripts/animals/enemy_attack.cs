using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_attack : MonoBehaviour
{
    public Transform hitBox;
    public LayerMask enemiesToHit;
    public float attackRange;
    public int damage;
    public bool isRock;

    private Collider2D[] enemiesInRange;

    public float delay;
    private float nextAttack;

    private Animator animator;
    private ParticleSystem jump;

    void Start()
    {
        //Wann er das nächste mal schlagen darf, nach jedem schlag
        nextAttack = delay;
        animator = GetComponent<Animator>();
        //Animationen der gegner
        if (isRock) animator.speed = delay - 0.275f * 3;
        //ist es ein Rock-Slime
        jump = GetSystem("jump_slime");
        //Particles zum Springen
    }

    void Update()
    {
        //Darf er Schlagen Nein ->Zeit abziehen Ja->Schlagen
        if (nextAttack > 0)
        {
            nextAttack -= Time.deltaTime;
        }else {
            enemiesInRange = Physics2D.OverlapCircleAll(hitBox.position, attackRange, enemiesToHit);
            //Ist der Hauptcharackter in reichweite 
            if (enemiesInRange.Length > 0)
            {
                enemiesInRange[0].GetComponent<movement>().takeDamage(damage);
                Debug.Log("Doing damage to player!");

            }
            jump.Play();
            //Particles
            nextAttack = delay;
            //Wartezeit bis zur nächsten Attacke
        }
    }

    private void OnDrawGizmosSelected()
    {
        //gizmos um das leere objekt in eine sphere zu machen
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitBox.position, attackRange);
    }

    private ParticleSystem GetSystem(string systemName)
    {
        Component[] children = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem childParticleSystem in children)
        {
            if (childParticleSystem.name == systemName)
            {
                return childParticleSystem;
            }
        }
        return null;
    }
}
