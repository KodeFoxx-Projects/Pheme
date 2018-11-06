using Kodefoxx.Pheme.Shared.Domain.Notifications;

namespace Kodefoxx.Pheme.Shared.Domain.Targets
{
    /// <summary>
    /// Marker interface for a target.
    /// </summary>
    /// <typeparam name="TTargetNotification">The type of <see cref="INotification"/>.</typeparam>
    public interface ITarget<in TTargetNotification>
        where TTargetNotification : ITargetNotification
    {
        /// <summary>
        /// Publishes a message to the target.
        /// </summary>
        /// <param name="notification">The <see cref="INotification"/> to send.</param>
        void Publish(TTargetNotification notification);
    }
}