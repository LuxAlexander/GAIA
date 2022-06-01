using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.SceneManagement;

public class movement : MonoBehaviour
{

	public float speed;
	public HealthBar healthBar;
	public int health;

	private Rigidbody2D rb;
	private Vector2 move;
	private Animator animator;
	private int currentHealth;
	/*private ParticleSystem hit;
	private ParticleSystem heal;
	private ParticleSystem death;*/
	public AudioSource save;
	
	void Start()
	{
		//componenetn reinholen vom character
		rb = GetComponent<Rigidbody2D>();

		rb.constraints = RigidbodyConstraints2D.FreezeRotation;

		animator = GetComponent<Animator>();

		/*currentHealth = health;
		healthBar.setMaxHealth(health);*/
	}

	void Update()
	{

		if (PlayerPrefs.GetInt("Back") == 1)
		{
			continuegame();
			PlayerPrefs.SetInt("Back", 2);
		}
		if (PlayerPrefs.GetInt("Back") == 3)
		{
			savePlayer();
			PlayerPrefs.SetInt("Back", 2);
		}
		/*
		if (currentHealth <= 0)
		{
			loadPlayer();
			currentHealth = health;
		}*/

		//reinholen der Inputs durch die achsen
		move.x = Input.GetAxisRaw("Horizontal");
		move.y = Input.GetAxisRaw("Vertical");

		//überprüfen ob man sich diagonal bewegt -> sonst bewegt sich der player zu schnell diagonal
		if (Mathf.Abs(move.x) == 1 && Mathf.Abs(move.y) == 1) move = move / 1.5f;

		//animator variablen setzen
		animator.SetFloat("Horizontal", move.x);
		animator.SetFloat("Vertical", move.y);
		animator.SetFloat("Speed", move.sqrMagnitude);

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			savecurrentprogress();
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
		if (Input.GetKey("l")) {
			loadPlayer();
		}
		if (Input.GetKey("o"))
		{
			savePlayer();
		}
	}

	void FixedUpdate()
	{
		//rigidbody bewegen
		rb.MovePosition(rb.position + move * speed * Time.fixedDeltaTime);
	}

	public void takeDamage(int damage)
	{
		currentHealth -= damage;
		healthBar.setHealth(currentHealth);
		//hit.Play();
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

	public void HealPlayer()
	{
		currentHealth = health;
		healthBar.setHealth(currentHealth);
		//heal.Play();
	}

	public int getHealth()
	{
		return currentHealth;
	}

	public void savePlayer()
	{
		if (!save.isPlaying)
		{
			save.Play();
		}
		SaveSystem.savePlayer(this);
	}
	public void savecurrentprogress()
	{
		SaveSystem.savecurrent(this);
	}
	public void continuegame()
	{
		PlayerData data = SaveSystem.currentgame();
		currentHealth = data.health;

		Vector3 position;
		position.x = data.position[0];
		position.y = data.position[1];
		position.z = data.position[2];
		transform.position = position;
	}
	public void loadPlayer()
    {
		PlayerData data = SaveSystem.LoadPlayer();
		currentHealth = data.health;

		Vector3 position;
		position.x = data.position[0];
		position.y = data.position[1];
		position.z = data.position[2];
		transform.position = position;
	}
}

