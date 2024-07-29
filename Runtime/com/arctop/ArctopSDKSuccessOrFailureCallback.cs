
using System;
using UnityEngine;

namespace com.arctop
{
#if UNITY_ANDROID
    public class ArctopSDKSuccessOrFailureCallback : AndroidJavaProxy
    {
        public event Action OnSuccess;
        // TODO: Response codes
        public event Action<int> OnFailure;
        public ArctopSDKSuccessOrFailureCallback()
            : base("com.arctop.unity.IArctopSdkSuccessOrFailureCallback")
        {
        }

        void onSuccess()
        {
            OnSuccess?.Invoke();
        }

        void onFailure(int error)
        {
            OnFailure?.Invoke(error);
        }
    }
#endif
}