using Castle.DynamicProxy;
using Serilog;

namespace OnBoardingSystem.Service.Interceptors
{
    public class TaskServiceActionLogger : IInterceptor
    {
        /// <inheritdoc/>
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

                    var theTask = (Task)invocation.ReturnValue;
                    if (theTask.Exception != null)
                    {
                        throw new Exception("Task Invocation failed.", theTask.Exception);
                    }

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
