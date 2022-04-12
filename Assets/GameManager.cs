using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    //Variables pour le score
    int multiplier = 0;
    int streak = 0;

    //Variables pour les "règles" du jeu
    public List<Image> HPs = new List<Image>();
    public bool isGameOver = false; //marche pas comme prévu

    //Variables pour la génération des cupcakes
    public GameObject cupcake;
    public List<Transform> spawners;
    System.Random rnd = new System.Random();
    public float Timer = 0.5f;
    int spawnerID;

    public Animator animator;

    // Use this for initialization
    void Start () {
        PlayerPrefs.SetInt("isGameOver", 0);
        PlayerPrefs.SetInt("HP", 3);
        PlayerPrefs.SetInt("score", 0);
        PlayerPrefs.SetInt("HighestStreak", 0);
        PlayerPrefs.SetInt("Streak", 0);

        foreach (Image image in HPs)
        {
            image.enabled = false;
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerPrefs.GetInt("HP") > 0)
        {
            minusHealth();
            collision.gameObject.GetComponent<AudioSource>().Play();
        }
        else
        {
            isGameOver = true;
            gameOver();
        }
        Destroy(collision.gameObject,0.5f);
    }

    public void AddStreak()
    {
        streak++;
        multiplier = streak * 10;
        UpdateGUI();
        if (streak > PlayerPrefs.GetInt("HighestStreak"))
        {
            PlayerPrefs.SetInt("HighestStreak", streak);
        }
    }

    public void resetStreak()
    {
        streak = 0;
        multiplier = 0;
        UpdateGUI();
        
    }

    public int GetScore()
    {
        return 100 + multiplier;
    }

    void UpdateGUI()
    {
        PlayerPrefs.SetInt("Streak", streak);
        PlayerPrefs.SetInt("scoreAdded", 100+multiplier);
    }

    void minusHealth()
    {
        PlayerPrefs.SetInt("HP", PlayerPrefs.GetInt("HP")-1);
        HPs[PlayerPrefs.GetInt("HP")].enabled = true;
        
    }

    void gameOver()
    {
        
        if (PlayerPrefs.GetInt("HighestScore") < PlayerPrefs.GetInt("score"))   
        {
            PlayerPrefs.SetInt("HighestScore", PlayerPrefs.GetInt("score"));
        }
        animator.SetTrigger("FadeOut");
        //SceneManager.LoadScene(1);
    }

    
}
