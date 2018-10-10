using System;
using System.Text;
using Kodefoxx.Pheme.Shared.Domain.Notifications;
using Microsoft.Extensions.Logging;

namespace Kodefoxx.Pheme.Shared.Domain.Tests.Notifications
{
    /// <summary>
    /// A <see cref="NullNotificationConverter{TTargetNotification}"/> with specific overrides for before and after.
    /// </summary>
    public sealed class NullToStringNotificationConverter : NullNotificationConverter<string>
    {
        /// <summary>
        /// Holds the state.
        /// </summary>
        private readonly StringBuilder _stringBuilder;

        /// <summary>
        /// Function that returns an empty string.
        /// </summary>
        private readonly Func<string> _nullFunction = () => "";

        /// <summary>
        /// Optional function executed "before" conversion takes place.
        /// </summary>
        public Func<string> BeforeFunction { get; set; }

        /// <summary>
        /// Optional function executed "after" conversion takes place.
        /// </summary>
        public Func<string> AfterFunction { get; set; }
        
        /// <summary>
        /// Creates a new <see cref="NullToStringNotificationConverter"/> instance.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to be used.</param>
        /// <param name="beforeFunction">Optional function executed "before" conversion takes place.</param>
        /// <param name="afterFunction">Optional function executed "after" conversion takes place.</param>
        public NullToStringNotificationConverter(
            ILogger<NotificationConverter<NullNotification, string>> logger,
            Func<string> beforeFunction = null, Func<string> afterFunction = null
        ) : base(logger)
        {            
            _stringBuilder = new StringBuilder();
            BeforeFunction = beforeFunction ?? _nullFunction;
            AfterFunction = afterFunction ?? _nullFunction;
        }

        /// <inheritdoc />
        protected override NullNotification BeforeConvertToTargetNotification(NullNotification phemeNotification)
        {
            _stringBuilder.Append(BeforeFunction?.Invoke() ?? _nullFunction());
            return phemeNotification;
        }

        /// <inheritdoc />
        protected override string AfterConvertToTargetNotification(
            (string TargetNotification, NullNotification PhemeNotification) conversionResult
        ) => _stringBuilder.Append(AfterFunction?.Invoke() ?? _nullFunction()).ToString()
        ;
    }
}
