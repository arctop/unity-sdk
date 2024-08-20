using System;

namespace com.arctop
{
    /// <summary>
    /// Helper static class holding constants used throughout the Arctop SDK
    /// </summary>
    public static class ArctopSDK
    {   
        /// <summary>
        /// Permission name used in Android
        /// </summary>
        public const string ARCTOP_PERMISSION = "com.arctop.permission.ARCTOP_DATA";
        /// <summary>
        /// Connection status of headband
        /// </summary>
        [Serializable]
        public enum ConnectionState {
            Unknown,
            Connecting,
            Connected,
            ConnectionFailed,
            Disconnected,
            DisconnectedUponRequest
        }
        /// <summary>
        /// QA Failure type, for realtime QA callbacks
        /// </summary>
        [Serializable]
        public enum QAFailureType
        {
            Passed,
            HeadbandOffHead,
            MotionTooHigh,
            EEGFailure
        }
        /// <summary>
        /// SDK Bind errors for Android
        /// </summary>
        [Serializable]
        public enum BindError
        {
            /// <summary>
            /// Denotes that the SDK service wasn't found.
            /// This means that the Arctop app isn't installed on the device
            /// </summary>
            ServiceNotFound,
            /// <summary>
            /// Should never really be an issue, but somehow, the system found multiple services
            /// registered to handle the Arctop signature.
            /// </summary>
            MultipleServicesFound,
            /// <summary>
            /// Permission to query services was denied, check your manifest
            /// </summary>
            PermissionDenied,
            UnknownError
        }
        /// <summary>
        /// User calibration status responses
        /// </summary>
        [Serializable]
        public enum UserCalibrationStatus 
        {
            /// <summary>
            /// The user is not calibrated, direct them to the Arctop app for calibration
            /// </summary>
            NeedsCalibration,
            /// <summary>
            /// Calibration has been completed and is in process, models should be available
            /// within a few minutes
            /// </summary>
            CalibrationDone,
            /// <summary>
            /// Models are available and predictions can be run
            /// </summary>
            ModelsAvailable,
            /// <summary>
            /// User has been blocked from the system
            /// </summary>
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