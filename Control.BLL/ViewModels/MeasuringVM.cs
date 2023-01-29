using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Control.BLL.ViewModels
{
	public sealed class MeasuringVM:BaseViewModel
	{

		[Required]
		[DisplayName("Measuring")]
		public string? Name { get; set; }

		[Required]
		[DisplayName("Code")]
		public string? Code { get; set; }
	}
}
