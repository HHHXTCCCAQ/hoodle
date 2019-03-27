using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Wait for me, I don't want to let you down
//love you into disease, but no medicine can.
//Created By HeXiaoTao
public class PlayAnimation : MonoBehaviour
{

    // Use this for initialization
    private Animation animation;
    public ParticleSystem particleSystemprefab;
    private ParticleSystem particleSystem;
    void Start()
    {
        animation = this.GetComponent<Animation>();

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag.Equals("Ball"))
        {
            if (particleSystemprefab != null)
            {
                particleSystem = Instantiate(particleSystemprefab, collision.transform.position, Quaternion.Euler(new Vector3(90,0,0)));
                particleSystem.Play();
            }
            if (animation != null)
                animation.Play();
        }
        Destroy(particleSystem, 0.5f);
    }


}
