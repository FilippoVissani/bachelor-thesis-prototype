using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hologram : MonoBehaviour
{
    private GameObject marker;
    private GameObject hologram;
    private GameObject hologramMenu;
    private GameObject hologramSpawnButton;

    public Hologram(string marker, GameObject hologram, GameObject hologramMenuPrefab, GameObject hologramSpawnButtonPrefab)
    {
        
        this.marker = GameObject.Find(marker);

        this.hologramSpawnButton = Instantiate(hologramSpawnButtonPrefab);
        this.hologramSpawnButton.transform.parent = GameObject.Find(marker).transform;
        this.hologramSpawnButton.GetComponent<Interactable>().OnClick.AddListener(SpawnHologram);
        /*
        this.hologram = Instantiate(hologram);
        this.hologram.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        this.hologram.AddComponent<BoxCollider>();
        this.hologram.AddComponent<ObjectManipulator>();
        this.hologram.AddComponent<BoundsControl>();
        this.hologram.transform.position = new Vector3(0, 0, 1);*/
        /*
        hologramMenu = Instantiate(hologramMenuPrefab);
        hologramMenu.transform.localScale = new Vector3(2, 2, 2);
        hologramMenu.transform.parent = this.hologram.transform;
        hologramMenu.transform.position = new Vector3(0, -0.3f, 0);*/
        /*
        this.hologramMenu = Instantiate(hologramSpawnButtonPrefab);
        this.hologramMenu.transform.parent = this.hologram.transform;
        this.hologramMenu.transform.position = new Vector3(0, -0.3f, 0);
        this.hologramMenu.GetComponent<Interactable>().OnClick.AddListener(DeleteHologram);*/
    }

    private void SpawnHologram()
    {
        GameObject hologram = Instantiate(Resources.Load<GameObject>("Prefabs/heart"));
        hologram.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        hologram.AddComponent<BoxCollider>();
        hologram.AddComponent<ObjectManipulator>();
        hologram.transform.position = new Vector3(GameObject.Find("marker1").transform.position.x,
            GameObject.Find("marker1").transform.position.y + 0.2f,
            GameObject.Find("marker1").transform.position.z);
    }

    private void DeleteHologram()
    {
    }
}
