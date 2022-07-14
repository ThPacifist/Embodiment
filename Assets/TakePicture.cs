using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TakePicture : MonoBehaviour
{
    public string filename;
    InputAction takePicture;

    private void Awake()
    {
        takePicture = new InputAction("screenshot", binding: "<Keyboard>/v");
    }

    protected virtual void OnEnable()
    {
        takePicture.Enable();
    }

    protected virtual void OnDisable()
    {
        takePicture.Disable();
    }

    private void Start()
    {
        takePicture.performed += ctx => {
            Debug.Log("Took Screenshot");
            ScreenCapture.CaptureScreenshot("C:\\Users\\DSU Student\\Documents\\GitHub\\Embodiment\\Assets\\ScreenCaptures\\" + filename + ".png");
        };
    }
}
