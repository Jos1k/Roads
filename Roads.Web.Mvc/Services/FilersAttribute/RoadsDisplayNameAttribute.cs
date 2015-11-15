using System.Web.Mvc.Html;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    ///     Overwrite DisplayNameAttribute class for setting up name by labelId.
    /// </summary>
    public class RoadsDisplayNameAttribute : DisplayNameAttribute
    {
        private readonly string _labelId;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RoadsDisplayNameAttribute" /> class.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        public RoadsDisplayNameAttribute(string labelId)
        {
            _labelId = labelId;
            DisplayNameValue = RoadsExtensionMethods.GetLabel(_labelId);
        }

        /// <summary>
        ///     Gets the display name for a property, event, or public void method that takes no arguments stored in this
        ///     attribute.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return RoadsExtensionMethods.GetLabel(_labelId);
            }
        }
    }
}