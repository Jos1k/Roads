using System.Web.Mvc.Html;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    ///     Overwrite RequiredAttribute class for setting up error message by labelId.
    /// </summary>
    public class RoadsRequiredAttribute : RequiredAttribute
    {
        private readonly string _labelId;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RoadsRequiredAttribute" /> class.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        public RoadsRequiredAttribute(string labelId)
        {
            _labelId = labelId;
            ErrorMessage = RoadsExtensionMethods.GetLabel(labelId);
        }

        /// <summary>
        ///     Checks that the value of the required data field is not empty.
        /// </summary>
        /// <param name="value">The data field value to validate.</param>
        /// <returns>
        ///     true if validation is successful; otherwise, false.
        /// </returns>
        public override bool IsValid(object value)
        {
            ErrorMessage = RoadsExtensionMethods.GetLabel(_labelId);
            return base.IsValid(value);
        }
    }
}