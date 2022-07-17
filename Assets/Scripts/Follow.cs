using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform Target;

    // Update is called once per frame
    void Update()
    {
        if(Target != null)
        transform.position = Target.transform.position;
    }
}
