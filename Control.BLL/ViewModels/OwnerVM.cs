namespace Control.BLL.ViewModels;

public sealed class OwnerVM : BaseVM
{
    #region Own properties

    [DisplayName("Full shop name")]
    public string? FullShop { get; set; }

    [Required]
    [DisplayName("Short shop name")]
    public string? ShortShop { get; set; }

    [DisplayName("Full production name")]
    public string? FullProduction { get; set; }

    [DisplayName("Short production name")]
    public string? ShortProduction { get; set; }

    [DisplayName("Short name")]
    public string? ShortName { get => $"{ShortShop} {ShortProduction}".TrimEnd();}

    [DisplayName("Full name")]
    public string? FullName { get => $"{FullShop} {FullProduction}".TrimEnd(); }

    [DisplayName("Shop code")]
    public string? ShopCode { get; set; }

    [DisplayName("Shop phone")]
    public string? Phone { get; set; }

    #endregion

    #region IncludedPropertiesId

    [Required]
    [DisplayName("Master")]
    public string? MasterId { get; set; }
    public string? NormMasterId { get => MasterId!.ToUpper(); }

    #endregion

    #region IncludedProperties

    public MasterVM? Master { get; set; }

    #endregion
}