using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Control.BLL.ViewModels
{
	public sealed class OperationVM
	{
		public Guid OperationId { get; set; }

		[Required]
		[DisplayName("Name")]
		public string? Name { get; set; }
	}
}
