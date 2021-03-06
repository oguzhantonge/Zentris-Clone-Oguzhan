using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placeObjectOnGrid : MonoBehaviour
{
    public Transform gridCellPrefab;
  
    public Transform onMousePrefab;
   

    public Vector3 smoothMousePosition;
    [SerializeField]
    private int height;

    [SerializeField]
    private int width;

    public Vector3 mousePosition;
    private Grids[,] Gridss;

    private Plane plane;
    public ScoreScript scorescript;

    void Start()
    {
        CreateGrid();
        plane = new Plane(Vector3.up, transform.position);

    }

    
    void Update()
    {
       
        GetMousePosition();
        if (onMousePrefab)
        {
           
            ObjFollowMouse objfollowmouse = null;
            objfollowmouse = onMousePrefab.GetComponent<ObjFollowMouse>();
         

           
            if (IsAvailablePosition(objfollowmouse))
            {
                if (Input.GetMouseButtonUp(0))
                {
                    StoreShapeInGrid(objfollowmouse);
                }
            }
            if (IsAvailablePosition(objfollowmouse))
            {
                List<Vector3> a = new List<Vector3>();
                foreach (Transform child in objfollowmouse.transform)
                {
                   
                    Vector3 pos = Vector3Int.RoundToInt(child.position);
                    Gridss[(int)pos.x,(int) pos.z].obj.GetComponent<Renderer>().material.color = Color.white;
                    a.Add(pos);
                }
                for(int i = 0; i < width; i++)
                {
                    for(int j = 0; j < height; j++)
                    {
                        for (int t = 0; t < a.Count; t++)
                        {
                            if (i == a[t].x && j == a[t].z)
                            {
                                j++;
                            }
                        }
                        if (j < height)
                        {
                            Gridss[i, j].obj.GetComponent<Renderer>().material.color = Color.red;
                        }
                        
                    }
                }
            }
        }
      
    }
    void GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (plane.Raycast(ray, out var enter))
        {
            mousePosition = ray.GetPoint(enter);
            smoothMousePosition = mousePosition;
            mousePosition.y = 0;
            mousePosition = Vector3Int.RoundToInt(mousePosition);

        }
    }

    private void CreateGrid()
    {
        Gridss = new Grids[width, height];
        var name = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector3 worldPosiion = new Vector3(i, 0, j);
                Transform obj = Instantiate(gridCellPrefab, worldPosiion, Quaternion.identity);
                obj.name = "Cell (x =  " + i.ToString() +"y = " + j.ToString();
                Gridss[i, j] = new Grids(worldPosiion, obj,new Vector3(999,999,999),null);
                name++;
                obj.transform.parent = GameObject.Find("Board").transform;
                
            }
        }
    }
    bool IsFull(int x, int y, ObjFollowMouse objfollowmouse)
    {

        return (Gridss[x, y].cubePosition != new Vector3(999, 999, 999) && Gridss[x,y].cubeGameObject != null);
    }
    bool IsInBoard(int x, int z)
    {
        return (x >= 0 && x < width && z >= 0 && z < height);
    }

    public bool IsAvailablePosition(ObjFollowMouse objfollowmouse)
    {
        foreach(Transform child in objfollowmouse.transform)
        {
            
            Vector3 pos = Vector3Int.RoundToInt(child.position);
           
            if (!IsInBoard((int)pos.x, (int)pos.z))
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        Gridss[i, j].obj.GetComponent<Renderer>().material.color = Color.red;
                    }
                }
                        return false;
            }
            if (IsFull((int)pos.x, (int)pos.z, objfollowmouse))
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        Gridss[i, j].obj.GetComponent<Renderer>().material.color = Color.red;
                    }
                }

                return false;
            }
           
        }
       
        return true;
    }

    public void StoreShapeInGrid(ObjFollowMouse objfollowmouse)
    {
        if(objfollowmouse == null)
        {
            return;
        }
        else { 
            foreach(Transform child in objfollowmouse.transform)
            {
                Vector3 pos = Vector3Int.RoundToInt(child.transform.position);
                pos.y = (Gridss[(int)pos.x, (int)pos.z].obj.transform.position.y) + 0.14f;
                Gridss[(int)pos.x, (int)pos.z].cubeGameObject = child;
                child.position = pos;
                onMousePrefab.GetComponent<ObjFollowMouse>().isOnGrid = true;
                Gridss[(int)pos.x, (int)pos.z].cubePosition = new Vector3(123, 12, 123);


            }
            objfollowmouse = null;
            onMousePrefab = null;
            ClearAllRowsandColumns(); // Checks for the should clear rows ( dolumu row'un hepsi)
        }
    }

   
    bool IsComplete(int z)
    {
        for (int x = 0; x < width; ++x)
        {
            if (Gridss[x, z].cubeGameObject == null)
            {
                return false;
            }

        }
        return true;
    }
    bool IsCompleteColumn(int x)
    {
        for (int z = 0; z < width; ++z)
        {
            if (Gridss[x, z].cubeGameObject == null)
            {
                return false;
            }

        }
        return true;
    }
    void ClearRow(int z)
    {
        scorescript.scoreCount += 100;
        
        for (int x = 0; x < width; ++x)
        {
            if (Gridss[x, z] != null)
            {
                Destroy(Gridss[x, z].cubeGameObject.gameObject);

            }
            Gridss[x, z].cubeGameObject = null;
            Gridss[x, z].cubePosition = new Vector3(999, 999, 999);

        }

    }
    
    void ClearColumn(int x)
    {
        scorescript.scoreCount += 100;
        for (int z = 0; z < width; ++z)
        {
            if (Gridss[x, z].cubeGameObject != null)
            {
                Destroy(Gridss[x, z].cubeGameObject.gameObject);

            }
            Gridss[x, z].cubeGameObject = null;
            Gridss[x, z].cubePosition = new Vector3(999, 999, 999);

        }

    }


    public void ClearAllRowsandColumns()
    {
        for (int x = 0; x < width; ++x)
        {
            if (IsCompleteColumn(x))
            {
                for (int z = 0; z < height; ++z)
                {
                    if (IsComplete(z))
                    {
                        ClearRow(z);
                        ClearColumn(x);
                        z--;
                    }
                }
                ClearColumn(x);
            }
        }
        for (int z = 0; z < height; ++z)
        {
            if (IsComplete(z))
            {
                ClearRow(z);

                z--;
            }
        }
    }

}

public class Grids
{
   
    public Vector3 CellPosition;
    public Transform obj;
    public Vector3 cubePosition;
    
    public Transform cubeGameObject;
    public Grids(Vector3 cellPosition, Transform obj, Vector3 cubePosition, Transform cubeGameObject)
    {
       
        this.CellPosition = cellPosition;
        this.obj = obj;
        this.cubePosition = cubePosition;
        this.cubeGameObject = cubeGameObject;
    }




}