using Microsoft.Extensions.Logging;

namespace Kodefoxx.Pheme.Shared.Domain.Notifications
{
    /// <summary>
    /// Implementation which converts a <typeparamref name="TPhemeNotification"/> to the given <typeparamref name="TTargetNotification"/>.
    /// </summary>
    /// <typeparam name="TPhemeNotification">The pheme based <see cref="INotification"/>.</typeparam>
    /// <typeparam name="TTargetNotification">The type of the target notification.</typeparam>
    public abstract class NotificationConverter<TPhemeNotification, TTargetNotification> 
        : INotificationConverter<TPhemeNotification, TTargetNotification>
        where TPhemeNotification : INotification
    {
        /// <summary>
        /// Holds a reference to the <see cref="ILogger"/>.
        /// </summary>
        private readonly ILogger<NotificationConverter<TPhemeNotification, TTargetNotification>> _logger;

        /// <summary>
        /// Creates a new <see cref="NotificationConverter{TPhemeNotification,TTargetNotification}"/> instance.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to use.</param>
        protected NotificationConverter(ILogger<NotificationConverter<TPhemeNotification, TTargetNotification>> logger)
            => _logger = logger
        ;

        /// <inheritdoc cref="INotificationConverter{TPhemeNotification,TTargetNotification}"/>                
        public TTargetNotification ConvertToTargetNotification(TPhemeNotification phemeNotification)
        {
            _logger.LogTrace($"Enter '{nameof(ConvertToTargetNotification)}' with {typeof(TPhemeNotification)}: '{phemeNotification?.ToString() ?? "{null}"}'.");            

            var targetNotification = AfterConvertToTargetNotification(
                ConvertToTargetNotificationImplementation(
                    BeforeConvertToTargetNotification(phemeNotification)
                )
            );

            _logger.LogTrace($"Exit '{nameof(ConvertToTargetNotification)}'");

            return targetNotification;
        }

        /// <summary>
        /// Concrete implementation of a <typeparamref name="TPhemeNotification"/> to a <typeparamref name="TTargetNotification"/>.
        /// </summary>
        /// <param name="phemeNotification">The <typeparamref name="TPhemeNotification"/> to ne converted.</param>
        /// <returns>A tuple containing the <see cref="TTargetNotification"/> and the <see cref="TPhemeNotification"/>.</returns>
        protected abstract (TTargetNotification TargetNotification, TPhemeNotification PhemeNotification) ConvertToTargetNotificationImplementation(
            TPhemeNotification phemeNotification
        );

        /// <summary>
        /// Pre-processing before the conversion method gets invoked.
        /// </summary>
        /// <param name="phemeNotification">The <see cref="TPhemeNotification"/> to be converted.</param>
        /// <returns>The (modified/pre-processed) <typeparamref name="TPhemeNotification"/>.</returns>
        protected virtual TPhemeNotification BeforeConvertToTargetNotification(
            TPhemeNotification phemeNotification
        )
        {
            _logger.LogTrace($"Enter '{nameof(BeforeConvertToTargetNotification)}' with {typeof(TPhemeNotification)}: '{phemeNotification?.ToString() ?? "{null}"}'.");
            _logger.LogTrace($"Exit '{nameof(BeforeConvertToTargetNotification)}'");

            return phemeNotification;
        }

        /// <summary>
        /// Post-processing after the conversion method was invoked.
        /// </summary>
        /// <param name="conversionResult">A tuple containing the <typeparamref name="TPhemeNotification"/> and the <typeparamref name="TTargetNotification"/>.</param>
        /// <returns>The actual converted <typeparamref name="TPhemeNotification"/> as a <typeparamref name="TTargetNotification"/>.</returns>
        protected virtual TTargetNotification AfterConvertToTargetNotification(
            (TTargetNotification TargetNotification, TPhemeNotification PhemeNotification) conversionResult
        )
        {
            _logger.LogTrace($"Enter '{nameof(AfterConvertToTargetNotification)}' with {typeof(TPhemeNotification)}: '{conversionResult.PhemeNotification?.ToString() ?? "{null}"}' and {typeof(TTargetNotification)}: '{conversionResult.TargetNotification?.ToString() ?? "{null}"}'.");
            _logger.LogTrace($"Exit '{nameof(AfterConvertToTargetNotification)}'");

            return conversionResult.TargetNotification;
        }
    }
}
