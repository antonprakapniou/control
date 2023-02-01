using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Control.BLL.ViewModels
{
	public sealed class MeasuringVM:BaseViewModel
	{

		[Required]
		[DisplayName("Name")]
		public string? Name { get; set; }

		[Required]
		[DisplayName("Code")]
		public string? Code { get; set; }

		[DisplayName("Measuring")]
        public string? FullName { get; set; }
    }
}
