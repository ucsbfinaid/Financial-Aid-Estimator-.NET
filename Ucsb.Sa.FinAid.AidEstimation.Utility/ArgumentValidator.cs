using System;
using System.Collections.Generic;
using Ucsb.Sa.FinAid.AidEstimation.EfcCalculation;

namespace Ucsb.Sa.FinAid.AidEstimation.Utility
{
    public class ArgumentValidator
    {
        public List<ValidationError> Errors { get; private set; }

        private const double MaxMoneyValue = 999999999;
        private const double MinMoneyValue = -999999999;

        public ArgumentValidator()
        {
            Errors = new List<ValidationError>();
        }

        /// <summary>
        /// Attempts to parse the input into a boolean. If the parsing fails, a <see cref="ValidationError"/> is
        /// generated (using the provided parameter and message) and added to the validator's list of errors
        /// </summary>
        /// <param name="input">Value to parse</param>
        /// <param name="inputDisplayName">Display name for the value being parsed</param>
        /// <param name="inputParameterName">Parameter name (identifiable key) for the value being parsed</param>
        /// <returns>The parsed boolean, or "false" if parsing fails</returns>
        public bool ValidateBoolean(
                string input,
                string inputDisplayName,
                string inputParameterName
            )
        {
            ValidateInputInfo(inputDisplayName, inputParameterName);

            bool value;

            // Provided?
            if (input == null)
            {
                Errors.Add(GetNoValueError(inputDisplayName, inputParameterName));
                return false; // Stop validation
            }

            // Parsing
            if (!Boolean.TryParse(input, out value))
            {
                Errors.Add(GetConversionError(inputDisplayName, inputParameterName));
                return false;
            }

            return value;
        }

        /// <summary>
        /// Attempts to parse the input into a double value that lies within the maximum and minimum range
        /// (0-999999999). If the parsing fails, a <see cref="ValidationError"/> is
        /// generated (using the provided parameter and message) and added to the validator's list of errors
        /// </summary>
        /// <param name="input">Value to parse</param>
        /// <param name="inputDisplayName">Display name for the value being parsed</param>
        /// <param name="inputParameterName">Parameter name (identifiable key) for the value being parsed</param>
        /// <returns>The parsed value, or "0" if parsing fails</returns>
        public double ValidatePositiveMoneyValue(string input, string inputDisplayName, string inputParameterName)
        {
            ValidateInputInfo(inputDisplayName, inputParameterName);

            double value;

            // Provided?
            if (String.IsNullOrEmpty(input))
            {
                Errors.Add(GetNoValueError(inputDisplayName, inputParameterName));
                return 0; // Stop validation
            }

            // Parsing
            if (!Double.TryParse(input, out value))
            {
                Errors.Add(GetConversionError(inputDisplayName, inputParameterName));
                return 0; // Stop validation
            }

            // Maximum Range
            if (value >= MaxMoneyValue)
            {
                Errors.Add(GetMoneyMaximumError(inputDisplayName, inputParameterName));
                return 0; // Stop validation
            }

            // Minimum Range
            if (value < 0)
            {
                Errors.Add(GetPositiveError(inputDisplayName, inputParameterName));
                return 0; // Stop validation
            }

            return value;
        }

        /// <summary>
        /// Attempts to parse the input into a double value that lies within the maximum and minimum range
        /// (-999999999-999999999). If the parsing fails, a <see cref="ValidationError"/> is
        /// generated (using the provided parameter and message) and added to the validator's list of errors
        /// </summary>
        /// <param name="input">Value to parse</param>
        /// <param name="inputDisplayName">Display name for the value being parsed</param>
        /// <param name="inputParameterName">Parameter name (identifiable key) for the value being parsed</param>
        /// <returns>The parsed value, or "0" if parsing fails</returns>
        public double ValidateMoneyValue(
                string input,
                string inputDisplayName,
                string inputParameterName
            )
        {
            ValidateInputInfo(inputDisplayName, inputParameterName);

            double value;

            // Provided?
            if (String.IsNullOrEmpty(input))
            {
                Errors.Add(GetNoValueError(inputDisplayName, inputParameterName));
                return 0; // Stop validation
            }

            // Parsing
            if (!Double.TryParse(input, out value))
            {
                Errors.Add(GetConversionError(inputDisplayName, inputParameterName));
                return 0; // Stop validation
            }

            // Maximum Range
            if (value >= MaxMoneyValue)
            {
                Errors.Add(GetMoneyMaximumError(inputDisplayName, inputParameterName));
                return 0; // Stop validation
            }

            // Minimum Range
            if (value <= MinMoneyValue)
            {
                Errors.Add(GetMoneyMinimumError(inputDisplayName, inputParameterName));
                return 0; // Stop validation
            }

            return value;
        }

        /// <summary>
        /// Attempts to parse the input into a positive integer value (greater than 0).
        /// If the parsing fails, a <see cref="ValidationError"/> is generated (using the provided parameter and
        /// message) and added to the validator's list of errors
        /// </summary>
        /// <param name="input">Value to parse</param>
        /// <param name="inputDisplayName">Display name for the value being parsed</param>
        /// <param name="inputParameterName">Parameter name (identifiable key) for the value being parsed</param>
        /// <returns>The parsed value, or "0" if parsing fails</returns>
        public int ValidateNonZeroInteger(
                string input,
                string inputDisplayName,
                string inputParameterName
            )
        {
            ValidateInputInfo(inputDisplayName, inputParameterName);

            int value;

            // Provided?
            if (String.IsNullOrEmpty(input))
            {
                Errors.Add(GetNoValueError(inputDisplayName, inputParameterName));
                return 0; // Stop validation
            }

            // Parsing
            if (!Int32.TryParse(input, out value))
            {
                Errors.Add(GetConversionError(inputDisplayName, inputParameterName));
                return 0; // Stop validation
            }

            if (value <= 0)
            {
                Errors.Add(GetNonZeroError(inputDisplayName, inputParameterName));
                return 0; // Stop validation
            }

            return value;
        }

