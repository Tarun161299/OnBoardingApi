

namespace OnBoardingSystem.Service.Interceptors
{
    using System;
    using System.Linq;
    using Castle.DynamicProxy;
    using Serilog;

    public class ServiceActionLogger : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            try
            {
                if (invocation != null)
                {
                    Log.Debug(
                        "Calling method {0} with parameters {1}... ",
                        invocation.Method.Name,
                        string.Join(", ", invocation.Arguments.Select(a => (a ?? string.Empty).ToString()).ToArray()));

                    invocation.Proceed();

                    Log.Debug(
                        "Done with method {0}: result was {1}.",
                        invocation.Method.Name,
                        invocation.ReturnValue);
                }
                else
                {
                    throw new NullReferenceException();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                throw;
            }
        }
    }
}
