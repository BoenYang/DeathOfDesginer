using UnityEngine;
using System.Collections;

public class OnDisableDebug : MonoBehaviour {

    void OnDisable()
    {
        Debug.Log(name + " disable ");
    }
}
