using System;

namespace Ucsb.Sa.FinAid.AidEstimation
{
    /// <summary>
    /// Represents a ranged Financial Aid award. For example: "$500 - $700".
    /// Negative amounts are not allowed within a range; negative amounts will be
    /// set to zero
    /// </summary>
    public class AwardRange
    {
        public double Maximum
        {
            get;
            set;
        }

        public double Minimum
        {
            get;
            set;
        }

        /// <summary>
        /// Creates a range using the provided target value and buffer value. For example,
        /// if the "value" is "500" and the "buffer" is "200", the resulting range will be
        /// "$300 - $700". If, after the buffer calculation, either of the range values are
        /// negative, they will be set to zero
        /// </summary>
        public static AwardRange GetRangeFromValue(double value, double buffer)
        {
            return GetRangeFromValue(value, buffer, buffer, 0);
        }

        public static AwardRange GetRangeFromValue(double value, double lowerBuffer, double upperBuffer)
        {
            return GetRangeFromValue(value, lowerBuffer, upperBuffer, 0);
        }

        /// <summary>
        /// Creates a range using the provided target value and buffer values. For example,
        /// if the "value" is "500", the "lowerBuffer" is "300", and the "upperBuffer" is
        /// "100", the resulting range will be "$200 - $600". If, after the buffer calculation,
        /// either of the range values are negative OR either of the range values is less than
        /// the "minimumAwardValue", the values will be set to zero
        /// </summary>
        public static AwardRange GetRangeFromValue(double value, double lowerBuffer, double upperBuffer, double minimumAwardValue)
        {
            if (lowerBuffer < 0 || upperBuffer < 0)
            {
                throw new ArgumentException("Buffer values can not be negative");
            }

            return new AwardRange(value - lowerBuffer, value + upperBuffer, minimumAwardValue);
        }

        public AwardRange(double minimum, double maximum)
            : this(minimum, maximum, 0)
        {
        }

        public AwardRange(double minimum, double maximum, double minimumAwardValue)
        {
            if (minimumAwardValue < 0)
            {
                throw new ArgumentException("Minimum award value can not be less than zero");
            }

            if (minimum > maximum)
            {
                throw new ArgumentException("Minimum range value can not be greater than maximum range value");
            }

            Minimum = (minimum <= 0 || minimum < minimumAwardValue) ? 0 : minimum;
            Maximum = (maximum <= 0 || maximum < minimumAwardValue) ? 0 : maximum;
        }

        /// <summary>
        /// Displays the range in the following format: "${Minimum} - ${Maximum}". If the two amounts
        /// are equal, only a single amount displays
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return (Minimum == Maximum)
                ? Minimum.ToString("C")
                : String.Format("{0} - {1}", Minimum.ToString("C"), Maximum.ToString("C"));
        }
    }
}
