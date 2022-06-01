using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class movement_enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform player;
    public float speed;
    private Vector3 to_player;
    private Vector3 scale;
    public float aggro_range;
    public float min_distance;

    public int health;
    private int currentHealth;

    public HealthBar healthBar;

    public Animator animator;

    private ParticleSystem hit;

    public AudioSource deadsound;
    void Start()
    {
        //Slimes
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        scale = transform.localScale;
        //Leben des Slimes
        currentHealth = health;
        healthBar.setMaxHealth(health);
        //Particles
        hit = GetSystem("hit_slime");
    }

    void FixedUpdate()
    {
        //enemy zerstören wenn er besiegt worden ist
        if(currentHealth <= 0)
        {
            if (!deadsound.isPlaying)
            {
                deadsound.Play();
            }
            Destroy(gameObject);
        }

        //eingehen in den aggro bereich
        if (Mathf.Abs(rb.position.x - player.position.x) < aggro_range || Mathf.Abs(rb.position.y - player.position.y) < aggro_range)
        {
            to_player = (player.transform.position - transform.position).normalized;
            //eingehen in min. bereich
            if (Mathf.Abs(rb.position.x - player.position.x) < min_distance || Mathf.Abs(rb.position.y - player.position.y) < min_distance)
            {
                to_player = Vector2.zero;

            }
            //austreten aus min bereich
            if (Mathf.Abs(rb.position.x - player.position.x) > min_distance || Mathf.Abs(rb.position.y - player.position.y) > min_distance+0.5)
            {
                to_player = (player.transform.position - transform.position).normalized;
            }
        }
        //beim austreten aus dem aggro bereich wird der speed auf 0 gestellt (gegner bleibt stehen)
        if (Mathf.Abs(rb.position.x - player.position.x) > aggro_range || Mathf.Abs(rb.position.y - player.position.y) > aggro_range)
        {
            to_player = Vector2.zero;
        }
        //eigentliche bewegen des gegners
        rb.velocity = new Vector2(to_player.x, to_player.y) * speed;       
    }

    public void takeDamage(int damage)
    {
        //Reduzierung der Leben und particles
        currentHealth -= damage;
        healthBar.setHealth(currentHealth);
        hit.Play();
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
