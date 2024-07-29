using System;
using UnityEngine;
#if UNITY_ANDROID
using UnityEngine.Android;
#endif
namespace com.arctop
{
    public class ArctopAndroidSDKPermissionBehaviour : MonoBehaviour
    {
        [SerializeField] protected ArctopNativeClient arctopClient;
        [SerializeField] protected bool createSDKOnPermissionGranted;
        protected void Start()
        {
            #if UNITY_ANDROID
            if (Permission.HasUserAuthorizedPermission(ArctopSDK.ARCTOP_PERMISSION))
            {
                if (createSDKOnPermissionGranted)
                {
                    arctopClient?.CreateArctopSDK();
                }
            }
            else
            {
                var callbacks = new PermissionCallbacks();
                callbacks.PermissionDenied += PermissionCallbacks_PermissionDenied;
                callbacks.PermissionGranted += PermissionCallbacks_PermissionGranted;
                callbacks.PermissionDeniedAndDontAskAgain += PermissionCallbacks_PermissionDeniedAndDontAskAgain;
                Permission.RequestUserPermission(ArctopSDK.ARCTOP_PERMISSION, callbacks);
            }
            #else
            Debug.Log("Calling create");
            arctopClient?.CreateArctopSDK();
            #endif
        }
#if UNITY_ANDROID
        protected void PermissionCallbacks_PermissionDeniedAndDontAskAgain(string permissionName)
        {
            Debug.Log($"{permissionName} PermissionDeniedAndDontAskAgain");
        }
        
        protected void PermissionCallbacks_PermissionGranted(string permissionName)
        {
            Debug.Log($"{permissionName} PermissionCallbacks_PermissionGranted");
            if (permissionName.Equals(ArctopSDK.ARCTOP_PERMISSION))
            {
                if (createSDKOnPermissionGranted)
                {
                    arctopClient?.CreateArctopSDK();
                }
            }
        }
        
        protected void PermissionCallbacks_PermissionDenied(string permissionName)
        {
            Debug.Log($"{permissionName} PermissionCallbacks_PermissionDenied");
        }
#endif
    }
}