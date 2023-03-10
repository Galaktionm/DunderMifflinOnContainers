// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: product.proto
// </auto-generated>
#pragma warning disable 0414, 1591, 8981
#region Designer generated code

using grpc = global::Grpc.Core;

namespace ProductService {
  public static partial class ProductGrpcProtoService
  {
    static readonly string __ServiceName = "Products.ProductGrpcProtoService";

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
    static readonly grpc::Marshaller<global::ProductService.OrderedProducts> __Marshaller_Products_OrderedProducts = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProductService.OrderedProducts.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ProductService.CheckResult> __Marshaller_Products_CheckResult = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProductService.CheckResult.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::ProductService.OrderedProducts, global::ProductService.CheckResult> __Method_CheckAvailableProducts = new grpc::Method<global::ProductService.OrderedProducts, global::ProductService.CheckResult>(
        grpc::MethodType.Unary,
        __ServiceName,
        "CheckAvailableProducts",
        __Marshaller_Products_OrderedProducts,
        __Marshaller_Products_CheckResult);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::ProductService.ProductReflection.Descriptor.Services[0]; }
    }

    /// <summary>Client for ProductGrpcProtoService</summary>
    public partial class ProductGrpcProtoServiceClient : grpc::ClientBase<ProductGrpcProtoServiceClient>
    {
      /// <summary>Creates a new client for ProductGrpcProtoService</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public ProductGrpcProtoServiceClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for ProductGrpcProtoService that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public ProductGrpcProtoServiceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected ProductGrpcProtoServiceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected ProductGrpcProtoServiceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::ProductService.CheckResult CheckAvailableProducts(global::ProductService.OrderedProducts request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return CheckAvailableProducts(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::ProductService.CheckResult CheckAvailableProducts(global::ProductService.OrderedProducts request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_CheckAvailableProducts, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::ProductService.CheckResult> CheckAvailableProductsAsync(global::ProductService.OrderedProducts request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return CheckAvailableProductsAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::ProductService.CheckResult> CheckAvailableProductsAsync(global::ProductService.OrderedProducts request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_CheckAvailableProducts, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected override ProductGrpcProtoServiceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new ProductGrpcProtoServiceClient(configuration);
      }
    }

  }
}
#endregion
