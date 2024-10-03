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
    public Tools currObject;
    public Tools emptObject;
    public int currIndex;
    public Manager managers;

    void Start()
    {
        for (int i = 0; i < toolsObject.Count; i++)
        {
            toolsObject[i].itemHName = toolsObject[i].item.name;
        }
        cam = Camera.main;

            //(typeof(MeshRenderer)) as MeshRenderer[];
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
                        //toolsObject[i].item.GetComponent<MeshRenderer>().materials[0].
                        Debug.Log(toolsObject[i].name);
                        currObject = toolsObject[i];
                        Tools too = new Tools();
                        too.item = currObject.item;
                        too.name = currObject.name;
                        too.itemHName = currObject.itemHName;
                        managers.currObject = too;
                        currIndex = i;
                        managers.currIndex = currIndex;
                        break;
                    }
                    else {
                        currObject = emptObject;
                        managers.currObject = emptObject;
                    }
                }
            }
        }

    }
    private void OnDisable()
    {
        MeshRenderer[] meshs = FindObjectsByType<MeshRenderer>(FindObjectsSortMode.None);
        foreach (MeshRenderer a in meshs)
        {
            a.staticShadowCaster = true;
            a.staticShadowCaster = true;
            a.receiveGI = ReceiveGI.LightProbes;
        }
    }
}

