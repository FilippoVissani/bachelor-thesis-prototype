using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class PositionManager : MonoBehaviour
{
    public GameObject root;

    public void UpdatePosition()
    {
        IEnumerable<ObserverBehaviour> trackedObserverBehaviours = VuforiaBehaviour.Instance.World.GetTrackedObserverBehaviours();
        foreach(ObserverBehaviour ob in trackedObserverBehaviours)
        {
            root.transform.position = ob.transform.position;
            root.transform.rotation = ob.transform.rotation;
        }    
    }
}
