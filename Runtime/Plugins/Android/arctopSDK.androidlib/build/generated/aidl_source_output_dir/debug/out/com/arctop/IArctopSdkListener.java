/*
 * This file is auto-generated.  DO NOT MODIFY.
 */
package com.arctop;
/**
 * SDK Listener interface.
 * Provides callbacks from service into client
 */
public interface IArctopSdkListener extends android.os.IInterface
{
  /** Default implementation for IArctopSdkListener. */
  public static class Default implements com.arctop.IArctopSdkListener
  {
    /**
     * Reports headband connection status changes.
     * See {@link ArctopSDK#ConnectionState} for valid values
     * @param previousConnection the previous connection status
     * @param currentConnection the current connection status
     */
    @Override public void onConnectionChanged(int previousConnection, int currentConnection) throws android.os.RemoteException
    {
    }
    /**
     * Reports a value changed during prediciton.
     * @param key the value's key name {@link ArctopSDK#PredictionValues}
     * @param value the current value
     */
    @Override public void onValueChanged(java.lang.String key, float value) throws android.os.RemoteException
    {
    }
    /**
     * Reports QA status during prediction
     * See {@link ArctopSDK#QAFailureType}
     * @param passed did QA pass for current run
     * @param type if QA failed, provides the reason for failure
     */
    @Override public void onQAStatus(boolean passed, int type) throws android.os.RemoteException
    {
    }
    /** Notifies client that a running session has completed */
    @Override public void onSessionComplete() throws android.os.RemoteException
    {
    }
    /**
     * Callback for SDK errors encountered during opertion
     * see {@link ArctopSDK#ResponseCodes} for valid codes
     * @param errorCode the current error code
     * @param message extra data on the error
     */
    @Override public void onError(int errorCode, java.lang.String message) throws android.os.RemoteException
    {
    }
    @Override
    public android.os.IBinder asBinder() {
      return null;
    }
  }
  /** Local-side IPC implementation stub class. */
  public static abstract class Stub extends android.os.Binder implements com.arctop.IArctopSdkListener
  {
    /** Construct the stub at attach it to the interface. */
    public Stub()
    {
      this.attachInterface(this, DESCRIPTOR);
    }
    /**
     * Cast an IBinder object into an com.arctop.IArctopSdkListener interface,
     * generating a proxy if needed.
     */
    public static com.arctop.IArctopSdkListener asInterface(android.os.IBinder obj)
    {
      if ((obj==null)) {
        return null;
      }
      android.os.IInterface iin = obj.queryLocalInterface(DESCRIPTOR);
      if (((iin!=null)&&(iin instanceof com.arctop.IArctopSdkListener))) {
        return ((com.arctop.IArctopSdkListener)iin);
      }
      return new com.arctop.IArctopSdkListener.Stub.Proxy(obj);
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
        case TRANSACTION_onConnectionChanged:
        {
          int _arg0;
          _arg0 = data.readInt();
          int _arg1;
          _arg1 = data.readInt();
          this.onConnectionChanged(_arg0, _arg1);
          break;
        }
        case TRANSACTION_onValueChanged:
        {
          java.lang.String _arg0;
          _arg0 = data.readString();
          float _arg1;
          _arg1 = data.readFloat();
          this.onValueChanged(_arg0, _arg1);
          break;
        }
        case TRANSACTION_onQAStatus:
        {
          boolean _arg0;
          _arg0 = (0!=data.readInt());
          int _arg1;
          _arg1 = data.readInt();
          this.onQAStatus(_arg0, _arg1);
          break;
        }
        case TRANSACTION_onSessionComplete:
        {
          this.onSessionComplete();
          break;
        }
        case TRANSACTION_onError:
        {
          int _arg0;
          _arg0 = data.readInt();
          java.lang.String _arg1;
          _arg1 = data.readString();
          this.onError(_arg0, _arg1);
          break;
        }
        default:
        {
          return super.onTransact(code, data, reply, flags);
        }
      }
      return true;
    }
    private static class Proxy implements com.arctop.IArctopSdkListener
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
      /**
       * Reports headband connection status changes.
       * See {@link ArctopSDK#ConnectionState} for valid values
       * @param previousConnection the previous connection status
       * @param currentConnection the current connection status
       */
      @Override public void onConnectionChanged(int previousConnection, int currentConnection) throws android.os.RemoteException
      {
        android.os.Parcel _data = android.os.Parcel.obtain();
        try {
          _data.writeInterfaceToken(DESCRIPTOR);
          _data.writeInt(previousConnection);
          _data.writeInt(currentConnection);
          boolean _status = mRemote.transact(Stub.TRANSACTION_onConnectionChanged, _data, null, android.os.IBinder.FLAG_ONEWAY);
        }
        finally {
          _data.recycle();
        }
      }
      /**
       * Reports a value changed during prediciton.
       * @param key the value's key name {@link ArctopSDK#PredictionValues}
       * @param value the current value
       */
      @Override public void onValueChanged(java.lang.String key, float value) throws android.os.RemoteException
      {
        android.os.Parcel _data = android.os.Parcel.obtain();
        try {
          _data.writeInterfaceToken(DESCRIPTOR);
          _data.writeString(key);
          _data.writeFloat(value);
          boolean _status = mRemote.transact(Stub.TRANSACTION_onValueChanged, _data, null, android.os.IBinder.FLAG_ONEWAY);
        }
        finally {
          _data.recycle();
        }
      }
      /**
       * Reports QA status during prediction
       * See {@link ArctopSDK#QAFailureType}
       * @param passed did QA pass for current run
       * @param type if QA failed, provides the reason for failure
       */
      @Override public void onQAStatus(boolean passed, int type) throws android.os.RemoteException
      {
        android.os.Parcel _data = android.os.Parcel.obtain();
        try {
          _data.writeInterfaceToken(DESCRIPTOR);
          _data.writeInt(((passed)?(1):(0)));
          _data.writeInt(type);
          boolean _status = mRemote.transact(Stub.TRANSACTION_onQAStatus, _data, null, android.os.IBinder.FLAG_ONEWAY);
        }
        finally {
          _data.recycle();
        }
      }
      /** Notifies client that a running session has completed */
      @Override public void onSessionComplete() throws android.os.RemoteException
      {
        android.os.Parcel _data = android.os.Parcel.obtain();
        try {
          _data.writeInterfaceToken(DESCRIPTOR);
          boolean _status = mRemote.transact(Stub.TRANSACTION_onSessionComplete, _data, null, android.os.IBinder.FLAG_ONEWAY);
        }
        finally {
          _data.recycle();
        }
      }
      /**
       * Callback for SDK errors encountered during opertion
       * see {@link ArctopSDK#ResponseCodes} for valid codes
       * @param errorCode the current error code
       * @param message extra data on the error
       */
      @Override public void onError(int errorCode, java.lang.String message) throws android.os.RemoteException
      {
        android.os.Parcel _data = android.os.Parcel.obtain();
        try {
          _data.writeInterfaceToken(DESCRIPTOR);
          _data.writeInt(errorCode);
          _data.writeString(message);
          boolean _status = mRemote.transact(Stub.TRANSACTION_onError, _data, null, android.os.IBinder.FLAG_ONEWAY);
        }
        finally {
          _data.recycle();
        }
      }
    }
    static final int TRANSACTION_onConnectionChanged = (android.os.IBinder.FIRST_CALL_TRANSACTION + 0);
    static final int TRANSACTION_onValueChanged = (android.os.IBinder.FIRST_CALL_TRANSACTION + 1);
    static final int TRANSACTION_onQAStatus = (android.os.IBinder.FIRST_CALL_TRANSACTION + 2);
    static final int TRANSACTION_onSessionComplete = (android.os.IBinder.FIRST_CALL_TRANSACTION + 3);
    static final int TRANSACTION_onError = (android.os.IBinder.FIRST_CALL_TRANSACTION + 4);
  }
  public static final java.lang.String DESCRIPTOR = "com.arctop.IArctopSdkListener";
  /**
   * Reports headband connection status changes.
   * See {@link ArctopSDK#ConnectionState} for valid values
   * @param previousConnection the previous connection status
   * @param currentConnection the current connection status
   */
  public void onConnectionChanged(int previousConnection, int currentConnection) throws android.os.RemoteException;
  /**
   * Reports a value changed during prediciton.
   * @param key the value's key name {@link ArctopSDK#PredictionValues}
   * @param value the current value
   */
  public void onValueChanged(java.lang.String key, float value) throws android.os.RemoteException;
  /**
   * Reports QA status during prediction
   * See {@link ArctopSDK#QAFailureType}
   * @param passed did QA pass for current run
   * @param type if QA failed, provides the reason for failure
   */
  public void onQAStatus(boolean passed, int type) throws android.os.RemoteException;
  /** Notifies client that a running session has completed */
  public void onSessionComplete() throws android.os.RemoteException;
  /**
   * Callback for SDK errors encountered during opertion
   * see {@link ArctopSDK#ResponseCodes} for valid codes
   * @param errorCode the current error code
   * @param message extra data on the error
   */
  public void onError(int errorCode, java.lang.String message) throws android.os.RemoteException;
}
