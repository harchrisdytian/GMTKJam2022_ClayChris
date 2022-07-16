using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public int hp = 5;
    public static  Enums.Sides[] currentOrientation = new Enums.Sides[6] {Enums.Sides.Up, Enums.Sides.Front,Enums.Sides.Down, Enums.Sides.Back, Enums.Sides.Left,Enums.Sides.Right};
    [SerializeField]
    public Enums.Sides side;
    private Quaternion nextRotation;
    
    public 


    void SetSide(Enums.Sides CurrentSide)
    {
        switch (CurrentSide)
        {
            case Enums.Sides.Up:
                gameObject.transform.rotation = Quaternion.identity;
                break;
            case Enums.Sides.Down:
                gameObject.transform.localRotation = gameObject.transform.localRotation * Quaternion.Euler(180, 0, 0);
                break;
            case Enums.Sides.Left:
                gameObject.transform.localRotation = gameObject.transform.localRotation * Quaternion.Euler(0, 0, 90);
                break;
            case Enums.Sides.Right:
                gameObject.transform.localRotation = gameObject.transform.localRotation * Quaternion.Euler(0, 0, -90);
                break;
            case Enums.Sides.Front:
                gameObject.transform.localRotation = gameObject.transform.localRotation * Quaternion.Euler(90, 0, 0);
                break;
            case Enums.Sides.Back:
                gameObject.transform.localRotation = gameObject.transform.localRotation * Quaternion.Euler(-90, 0, 0);
                break;
        }
        side = CurrentSide;
    }

    void RotateBack()
    {
        gameObject.transform.position -= Vector3.forward;
        transform.Rotate(-90, 0, 0,Space.Self);
        Enums.Sides tempSide;

        tempSide = currentOrientation[0];
        currentOrientation[0] = currentOrientation[3];
        currentOrientation[3] = currentOrientation[2];
        currentOrientation[2] = currentOrientation[1];
        currentOrientation[1] = tempSide;
        side = currentOrientation[0];

    }
    void RotateForward()
    {
        gameObject.transform.position += Vector3.forward;
        transform.Rotate(90, 0, 0, Space.Self);
        Enums.Sides tempSide;

        tempSide = currentOrientation[0];
        currentOrientation[0] = currentOrientation[1];
        currentOrientation[1] = currentOrientation[2];
        currentOrientation[2] = currentOrientation[3];
        currentOrientation[3] = tempSide;
        side = currentOrientation[0];

    }
    void RotateLeft()
    {
        gameObject.transform.position += Vector3.left;
        gameObject.transform.Rotate(0, 0, 90,Space.World);
        Enums.Sides tempSide;

        tempSide = currentOrientation[0];
        currentOrientation[0] = currentOrientation[5];
        currentOrientation[5] = currentOrientation[3];
        currentOrientation[3] = currentOrientation[4];
        currentOrientation[4] = tempSide;
        side = currentOrientation[0];

    }
    void RotateRight()
    {
        gameObject.transform.position -= Vector3.left;
        gameObject.transform.Rotate(0, 0, -90, Space.World);
        Enums.Sides tempSide;

        tempSide = currentOrientation[0];
        currentOrientation[0] = currentOrientation[4];
        currentOrientation[4] = currentOrientation[3];
        currentOrientation[3] = currentOrientation[5];
        currentOrientation[5] = tempSide;
        side = currentOrientation[0];

    }
    void Start()
    {
        SetSide(side);
    }

    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            RotateForward();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            RotateBack();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            RotateLeft();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            RotateRight();
        }
    }
    
}
