using UnityEngine;
using System.Collections;

public abstract class ZoneLoader : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LoadZone();
        }
    }

    abstract public void LoadZone();
}
