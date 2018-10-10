using System.Threading.Tasks;
using Kodefoxx.Pheme.Shared.Domain.Notifications;

namespace Kodefoxx.Pheme.Shared.Domain.Targets
{
    /// <summary>
    /// Defines a target where a <see cref="INotification"/> can be sent to.
    /// </summary>
    public interface ITarget
    {
        /// <summary>
        /// Publishes the given <paramref name="notification"/>.
        /// </summary>
        /// <param name="notification">The <see cref="INotification"/> to publish.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        Task Publish(INotification notification);
    }
}