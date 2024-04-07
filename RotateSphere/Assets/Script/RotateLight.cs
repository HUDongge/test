using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLight : MonoBehaviour
{
    public Transform center; //圆盘中心点
    private float radius;     //圆盘半径

    private Vector3 screenSpace;
    private Vector3 offset;
    private Vector3 curScreenSpace;
    private Vector3 CurPosition;

    Camera mCamera;
    // Use this for initialization
    void Start()
    {
        mCamera = Camera.main;
        radius = (transform.position - center.position).magnitude;
        Debug.Log("first" + radius);
        StartCoroutine(OnMouseDown());

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnMouseEnter()
    {
        Debug.Log("OnMouseEnter");
    }
    void OnMouseExit()
    {
        Debug.Log("OnMouseExit");
    }
    private void OnMouseUp()
    {
        Debug.Log("OnMouseUp");
    }

    IEnumerator OnMouseDown()
    {
        Debug.Log("OnMouseDown");
        if (mCamera)
        {
            //转换对象到当前屏幕位置
            Vector3 screenPosition = mCamera.WorldToScreenPoint(transform.position);

            //鼠标屏幕坐标
            Vector3 mScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z);
            //获得鼠标和对象之间的偏移量
            Vector3 offset = transform.position - mCamera.ScreenToWorldPoint(mScreenPosition);
            //print ("drag starting:"+transform.name);

            //若鼠标左键一直按着则循环继续
            while (Input.GetMouseButton(0))
            {

                //鼠标屏幕上新位置
                mScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z);
                //judge是鼠标和屏幕位置和盘子中心的位置之间的距离
                float judge = (mCamera.ScreenToWorldPoint(mScreenPosition) - center.position).magnitude;
                Debug.Log(radius);
                Debug.Log("judge" + judge);
                //判断该距离是否在半径的一定范围内，这里设置的范围是0.1f，如果在的话就更新对象坐标
                if (judge >= radius-0.1f && judge <= radius + 0.1f)
                {
                    // 对象新坐标
                    transform.position = offset + mCamera.ScreenToWorldPoint(mScreenPosition);
                }

                //协同，等待下一帧继续
                yield return new WaitForFixedUpdate();
            }

            Debug.Log("drag compeleted");

        }
    }
}