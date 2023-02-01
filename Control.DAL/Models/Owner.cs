namespace Control.DAL.Models
{
	public sealed class Owner:BaseModel
	{
		public string? FullShop { get; set; }
        public string? ShortShop { get; set; }
        public string? FullProduction { get; set; }
        public string? ShortProduction { get; set; }
        public string? ShortName { get => $"{ShortShop} {ShortProduction}".TrimEnd(); set { } }
        public string? ShopCode { get; set; }
        public string? Master { get; set; }
		public string? Phone { get; set; }
		public string? Email { get; set; }
		public ICollection<Position>? Positions { get; set; }
	}
}
