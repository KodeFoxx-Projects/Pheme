namespace Kodefoxx.Pheme.Shared.Domain.Notifications
{
    /// <summary>
    /// Represents a converter from a <typeparamref name="TPhemeNotification"/> to a <see cref="TTargetNotification"/>.
    /// </summary>
    /// <typeparam name="TPhemeNotification">The pheme based <see cref="INotification"/>.</typeparam>
    /// <typeparam name="TTargetNotification">The type of the target notification.</typeparam>
    public interface INotificationConverter<in TPhemeNotification, out TTargetNotification>
        where TPhemeNotification : INotification        
    {
        /// <summary>
        /// Converts a given <paramref name="phemeNotification"/> to the type of a <typeparamref name="TTargetNotification"/>.
        /// </summary>
        /// <param name="phemeNotification">The <see cref="INotification"/> to convert.</param>
        /// <returns>A <typeparamref name="TTargetNotification"/> representation of the given <paramref name="phemeNotification"/>.</returns>
        TTargetNotification ConvertToTargetNotification(TPhemeNotification phemeNotification);
    }
}
