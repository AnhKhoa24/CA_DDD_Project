using System.Reflection;
using ErrorOr;
using MediatR;

namespace Application.Common.Behaviors;
internal static class PipelineReflectionHelper
{
   public static async Task<TResponse> InvokeWithGenericInnerType<TRequest, TResponse>(
       object instance,
       string methodName,
       TRequest request,
       RequestHandlerDelegate<TResponse> next,
       CancellationToken cancellationToken)
   {
      var responseType = typeof(TResponse);

      if (!responseType.IsGenericType || responseType.GetGenericTypeDefinition() != typeof(ErrorOr<>))
         return await next(); 

      var actualType = responseType.GetGenericArguments()[0];

      var method = instance
          .GetType()
          .GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic)!
          .MakeGenericMethod(actualType);

      var task = (Task<TResponse>)method.Invoke(instance, new object[] { request!, next, cancellationToken })!;
      return await task;
   }
}
