using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Wait for me, I don't want to let you down
//love you into disease, but no medicine can.
//Created By HeXiaoTao
public class RacketController : MonoBehaviour
{
    private Animation animation;

    private void Start()
    {
        animation = GetComponent<Animation>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!Config.IsPause)
        {
            UseKeyboard();
            UseAndroid();
        }
       
    }
    /// <summary>
    /// 在PC上操作
    /// </summary>
    private void UseKeyboard()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (this.transform.localPosition.x > -277)
                this.transform.localPosition += new Vector3(-20, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (this.transform.localPosition.x < 277)
                this.transform.localPosition += new Vector3(20, 0, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {

            this.transform.localRotation = Quaternion.Lerp(this.transform.localRotation, Quaternion.Euler(0, 0, 20), Time.deltaTime * 2);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {

            this.transform.localRotation = Quaternion.Lerp(this.transform.localRotation, Quaternion.Euler(0, 0, -20), Time.deltaTime * 2);
        }
    }
    #region  判断按钮是否长按
    private bool UpPress = false;
    private bool DownPress = false;
    private bool LeftPress = false;
    private bool RightPress = false;
    public void UpButtonLongPress(bool isPress)
    {
        UpPress = isPress;
    }
    public void DownButtonLongPress(bool isPress)
    {
        DownPress = isPress;
    }
    public void LeftButtonLongPress(bool isPress)
    {
        LeftPress = isPress;
    }
    public void RightButtonLongPress(bool isPress)
    {
        RightPress = isPress;
    }
    #endregion
    /// <summary>
    /// 在Android上操作
    /// </summary>
    private void UseAndroid()
    {
        if (LeftPress)
        {
            if (this.transform.localPosition.x > -277)
                this.transform.localPosition += new Vector3(-20, 0, 0);
        }
        if (RightPress)
        {
            if (this.transform.localPosition.x < 277)
                this.transform.localPosition += new Vector3(20, 0, 0);
        }
        if (UpPress)
        {

            this.transform.localRotation = Quaternion.Lerp(this.transform.localRotation, Quaternion.Euler(0, 0, -20), Time.deltaTime * 2);
        }
        if (DownPress)
        {

            this.transform.localRotation = Quaternion.Lerp(this.transform.localRotation, Quaternion.Euler(0, 0, 20), Time.deltaTime * 2);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag.Equals("Ball"))
        {
            if (animation != null)
                animation.Play();
        }
    }

}
