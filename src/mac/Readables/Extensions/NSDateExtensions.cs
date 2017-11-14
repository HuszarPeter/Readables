using System;
using Foundation;

namespace Readables.Extensions
{
    public static class NSDateExtensions
    {
        static NSDateFormatter longDateFormatter = new NSDateFormatter
        {
            DateStyle = NSDateFormatterStyle.Long,
            TimeStyle = NSDateFormatterStyle.None
        };

        public static NSDate AsNSDate(this DateTime date) {
            DateTime reference = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(2001, 1, 1, 0, 0, 0));
            return NSDate.FromTimeIntervalSinceReferenceDate((date - reference).TotalSeconds);
        }

        public static String AsSystemDateString(this DateTime date) {
            return longDateFormatter.StringFor(date.AsNSDate());
        }
    }
}
