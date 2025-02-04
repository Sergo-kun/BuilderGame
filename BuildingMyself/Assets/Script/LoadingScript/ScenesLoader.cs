using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesLoader : MonoBehaviour // used to load scenes in unity
{
    public enum GameScene {
        Menu,
        Game,
        Options,
        Loader

    }

    private static Action onSceneLoadedCallback;

    public static void Load(GameScene scene) {

        onSceneLoadedCallback = () => {
            SceneManager.LoadScene(scene.ToString());
        };

        SceneManager.LoadScene(GameScene.Loader.ToString());

    }

    public static void ExecuteSceneLoadCallback() {
        if (onSceneLoadedCallback != null) {
            onSceneLoadedCallback();
            onSceneLoadedCallback = null;

        }
    }
}

