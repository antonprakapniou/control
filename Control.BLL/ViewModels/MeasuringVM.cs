using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Control.BLL.ViewModels
{
	public sealed class MeasuringVM
	{
		public Guid Id { get; set; }

		[Required]
		[DisplayName("Name")]
		public string? Name { get; set; }

		[DisplayName("Code")]
		public string? Code { get; set; }
	}
}
