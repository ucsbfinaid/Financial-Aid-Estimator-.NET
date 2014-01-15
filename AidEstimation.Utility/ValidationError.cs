using System;

namespace Ucsb.Sa.FinAid.AidEstimation.Utility
{
    /// <summary>
    /// Validation error
    /// </summary>
    public class ValidationError
    {
        /// <summary>
        /// Name of the parameter that caused the validation error
        /// </summary>
        public string Parameter
        {
            get;
            set;
        }

        /// <summary>
        /// Message describing the validation error
        /// </summary>
        public string Message
        {
            get;
            set;
        }

        /// <summary>
        /// Constructs a new <see cref="ValidationError"/> with no parameter name
        /// </summary>
        public ValidationError(string message) : this(String.Empty, message)
        {
        }

        /// <summary>
        /// Constructs a new <see cref="ValidationError"/> with the specified error parameter name and error message
        /// </summary>
        public ValidationError(string parameter, string message)
        {
            Parameter = parameter;
            Message = message;
        }
    }
}