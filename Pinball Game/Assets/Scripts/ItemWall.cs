using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Wait for me, I don't want to let you down
//love you into disease, but no medicine can.
//Created By HeXiaoTao
public class ItemWall : MonoBehaviour
{

    // Use this for initialization
    private ChangeColor changeColor;
    private new Animation animation;
    private new ParticleSystem particleSystem;
    [SerializeField]
    private int blood = 2;
    void Start()
    {
        animation = GetComponent<Animation>();
        changeColor = GetComponent<ChangeColor>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag.Equals("Ball"))
        {
            blood--;
            particleSystem = ObjPool._instance.GetObj(Config.BoomParticle1).GetComponent<ParticleSystem>();
            particleSystem.transform.position = collision.transform.position;           
            particleSystem.Play();
            StartCoroutine(RecycleObj(particleSystem.gameObject));
            if (changeColor != null)
                changeColor.ChangeItemColor();
            if (animation != null)
                animation.Play();
            if (blood<=0)
                Destroy(this.gameObject);
        }
    }
    private IEnumerator RecycleObj(GameObject obj)
    {
        yield return new WaitForSeconds(0.5f);
        ObjPool._instance.RecycleObj(obj);
    }

}
