using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Control.BLL.ViewModels
{
    public sealed class CategoryVM
    {
        public Guid CategoryId { get; set; }

        [Required]
        [DisplayName("Name")]
        public string? Name { get; set; }
    }
}
