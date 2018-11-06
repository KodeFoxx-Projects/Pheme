using Kodefoxx.Pheme.Shared.Domain.Notifications;

namespace Kodefoxx.Pheme.Shared.Domain.Converters
{
    /// <summary>
    /// Converts <typeparamref name="TSourceNotification"/> and <typeparamref name="TTargetNotification"/> back and forth.
    /// </summary>
    public interface ISourceToTargetNotificationConverter<TSourceNotification, TTargetNotification>
        : INotificationConverter<TSourceNotification, TTargetNotification>
        where TSourceNotification : ISourceNotification, new()
        where TTargetNotification : ITargetNotification, new()
    { }
}
