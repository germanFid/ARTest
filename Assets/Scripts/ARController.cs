using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR.ARSubsystems;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public interface IArController 
{
    public void StartAR(Texture2D imageTarget);
    public void StopAR();
}

public class ARController : MonoBehaviour, IArController
{
    public ARSession session;
    public ARTrackedImageManager TrackedImageManager;
    private XRReferenceImageLibrary imageLibrary;
    public void StartAR(Texture2D imageTarget)
    {
        imageLibrary.Add();
        imageLibrary.SetName(0,"img");
        imageLibrary.SetSize(0, new Vector2(0.3f, 0.3f));
        
        TrackedImageManager.referenceLibrary = imageLibrary;
        TrackedImageManager.enabled = true;
        session.enabled = true;
    }

    public void StopAR()
    {
        // TODO: Add image reset on screen (2) exit
        session.enabled = false;
        if (imageLibrary.count != 0)
            imageLibrary.RemoveAt(0);
    }

    private void Start()
    {
        session.enabled = false;
        imageLibrary = ScriptableObject.CreateInstance<XRReferenceImageLibrary>();
    }
}
