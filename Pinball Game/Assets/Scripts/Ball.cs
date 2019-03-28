using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Wait for me, I don't want to let you down
//love you into disease, but no medicine can.
//Created By HeXiaoTao
public class Ball : MonoBehaviour
{
    // Use this for initialization
    [SerializeField]
    private GameObject Gameover;
    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.GetComponent<Rigidbody2D>().velocity.magnitude) > 280)
        {
            transform.GetComponent<Rigidbody2D>().velocity = transform.GetComponent<Rigidbody2D>().velocity / 1.1f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name.Equals("GameOverLine"))
        {
            if (!Gameover.activeSelf)
                Gameover.SetActive(true);
        }

    }
}
