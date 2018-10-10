using Microsoft.Extensions.Logging;

namespace Kodefoxx.Pheme.Shared.Domain.Notifications
{
    /// <summary>
    /// Implementation which converts a <see cref="NullNotification"/> to the given <typeparamref name="TTargetNotification"/>.
    /// </summary>
    /// <typeparam name="TTargetNotification">The type of the target notification.</typeparam>
    public class NullNotificationConverter<TTargetNotification> 
        : NotificationConverter<NullNotification, TTargetNotification>
    {
        /// <summary>
        /// Creates a new <see cref="NullNotificationConverter{TTargetNotification}"/>.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to be used.</param>
        public NullNotificationConverter(ILogger<NotificationConverter<NullNotification, TTargetNotification>> logger)
            : base(logger)
        { }
        
        /// <inheritdoc cref="NotificationConverter{TPhemeNotification,TTargetNotification}"/>        
        protected override (TTargetNotification TargetNotification, NullNotification PhemeNotification) ConvertToTargetNotificationImplementation(
            NullNotification phemeNotification
        ) => (default(TTargetNotification), phemeNotification);
    }
}
