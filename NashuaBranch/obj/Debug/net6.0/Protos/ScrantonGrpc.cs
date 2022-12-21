// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/scranton.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace ScrantonBranch {
  public static partial class ScrantonGrpcService
  {
    static readonly string __ServiceName = "ScrantonProducts.ScrantonGrpcService";

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ScrantonBranch.OrderedProducts> __Marshaller_ScrantonProducts_OrderedProducts = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ScrantonBranch.OrderedProducts.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ScrantonBranch.CheckResult> __Marshaller_ScrantonProducts_CheckResult = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ScrantonBranch.CheckResult.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ScrantonBranch.NOrdersRequest> __Marshaller_ScrantonProducts_NOrdersRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ScrantonBranch.NOrdersRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ScrantonBranch.NOrders> __Marshaller_ScrantonProducts_NOrders = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ScrantonBranch.NOrders.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::ScrantonBranch.OrderedProducts, global::ScrantonBranch.CheckResult> __Method_PlaceOrder = new grpc::Method<global::ScrantonBranch.OrderedProducts, global::ScrantonBranch.CheckResult>(
        grpc::MethodType.Unary,
        __ServiceName,
        "PlaceOrder",
        __Marshaller_ScrantonProducts_OrderedProducts,
        __Marshaller_ScrantonProducts_CheckResult);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::ScrantonBranch.NOrdersRequest, global::ScrantonBranch.NOrders> __Method_GetOrderNumber = new grpc::Method<global::ScrantonBranch.NOrdersRequest, global::ScrantonBranch.NOrders>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetOrderNumber",
        __Marshaller_ScrantonProducts_NOrdersRequest,
        __Marshaller_ScrantonProducts_NOrders);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::ScrantonBranch.ScrantonReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of ScrantonGrpcService</summary>
    [grpc::BindServiceMethod(typeof(ScrantonGrpcService), "BindService")]
    public abstract partial class ScrantonGrpcServiceBase
    {
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::ScrantonBranch.CheckResult> PlaceOrder(global::ScrantonBranch.OrderedProducts request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::ScrantonBranch.NOrders> GetOrderNumber(global::ScrantonBranch.NOrdersRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static grpc::ServerServiceDefinition BindService(ScrantonGrpcServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_PlaceOrder, serviceImpl.PlaceOrder)
          .AddMethod(__Method_GetOrderNumber, serviceImpl.GetOrderNumber).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static void BindService(grpc::ServiceBinderBase serviceBinder, ScrantonGrpcServiceBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_PlaceOrder, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::ScrantonBranch.OrderedProducts, global::ScrantonBranch.CheckResult>(serviceImpl.PlaceOrder));
      serviceBinder.AddMethod(__Method_GetOrderNumber, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::ScrantonBranch.NOrdersRequest, global::ScrantonBranch.NOrders>(serviceImpl.GetOrderNumber));
    }

  }
}
#endregion