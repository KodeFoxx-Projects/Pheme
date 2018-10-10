using System.Threading.Tasks;
using Kodefoxx.Pheme.Shared.Domain.Notifications;

namespace Kodefoxx.Pheme.Shared.Domain.Targets
{
    /// <summary>
    /// Defines an implementation which does nothing with the incoming <see cref="INotification"/>.
    /// </summary>
    public sealed class NullTarget : ITarget
    {
        /// <inheritdoc cref="ITarget"/>        
        public Task Publish(INotification notification)
            => Task.CompletedTask
        ;
    }
}
