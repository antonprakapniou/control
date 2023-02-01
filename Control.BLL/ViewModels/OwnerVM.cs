using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Control.BLL.ViewModels
{
	public sealed class OwnerVM:BaseViewModel
	{
		[DisplayName("Full shop name")]
        public string? FullShop { get; set; }

		[Required]
        [DisplayName("Short shop name")]
        public string? ShortShop { get; set; }

        [DisplayName("Full production name")]
        public string? FullProduction { get; set; }

        [DisplayName("Short production name")]
        public string? ShortProduction { get; set; }

        [DisplayName("Name")]
        public string? ShortName { get; set; }

        [DisplayName("Shop code")]
        public string? ShopCode { get; set; }

        [DisplayName("Master")]
		public string? Master { get; set; }

		[DisplayName("Phone")]
		public string? Phone { get; set; }

		[DisplayName("Email")]
		[EmailAddress]
		public string? Email { get; set; }
	}
}
