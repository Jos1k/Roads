using System.Web.Mvc.Html;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Overwrite RegularExpressionAttribute class for setting up error message by labelId.
    /// </summary>
    public class RoadsRegularExpressionAttribute : RegularExpressionAttribute
    {
        private readonly string _labelId;
        /// <summary>
        /// Initializes a new instance of the <see cref="RoadsRegularExpressionAttribute"/> class.
        /// </summary>
        /// <param name="pattern">The pattern.</param>
        /// <param name="labelId">The label identifier.</param>
        public RoadsRegularExpressionAttribute(string pattern, string labelId)
            : base(pattern)
        {
            _labelId = labelId;
            this.ErrorMessage = RoadsExtensionMethods.GetLabel(labelId);
        }

        /// <summary>
        /// Checks whether the value entered by the user matches the regular expression pattern.
        /// </summary>
        /// <param name="value">The data field value to validate.</param>
        /// <returns>
        /// true if validation is successful; otherwise, false.
        /// </returns>
        public override bool IsValid(object value)
        {
            ErrorMessage = RoadsExtensionMethods.GetLabel(_labelId);
            return base.IsValid(value);
        }
    }
}