        /// <summary>
        /// Attempts to parse the input into a <see cref="MaritalStatus"/> enumeration value.
        /// If the parsing fails, a <see cref="ValidationError"/> is generated (using the provided parameter
        /// and message) and added to the validator's list of errors
        /// </summary>
        /// <param name="input">Value to parse</param>
        /// <param name="inputDisplayName">Display name for the value being parsed</param>
        /// <param name="inputParameterName">Parameter name (identifiable key) for the value being parsed</param>
        /// <returns>The parsed value, or <see cref="MaritalStatus.None"/> if parsing fails</returns>
        public MaritalStatus ValidateMaritalStatus(
                string input,
                string inputDisplayName,
                string inputParameterName
            )
        {
            ValidateInputInfo(inputDisplayName, inputParameterName);

            MaritalStatus maritalStatus;

            // Provided?
            if (String.IsNullOrEmpty(input))
            {
                Errors.Add(GetNoValueError(inputDisplayName, inputParameterName));
                return MaritalStatus.None; // Stop validation
            }

            // Parsing
            if (input.Equals("single", StringComparison.CurrentCultureIgnoreCase))
            {
                maritalStatus = MaritalStatus.SingleSeparatedDivorced;
            }
            else if (input.Equals("married", StringComparison.CurrentCultureIgnoreCase))
            {
                maritalStatus = MaritalStatus.MarriedRemarried;
            }
            else
            {
                Errors.Add(GetConversionError(inputDisplayName, inputParameterName));
                return MaritalStatus.None; // Stop validation
            }

            return maritalStatus;
        }

        /// <summary>
        /// Attempts to parse the input into a <see cref="UnitedStatesStateOrTerritory"/> enumeration value.
        /// If the parsing fails, a <see cref="ValidationError"/> is generated (using the provided parameter
        /// and message) and added to the validator's list of errors
        /// </summary>
        /// <param name="input">Value to parse</param>
        /// <param name="inputDisplayName">Display name for the value being parsed</param>
        /// <param name="inputParameterName">Parameter name (identifiable key) for the value being parsed</param>
        /// <returns>The parsed value, or <see cref="UnitedStatesStateOrTerritory.Other"/> if parsing fails</returns>
        public UnitedStatesStateOrTerritory ValidateUnitedStatesStateOrTerritory(
                string input,
                string inputDisplayName,
                string inputParameterName
            )
        {
            ValidateInputInfo(inputDisplayName, inputParameterName);

            // Provided?
            if (String.IsNullOrEmpty(input))
            {
                Errors.Add(GetNoValueError(inputDisplayName, inputParameterName));
                return UnitedStatesStateOrTerritory.Other;
            }

            // Parsing
            try
            {
                return (UnitedStatesStateOrTerritory)Enum.Parse(typeof(UnitedStatesStateOrTerritory), input, true);
            }
            catch (Exception)
            {
                Errors.Add(GetConversionError(inputDisplayName, inputParameterName));
                return UnitedStatesStateOrTerritory.Other;
            }
        }

        private static ValidationError GetNoValueError(string inputDisplayName, string inputParameterName)
        {
            return GetError("No value provided for {0}", inputDisplayName, inputParameterName);
        }

        private static ValidationError GetConversionError(string inputDisplayName, string inputParameterName)
        {
            return GetError("Invalid value for {0}", inputDisplayName, inputParameterName);
        }

        private static ValidationError GetMoneyMaximumError(string inputDisplayName, string inputParameterName)
        {
            return GetError("{0} must be less than $999,999,999.00", inputDisplayName, inputParameterName);
        }

        private static ValidationError GetMoneyMinimumError(string inputDisplayName, string inputParameterName)
        {
            return GetError("{0} must be more than -$999,999,999.00", inputDisplayName, inputParameterName);
        }

        private static ValidationError GetPositiveError(string inputDisplayName, string inputParameterName)
        {
            return GetError("{0} must be a positive number", inputDisplayName, inputParameterName);
        }

        private static ValidationError GetNonZeroError(string inputDisplayName, string inputParameterName)
        {
            return GetError("{0} must be greater than zero", inputDisplayName, inputParameterName);
        }

        private static ValidationError GetError(string errorMessage, string inputDisplayName, string inputParameterName)
        {
            if (String.IsNullOrEmpty(errorMessage))
            {
                throw new ArgumentException("No error message provided");
            }

            string message = String.Format(errorMessage, inputDisplayName);
            ValidationError error = new ValidationError(inputParameterName, message);
            return error;
        }

        private void ValidateInputInfo(string inputDisplayName, string inputParameterName)
        {
            if (String.IsNullOrEmpty(inputDisplayName))
            {
                throw new ArgumentException("No input display name provided");
            }

            if (String.IsNullOrEmpty(inputParameterName))
            {
                throw new ArgumentException("No input parameter name provided");
            }
        }
    }
}
