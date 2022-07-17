using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public int hp = 10;
    public Enums.Sides[] currentOrientation = new Enums.Sides[6] {Enums.Sides.Up, Enums.Sides.Front,Enums.Sides.Down, Enums.Sides.Back, Enums.Sides.Left,Enums.Sides.Right};
    [SerializeField]
    public Enums.Sides side;
    [SerializeField]
    private float speed = 1f;
    private Quaternion nextRotation;
    public Transform TargetPosition;
    private bool _isRotating = false, _pause = false;
    public AudioSource thud;
    public ParticleSystem dustCloud;
    public Animation Animation;



    void RotateBack()
    {
        if (!CheckDir(Vector3.back))
        {
            return;
        }
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
        if (!CheckDir(Vector3.forward))
        {
            return;
        }
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
        if (!CheckDir(Vector3.left))
        {
            return;
        }
        MakeRotate(Vector3.left);
        Enums.Sides tempSide;

        tempSide = currentOrientation[0];
        currentOrientation[0] = currentOrientation[5];
        currentOrientation[5] = currentOrientation[2];
        currentOrientation[2] = currentOrientation[4];
        currentOrientation[4] = tempSide;
        side = currentOrientation[0];

    }
    void RotateRight()
    {
        if (!CheckDir(Vector3.right))
        {
            return;
        }
        MakeRotate(Vector3.right);
        Enums.Sides tempSide;

        tempSide = currentOrientation[0];
        currentOrientation[0] = currentOrientation[4];
        currentOrientation[4] = currentOrientation[2];
        currentOrientation[2] = currentOrientation[5];
        currentOrientation[5] = tempSide;
        side = currentOrientation[0];

    }
    void Start()
    {
        TargetPosition = FindObjectOfType<PlayerController>().transform;
    }

    
    // Update is called once per frame
    void Update()
    {
        if(TargetPosition == null)
        { 
            TargetPosition = FindObjectOfType<PlayerController>().transform;
        }

        if (!_isRotating && !_pause)
        {
            var direction = TargetPosition.position - transform.position;
            if ( Mathf.Abs(direction.x) > Mathf.Abs(direction.z))
            {
                if(direction.x > 0)
                    RotateRight();
                else
                    RotateLeft();

            }
            if ( Mathf.Abs(direction.z) > Mathf.Abs(direction.x))
            {
                if(direction.z > 0)
                    RotateForward();
                else
                    RotateBack();
            }
        }
    }
    
    bool CheckDir(Vector3 dir)
    {
        return !Physics.Raycast(transform.position, dir, 0.5F);
    }
    void MakeRotate(Vector3 dir)
    {

        var anchor = gameObject.transform.position + (Vector3.down + dir) * 0.5f;
        var axis = Vector3.Cross(Vector3.up, dir);
        if (CheckDir(dir))
        { 
         StartCoroutine(Roll(anchor,axis,CheckSide()));
        }
    }

    IEnumerator Roll(Vector3 anchor, Vector3 axis, bool pause = false)
    {
        _isRotating = true;
        if (pause)
        {
            yield return new WaitForSeconds(1f);
        }
        if(Random.value > 0.8f)
        {
            yield return new WaitForSeconds(0.2f);
        }
           for(int i=0; i < (90 / speed); i++)
        {
            gameObject.transform.RotateAround(anchor, axis, speed);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.1f);
        thud.Play();
        //dustCloud.Play();
        _isRotating = false;
    }
    
    bool CheckSide()
    {
        if(side == Enums.Sides.Right)
        { 
            DoDamage(); 
            return true;
        }
        return false;
    }

    void DoDamage()
    {
        hp -= 1;
        Animation.Play();
        if(hp <= 0)
        {
            gameObject.SetActive(false);
                
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().Death();
        }
    }
}
