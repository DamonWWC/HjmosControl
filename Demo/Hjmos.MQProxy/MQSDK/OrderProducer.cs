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

public class OrderProducer : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal OrderProducer(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(OrderProducer obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~OrderProducer() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          ONSClient4CPPPINVOKE.delete_OrderProducer(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public OrderProducer() : this(ONSClient4CPPPINVOKE.new_OrderProducer(), true) {
    SwigDirectorConnect();
  }

  public virtual void start() {
    ONSClient4CPPPINVOKE.OrderProducer_start(swigCPtr);
  }

  public virtual void shutdown() {
    ONSClient4CPPPINVOKE.OrderProducer_shutdown(swigCPtr);
  }

  public virtual SendResultONS send(Message msg, string shardingKey) {
    SendResultONS ret = new SendResultONS(ONSClient4CPPPINVOKE.OrderProducer_send(swigCPtr, Message.getCPtr(msg), shardingKey), true);
    if (ONSClient4CPPPINVOKE.SWIGPendingException.Pending) throw ONSClient4CPPPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private void SwigDirectorConnect() {
    if (SwigDerivedClassHasMethod("start", swigMethodTypes0))
      swigDelegate0 = new SwigDelegateOrderProducer_0(SwigDirectorstart);
    if (SwigDerivedClassHasMethod("shutdown", swigMethodTypes1))
      swigDelegate1 = new SwigDelegateOrderProducer_1(SwigDirectorshutdown);
    if (SwigDerivedClassHasMethod("send", swigMethodTypes2))
      swigDelegate2 = new SwigDelegateOrderProducer_2(SwigDirectorsend);
    ONSClient4CPPPINVOKE.OrderProducer_director_connect(swigCPtr, swigDelegate0, swigDelegate1, swigDelegate2);
  }

  private bool SwigDerivedClassHasMethod(string methodName, global::System.Type[] methodTypes) {
    global::System.Reflection.MethodInfo methodInfo = this.GetType().GetMethod(methodName, global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.NonPublic | global::System.Reflection.BindingFlags.Instance, null, methodTypes, null);
    bool hasDerivedMethod = methodInfo.DeclaringType.IsSubclassOf(typeof(OrderProducer));
    return hasDerivedMethod;
  }

  private void SwigDirectorstart() {
    start();
  }

  private void SwigDirectorshutdown() {
    shutdown();
  }

  private global::System.IntPtr SwigDirectorsend(global::System.IntPtr msg, string shardingKey) {
    return SendResultONS.getCPtr(send(new Message(msg, false), shardingKey)).Handle;
  }

  public delegate void SwigDelegateOrderProducer_0();
  public delegate void SwigDelegateOrderProducer_1();
  public delegate global::System.IntPtr SwigDelegateOrderProducer_2(global::System.IntPtr msg, string shardingKey);

  private SwigDelegateOrderProducer_0 swigDelegate0;
  private SwigDelegateOrderProducer_1 swigDelegate1;
  private SwigDelegateOrderProducer_2 swigDelegate2;

  private static global::System.Type[] swigMethodTypes0 = new global::System.Type[] {  };
  private static global::System.Type[] swigMethodTypes1 = new global::System.Type[] {  };
  private static global::System.Type[] swigMethodTypes2 = new global::System.Type[] { typeof(Message), typeof(string) };
}

}
