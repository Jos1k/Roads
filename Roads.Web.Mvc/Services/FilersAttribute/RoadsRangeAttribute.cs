using System.Web.Mvc.Html;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Overwrite RangeAttribute class for setting up error message by labelId.
    /// </summary>
    public class RoadsRangeAttribute : RangeAttribute
    {
        private string _errorMessage { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RoadsRangeAttribute"/> class.
        /// </summary>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        /// <param name="labelId">The label identifier.</param>
        public RoadsRangeAttribute(double minimum, double maximum, string labelId) : base(minimum, maximum)
        {
            SetErrorMessage(labelId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RoadsRangeAttribute"/> class.
        /// </summary>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        /// <param name="labelId">The label identifier.</param>
        public RoadsRangeAttribute(int minimum, int maximum, string labelId)
            : base(minimum, maximum)
        {
            SetErrorMessage(labelId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RoadsRangeAttribute"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        /// <param name="labelId">The label identifier.</param>
        public RoadsRangeAttribute(Type type, string minimum, string maximum, string labelId)
            : base(type, minimum, maximum)
        {
            SetErrorMessage(labelId);
        }

        /// <summary>
        /// Sets the error message by label id.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        private void SetErrorMessage(string labelId)
        {
            this._errorMessage = RoadsExtensionMethods.GetLabel(labelId);
        }

        /// <summary>
        /// Formats the error message that is displayed when range validation fails.
        /// </summary>
        /// <param name="name">The name of the field that caused the validation failure.</param>
        /// <returns>
        /// The formatted error message.
        /// </returns>
        public override string FormatErrorMessage(string name)
        {

            return this._errorMessage;
        }
    }
}