using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ZoneLoader1Back : ZoneLoader {

    override public void LoadZone()
    {
        SceneManager.LoadScene(0);
    }
}
