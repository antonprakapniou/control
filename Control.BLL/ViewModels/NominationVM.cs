using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Control.BLL.ViewModels
{
	public sealed class NominationVM:BaseViewModel
	{

		[Required]
		[DisplayName("Name")]
		public string? Name { get; set; }
	}
}
