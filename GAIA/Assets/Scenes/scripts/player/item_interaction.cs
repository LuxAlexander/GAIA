using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class item_interaction : MonoBehaviour
{
    private GameObject[] food;
    void Start()
    {
        food = GameObject.FindGameObjectsWithTag("Food");
    }

    void Update()
    {
        for (int i = 0; i < food.Length; i++)
        {
            if(food[i] != null)
            {
                if (this.GetComponent<BoxCollider2D>().IsTouching(food[i].GetComponent<CircleCollider2D>()))
                {
                    if (Input.GetKeyDown("space"))
                    {
                        food[i].SetActive(false);
                    }
                }
            }
        }
    }
}
