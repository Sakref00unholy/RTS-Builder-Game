using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEmitter : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        EventManager.TriggerEvent("Test");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class TestListener : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.AddListener("Test", _OnTest);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener("Test", _OnTest);
    }

    private void _OnTest()
    {
        Debug.Log("Received the 'Test' event!");
    }
}
