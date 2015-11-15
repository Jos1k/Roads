using System.Web.Mvc.Html;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    ///     Overwrite StringLengthAttribute class for setting up error message by labelId.
    /// </summary>
    public class RoadsStringLengthAttribute : StringLengthAttribute
    {
        private readonly string _labelId;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RoadsStringLengthAttribute" /> class.
        /// </summary>
        /// <param name="lenght">The lenght.</param>
        /// <param name="labelId">The label identifier.</param>
        public RoadsStringLengthAttribute(int lenght, string labelId)
            : base(lenght)
        {
            _labelId = labelId;
            ErrorMessage = RoadsExtensionMethods.GetLabel(labelId);
        }

        /// <summary>
        ///     Determines whether a specified object is valid.
        /// </summary>
        /// <param name="value">The object to validate.</param>
        /// <returns>
        ///     true if the specified object is valid; otherwise, false.
        /// </returns>
        public override bool IsValid(object value)
        {
            ErrorMessage = RoadsExtensionMethods.GetLabel(_labelId);
            return base.IsValid(value);
        }
    }
}