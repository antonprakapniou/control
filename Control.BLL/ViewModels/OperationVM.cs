using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Control.BLL.ViewModels
{
	public sealed class OperationVM
	{
		public Guid Id { get; set; }

		[Required]
		[DisplayName("Name")]
		public string? Name { get; set; }
	}
}
