using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GeneratingCubes : MonoBehaviour
{
    public Transform[] cubes;
    public Image shape01;
    public Image shape02;
    public Image shape03;
    public Image[] shapes;

    public placeObjectOnGrid placeobjectscript;
  
    void Update()
    {
        refreshImages();
    }
    void refreshImages()
    {
        if (check() && placeobjectscript.onMousePrefab == null)
        {
            shape01.enabled = true;
            shape02.enabled = true;
            shape03.enabled = true;
        }
    }
   
    bool check()
    {
        foreach(Image x in shapes)
        {
            if(x.enabled == true)
            {
                return false;
            }
        }
        return true;
    }
    
   
    public void OnMouseClickUI1()
    {
        if(placeobjectscript.onMousePrefab == null) { 
            int x = Random.Range(0, cubes.Length);
            if (placeobjectscript.onMousePrefab == null)
            {
                placeobjectscript.onMousePrefab = Instantiate(cubes[x], placeobjectscript.mousePosition, Quaternion.identity);
            }
            shape01.enabled = false;
        }
    }
    public void OnMouseClickUI2()
    {
        if (placeobjectscript.onMousePrefab == null) { 
            int x = Random.Range(0, cubes.Length);
            if (placeobjectscript.onMousePrefab == null)
            {
                placeobjectscript.onMousePrefab = Instantiate(cubes[x], placeobjectscript.mousePosition, Quaternion.identity);
            }
            shape02.enabled = false;
        }
    }
    public void OnMouseClickUI3()
    {
        if (placeobjectscript.onMousePrefab == null) { 
            int x = Random.Range(0, cubes.Length);
            if (placeobjectscript.onMousePrefab == null)
            {
                placeobjectscript.onMousePrefab = Instantiate(cubes[x], placeobjectscript.mousePosition, Quaternion.identity);
            }
            shape03.enabled = false;
        }
    }

}
