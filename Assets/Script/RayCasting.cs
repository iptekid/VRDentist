using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCasting : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform aimPoint;
    public Transform startPoint;
    //public LineRenderer lineRead;
    public float raycastDistance;
    Ray ray;
    RaycastHit hit;
    Camera cam;
    public List<Tools> toolsObject;
    public bool allowScan;
    public Tools currObject;
    public Tools emptObject;
    public int currIndex;
    public Manager managers;
    public List<GameObject> SpotTargets;
    public List<ParticlesObject> SpotTargetsParticle;

    void Start()
    {
        for (int i = 0; i < toolsObject.Count; i++)
        {
            toolsObject[i].itemHName = toolsObject[i].item.name;
        }
        cam = Camera.main;
        for (int i = 0; i < SpotTargetsParticle.Count; i++) {
            SpotTargetsParticle[i].parentsPortal = SpotTargetsParticle[i].parents.transform.GetChild(0).GetChild(0).gameObject;
            SpotTargetsParticle[i].parentsUp = SpotTargetsParticle[i].parents.transform.GetChild(0).GetChild(1).gameObject;
            SpotTargetsParticle[i].portal = SpotTargetsParticle[i].parentsPortal.transform.GetChild(0).GetComponent<ParticleSystem>();
            for (int j = 0; j < SpotTargetsParticle[i].parentsUp.transform.childCount; j++) {
                SpotTargetsParticle[i].upP.Add(SpotTargetsParticle[i].parentsUp.transform.GetChild(j).GetComponent<ParticleSystem>());
            }

        }
            //(typeof(MeshRenderer)) as MeshRenderer[];
    }

    // Update is called once per frame
    void Update()
    {
        //lineRead.SetPosition(0, startPoint.position);
        //lineRead.SetPosition(1, aimPoint.position);
        //ray = cam.ScreenPointToRay(Input.mousePosition);
        if (allowScan)
        {
            ray = cam.ViewportPointToRay(new Vector3(.5f, .5f, 0));
            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                //Debug.Log(hit.transform.name);
                string nameofHit = hit.transform.name;
                for (int i = 0; i < toolsObject.Count; i++)
                {
                    if (nameofHit == toolsObject[i].itemHName)
                    {
                        //toolsObject[i].item.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
                        toolsObject[i].item.GetComponent<MeshRenderer>().material.SetFloat("_UseEmissiveMap", 0.0f);
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
                    else
                    {
                        toolsObject[i].item.GetComponent<MeshRenderer>().material.SetFloat("_UseEmissiveMap", 1.0f);

                        currObject = emptObject;
                        managers.currObject = emptObject;
                    }
                }
            }
            else
            {
                for (int i = 0; i < toolsObject.Count; i++)
                {
                    toolsObject[i].item.GetComponent<MeshRenderer>().material.SetFloat("_UseEmissiveMap", 1.0f);
                }
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        int index = 99;
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "SpotTarget-Kids") {

            //SpotTargetsParticle[0].parentsUp.SetActive(true);
            index = 0;
        }
        else if (collision.gameObject.name == "SpotTarget-Mouth")
        {
            //SpotTargetsParticle[1].parentsUp.SetActive(true);
            index = 1;
        }
        else if (collision.gameObject.name == "SpotTarget-Tools")
        {
            //SpotTargetsParticle[2].parentsUp.SetActive(true);
            index = 2;
        }
        if (index != 99) {
            foreach (ParticleSystem particle in SpotTargetsParticle[index].upP)
            {
                particle.Play();
            }
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        int index = 99;


        if (collision.gameObject.name == "SpotTarget-Kids" )
        {
            //SpotTargetsParticle[0].parentsUp.SetActive(false);
            index = 0;
        }
        else if (collision.gameObject.name == "SpotTarget-Mouth")
        {
            //SpotTargetsParticle[1].parentsUp.SetActive(false);
            index = 1;
        }
        else if (collision.gameObject.name == "SpotTarget-Tools")
        {
            //SpotTargetsParticle[2].parentsUp.SetActive(false);
            index = 2;
        }
        if (index != 99)
        {
            foreach (ParticleSystem particle in SpotTargetsParticle[index].upP)
            {
                particle.Stop();
            }
        }
    }

    //private void OnDisable()
    //{
    //    MeshRenderer[] meshs = FindObjectsByType<MeshRenderer>(FindObjectsSortMode.None);
    //    foreach (MeshRenderer a in meshs)
    //    {
    //        a.staticShadowCaster = true;
    //        a.staticShadowCaster = true;
    //        a.receiveGI = ReceiveGI.LightProbes;
    //    }
    //}
}

