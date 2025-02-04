using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;



public class PersistableSO : MonoBehaviour {
    [Header("Meta")]
    public string persisterName;
    [Header("Scriptable Objects")]
    public List<ScriptableObject> objectsToPersist;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    protected void OnEnable() {

        ForeachOnEnable();

        ForOnEnable();



    }

    protected void OnDisable() {
        ForOnDisable();


    }

    private void ForOnDisable() {
        for (int i = 0; i < objectsToPersist.Count; i++) {
            string path = $"{Application.temporaryCachePath}/{persisterName}_{i}.pso";

            if (objectsToPersist[i] is CityStatusSO cityStatus) {
                cityStatus.BeforeSaving(); // Convert dictionary to list before saving
            }

            using (FileStream file = File.Create(path)) {
                var json = JsonUtility.ToJson(objectsToPersist[i]);
                new BinaryFormatter().Serialize(file, json);
            }
        }
    }

    private void ForeachOnEnable() {
        foreach (var obj in FindObjectsByType<PersistableSO>(FindObjectsSortMode.None)) {
            if (obj.gameObject.name == gameObject.name && obj.gameObject != gameObject) {
                Destroy(obj.gameObject);
            }
        }
    }

    private void ForOnEnable() {
        for (int i = 0; i < objectsToPersist.Count; i++) {
            string path = $"{Application.temporaryCachePath}/{persisterName}_{i}.pso";

            if (File.Exists(path)) {

                using (FileStream file = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read)) {
                    var json = (string)new BinaryFormatter().Deserialize(file);
                    JsonUtility.FromJsonOverwrite(json, objectsToPersist[i]);

                    if (objectsToPersist[i] is CityStatusSO cityStatus) {
                        cityStatus.AfterLoading(); // Convert list back to dictionary after loading
                    }
                }
            }
        }
    }
}
