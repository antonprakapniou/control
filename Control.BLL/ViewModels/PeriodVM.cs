using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Control.BLL.ViewModels
{
	public sealed class PeriodVM
	{
		public Guid PeriodId { get; set; }

		[Required]
		[DisplayName("Name")]
		public string? Name { get; set; }
	}
}
