using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    private bool hasFirstTimeUpdate = true;

    private void Update() {
        if (hasFirstTimeUpdate) {
            hasFirstTimeUpdate = false;
            ScenesLoader.ExecuteSceneLoadCallback();
        }
    }
}
