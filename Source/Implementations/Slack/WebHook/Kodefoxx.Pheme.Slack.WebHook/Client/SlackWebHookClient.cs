using System;
using Kodefoxx.Pheme.Shared.Domain.Converters;
using Kodefoxx.Pheme.Shared.Domain.Targets;
using Kodefoxx.Pheme.Shared.Infrastructure.Logging;
using Kodefoxx.Pheme.Slack.WebHook.Notifications;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Kodefoxx.Pheme.Slack.WebHook.Client
{
    /// <summary>
    /// The implementation for communication to slack via a web hook.
    /// For more info, see: https://api.slack.com/incoming-webhooks.
    /// </summary>
    public sealed class SlackWebHookClient : ITarget<SlackWebHookNotification>
    {
        private readonly INotificationConverter<string, SlackWebHookNotification> _notificationToStringConverter;
        private readonly ILogger<SlackWebHookClient> _logger;

        /// <summary>
        /// Creates a new <see cref="SlackWebHookClient"/>.
        /// </summary>
        /// <param name="notificationToStringConverter">Used to convert an <see cref="SlackWebHookNotification"/> to a <see cref="string"/></param>
        /// <param name="logger">The logger used.</param>
        public SlackWebHookClient(
            INotificationConverter<string, SlackWebHookNotification> notificationToStringConverter,
            ILogger<SlackWebHookClient> logger = null
        )
        {
            _notificationToStringConverter = notificationToStringConverter;
            _logger = logger ?? new NullLogger<SlackWebHookClient>();            
        }

        /// <inheritdoc />        
        public void Publish(SlackWebHookNotification notification)
            => _logger.LogWithTryCatch(() => {
                    var jsonString = GetNotificationAsSlackJsonString(notification);
                }
            )
        ;

        /// <summary>
        /// Converts the given <see cref="notification"/> as a json string.
        /// </summary>
        /// <param name="notification">The notification to be converted.</param>        
        private string GetNotificationAsSlackJsonString(SlackWebHookNotification notification)
            => _logger.LogWithTryCatch(() => _notificationToStringConverter.Convert(notification))
        ;
    }
}