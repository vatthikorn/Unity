using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ZoneLoader2Front : ZoneLoader {

    override public void LoadZone()
    {
        SceneManager.LoadScene(1);
    }
}
