using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour {

    SpriteRenderer sr;
    public KeyCode key;
    bool active = false;
    GameObject note;
    GameObject gameManager;
    Color old;
    

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start () {
        old = sr.color;
        gameManager = GameObject.Find("GameManager");
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(key))
        {
            StartCoroutine(Pressed());
        }

        if (Input.GetKeyDown(key) && active && gameManager.GetComponent<GameManager>().isGameOver == false)
        {
            Destroy(note);
            AddScore();
            gameObject.GetComponent<AudioSource>().Play();
            gameManager.GetComponent<GameManager>().AddStreak();
        }
        else if(Input.GetKeyDown(key) && !active && gameManager.GetComponent<GameManager>().isGameOver == false)
        {
            NoNotePress();
            gameManager.GetComponent<GameManager>().resetStreak();
        }
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        active = true;
        if(collision.gameObject.tag =="Note")
        {
            note = collision.gameObject;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        active = false;

    }

    void AddScore(){
        PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + gameManager.GetComponent<GameManager>().GetScore());
        //Debug.Log(gameManager.GetComponent<GameManager>().GetScore());
    }

    void NoNotePress()
    {
        PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") - 20);
    }


    IEnumerator Pressed(){
        Color old = sr.color;
        sr.color = new Color(0,0,0);
        yield return new WaitForSeconds(0.05f);
        sr.color = old;
    }

}
