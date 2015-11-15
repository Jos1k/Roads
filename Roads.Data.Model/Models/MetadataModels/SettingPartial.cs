using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Roads.DataBase.Model.Models
{
	/// <summary>
	/// Partial class of Setting that allows using metadata.
	/// </summary>
	[MetadataType(typeof(SettingPartialMetadata))]
	public partial class Setting
	{
	}

	/// <summary>
	/// This class created to declare DataContract for using Setting type in WCF data contract.
	/// </summary>
	[DataContract]
	public class SettingPartialMetadata
	{
        [Key]
		public int SettingId { get; set; }
	}
}
