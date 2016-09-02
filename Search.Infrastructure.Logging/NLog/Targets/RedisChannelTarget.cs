// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RedisChannelTarget.cs" company="Frontiers">
//   © 2007 - 2016 Frontiers Media S.A. All Rights Reserved
// </copyright>
// <summary>
//   Defines the RedisChannelTarget type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using NLog;
using NLog.Common;
using NLog.Config;
using NLog.Targets;
using StackExchange.Redis;
using System;

namespace Search.Infrastructure.Logging.NLog.Targets
{
    [Target("RedisChannel")]
    public sealed class RedisChannelTarget : TargetWithLayout
    {
        [RequiredParameter]
        public String Host { get; set; }

        [RequiredParameter]
        public string Key { get; set; }

        private static Lazy<ConnectionMultiplexer> _connectionMultiplexer;

        public RedisChannelTarget()
        {
            _connectionMultiplexer = new Lazy<ConnectionMultiplexer>(
                () => ConnectionMultiplexer.Connect(Host));
        }

        public ISubscriber GetSubscriber()
        {
            return _connectionMultiplexer.Value.GetSubscriber();
        }

        protected override void Write(LogEventInfo logEvent)
        {
            try
            {
                GetSubscriber().Publish(Key, Layout.Render(logEvent), CommandFlags.FireAndForget);
            }
            catch (Exception exception)
            {
                InternalLogger.Error(exception.Message);
            }
        }
    }
}