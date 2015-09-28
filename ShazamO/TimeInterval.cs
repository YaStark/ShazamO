using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShazamO
{
    public struct TimeInterval
    {
        public DateTime Begin { get; set; }

        public DateTime End { get; set; }

        public TimeSpan Length {
            get
            {
                return End.Subtract(Begin);
            }
            set
            {
                End = Begin.Add(value);
            }
        }

        public TimeInterval(DateTime begin, DateTime end) : this()
        {
            Begin = begin;
            End = end;
        }

        public TimeInterval(DateTime begin, TimeSpan length) : this()
        {
            Begin = begin;
            Length = length;
        }

        public bool Within(TimeInterval interval)
        {
            return (Begin.TimeOfDay <= interval.Begin.TimeOfDay) 
                    && (End.TimeOfDay >= interval.End.TimeOfDay);
        }

        public override string ToString()
        {
            return String.Format("{0:HH:mm:ss} - {1:HH:mm:ss}", Begin, End);
        }
    }
}
