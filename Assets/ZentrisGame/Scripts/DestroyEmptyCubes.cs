using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEmptyCubes : MonoBehaviour
{
    
    void Update()
    {
        if(transform.childCount == 0)
        {
            Destroy(transform.gameObject);
        }
    }
}
