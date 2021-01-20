using System;

namespace Vostok.Tracing.Hercules.Helpers
{
    internal static class DatetimeHelper
    {
        public static DateTimeOffset Timestamp(DateTime utcTimestamp, long utcOffset)
        {
            var offset = TimeSpan.FromTicks(utcOffset);
            return new DateTimeOffset(DateTime.SpecifyKind(utcTimestamp + offset, DateTimeKind.Unspecified), offset);
        }
    }
}