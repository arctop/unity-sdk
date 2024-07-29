using System;

namespace com.arctop
{
    public static class ArctopSDK
    {   
        
        public const string ARCTOP_PERMISSION = "com.arctop.permission.ARCTOP_DATA";
        [Serializable]
        public enum ConnectionState {
            Unknown,
            Connecting,
            Connected,
            ConnectionFailed,
            Disconnected,
            DisconnectedUponRequest
        }
        [Serializable]
        public enum QAFailureType
        {
            Passed,
            HeadbandOffHead,
            MotionTooHigh,
            EEGFailure
        }
        [Serializable]
        public enum BindError
        {
            ServiceNotFound,
            MultipleServicesFound,
            PermissionDenied,
            UnknownError
        }
        [Serializable]
        public enum UserCalibrationStatus 
        {
            NeedsCalibration,
            CalibrationDone,
            ModelsAvailable,
            Blocked
        }
        [Serializable]
        public enum ResponseCodes
        {
            UnknownError = -200,
            NotAllowed = -100,
            Success = 0,
            NotInitialized = -1,
            AlreadyInitialized = -2,
            APIKeyError = -3,
            ModelDownloadError = -4,
            SessionUpdateFailure = -5,
            SessionUploadFailure = -6,
            UserNotLoggedIn = -7,
            CheckCalibrationFailed = -8,
            SessionCreateFailure = -9,
            ServerConnectionError = -10,
            ModelsNotAvailable = -11,
            PredictionNotAvailable = -12,
        }
        [Serializable]
        public enum LoginStatus
        {
            NotLoggedIn = 0,
            LoggedIn = 1
        }

        [Serializable]
        public enum Predictions
        {
            ZONE,
            GAME_ZONE,
            SLEEP
        }
    }
}