using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Control.BLL.ViewModels
{
	public sealed class UnitsVM
	{
		public Guid UnitsId { get; set; }

		[Required]
		[DisplayName("Name")]
		public string? Name { get; set; }
	}
}
