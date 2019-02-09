using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour {

    public int kecepatanBola;
    Rigidbody2D rigid;

    int scoreP1;
    int scoreP2;

    Text scoreUIP1;
    Text scoreUIP2;

    GameObject panelSelesai;
    Text txtPemenang;

    GameObject panelPaused;
    GameObject btnPaused;
    public bool paused;

    GameObject player1;
    GameObject player2;

    AudioSource audioPlayer;
    public AudioClip suaraBola;
    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        Vector2 arah = new Vector2(-2, 0).normalized;
        rigid.AddForce(arah * kecepatanBola);

        scoreP1 = 0;
        scoreP2 = 0;

        scoreUIP1 = GameObject.Find("ScoreP1").GetComponent<Text>();
        scoreUIP2 = GameObject.Find("ScoreP2").GetComponent<Text>();

        panelSelesai = GameObject.Find("PanelSelesai");
        panelSelesai.SetActive(false);

        panelPaused = GameObject.Find("PanelPaused");
        panelPaused.SetActive(false);

        btnPaused = GameObject.Find("BtnPause");
        btnPaused.SetActive(true);

        player1 = GameObject.Find("Player1");
        player1.SetActive(true);

        player2 = GameObject.Find("Player2");
        player2.SetActive(true);

        audioPlayer = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {

    }
    // Reset Bola
    void ResetBall()
    {
        transform.localPosition = new Vector2(0, 0);
        rigid.velocity = new Vector2(0, 0);
    }
    // Tampil Score
    void TampilkanScore()
    {
        Debug.Log("Score P1: " + scoreP1 + " Score P2: " + scoreP2);
        scoreUIP1.text = scoreP1 + "";
        scoreUIP2.text = scoreP2 + "";
    }

    //Button Game Paused
    public void GamePaused()
    {
        paused = !paused;
        if (paused)
        {
            Time.timeScale = 0;
            panelPaused.SetActive(true);
            btnPaused.SetActive(false);
        }else if (!paused)
        {
            Time.timeScale = 1;
            panelPaused.SetActive(false);
            btnPaused.SetActive(true);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioPlayer.PlayOneShot(suaraBola);
        if(collision.gameObject.name == "TepiKanan")
        {
            scoreP1 += 1;
            TampilkanScore();
            ResetBall();
            Vector2 arah = new Vector2(2, 0).normalized;
            rigid.AddForce(arah * kecepatanBola);
        }
        if(collision.gameObject.name == "TepiKiri")
        {
            scoreP2 += 1;
            TampilkanScore();
            ResetBall();
            Vector2 arah = new Vector2(-2, 0).normalized;
            rigid.AddForce(arah * kecepatanBola);
        }

        if (collision.gameObject.name == "Player1" || collision.gameObject.name == "Player2")
        {
            float sudut = (transform.position.y - collision.transform.position.y) * 3f;
            Vector2 arah = new Vector2(rigid.velocity.x, sudut).normalized;
            rigid.velocity = new Vector2(0, 0);
            rigid.AddForce(arah * kecepatanBola * 2);
        }

        if(scoreP1 == 5)
        {
            panelSelesai.SetActive(true);
            btnPaused.SetActive(false);
            txtPemenang = GameObject.Find("Pemenang").GetComponent<Text>();
            txtPemenang.text = "Player 1 \n Win!";
            player1.SetActive(false);
            player2.SetActive(false);
            Destroy(gameObject);
            return;
        }
        if(scoreP2 == 5)
        {
            panelSelesai.SetActive(true);
            btnPaused.SetActive(false);
            txtPemenang = GameObject.Find("Pemenang").GetComponent<Text>();
            txtPemenang.text = "Player 2 \n Win!";
            player1.SetActive(false);
            player2.SetActive(false);
            Destroy(gameObject);
            return;
        }
    }
}
