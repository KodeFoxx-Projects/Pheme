using Kodefoxx.Pheme.Shared.Domain.Notifications;

namespace Kodefoxx.Pheme.Shared.Domain.Converters
{
    /// <summary>
    /// Converts <typeparamref name="TIn"/> and <typeparamref name="TNotification"/> back and forth.
    /// </summary>
    public interface INotificationConverter<TIn, TNotification>
        where TNotification : INotification, new()
    {
        /// <summary>
        /// Convert the incoming <paramref name="@object"/> to a <typeparamref name="TNotification"/>.
        /// </summary>
        /// <param name="object">The object to be converted.</param>        
        TNotification Convert(TIn @object);

        /// <summary>
        /// Convert the incoming <paramref name="notification"/> to a <typeparamref name="TIn"/>.
        /// </summary>
        /// <param name="notification">The notification to be converted.</param>
        TIn Convert(TNotification notification);
    }
}
