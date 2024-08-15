using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using AOT;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace com.arctop
{
    public class ArctopNativeClient : MonoBehaviour
    {
        [SerializeField] private string m_ApiKey;
        [SerializeField] private bool HandleSDKInit;
        [SerializeField] private bool HandleSDKDestroy;
        
        [Header("In Editor Debug Only")] 
        [SerializeField]
        private bool m_SimulateRealtime;
        [SerializeField]
        private bool m_SimulateSignalQuality;
        [SerializeField] 
        private ArctopSDK.LoginStatus m_SimulatedUserLoggedInResponse;
        [SerializeField] 
        private ArctopSDK.UserCalibrationStatus m_SimulatedUserCalibrationResponse;
        [SerializeField]
        private bool m_LoginSuccessfullyInEditor = true;
        [SerializeField]
        private bool m_LogoutSuccessfullyInEditor = true;
        [SerializeField] [Tooltip("Allows you to change the connection status in editor")]
        private ArctopSDK.ConnectionState m_currentDebugConnection = ArctopSDK.ConnectionState.Unknown;
        
        [Header("Events")]
        [SerializeField] private UnityEvent OnUserLoggedIn;
        [SerializeField] private UnityEvent<ArctopSDK.ResponseCodes> OnUserLoginFailed;
        [SerializeField] private UnityEvent<bool> IsUserLoggedIn;
        [SerializeField] private UnityEvent OnUserLogout;
        [SerializeField] private UnityEvent<ArctopSDK.ResponseCodes> OnUserLogoutFailed;
        [SerializeField] private UnityEvent<ArctopSDK.UserCalibrationStatus> OnCalibrationStatus;
        [SerializeField] private UnityEvent<ArctopSDK.ResponseCodes> OnCalibrationStatusError;
        [SerializeField] private UnityEvent<string[]> OnDeviceListUpdated;
        [SerializeField] private UnityEvent OnSDKInitSuccess;
        [SerializeField] private UnityEvent<ArctopSDK.ResponseCodes> OnSDKInitFailed;
        [SerializeField] private UnityEvent OnPredictionStart;
        [SerializeField] private UnityEvent<ArctopSDK.ResponseCodes> OnPredictionStartFailed;
        [SerializeField] private UnityEvent<ArctopSDK.ConnectionState, ArctopSDK.ConnectionState> OnConnectionStateChanged;
        [SerializeField] private UnityEvent<string, float> OnRealtimeValue;
        [SerializeField] private UnityEvent<bool, ArctopSDK.QAFailureType> OnQAStatus;
        [SerializeField] private UnityEvent OnPredictionEnd;
        [SerializeField] private UnityEvent<ArctopSDK.ResponseCodes> OnPredictionEndFailed;
        [SerializeField] private UnityEvent<string> OnSignalQuality;
        [SerializeField] private UnityEvent OnSessionComplete;

        [Header("Android only Events")]
        [SerializeField] private UnityEvent<ArctopSDK.BindError> OnSDKCreateFailed;

#if UNITY_ANDROID
        private ArctopSDKCallback mAndroidSDKCallback;
#endif
        private static ArctopNativeClient instance;
        private ConcurrentQueue<Action> m_actions;
        private HashSet<string> m_headbandDevices;
        private void Awake()
        {
            instance = this;
            m_actions = new ConcurrentQueue<Action>();
            m_headbandDevices = new HashSet<string>();
#if UNITY_EDITOR

#elif UNITY_IOS
            ArctopNativePlugin.arctopSDKSetConnectionCallback(onConnectionChanged);
            ArctopNativePlugin.arctopSDKSetValueChangedCallback(onValueChangedCallback);
            ArctopNativePlugin.arctopSDKSetQAStatusCallback(onQAStatusCallback);
            ArctopNativePlugin.arctopSDKSetSignalQualityCallback(onSignalQualityCallback);
#elif UNITY_ANDROID
            mAndroidSDKCallback = new ArctopSDKCallback();
#endif
            if (instance.HandleSDKInit)
            {
                CreateArctopSDK();
            }
        }
        
        public void CreateArctopSDK()
        {
#if UNITY_EDITOR
            onSDKInit();
#elif UNITY_IOS
            ArctopNativePlugin.arctopSDKInit(m_ApiKey ,Application.identifier, onSDKInit, onSDKInitFailed);
#elif UNITY_ANDROID
            ArctopNativePlugin.arctopSDKCreate(onSDKCreate, onSDKCreateFailed);
#endif
        }
        
#if UNITY_ANDROID
        private static void onSDKCreate()
        {
            instance.mAndroidSDKCallback.OnConnectionStatusCallback += onConnectionChanged;
            instance.mAndroidSDKCallback.OnScanResultCallback += onDeviceDetected;
            instance.mAndroidSDKCallback.OnSignalQualityCallback += onSignalQualityCallback;
            instance.mAndroidSDKCallback.OnValueChangedCallback += onValueChangedCallback;
            instance.mAndroidSDKCallback.OnQAStatusCallback += onQAStatusCallback;
            instance.mAndroidSDKCallback.OnIsUserLoggedInCallback += onUserLoggedInCheck;
            instance.mAndroidSDKCallback.OnSessionCompleteCallback += onSessionComplete;
            
            ArctopNativePlugin.setSDKCallback(instance.mAndroidSDKCallback);
            AddAction(() => { instance.androidInitSDK(); });
        }

        private static void onSDKCreateFailed(int error)
        {
            var bindError = (ArctopSDK.BindError)Enum.ToObject(typeof(ArctopSDK.BindError), error);
            AddAction( ()=> { instance.OnSDKCreateFailed.Invoke(bindError); });
             onSDKInitFailed((int)ArctopSDK.ResponseCodes.NotInitialized);
        }
        
        private void androidInitSDK()
        {
            ArctopNativePlugin.arctopSDKInit(m_ApiKey, onSDKInit, onSDKInitFailed);
        }
        
        
#endif

#if UNITY_EDITOR

        private void LateUpdate()
        {
            if (m_SimulateRealtime)
            {
                realtimeSimulation();
            }

            if (m_SimulateSignalQuality)
            {
                signalQualitySimulation();
            }
        }

        private void realtimeSimulation()
        {
            //"heart_rate";
            //"zone_state";
            //"focus";
            //"enjoyment";
            //"avg_motion";
            if (m_currentDebugConnection != ArctopSDK.ConnectionState.Connected) return;
            if ((Time.frameCount - m_lastPredictionFrame) % 60 != 0) return;
            onValueChangedCallback("heart_rate" , Random.value * 60 + 60);
            onValueChangedCallback("focus" , Random.value * 60 + 40);
            onValueChangedCallback("enjoyment" , Random.value * 60 + 40);
            onQAStatusCallback(true , -1);
        }

        private float[] m_signalQualityValues = new[] { 113.0f, 113.0f, 113.0f, 113.0f };
        
        
        private int m_lastFrame = 0;
        private int m_lastPredictionFrame = 0;
        private void signalQualitySimulation()
        {
            if (m_currentDebugConnection == ArctopSDK.ConnectionState.Connected)
            {
                if ((Time.frameCount - m_lastFrame) % 120 != 0) return;
                for (int i = 0; i < m_signalQualityValues.Length; i++)
                {
                    m_signalQualityValues[i] -= 10.0f;
                    if (m_signalQualityValues[i] < 0)
                    {
                        m_signalQualityValues[i] = 0f;
                    }
                }

                onSignalQualityCallback(string.Join(',', m_signalQualityValues));
            }
            else
            {
                m_lastFrame = Time.frameCount;
                for (int i = 0; i < m_signalQualityValues.Length; i++)
                {
                    m_signalQualityValues[i] = 113.0f;
                }
                onSignalQualityCallback(string.Join(',', m_signalQualityValues));
            }

        }
#endif
        
        [MonoPInvokeCallback(typeof(ArctopNativePlugin.SuccessCallback))]
        private static void onSDKInit()
        {
            AddAction( ()=> { instance.OnSDKInitSuccess.Invoke(); });
        }
        [MonoPInvokeCallback(typeof(ArctopNativePlugin.FailureWithCodeCallback))]
        private static void onSDKInitFailed(int error)
        {
            AddAction( ()=> { instance.OnSDKInitFailed.Invoke(getResponse(error)); });
        }
        
        private void Update()
        {
            if (m_actions.TryDequeue(out var nextAction))
            {
                nextAction();
            }
        }

        private static void AddAction(Action next)
        {
            instance.m_actions.Enqueue(next);
        }

        private void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
#if UNITY_EDITOR

#elif UNITY_IOS
        ArctopNativePlugin.arctopSDKSetConnectionCallback(null);
        ArctopNativePlugin.arctopSDKSetValueChangedCallback(null);
        ArctopNativePlugin.arctopSDKSetQAStatusCallback(null);
        ArctopNativePlugin.arctopSDKSetSignalQualityCallback(null);
        if (HandleSDKDestroy)
        {
            ArctopNativePlugin.arctopSDKShutdown();
        }
#elif UNITY_ANDROID
        if (HandleSDKDestroy)
        {
            ArctopNativePlugin.arctopSDKShutdown();
        }
#endif
        }

        public void CheckUserLoggedIn()
        {
#if UNITY_EDITOR
            onUserLoggedInCheck(m_SimulatedUserLoggedInResponse == ArctopSDK.LoginStatus.LoggedIn);
#elif UNITY_IOS || UNITY_ANDROID
            ArctopNativePlugin.arctopSDKIsUserLoggedIn(onUserLoggedInCheck);
#endif
        }
        
        [MonoPInvokeCallback(typeof(ArctopNativePlugin.IsUserLoggedInCallback))]
        private static void onUserLoggedInCheck(bool loggedIn)
        {
            AddAction( ()=> { instance.IsUserLoggedIn.Invoke(loggedIn); });
        }

        public void GetUserCalibrationStatus()
        {
#if UNITY_EDITOR
            AddAction(() => { instance.OnCalibrationStatus.Invoke(m_SimulatedUserCalibrationResponse); });
#elif UNITY_IOS || UNITY_ANDROID
            ArctopNativePlugin.arctopSDKGetUserCalibrationStatus(onCalibrationStatusSuccess, onCalibrationStatusFailure );
#endif
        }

        [MonoPInvokeCallback(typeof(ArctopNativePlugin.SuccessWithIntCallback))]
        private static void onCalibrationStatusSuccess(int status)
        {
            var response = (ArctopSDK.UserCalibrationStatus)Enum.ToObject(typeof(ArctopSDK.UserCalibrationStatus), status);
            AddAction(() => { instance.OnCalibrationStatus.Invoke(response); });
        }
        
        [MonoPInvokeCallback(typeof(ArctopNativePlugin.FailureWithCodeCallback))]
        private static void onCalibrationStatusFailure(int error)
        {
            AddAction(() => { instance.OnCalibrationStatusError.Invoke(getResponse(error)); });
        }

        
        public void LoginUser(string otp = "")
        {
#if UNITY_EDITOR
            if (m_LoginSuccessfullyInEditor)
            {
                m_SimulatedUserLoggedInResponse = ArctopSDK.LoginStatus.LoggedIn;
                onLoginSuccess();
            }
            else
            {
                m_SimulatedUserLoggedInResponse = ArctopSDK.LoginStatus.NotLoggedIn;
                onLoginFailed((int)ArctopSDK.ResponseCodes.UnknownError);
            }
#elif UNITY_IOS 
            ArctopNativePlugin.arctopSDKLogin(otp,onLoginSuccess,onLoginFailed);
#elif UNITY_ANDROID
            ArctopNativePlugin.arctopSDKLogin(onLoginSuccess,onLoginFailed);
#endif
        }

        public void LogoutUser()
        {
#if UNITY_EDITOR
            if (m_LogoutSuccessfullyInEditor)
            {
                onUserLogout();
                m_SimulatedUserLoggedInResponse = ArctopSDK.LoginStatus.NotLoggedIn;
            }
            else
            {
                onUserLogoutFailed((int)ArctopSDK.ResponseCodes.UnknownError);
            }
#elif UNITY_IOS || UNITY_ANDROID
            ArctopNativePlugin.arctopSDKLogout(onUserLogout,onUserLogoutFailed);
#endif
            
        }

        [MonoPInvokeCallback(typeof(ArctopNativePlugin.SuccessCallback))]
        private static void onLoginSuccess()
        {
            AddAction(() => { instance.OnUserLoggedIn.Invoke(); });
        }
        [MonoPInvokeCallback(typeof(ArctopNativePlugin.FailureWithCodeCallback))]
        private static void onLoginFailed(int error)
        {
            AddAction(() => { instance.OnUserLoginFailed.Invoke(getResponse(error)); });
        }
        [MonoPInvokeCallback(typeof(ArctopNativePlugin.SuccessCallback))]
        private static void onUserLogout()
        {
            AddAction(() => { instance.OnUserLogout.Invoke(); });
        }
        [MonoPInvokeCallback(typeof(ArctopNativePlugin.FailureWithCodeCallback))]
        private static void onUserLogoutFailed(int error)
        {
            AddAction(() => { instance.OnUserLogoutFailed.Invoke(getResponse(error)); });
        }
        
        public void ScanForDevices()
        {
#if UNITY_EDITOR
            StartCoroutine(scanForMockDevices());
#elif UNITY_IOS || UNITY_ANDROID
            ArctopNativePlugin.arctopSDKScanForDevices(onDeviceDetected);
#endif
        }
        
#if UNITY_EDITOR
        private IEnumerator scanForMockDevices()
        {
            yield return new WaitForSeconds(1f);
            onDeviceDetected("Mock 1");
            yield return new WaitForSeconds(0.5f);
            onDeviceDetected("Mock 2");
        }

        private IEnumerator connectToMockDevice()
        {
            yield return new WaitForSeconds(0.5f);
            /*Connecting = 1,
            Connected = 2,*/
            onConnectionChanged(0, 1);
            yield return new WaitForSeconds(0.5f);
            onConnectionChanged(1, 2);
        }
        private IEnumerator disconnectMockDevice()
        {
            yield return new WaitForSeconds(0.5f);
            onConnectionChanged(2, 5);
        }

        private IEnumerator completeInEditorSession()
        {
            DisconnectDevice();
            yield return new WaitForSeconds(1f);
            onPredictionEndSuccess();
            yield return new WaitForSeconds(2f);
            onSessionComplete();
        }
#endif
        [MonoPInvokeCallback((typeof(ArctopNativePlugin.ScanResultCallback)))]
        private static void onDeviceDetected(string deviceId)
        {
            AddAction(
                () =>
                {
                    instance.m_headbandDevices.Add(deviceId);
                    instance.OnDeviceListUpdated.Invoke(instance.m_headbandDevices.ToArray());
                });
        }

        public void ConnectToDeviceId(string deviceId)
        {
#if UNITY_EDITOR
            StartCoroutine(connectToMockDevice());
#elif UNITY_IOS || UNITY_ANDROID
            ArctopNativePlugin.arctopSDKConnectToDeviceId(deviceId);
#endif
        }
        [MonoPInvokeCallback((typeof(ArctopNativePlugin.ConnectionStatusCallback)))]
        private static void onConnectionChanged(int previous,int current)
        {
            var prev = (ArctopSDK.ConnectionState)Enum.ToObject(typeof(ArctopSDK.ConnectionState), previous);
            var curr = (ArctopSDK.ConnectionState)Enum.ToObject(typeof(ArctopSDK.ConnectionState), current);
#if UNITY_EDITOR
            instance.m_currentDebugConnection = curr;
#endif
            AddAction(
                () =>
                {
                    instance.OnConnectionStateChanged.Invoke(prev, curr);
                });
        }
        
        public void StartPrediction(ArctopSDK.Predictions prediction)
        {
#if UNITY_EDITOR
            onPredictionStartSuccess();
            m_lastPredictionFrame = Time.frameCount;
#elif UNITY_IOS || UNITY_ANDROID
            var predictionName = prediction.ToString().ToLower();
            ArctopNativePlugin.arctopSDKStartPredictions(predictionName, onPredictionStartSuccess, onPredictionStartFailed);
#endif
        }
        
        [MonoPInvokeCallback(typeof(ArctopNativePlugin.SuccessCallback))]
        private static void onPredictionStartSuccess()
        {
            Debug.Log("onPredictionStart");
            AddAction(() => { instance.OnPredictionStart.Invoke(); });
        }
        
        [MonoPInvokeCallback(typeof(ArctopNativePlugin.FailureWithCodeCallback))]
        private static void onPredictionStartFailed(int error)
        {
            AddAction(() => { instance.OnPredictionStartFailed.Invoke(getResponse(error)); });
        }
        
        public void FinishPrediction()
        {
#if UNITY_EDITOR
            StartCoroutine(completeInEditorSession());
#elif UNITY_IOS || UNITY_ANDROID
            ArctopNativePlugin.arctopSDKEndPrediction(onPredictionEndSuccess, onPredictionEndFailed);
#endif
        }

        public void SetUserMarker(string data)
        {
#if UNITY_EDITOR
            Debug.Log($"Set marker {data}");
#elif UNITY_IOS || UNITY_ANDROID
            ArctopNativePlugin.arctopSDKWriteUserMarker(data);
#endif
        }
        
        [MonoPInvokeCallback(typeof(ArctopNativePlugin.SuccessCallback))]
        private static void onPredictionEndSuccess()
        {
            AddAction(() => { instance.OnPredictionEnd.Invoke(); });
            #if UNITY_IOS
            // On iOS, prediction end successfully also marks the end of the session
            // so to maintain code flow, this is called here
            onSessionComplete();
            #endif
        }
        
        [MonoPInvokeCallback(typeof(ArctopNativePlugin.FailureWithCodeCallback))]
        private static void onPredictionEndFailed(int error)
        {
            AddAction(() => { instance.OnPredictionEndFailed.Invoke(getResponse(error)); });
        }
        
        private static void onSessionComplete()
        {
            AddAction( ()=> { instance.OnSessionComplete.Invoke(); });
        }

       
        public void DisconnectDevice()
        {
#if UNITY_EDITOR
            StartCoroutine(disconnectMockDevice());
#elif UNITY_IOS || UNITY_ANDROID
            ArctopNativePlugin.arctopSDKDisconnectDevice();
#endif
        }
        [MonoPInvokeCallback(typeof(ArctopNativePlugin.QAStatusCallback))]
        private static void onQAStatusCallback(bool passed, int errorCode)
        {
            var code = (ArctopSDK.QAFailureType)Enum.ToObject(typeof(ArctopSDK.QAFailureType), errorCode);
            AddAction(() => { instance.OnQAStatus.Invoke(passed,code); });
        }
        [MonoPInvokeCallback(typeof(ArctopNativePlugin.ValueChangedCallback))]
        private static void onValueChangedCallback(string key, float value)
        {
            AddAction(() => { instance.OnRealtimeValue.Invoke(key,value); });
        }
        [MonoPInvokeCallback(typeof(ArctopNativePlugin.SignalQualityCallback))]
        private static void onSignalQualityCallback(string signalQuality)
        {
            AddAction(() => { instance.OnSignalQuality.Invoke(signalQuality); });
        }

        private static ArctopSDK.ResponseCodes getResponse(int value)
        {
            return (ArctopSDK.ResponseCodes)Enum.ToObject(typeof(ArctopSDK.ResponseCodes), value);
        }
        
    }
    
   
}