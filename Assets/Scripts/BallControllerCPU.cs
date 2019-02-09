using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallControllerCPU : MonoBehaviour {
    public int kecepatanBola;
    Rigidbody2D rigid;

    int scoreP1;
    int scoreCPU;
    Text scoreUIP1;
    Text scoreUICPU;

    GameObject panelSelesai;
    Text txtPemenang;

    GameObject panelPaused;
    GameObject btnPaused;
    public bool paused;

    GameObject player1;
    GameObject cpu;

    AudioSource audioCpu;
    public AudioClip suaraBola;
    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody2D>();
        Vector2 arah = new Vector2(-2, 0).normalized;
        rigid.AddForce(arah * kecepatanBola);

        scoreP1 = 0;
        scoreCPU = 0;

        scoreUIP1 = GameObject.Find("ScoreP1").GetComponent<Text>();
        scoreUICPU = GameObject.Find("ScoreCPU").GetComponent<Text>();

        panelSelesai = GameObject.Find("PanelSelesai");
        panelSelesai.SetActive(false);

        panelPaused = GameObject.Find("PanelPaused");
        panelPaused.SetActive(false);

        btnPaused = GameObject.Find("BtnPause");
        btnPaused.SetActive(true);

        player1 = GameObject.Find("Player1");
        player1.SetActive(true);

        cpu = GameObject.Find("CPU");
        cpu.SetActive(true);

        audioCpu = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    //Reset Ball
    void ResetBall()
    {
        transform.localPosition = new Vector2(0, 0);
        rigid.velocity = new Vector2(0, 0);
    }

    //Tampil Score Bola
    void TampilkanScore()
    {
        Debug.Log("Score P1: " + scoreP1 + " Score CPU: " + scoreCPU);
        scoreUIP1.text = scoreP1 + "";
        scoreUICPU.text = scoreCPU + "";
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
        }
        else if (!paused)
        {
            Time.timeScale = 1;
            panelPaused.SetActive(false);
            btnPaused.SetActive(true);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioCpu.PlayOneShot(suaraBola);
        if (collision.gameObject.name == "TepiKanan")
        {
            scoreP1 += 1;
            TampilkanScore();
            ResetBall();
            Vector2 arah = new Vector2(2, 0).normalized;
            rigid.AddForce(arah * kecepatanBola);
        }
        if (collision.gameObject.name == "TepiKiri")
        {
            scoreCPU += 1;
            TampilkanScore();
            ResetBall();
            Vector2 arah = new Vector2(-2, 0).normalized;
            rigid.AddForce(arah * kecepatanBola);
        }

        if (collision.gameObject.name == "Player1" || collision.gameObject.name == "CPU")
        {
            float sudut = (transform.position.y - collision.transform.position.y) * 3f;
            Vector2 arah = new Vector2(rigid.velocity.x, sudut).normalized;
            rigid.velocity = new Vector2(0, 0);
            rigid.AddForce(arah * kecepatanBola * 2);
        }

        if (scoreP1 == 5)
        {
            panelSelesai.SetActive(true);
            btnPaused.SetActive(false);
            txtPemenang = GameObject.Find("Pemenang").GetComponent<Text>();
            txtPemenang.text = "Player 1 \n Win!";
            Destroy(gameObject);
            cpu.SetActive(false);
            player1.SetActive(false);
            return;
        }
        if (scoreCPU == 5)
        {
            panelSelesai.SetActive(true);
            btnPaused.SetActive(false);
            txtPemenang = GameObject.Find("Pemenang").GetComponent<Text>();
            txtPemenang.text = "CPU\n Win!";
            cpu.SetActive(false);
            player1.SetActive(false);
            Destroy(gameObject);
            return;
        }
    }
}
