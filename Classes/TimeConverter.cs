using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BerlinClock
{
    public class TimeConverter : ITimeConverter
    {
        private const char RedLamp = 'R';
        private const char YellowLamp = 'Y';
        private const char OffLamp = 'O';

        public string convertTime(string aTime)
        {
            if (aTime == null)
            {
                throw new ArgumentNullException(nameof(aTime));
            }

            var timeTuple = ParseTime(aTime);

            int hours = timeTuple.Item1,
                minutes = timeTuple.Item2,
                seconds = timeTuple.Item3;

            return new StringBuilder()

                // First row
                .Append(seconds % 2 == 0 ? YellowLamp : OffLamp)
                .AppendLine()

                // Second row
                .Append(RedLamp, hours / 5)
                .Append(OffLamp, 4 - hours / 5)
                .AppendLine()

                // Third row
                .Append(RedLamp, hours % 5)
                .Append(OffLamp, 4 - hours % 5)
                .AppendLine()

                // Fourth row
                .Append(YellowLamp, Math.Min(minutes / 5, 2))
                .Append(RedLamp, Math.Min(minutes / 15, 1))
                .Append(YellowLamp, minutes / 25)
                .Append(RedLamp, minutes / 30)
                .Append(YellowLamp, minutes / 35)
                .Append(YellowLamp, minutes / 40)
                .Append(RedLamp, minutes / 45)
                .Append(YellowLamp, minutes / 50)
                .Append(YellowLamp, minutes / 55)
                .Append(OffLamp, 11 - minutes / 5)
                .AppendLine()

                // Fifth row
                .Append(YellowLamp, minutes % 5)
                .Append(OffLamp, 4 - minutes % 5)
                .ToString();
        }

        private Tuple<int, int, int> ParseTime(string aTime)
        {
            var timeParts = aTime.Split(':');
            byte hours, minutes, seconds;

            if (timeParts.Length != 3
                || !byte.TryParse(timeParts[0], out hours)
                || !byte.TryParse(timeParts[1], out minutes)
                || !byte.TryParse(timeParts[2], out seconds)
                || hours > 24
                || minutes > 59
                || seconds > 59)
            {
                throw new ArgumentException("Invalid time string");
            }

            return new Tuple<int, int, int>(hours, minutes, seconds);
        }
    }
}
