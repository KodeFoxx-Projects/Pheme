using Kodefoxx.Pheme.Shared.Domain.Notifications;

namespace Kodefoxx.Pheme.Shared.Domain.Converters
{
    /// <summary>
    /// Converts a <see cref="TNotification"/> to a <see cref="string"/>.
    /// </summary>
    public abstract class NotificationToStringConverter<TNotification>
        : INotificationConverter<string, TNotification>
        where TNotification : INotification, new()
    {
        /// <inheritdoc />
        public abstract TNotification Convert(string @object);

        /// <inheritdoc />
        public abstract string Convert(TNotification notification);
    }
}
