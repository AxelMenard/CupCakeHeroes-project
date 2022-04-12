using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Controller : MonoBehaviour {

    public GameObject cupcake;
    public List<Transform> spawners;
    System.Random rnd = new System.Random();
    public float Timer = 0.5f;
    int spawnerID;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Timer -= Time.deltaTime;
        if (Timer <= 0f)
        {
            spawnerID = rnd.Next(spawners.Capacity);
            Instantiate(cupcake, spawners[spawnerID]);
            //Instantiate(cupcake, spawners[0]);
            Timer = 0.5f;
        }
    }
}
