using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NativeGalleryNamespace;

public class UiController : MonoBehaviour
{
    // public GameObject ImagePickerCanvas; // May be optimized by switching to Canvas, instead of GameObject
    public Canvas imagePickerCanvas;
    public Canvas arCanvas;
    
    private ARController _arController;

    private string s()
    {
        string str = "123";
        return str;
    }
    
    private string PickImage( int maxSize )
    {
        string toReturn = null;
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery( ( path ) =>
        {
            Debug.Log( "Image path: " + path );
            if ( path != null )
            {
                Texture2D texture = NativeGallery.LoadImageAtPath( path, maxSize );
                if( texture == null )
                {
                    Debug.Log( "Couldn't load texture from " + path );
                    return;
                } 
                toReturn = path;
            }
        } );
        
        Debug.Log( "Permission result: " + permission );
        return toReturn;
    }
    private void Awake()
    {
        _arController = FindObjectOfType<ARController>();
    }
    
    public void ARScreen()
    {
        imagePickerCanvas.enabled = false;
        arCanvas.enabled = true;
    }

    public void PickerScreen()
    {
        _arController.StopAR();
        arCanvas.enabled = false;
        imagePickerCanvas.enabled = true;
        
        string imgPath = PickImage(512);
        Texture2D texture = NativeGallery.LoadImageAtPath(imgPath);
        
        _arController.StartAR(texture);
    }

    private void Start()
    {
        PickerScreen();
    }
}
