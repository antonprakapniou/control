using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Control.BLL.ViewModels
{
	public sealed class OwnerVM:BaseViewModel
	{

		[Required]
		[DisplayName("Shop")]
		public string? Shop { get; set; }

		[DisplayName("Production")]
		public string? Production { get; set; }

		[DisplayName("Master")]
		public string? Master { get; set; }

		[DisplayName("Phone")]
		public string? Phone { get; set; }

		[DisplayName("Email")]
		[EmailAddress]
		public string? Email { get; set; }
	}
}
