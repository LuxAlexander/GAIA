using System;
using UnityEngine;
using System.Collections;
using System.Text;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Omni : Animal{
    public float speed;

    private Rigidbody2D rgbdy2D;
    private Animator animator;
    public GameObject kid;
    private GameObject[] mate;

    private Vector2 movement;
    
    private float period =0.0f;
    // Use this for initialization
    private void Awake()
    {
        rgbdy2D = GetComponent<Rigidbody2D>();
        rgbdy2D.gravityScale = 0;
        animator = GetComponent<Animator>();
        movement = new Vector2();
    }

    void Start ()
    {
        base.Start();
        
        StartCoroutine(saveAnimal());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        base.FixedUpdate();
        mate = GameObject.FindGameObjectsWithTag(type.ToString());
        recreate();
        if (period > 3f) //Wenn die periode Überschrieten wird ca.20 sekunden, erhöht sich das alter
        {
            movement.x = Random.Range(-1f, 1f);
            movement.y = Random.Range(-1f, 1f);

            //überprüfen ob man sich diagonal bewegt -> sonst bewegt sich der player zu schnell diagonal
            if (Equals(Mathf.Abs(movement.x), 1f) && Equals(Mathf.Abs(movement.y), 1f)) movement = movement / 1.5f;

            //animator variablen setzen
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);

            rgbdy2D.velocity = new Vector2(movement.x * speed, movement.y * speed);
            period = 0;
        }
        period += UnityEngine.Time.deltaTime;
    }

    public void recreate()
    {
        if (breeding == true){
            for (int i = 0; i < mate.Length; i++)
            {
                if(mate[i] != null&& mate.Length<3901)
                {
                    if (this.GetComponent<BoxCollider2D>().IsTouching(mate[i].GetComponent<BoxCollider2D>()))
                    {
                        Debug.Log("Make love.");
                        breeding = false;
                        Instantiate(kid, this.transform.position, Quaternion.identity);
                        //particles.Play();
		       
                    }
                }
            }
        }
        //particles.Stop();
    }
    IEnumerator saveAnimal()
    {
        string saveanimal = "http://localhost/api/saveAnimal";
        string jsonanimal = JsonUtility.ToJson(this);
        Debug.Log(jsonanimal);
        Debug.Log(this.ID.ToString()+";"+this.type.ToString());
        UnityWebRequest saveanimalRequest = UnityWebRequest.Post(saveanimal, this.ID.ToString()+";"+this.type.ToString());

        yield return saveanimalRequest.SendWebRequest();

        Debug.Log("Result: " + saveanimalRequest.result);
        Debug.Log("ResponseCode: " + saveanimalRequest.responseCode);

        if (saveanimalRequest.result == UnityWebRequest.Result.ConnectionError) { 
            Debug.LogError(saveanimalRequest.error); 
            yield break;
        }
    }
}
