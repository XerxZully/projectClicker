using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = System.Numerics.Vector3;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform obj = null;
    [SerializeField] private Text scoreText = null;
    [SerializeField] private Text scoreAdd = null;
    [SerializeField] private Text gameOverText = null;
    [SerializeField] private Button repeatButton = null;
    [SerializeField] private int maxObjects = 5;
    [SerializeField] private float summonInterval = 1.0f;
    [SerializeField] private int maxHealth = 5;

    private bool ingame = true;
    private int objects = 0;
    private int score = 0;
    private int scoreView = 0;
    private int scoreAddView = 0;
    private float[] corners = { -7.0f, -4.0f, 7.0f, 4.0f }; //x_min, y_min, x_max, y_max

    public static GameManager Instanse { get; private set; } = null;
    
    private void Awake()
    {
        Instanse = this;
        
        init();
    }

    private void init()
    {
        obj.gameObject.GetComponent<Clicker>().init(maxHealth);
        repeatButton.onClick.AddListener( RepeatButtonOnClick );
        newGame();
    }

    private void newGame()
    {
        score = 0;
        scoreText.text = "Score: 0";
        objects = 0;
        ingame = true;
        gameOverText.enabled = !ingame;
        repeatButton.gameObject.SetActive(!ingame);
        
        GameObject[] list = GameObject.FindGameObjectsWithTag("Clickable");
        foreach ( GameObject item in list)
            item.gameObject.GetComponent<Clicker>().kill();

        InvokeRepeating("SummonCube", 1.0f, summonInterval );
    }

    private void endGame()
    {
        ingame = false;
        gameOverText.enabled = !ingame;
        repeatButton.gameObject.SetActive(!ingame);
        CancelInvoke("SummonCube");
    }
    
    public void RepeatButtonOnClick()
    {
        //newGame();
        SceneManager.LoadScene("StartMenu");
    }
    
    private void Update()
    {
        if ( ingame && Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if ( Physics.Raycast(ray, out hit) ) {
                if ( hit.transform.tag == "Clickable") {
                    if (hit.collider.gameObject.GetComponent<Clicker>().changeHealth(-1))
                        objects--;
                    addScore(10);
                }
            }
        }
    }

    public void addScore(int value)
    {
        score += value;
        scoreAddView += value;
        //scoreText.text = "Score: " + score.ToString();
        StartCoroutine(ScoreAnimation());
    }

    private IEnumerator ScoreAnimation()
    {
        while (scoreAddView >= 0)
        {
            scoreAdd.text = "+" + scoreAddView.ToString();
            scoreText.text = "Score: " + scoreView.ToString();
            scoreView++;
            scoreAddView--;
            //yield return null;
            yield return new WaitForSeconds(0.1f);
        }

        yield return null;
        scoreAdd.text = "";
    }

    private void SummonCube()
    {
        Transform inst;
    
        inst = Instantiate( obj );
        inst.position = new UnityEngine.Vector3( Random.Range(corners[0],corners[2]), Random.Range(corners[1],corners[3]), 0 );
        objects++;
        if (objects >= maxObjects)
            endGame();
    }
}
