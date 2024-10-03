using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Manager : MonoBehaviour
{
    // Start is called before the first frame update
    public RayCasting rayCasting;
    public FirstPersonController movement;
    public StarterAssetsInputs starterAssetsInputs;
    public List<ToolsPanel> toolsObject;
    public GameObject materiItems;
    public GameObject panelItems;
    public Tools currObject;
    public int currIndex;
    public bool allowShow;
    void Start()
    {
        ToAllowScan();
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && allowShow && currObject.item != null) {
            allowShow = false;
            AllowCastMove(false);
            ShowPanels();
        }
    }
    public void ToAllowScan() {
        currObject.item = null;
        allowShow = true;

        AllowCastMove(true);
        materiItems.SetActive(false);
        panelItems.SetActive(false);
        foreach(ToolsPanel tp in toolsObject) {
            tp.boxMateri.SetActive(false);
            tp.item.SetActive(false);
        }


    }

    public void AllowCastMove(bool stat) {
        rayCasting.enabled = stat;
        movement.enabled = stat;
        starterAssetsInputs.cursorInputForLook = stat;
        //starterAssetsInputs.cursorLocked = stat;
        Cursor.visible = !stat;
        starterAssetsInputs.enabled = stat;
    }
    public void ShowPanels() {
        
        materiItems.SetActive(true);
        panelItems.SetActive(true);
        toolsObject[currIndex].boxMateri.SetActive(true);
        toolsObject[currIndex].item.SetActive(true);
    }
}
[System.Serializable]
public class Tools
{
    public GameObject item;
    public string itemHName;
    public string name;
}
[System.Serializable]
public class ToolsPanel
{
    public GameObject item;
    public GameObject boxMateri;

}