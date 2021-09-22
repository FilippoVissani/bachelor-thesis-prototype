using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Vuforia;

public class HologramManager : MonoBehaviour
{
    public GameObject buttonPrefab;
    //public GameObject hologramMenuPrefab;

    public void addHologram(string marker, string hologram)
    {
        //Hologram hologram1 = new Hologram(marker, Resources.Load<GameObject>("Prefabs/heart"), hologramMenuPrefab, buttonPrefab);

        GameObject buttonInstance = Instantiate(buttonPrefab);
        buttonInstance.transform.eulerAngles = new Vector3(90f, 0f, 0f);
        buttonInstance.transform.parent = GameObject.Find(marker).transform;
        buttonInstance.GetComponent<Interactable>().OnClick.AddListener(() => SpawnHologram(marker, hologram));

        /*
        GameObject heart = Instantiate(Resources.Load<GameObject>("Prefabs/heart"));
        heart.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        heart.AddComponent<BoxCollider>();
        heart.AddComponent<ObjectManipulator>();
        heart.AddComponent<BoundsControl>();
        heart.transform.position = new Vector3(0,0,1);

        GameObject pressableButtonBar = Instantiate(hologramMenuPrefab);
        pressableButtonBar.transform.localScale = new Vector3(2, 2, 2);
        pressableButtonBar.transform.parent = heart.transform;
        pressableButtonBar.transform.position = new Vector3(0, -0.3f, 0);
        pressableButtonBar.transform.GetChild(1).GetChild(0).GetComponent<Interactable>().OnClick.AddListener(SpawnHologram);*/
    }

    private void SpawnHologram(string marker, string hologram)
    {
        GameObject hologramInstance = Instantiate(Resources.Load<GameObject>("Prefabs/" + hologram));
        hologramInstance.transform.position = new Vector3(GameObject.Find(marker).transform.position.x,
            GameObject.Find(marker).transform.position.y + 0.2f,
            GameObject.Find(marker).transform.position.z);
    }
}
