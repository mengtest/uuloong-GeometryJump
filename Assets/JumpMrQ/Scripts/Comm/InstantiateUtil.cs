// InstantiateManager.cs
// Author: Tan Yuqing
// Date: 2016/9/26
// Desc: 预制体实例化类
//
using UnityEngine;
using System.Collections;

// Analysis disable once ConvertToStaticType
public class InstantiateUtil: MonoBehaviour {

	public static GameObject GenerateFromPrefab (string path) {
        GameObject go = Instantiate (Resources.Load(path)) as GameObject;
        RectTransform rt = go.GetComponent<RectTransform> ();

        rt.localScale = Vector3.one;
        return go;
    }

    public static GameObject GenerateFromPrefab (string path, Transform parent) {
        GameObject go = Instantiate (Resources.Load (path), parent) as GameObject;
        RectTransform rt = go.GetComponent<RectTransform> ();

        rt.localScale = Vector3.one;
        return go;
    }
}
