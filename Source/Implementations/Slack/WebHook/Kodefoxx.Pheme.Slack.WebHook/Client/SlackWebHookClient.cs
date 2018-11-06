using System;
using System.Net;
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
                    Publish("", jsonString);
                }
            )
        ;

        /// <summary>
        /// Publishes the <paramref name="jsonString"/> to a given <see cref="webHookUrl"/>.
        /// </summary>
        /// <param name="webHookUrl">The url of the web hook to publish to.</param>
        /// <param name="jsonString">The json message to send to the web hook.</param>
        public void Publish(string webHookUrl, string jsonString)
            => _logger.LogWithTryCatch(() => {
                    using (var webClient = new WebClient())
                    {
                        _logger.LogTrace($"Start publishing via WebHook '{webHookUrl}', with content '{jsonString}'");
                        var response = webClient.UploadString(new Uri(webHookUrl), "POST", jsonString);
                        _logger.LogTrace($"Uploaded json string, response text was: '{response}'");
                    }
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