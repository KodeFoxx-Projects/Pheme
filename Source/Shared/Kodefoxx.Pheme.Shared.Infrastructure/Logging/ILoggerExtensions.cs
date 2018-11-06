using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;

namespace Kodefoxx.Pheme.Shared.Infrastructure.Logging
{
    /// <summary>
    /// Extension methods used for logging.
    /// </summary>
    public static class ILoggerExtensions
    {
        /// <summary>
        /// Logs the method being entered.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> used.</param>
        /// <param name="callerMemberName">The method.</param>
        public static ILogger LogEnterMethod(this ILogger logger, [CallerMemberName] string callerMemberName = "")
        {
            logger.LogTrace($"Entered method '{callerMemberName}'.");
            return logger;
        }

        /// <summary>
        /// Logs the method being exited.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> used.</param>
        /// <param name="callerMemberName">The method.</param>
        public static ILogger LogExitMethod(this ILogger logger, [CallerMemberName] string callerMemberName = "")
        {
            logger.LogTrace($"Exited method '{callerMemberName}'.");
            return logger;
        }

        /// <summary>
        /// Logs an action wrapped withing a try/catch method.
        /// </summary>                
        /// <param name="logger">The <see cref="ILogger"/> used.</param>
        /// /// <param name="action">The action to be executed.</param>
        /// <param name="callerMemberName">The method.</param>
        /// <returns></returns>
        public static void LogWithTryCatch(
            this ILogger logger, Action action, [CallerMemberName] string callerMemberName = "")
        {
            logger.LogEnterMethod(callerMemberName);

            try
            {
                action();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                throw;
            }
            finally
            {
                logger.LogExitMethod(callerMemberName);                
            }            
        }

        /// <summary>
        /// Logs an action wrapped withing a try/catch method.
        /// </summary>                
        /// <param name="logger">The <see cref="ILogger"/> used.</param>
        /// /// <param name="function">The action to be executed.</param>
        /// <param name="defaultValue">The default value to be returned when</param>
        /// <param name="throwOnException">Determines whether to throw on exception.</param>
        /// <param name="callerMemberName">The method.</param>
        /// <returns></returns>
        public static TResult LogWithTryCatch<TResult>(
            this ILogger logger, Func<TResult> function, TResult defaultValue = default(TResult), bool throwOnException = true, [CallerMemberName] string callerMemberName = "")
        {
            logger.LogEnterMethod(callerMemberName);            

            try
            {
                return function();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                if(!throwOnException)
                    return defaultValue;

                throw;
            }
            finally
            {                
                logger.LogExitMethod(callerMemberName);
            }            
        }
    }
}
