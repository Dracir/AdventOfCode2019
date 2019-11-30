using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CreateUICameraContextMenu : MonoBehaviour
{
  	[MenuItem("GameObject/UI/CreateUICamera", false, 100)]
    static void CreateUICamera()
    {
        var uiLayer = LayerMask.NameToLayer("UI");

        var cameraGo = new GameObject("UICamera");
        var camera = cameraGo.AddComponent<Camera>();
        camera.clearFlags = CameraClearFlags.Nothing;
        camera.orthographic = true;
        camera.cullingMask = 1 << uiLayer;
        cameraGo.layer = uiLayer;

        var canevasGo = new GameObject("Canevas");
        canevasGo.transform.parent = cameraGo.transform;
        canevasGo.layer = uiLayer;

        var canevas = canevasGo.AddComponent<Canvas>();
        canevas.worldCamera = camera;
        canevas.renderMode = RenderMode.ScreenSpaceCamera;

        var canevasScaler = canevasGo.AddComponent<CanvasScaler>();
        canevasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canevasScaler.referenceResolution = new Vector2(1920, 1080);
        canevasScaler.referencePixelsPerUnit = 16;

        //Pourquoi ?
        var graphicsRaycaster = canevasGo.AddComponent<GraphicRaycaster>();
    }
}
