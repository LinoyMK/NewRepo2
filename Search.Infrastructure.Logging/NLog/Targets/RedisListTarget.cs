// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RedisListTarget.cs" company="Frontiers">
//   © 2007 - 2016 Frontiers Media S.A. All Rights Reserved
// </copyright>
// <summary>
//   Defines the RedisListTarget type.
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
    [Target("RedisList")]
    public sealed class RedisListTarget : TargetWithLayout
    {
        [RequiredParameter]
        public String Host { get; set; }

        [RequiredParameter]
        public int Database { get; set; }

        [RequiredParameter]
        public string Key { get; set; }

        private static Lazy<ConnectionMultiplexer> _connectionMultiplexer;

        public RedisListTarget()
        {
            _connectionMultiplexer = new Lazy<ConnectionMultiplexer>(
                () => ConnectionMultiplexer.Connect(Host));
        }

        public ISubscriber GetSubscriber()
        {
            return _connectionMultiplexer.Value.GetSubscriber();
        }

        public IDatabase GetDatabase(int db)
        {
            return _connectionMultiplexer.Value.GetDatabase(db);
        }

        protected override void Write(LogEventInfo logEvent)
        {
            try
            {
                GetDatabase(Database).ListLeftPush(Key, Layout.Render(logEvent), When.Always, CommandFlags.FireAndForget);
            }
            catch (Exception exception)
            {
                InternalLogger.Error(exception.Message);
            }
        }
    }
}