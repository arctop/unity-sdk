/*
 * This file is auto-generated.  DO NOT MODIFY.
 */
package com.arctop;
public interface IArctopSdk extends android.os.IInterface
{
  /** Default implementation for IArctopSdk. */
  public static class Default implements com.arctop.IArctopSdk
  {
    @Override public int initializeArctop(java.lang.String apiKey) throws android.os.RemoteException
    {
      return 0;
    }
    /** Shuts down the sdk and releases resources */
    @Override public void shutdownSdk() throws android.os.RemoteException
    {
    }
    /**
     * Retrieves the user's login status.
     * @return int value from {@link ArctopSDK#LoginStatus}
     */
    @Override public int getUserLoginStatus() throws android.os.RemoteException
    {
      return 0;
    }
    /**
     * Checks the current user's calibration status
     * only calibrated users with available models can run predictions
     * @return int value from {@link ArctopSDK#UserCalibrationStatus}
     */
    @Override public int checkUserCalibrationStatus() throws android.os.RemoteException
    {
      return 0;
    }
    /**
     * Begins a prediction session for the desired prediction
     * @param predictionName the prediction component's name / key to run
     * @return int value from {@link ArctopSDK#ResponseCodes}
     */
    @Override public int startPredictionSession(java.lang.String predictionName) throws android.os.RemoteException
    {
      return 0;
    }
    /**
     * Finishes a running prediction session.
     * This will close out all the data files and upload them to arctopCloud
     * calls {@link IArctopSdkListener#onSessionComplete()} once the operation completed
     * the return code only pertains to the session close functionality, and is used to validate
     * that your app's call was accepted. You should still listen for the callback to complete.
     * @return int value from {@link ArctopSDK#ResponseCodes}
     */
    @Override public int finishSession() throws android.os.RemoteException
    {
      return 0;
    }
    /**
     * Requests a marker to be written into the current session's data files
     * Markers will be written with current timestamp
     * @param markerId numerical identifier of marker
     * @param line extra data line, can be plain text or JSON encoded values
     */
    @Override public void writeMarker(int markerId, java.lang.String line) throws android.os.RemoteException
    {
    }
    /**
     * Requests a marker to be written into the current session's data files with a specified timestamp
     * @param markerId numerical identifier of marker
     * @param line extra data line, can be plain text or JSON encoded values
     * @param timeStamp unix time stamp in MS to use for marker
     */
    @Override public void writeTimedMarker(int markerId, java.lang.String line, long timeStamp) throws android.os.RemoteException
    {
    }
    /**
     * Registers for SDK callbacks
     * @param listener IArctopSdkListener implementation
     * @return int value from {@link ArctopSDK#ResponseCodes}
     */
    @Override public int registerSDKCallback(com.arctop.IArctopSdkListener listener) throws android.os.RemoteException
    {
      return 0;
    }
    /**
     * Unregisters from SDK callbacks
     * @param listener previously registered listener
     */
    @Override public void unregisterSDKCallback(com.arctop.IArctopSdkListener listener) throws android.os.RemoteException
    {
    }
    /**
     * Requests connection to a sensor device via it's MAC Address
     * connection status is reported back via {@link IArctopSdkListener#onConnectionChanged(int previousConnection ,int currentConnection)}
     * @param macAddress the device's MAC address to attempt connection to
     */
    @Override public void connectSensorDevice(java.lang.String macAddress) throws android.os.RemoteException
    {
    }
    /**
     * Requests a disconnect from currently connected sensor device
     * connection status is reported back via {@link IArctopSdkListener#onConnectionChanged(int previousConnection ,int currentConnection)}
     */
    @Override public void disconnectSensorDevice() throws android.os.RemoteException
    {
    }
    @Override
    public android.os.IBinder asBinder() {
      return null;
    }
  }
  /** Local-side IPC implementation stub class. */
  public static abstract class Stub extends android.os.Binder implements com.arctop.IArctopSdk
  {
    /** Construct the stub at attach it to the interface. */
    public Stub()
    {
      this.attachInterface(this, DESCRIPTOR);
    }
    /**
     * Cast an IBinder object into an com.arctop.IArctopSdk interface,
     * generating a proxy if needed.
     */
    public static com.arctop.IArctopSdk asInterface(android.os.IBinder obj)
    {
      if ((obj==null)) {
        return null;
      }
      android.os.IInterface iin = obj.queryLocalInterface(DESCRIPTOR);
      if (((iin!=null)&&(iin instanceof com.arctop.IArctopSdk))) {
        return ((com.arctop.IArctopSdk)iin);
      }
      return new com.arctop.IArctopSdk.Stub.Proxy(obj);
    }
    @Override public android.os.IBinder asBinder()
    {
      return this;
    }
    @Override public boolean onTransact(int code, android.os.Parcel data, android.os.Parcel reply, int flags) throws android.os.RemoteException
    {
      java.lang.String descriptor = DESCRIPTOR;
      if (code >= android.os.IBinder.FIRST_CALL_TRANSACTION && code <= android.os.IBinder.LAST_CALL_TRANSACTION) {
        data.enforceInterface(descriptor);
      }
      switch (code)
      {
        case INTERFACE_TRANSACTION:
        {
          reply.writeString(descriptor);
          return true;
        }
      }
      switch (code)
      {
        case TRANSACTION_initializeArctop:
        {
          java.lang.String _arg0;
          _arg0 = data.readString();
          int _result = this.initializeArctop(_arg0);
          reply.writeNoException();
          reply.writeInt(_result);
          break;
        }
        case TRANSACTION_shutdownSdk:
        {
          this.shutdownSdk();
          reply.writeNoException();
          break;
        }
        case TRANSACTION_getUserLoginStatus:
        {
          int _result = this.getUserLoginStatus();
          reply.writeNoException();
          reply.writeInt(_result);
          break;
        }
        case TRANSACTION_checkUserCalibrationStatus:
        {
          int _result = this.checkUserCalibrationStatus();
          reply.writeNoException();
          reply.writeInt(_result);
          break;
        }
        case TRANSACTION_startPredictionSession:
        {
          java.lang.String _arg0;
          _arg0 = data.readString();
          int _result = this.startPredictionSession(_arg0);
          reply.writeNoException();
          reply.writeInt(_result);
          break;
        }
        case TRANSACTION_finishSession:
        {
          int _result = this.finishSession();
          reply.writeNoException();
          reply.writeInt(_result);
          break;
        }
        case TRANSACTION_writeMarker:
        {
          int _arg0;
          _arg0 = data.readInt();
          java.lang.String _arg1;
          _arg1 = data.readString();
          this.writeMarker(_arg0, _arg1);
          reply.writeNoException();
          break;
        }
        case TRANSACTION_writeTimedMarker:
        {
          int _arg0;
          _arg0 = data.readInt();
          java.lang.String _arg1;
          _arg1 = data.readString();
          long _arg2;
          _arg2 = data.readLong();
          this.writeTimedMarker(_arg0, _arg1, _arg2);
          reply.writeNoException();
          break;
        }
        case TRANSACTION_registerSDKCallback:
        {
          com.arctop.IArctopSdkListener _arg0;
          _arg0 = com.arctop.IArctopSdkListener.Stub.asInterface(data.readStrongBinder());
          int _result = this.registerSDKCallback(_arg0);
          reply.writeNoException();
          reply.writeInt(_result);
          break;
        }
        case TRANSACTION_unregisterSDKCallback:
        {
          com.arctop.IArctopSdkListener _arg0;
          _arg0 = com.arctop.IArctopSdkListener.Stub.asInterface(data.readStrongBinder());
          this.unregisterSDKCallback(_arg0);
          reply.writeNoException();
          break;
        }
        case TRANSACTION_connectSensorDevice:
        {
          java.lang.String _arg0;
          _arg0 = data.readString();
          this.connectSensorDevice(_arg0);
          break;
        }
        case TRANSACTION_disconnectSensorDevice:
        {
          this.disconnectSensorDevice();
          break;
        }
        default:
        {
          return super.onTransact(code, data, reply, flags);
        }
      }
      return true;
    }
    private static class Proxy implements com.arctop.IArctopSdk
    {
      private android.os.IBinder mRemote;
      Proxy(android.os.IBinder remote)
      {
        mRemote = remote;
      }
      @Override public android.os.IBinder asBinder()
      {
        return mRemote;
      }
      public java.lang.String getInterfaceDescriptor()
      {
        return DESCRIPTOR;
      }
      @Override public int initializeArctop(java.lang.String apiKey) throws android.os.RemoteException
      {
        android.os.Parcel _data = android.os.Parcel.obtain();
        android.os.Parcel _reply = android.os.Parcel.obtain();
        int _result;
        try {
          _data.writeInterfaceToken(DESCRIPTOR);
          _data.writeString(apiKey);
          boolean _status = mRemote.transact(Stub.TRANSACTION_initializeArctop, _data, _reply, 0);
          _reply.readException();
          _result = _reply.readInt();
        }
        finally {
          _reply.recycle();
          _data.recycle();
        }
        return _result;
      }
      /** Shuts down the sdk and releases resources */
      @Override public void shutdownSdk() throws android.os.RemoteException
      {
        android.os.Parcel _data = android.os.Parcel.obtain();
        android.os.Parcel _reply = android.os.Parcel.obtain();
        try {
          _data.writeInterfaceToken(DESCRIPTOR);
          boolean _status = mRemote.transact(Stub.TRANSACTION_shutdownSdk, _data, _reply, 0);
          _reply.readException();
        }
        finally {
          _reply.recycle();
          _data.recycle();
        }
      }
      /**
       * Retrieves the user's login status.
       * @return int value from {@link ArctopSDK#LoginStatus}
       */
      @Override public int getUserLoginStatus() throws android.os.RemoteException
      {
        android.os.Parcel _data = android.os.Parcel.obtain();
        android.os.Parcel _reply = android.os.Parcel.obtain();
        int _result;
        try {
          _data.writeInterfaceToken(DESCRIPTOR);
          boolean _status = mRemote.transact(Stub.TRANSACTION_getUserLoginStatus, _data, _reply, 0);
          _reply.readException();
          _result = _reply.readInt();
        }
        finally {
          _reply.recycle();
          _data.recycle();
        }
        return _result;
      }
      /**
       * Checks the current user's calibration status
       * only calibrated users with available models can run predictions
       * @return int value from {@link ArctopSDK#UserCalibrationStatus}
       */
      @Override public int checkUserCalibrationStatus() throws android.os.RemoteException
      {
        android.os.Parcel _data = android.os.Parcel.obtain();
        android.os.Parcel _reply = android.os.Parcel.obtain();
        int _result;
        try {
          _data.writeInterfaceToken(DESCRIPTOR);
          boolean _status = mRemote.transact(Stub.TRANSACTION_checkUserCalibrationStatus, _data, _reply, 0);
          _reply.readException();
          _result = _reply.readInt();
        }
        finally {
          _reply.recycle();
          _data.recycle();
        }
        return _result;
      }
      /**
       * Begins a prediction session for the desired prediction
       * @param predictionName the prediction component's name / key to run
       * @return int value from {@link ArctopSDK#ResponseCodes}
       */
      @Override public int startPredictionSession(java.lang.String predictionName) throws android.os.RemoteException
      {
        android.os.Parcel _data = android.os.Parcel.obtain();
        android.os.Parcel _reply = android.os.Parcel.obtain();
        int _result;
        try {
          _data.writeInterfaceToken(DESCRIPTOR);
          _data.writeString(predictionName);
          boolean _status = mRemote.transact(Stub.TRANSACTION_startPredictionSession, _data, _reply, 0);
          _reply.readException();
          _result = _reply.readInt();
        }
        finally {
          _reply.recycle();
          _data.recycle();
        }
        return _result;
      }
      /**
       * Finishes a running prediction session.
       * This will close out all the data files and upload them to arctopCloud
       * calls {@link IArctopSdkListener#onSessionComplete()} once the operation completed
       * the return code only pertains to the session close functionality, and is used to validate
       * that your app's call was accepted. You should still listen for the callback to complete.
       * @return int value from {@link ArctopSDK#ResponseCodes}
       */
      @Override public int finishSession() throws android.os.RemoteException
      {
        android.os.Parcel _data = android.os.Parcel.obtain();
        android.os.Parcel _reply = android.os.Parcel.obtain();
        int _result;
        try {
          _data.writeInterfaceToken(DESCRIPTOR);
          boolean _status = mRemote.transact(Stub.TRANSACTION_finishSession, _data, _reply, 0);
          _reply.readException();
          _result = _reply.readInt();
        }
        finally {
          _reply.recycle();
          _data.recycle();
        }
        return _result;
      }
      /**
       * Requests a marker to be written into the current session's data files
       * Markers will be written with current timestamp
       * @param markerId numerical identifier of marker
       * @param line extra data line, can be plain text or JSON encoded values
       */
      @Override public void writeMarker(int markerId, java.lang.String line) throws android.os.RemoteException
      {
        android.os.Parcel _data = android.os.Parcel.obtain();
        android.os.Parcel _reply = android.os.Parcel.obtain();
        try {
          _data.writeInterfaceToken(DESCRIPTOR);
          _data.writeInt(markerId);
          _data.writeString(line);
          boolean _status = mRemote.transact(Stub.TRANSACTION_writeMarker, _data, _reply, 0);
          _reply.readException();
        }
        finally {
          _reply.recycle();
          _data.recycle();
        }
      }
      /**
       * Requests a marker to be written into the current session's data files with a specified timestamp
       * @param markerId numerical identifier of marker
       * @param line extra data line, can be plain text or JSON encoded values
       * @param timeStamp unix time stamp in MS to use for marker
       */
      @Override public void writeTimedMarker(int markerId, java.lang.String line, long timeStamp) throws android.os.RemoteException
      {
        android.os.Parcel _data = android.os.Parcel.obtain();
        android.os.Parcel _reply = android.os.Parcel.obtain();
        try {
          _data.writeInterfaceToken(DESCRIPTOR);
          _data.writeInt(markerId);
          _data.writeString(line);
          _data.writeLong(timeStamp);
          boolean _status = mRemote.transact(Stub.TRANSACTION_writeTimedMarker, _data, _reply, 0);
          _reply.readException();
        }
        finally {
          _reply.recycle();
          _data.recycle();
        }
      }
      /**
       * Registers for SDK callbacks
       * @param listener IArctopSdkListener implementation
       * @return int value from {@link ArctopSDK#ResponseCodes}
       */
      @Override public int registerSDKCallback(com.arctop.IArctopSdkListener listener) throws android.os.RemoteException
      {
        android.os.Parcel _data = android.os.Parcel.obtain();
        android.os.Parcel _reply = android.os.Parcel.obtain();
        int _result;
        try {
          _data.writeInterfaceToken(DESCRIPTOR);
          _data.writeStrongInterface(listener);
          boolean _status = mRemote.transact(Stub.TRANSACTION_registerSDKCallback, _data, _reply, 0);
          _reply.readException();
          _result = _reply.readInt();
        }
        finally {
          _reply.recycle();
          _data.recycle();
        }
        return _result;
      }
      /**
       * Unregisters from SDK callbacks
       * @param listener previously registered listener
       */
      @Override public void unregisterSDKCallback(com.arctop.IArctopSdkListener listener) throws android.os.RemoteException
      {
        android.os.Parcel _data = android.os.Parcel.obtain();
        android.os.Parcel _reply = android.os.Parcel.obtain();
        try {
          _data.writeInterfaceToken(DESCRIPTOR);
          _data.writeStrongInterface(listener);
          boolean _status = mRemote.transact(Stub.TRANSACTION_unregisterSDKCallback, _data, _reply, 0);
          _reply.readException();
        }
        finally {
          _reply.recycle();
          _data.recycle();
        }
      }
      /**
       * Requests connection to a sensor device via it's MAC Address
       * connection status is reported back via {@link IArctopSdkListener#onConnectionChanged(int previousConnection ,int currentConnection)}
       * @param macAddress the device's MAC address to attempt connection to
       */
      @Override public void connectSensorDevice(java.lang.String macAddress) throws android.os.RemoteException
      {
        android.os.Parcel _data = android.os.Parcel.obtain();
        try {
          _data.writeInterfaceToken(DESCRIPTOR);
          _data.writeString(macAddress);
          boolean _status = mRemote.transact(Stub.TRANSACTION_connectSensorDevice, _data, null, android.os.IBinder.FLAG_ONEWAY);
        }
        finally {
          _data.recycle();
        }
      }
      /**
       * Requests a disconnect from currently connected sensor device
       * connection status is reported back via {@link IArctopSdkListener#onConnectionChanged(int previousConnection ,int currentConnection)}
       */
      @Override public void disconnectSensorDevice() throws android.os.RemoteException
      {
        android.os.Parcel _data = android.os.Parcel.obtain();
        try {
          _data.writeInterfaceToken(DESCRIPTOR);
          boolean _status = mRemote.transact(Stub.TRANSACTION_disconnectSensorDevice, _data, null, android.os.IBinder.FLAG_ONEWAY);
        }
        finally {
          _data.recycle();
        }
      }
    }
    static final int TRANSACTION_initializeArctop = (android.os.IBinder.FIRST_CALL_TRANSACTION + 0);
    static final int TRANSACTION_shutdownSdk = (android.os.IBinder.FIRST_CALL_TRANSACTION + 1);
    static final int TRANSACTION_getUserLoginStatus = (android.os.IBinder.FIRST_CALL_TRANSACTION + 2);
    static final int TRANSACTION_checkUserCalibrationStatus = (android.os.IBinder.FIRST_CALL_TRANSACTION + 3);
    static final int TRANSACTION_startPredictionSession = (android.os.IBinder.FIRST_CALL_TRANSACTION + 4);
    static final int TRANSACTION_finishSession = (android.os.IBinder.FIRST_CALL_TRANSACTION + 5);
    static final int TRANSACTION_writeMarker = (android.os.IBinder.FIRST_CALL_TRANSACTION + 6);
    static final int TRANSACTION_writeTimedMarker = (android.os.IBinder.FIRST_CALL_TRANSACTION + 7);
    static final int TRANSACTION_registerSDKCallback = (android.os.IBinder.FIRST_CALL_TRANSACTION + 8);
    static final int TRANSACTION_unregisterSDKCallback = (android.os.IBinder.FIRST_CALL_TRANSACTION + 9);
    static final int TRANSACTION_connectSensorDevice = (android.os.IBinder.FIRST_CALL_TRANSACTION + 10);
    static final int TRANSACTION_disconnectSensorDevice = (android.os.IBinder.FIRST_CALL_TRANSACTION + 11);
  }
  public static final java.lang.String DESCRIPTOR = "com.arctop.IArctopSdk";
  public int initializeArctop(java.lang.String apiKey) throws android.os.RemoteException;
  /** Shuts down the sdk and releases resources */
  public void shutdownSdk() throws android.os.RemoteException;
  /**
   * Retrieves the user's login status.
   * @return int value from {@link ArctopSDK#LoginStatus}
   */
  public int getUserLoginStatus() throws android.os.RemoteException;
  /**
   * Checks the current user's calibration status
   * only calibrated users with available models can run predictions
   * @return int value from {@link ArctopSDK#UserCalibrationStatus}
   */
  public int checkUserCalibrationStatus() throws android.os.RemoteException;
  /**
   * Begins a prediction session for the desired prediction
   * @param predictionName the prediction component's name / key to run
   * @return int value from {@link ArctopSDK#ResponseCodes}
   */
  public int startPredictionSession(java.lang.String predictionName) throws android.os.RemoteException;
  /**
   * Finishes a running prediction session.
   * This will close out all the data files and upload them to arctopCloud
   * calls {@link IArctopSdkListener#onSessionComplete()} once the operation completed
   * the return code only pertains to the session close functionality, and is used to validate
   * that your app's call was accepted. You should still listen for the callback to complete.
   * @return int value from {@link ArctopSDK#ResponseCodes}
   */
  public int finishSession() throws android.os.RemoteException;
  /**
   * Requests a marker to be written into the current session's data files
   * Markers will be written with current timestamp
   * @param markerId numerical identifier of marker
   * @param line extra data line, can be plain text or JSON encoded values
   */
  public void writeMarker(int markerId, java.lang.String line) throws android.os.RemoteException;
  /**
   * Requests a marker to be written into the current session's data files with a specified timestamp
   * @param markerId numerical identifier of marker
   * @param line extra data line, can be plain text or JSON encoded values
   * @param timeStamp unix time stamp in MS to use for marker
   */
  public void writeTimedMarker(int markerId, java.lang.String line, long timeStamp) throws android.os.RemoteException;
  /**
   * Registers for SDK callbacks
   * @param listener IArctopSdkListener implementation
   * @return int value from {@link ArctopSDK#ResponseCodes}
   */
  public int registerSDKCallback(com.arctop.IArctopSdkListener listener) throws android.os.RemoteException;
  /**
   * Unregisters from SDK callbacks
   * @param listener previously registered listener
   */
  public void unregisterSDKCallback(com.arctop.IArctopSdkListener listener) throws android.os.RemoteException;
  /**
   * Requests connection to a sensor device via it's MAC Address
   * connection status is reported back via {@link IArctopSdkListener#onConnectionChanged(int previousConnection ,int currentConnection)}
   * @param macAddress the device's MAC address to attempt connection to
   */
  public void connectSensorDevice(java.lang.String macAddress) throws android.os.RemoteException;
  /**
   * Requests a disconnect from currently connected sensor device
   * connection status is reported back via {@link IArctopSdkListener#onConnectionChanged(int previousConnection ,int currentConnection)}
   */
  public void disconnectSensorDevice() throws android.os.RemoteException;
}
