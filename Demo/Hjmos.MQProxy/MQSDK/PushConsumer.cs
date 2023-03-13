//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.12
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace Hjmos.MQProxy
{

public class PushConsumer : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal PushConsumer(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(PushConsumer obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~PushConsumer() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          ONSClient4CPPPINVOKE.delete_PushConsumer(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public PushConsumer() : this(ONSClient4CPPPINVOKE.new_PushConsumer(), true) {
    SwigDirectorConnect();
  }

  public virtual void start() {
    ONSClient4CPPPINVOKE.PushConsumer_start(swigCPtr);
  }

  public virtual void shutdown() {
    ONSClient4CPPPINVOKE.PushConsumer_shutdown(swigCPtr);
  }

  public virtual void subscribe(string topic, string subExpression, MessageListener listener) {
    ONSClient4CPPPINVOKE.PushConsumer_subscribe(swigCPtr, topic, subExpression, MessageListener.getCPtr(listener));
  }

  private void SwigDirectorConnect() {
    if (SwigDerivedClassHasMethod("start", swigMethodTypes0))
      swigDelegate0 = new SwigDelegatePushConsumer_0(SwigDirectorstart);
    if (SwigDerivedClassHasMethod("shutdown", swigMethodTypes1))
      swigDelegate1 = new SwigDelegatePushConsumer_1(SwigDirectorshutdown);
    if (SwigDerivedClassHasMethod("subscribe", swigMethodTypes2))
      swigDelegate2 = new SwigDelegatePushConsumer_2(SwigDirectorsubscribe);
    ONSClient4CPPPINVOKE.PushConsumer_director_connect(swigCPtr, swigDelegate0, swigDelegate1, swigDelegate2);
  }

  private bool SwigDerivedClassHasMethod(string methodName, global::System.Type[] methodTypes) {
    global::System.Reflection.MethodInfo methodInfo = this.GetType().GetMethod(methodName, global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.NonPublic | global::System.Reflection.BindingFlags.Instance, null, methodTypes, null);
    bool hasDerivedMethod = methodInfo.DeclaringType.IsSubclassOf(typeof(PushConsumer));
    return hasDerivedMethod;
  }

  private void SwigDirectorstart() {
    start();
  }

  private void SwigDirectorshutdown() {
    shutdown();
  }

  private void SwigDirectorsubscribe(string topic, string subExpression, global::System.IntPtr listener) {
    subscribe(topic, subExpression, (listener == global::System.IntPtr.Zero) ? null : new MessageListener(listener, false));
  }

  public delegate void SwigDelegatePushConsumer_0();
  public delegate void SwigDelegatePushConsumer_1();
  public delegate void SwigDelegatePushConsumer_2(string topic, string subExpression, global::System.IntPtr listener);

  private SwigDelegatePushConsumer_0 swigDelegate0;
  private SwigDelegatePushConsumer_1 swigDelegate1;
  private SwigDelegatePushConsumer_2 swigDelegate2;

  private static global::System.Type[] swigMethodTypes0 = new global::System.Type[] {  };
  private static global::System.Type[] swigMethodTypes1 = new global::System.Type[] {  };
  private static global::System.Type[] swigMethodTypes2 = new global::System.Type[] { typeof(string), typeof(string), typeof(MessageListener) };
}

}