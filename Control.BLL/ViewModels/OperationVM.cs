using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Control.BLL.ViewModels
{
	public sealed class OperationVM:BaseViewModel
	{

		[Required]
		[DisplayName("Operation")]
		public string? Name { get; set; }
	}
}
