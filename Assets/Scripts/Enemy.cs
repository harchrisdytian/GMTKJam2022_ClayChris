using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public int hp = 5;
    public static  Enums.Sides[] currentOrientation = new Enums.Sides[6] {Enums.Sides.Up, Enums.Sides.Front,Enums.Sides.Down, Enums.Sides.Back, Enums.Sides.Left,Enums.Sides.Right};
    [SerializeField]
    public Enums.Sides side;
    private float speed = 2f;
    private Quaternion nextRotation;
    public Transform TargetPosition;
    private bool _isRotating = false;
    
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
        MakeRotate(Vector3.back);
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
        MakeRotate(Vector3.forward);
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
        MakeRotate(Vector3.left);
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
        MakeRotate(Vector3.right);
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

    }

    
    // Update is called once per frame
    void Update()
    {
        if (!_isRotating)
        {
            var direction = transform.position - TargetPosition.position;
            if (direction.x > 0 && Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                RotateLeft();
            else
                RotateRight();
            if (direction.y > 0 && Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
                RotateForward();
            else
                RotateBack();
        }
    }
    
    void MakeRotate(Vector3 dir)
    {
        var anchor = gameObject.transform.position + (Vector3.down + dir) * 0.5f;
        var axis = Vector3.Cross(Vector3.up, dir);
        StartCoroutine(Roll(anchor,axis));
    }

    IEnumerator Roll(Vector3 anchor, Vector3 axis)
    {
        _isRotating = false;
           for(int i=0; i < (90 / speed); i++)
        {
            gameObject.transform.RotateAround(anchor, axis, speed);
            yield return new WaitForSeconds(0.01f);
        }
        _isRotating = true;
    }
}
