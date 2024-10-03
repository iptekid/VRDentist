using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCasting : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform aimPoint;
    public Transform startPoint;
    //public LineRenderer lineRead;
    Ray ray;
    RaycastHit hit;
    Camera cam;
    public List<Tools> toolsObject;
    public bool allowScan;
    void Start()
    {
        for (int i = 0; i < toolsObject.Count; i++)
        {
            toolsObject[i].itemHName = toolsObject[i].item.name;
        }
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //lineRead.SetPosition(0, startPoint.position);
        //lineRead.SetPosition(1, aimPoint.position);
        //ray = cam.ScreenPointToRay(Input.mousePosition);
        if (allowScan) {
            ray = cam.ViewportPointToRay(new Vector3(.5f, .5f, 0));
            if (Physics.Raycast(ray, out hit, 200))
            {
                //Debug.Log(hit.transform.name);
                string nameofHit = hit.transform.name;
                for (int i = 0; i < toolsObject.Count; i++)
                {
                    if (nameofHit == toolsObject[i].itemHName)
                    {
                        Debug.Log(toolsObject[i].name);
                    }
                }
            }
        }

    }
}

[System.Serializable]
public class Tools {
    public GameObject item;
    public string itemHName;
    public string name;
}