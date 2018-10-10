using System;
using Kodefoxx.Pheme.Shared.Domain.Notifications;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Kodefoxx.Pheme.Shared.Domain.Tests.Notifications
{
    public sealed class NotificationConverterTests
    {
        [Fact]
        public void Returns_null_when_no_before_or_after_implementations_are_provided()
        {
            var sut = CreateNullToStringNotificationConverter();
            var actual = sut.ConvertToTargetNotification(new NullNotification());
            Assert.Equal(String.Empty, actual);
        }

        [Fact]
        public void Returns_before_when_only_before_implementations_is_provided()
        {
            var sut = CreateNullToStringNotificationConverter();
            sut.BeforeFunction = () => "before";
            var actual = sut.ConvertToTargetNotification(new NullNotification());
            Assert.Equal("before", actual);
        }

        [Fact]
        public void Returns_after_when_only_after_implementations_is_provided()
        {
            var sut = CreateNullToStringNotificationConverter();
            sut.AfterFunction = () => "after";
            var actual = sut.ConvertToTargetNotification(new NullNotification());
            Assert.Equal("after", actual);
        }

        [Fact]
        public void Returns_beforeafter_when_both_before_and_after_implementations_is_provided()
        {
            var sut = CreateNullToStringNotificationConverter();
            sut.BeforeFunction = () => "before";
            sut.AfterFunction = () => "after";
            var actual = sut.ConvertToTargetNotification(new NullNotification());
            Assert.Equal("beforeafter", actual);
        }

        /// <summary>
        /// Gets a <see cref="INotificationConverter{TPhemeNotification,TTargetNotification}"/> with a <see cref="ILogger"/> instance.
        /// </summary>                
        public NullToStringNotificationConverter CreateNullToStringNotificationConverter(
            Func<string> beforeFunction = null, Func<string> afterFunction = null
        )
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddTransient<INotificationConverter<NullNotification, string>, NullToStringNotificationConverter>()
                .BuildServiceProvider()
            ;

            return serviceProvider.GetService<INotificationConverter<NullNotification, string>>() as NullToStringNotificationConverter;
        }
    }
}
