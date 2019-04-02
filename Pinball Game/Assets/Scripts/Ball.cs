using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Wait for me, I don't want to let you down
//love you into disease, but no medicine can.
//Created By HeXiaoTao
public class Ball : MonoBehaviour
{
    // Use this for initialization
    [SerializeField]
    private GameObject Gameover;
    private new Rigidbody2D rigidbody2D;

    [SerializeField]
    private Text scoreText;
    private int score = 0;
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.simulated = false;
    }
    void Update()
    {
        if (DelayStartGame._instance.startGame)
        {
            rigidbody2D.simulated = true;
            if (Mathf.Abs(transform.GetComponent<Rigidbody2D>().velocity.magnitude) > 280)
            {
                transform.GetComponent<Rigidbody2D>().velocity = transform.GetComponent<Rigidbody2D>().velocity / 1.1f;
            }
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name.Equals("GameOverLine"))
        {
            Config.IsGameOver = true;
            if (!Gameover.activeSelf)
                Gameover.SetActive(true);
        }     
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name.Equals("RightWall"))
        {
            collision.transform.GetComponent<Animation>().Play();
        }
        if (collision.transform.name.Equals("LeftWall"))
        {
            collision.transform.GetComponent<Animation>().Play();
        }
        if (collision.transform.tag.Equals("ItemWall"))
        {
            score++;
            scoreText.text = score.ToString();
        }
    }
}
