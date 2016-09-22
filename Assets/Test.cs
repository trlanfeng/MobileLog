using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(50,50,200,100),"Log"))
        {
            Debug.Log("这个是Log。");
        }
        if (GUI.Button(new Rect(50, 200, 200, 100), "Warning"))
        {
            Debug.LogWarning("这是一个Warning信息。");
        }
        if (GUI.Button(new Rect(50, 350, 200, 100), "Error"))
        {
            Debug.LogError("这是一个警告！");
        }
    }
}
