using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject Board;

    int state = 1;
    void Start()
    {
        Board = GameObject.Find("Board");
        CheckPosition();
    }

    Vector3 CameraPosition;
    Vector3 CameraRotation;
    void Update()
    {
        
       // print(state);
        //print(transform.position.x);
        
    }

    void CheckPosition()
    {

        switch (state)
        {
            case 1:
                CameraPosition = new Vector3(-2.45f, 7.34f, -2.45f);
                CameraRotation = new Vector3(45f, 45f, 0f);

                break;
            case 4:
                CameraPosition = new Vector3(-0.92f, 5.96f, 6.23f);
                CameraRotation = new Vector3(45f, 135f, 0f);
               
                break;
            case 3:
                CameraPosition = new Vector3(6.37f, 5.45f, 6.23f);
                CameraRotation = new Vector3(45f, 225f, 0f);
               
                break;
            case 2:
                CameraPosition = new Vector3(6.37f, 5.45f, -1.44f);
                CameraRotation = new Vector3(45f, 315f, 0f);
               
                break;
        }


    }


    public void ClickLeftButton()
    {
        state--;
        checkState();
        CheckPosition();
        transform.position = CameraPosition;
        transform.rotation = Quaternion.Euler(CameraRotation);
    }

    public void ClickRightButton()
    {
        state++;
        checkState();
        CheckPosition();
        transform.position = CameraPosition;
        transform.rotation = Quaternion.Euler(CameraRotation);
    }

     void checkState()
    {
        if (state == 0)
        {
            state = 4;
        }
        if (state == 5)
        {
            state = 1;
        }
    }

}
