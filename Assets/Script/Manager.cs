using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Manager : MonoBehaviour
{
    // Start is called before the first frame update
    public bool skiping;
    public RayCasting rayCasting;
    public FirstPersonController movement;
    public StarterAssetsInputs starterAssetsInputs;
    public List<ToolsPanel> toolsObject;
    public GameObject materiItems;
    public GameObject panelItems;
    public Tools currObject;
    public int currIndex;
    public bool allowShow;
    public GameObject middlePoint;
    public List<AudioClip> startClip;
    public List<AudioClip> materiClip;
    public AudioSource audioSource;
    public GameObject panelCharacter;
    public GameObject opening;

    void Start()
    {
        panelCharacter.SetActive(true);

        opening.SetActive(true);
        AllowCastMove(false);

        middlePoint.SetActive(false);

        if (skiping)
        {
            Invoke("StartAlls", .5f);
        }



    }
    public void StartAlls(){
        opening.SetActive(false);
        if (!skiping) { 
            StartCoroutine("StartBeginning");

        }
        else { 
            ToAllowScan();
        }
        //CheckAudioLenght();

    }
    public void CheckAudioLenght() {
        foreach (AudioClip audios in startClip) {
            Debug.Log(audios.length);
        }
    
    }
    public IEnumerator StartBeginning() {
        AllowCastMove(false);

        allowShow = false;
        audioSource.clip = startClip[0];
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length+1);
        panelCharacter.SetActive(false);
        audioSource.clip = startClip[1];
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length+1);
        middlePoint.SetActive(true);
        Cursor.visible = true;
        ToAllowScan();

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
        panelCharacter.SetActive(false);

        currObject.item = null;
        allowShow = true;
        Cursor.visible = true;

        AllowCastMove(true);
        materiItems.SetActive(false);
        panelItems.SetActive(false);
        foreach(ToolsPanel tp in toolsObject) {
            tp.boxMateri.SetActive(false);
            tp.item.SetActive(false);
        }


    }

    public void AllowCastMove(bool stat) {
        audioSource.Stop();
        
        rayCasting.enabled = stat;
        movement.enabled = stat;
        starterAssetsInputs.cursorInputForLook = stat;
        //starterAssetsInputs.cursorLocked = stat;
        Cursor.visible = !stat;
        starterAssetsInputs.enabled = stat;
        middlePoint.SetActive(stat);

    }
    public void ShowPanels() {
        materiItems.SetActive(true);
        panelItems.SetActive(true);
        toolsObject[currIndex].boxMateri.SetActive(true);
        toolsObject[currIndex].item.SetActive(true);
        
        audioSource.clip = materiClip[currIndex];
        audioSource.Play();

        if (currIndex == 4) {
        }
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
[System.Serializable]
public class Audioclips
{
    //public AudioClip 

}