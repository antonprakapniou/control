using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Control.BLL.ViewModels
{
	public sealed class NominationVM:BaseViewModel
	{

		[Required]
		[DisplayName("Nomination")]
		public string? Name { get; set; }
	}
}
