using UnityEngine;
using System.Collections;
using System.Diagnostics;
using UnityEngine.Networking;
using Debug = UnityEngine.Debug;

public enum Type { Snake, Otter, Wolf, Sheep}

public class Animal : MonoBehaviour {

	// Base attributes of Animals
    protected int ID;
    public Type type;
    //public ParticleSystem particles;
	public float age;
	public const float maxhealth = 100;
	[Range(1, maxhealth)] public float health;
    [Range(1, 100)] public float stamina;

    protected bool breeding;

    protected float periode =0.0f;
    private GameObject[] food;

    // Use this for initialization
    public void Start() {
        if(GetInstanceID()>=0){
            ID = GetInstanceID();
        }else ID = GetInstanceID() *(-1);

        breeding = false;
        periode = UnityEngine.Time.deltaTime;
        food = GameObject.FindGameObjectsWithTag("Food");
    }
	
	// Update is called once per frame
	public void FixedUpdate ()
    {
        eat();

		if (periode > 10f)//Wenn die periode Überschrieten wird ca.20 sekunden, erhöht sich das alter
        {
			//Tier wird älter
			age++;
            stamina -= 1;
            if(stamina <= 0)//Wenn das Tier keine Ausdauer mehr hat stirbt es.
            {
                Destroy(gameObject);
            }
            //Alle 10 Nachkommen zeugen
            if (age % 3 == 0)
            {
                breeding = true;
            }
			if (health <= 0) Destroy(gameObject);//Wenn das Tier keine HP mehr hat stirbt es.

			periode = 0;
		}
        periode += UnityEngine.Time.deltaTime;
    }

    public virtual void eat()
    {
        for (int i = 0; i < food.Length; i++)
        {
            if(food[i] != null)
            {
            	if (this.GetComponent<BoxCollider2D>().IsTouching(food[i].GetComponent<CircleCollider2D>()))
            	{
                    getHeal();
					food[i].SetActive(false);
            	}
        	}
		}
    }

    public virtual void getHeal()
    {
        if ((health + 10 ) <= maxhealth)
        {
            health += 10;
        }
    }

    public virtual void getdamage(int healthLost)
    {
        health -= healthLost;
    }
}
