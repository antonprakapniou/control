namespace Control.DAL.Models;

public sealed class Owner : BaseModel
{
    #region Own properties

    public string? FullShop { get; set; }
    public string? ShortShop { get; set; }
    public string? FullProduction { get; set; }
    public string? ShortProduction { get; set; }
    public string? ShopCode { get; set; }
    public string? Phone { get; set; }
    public ICollection<Position>? Positions { get; set; }

    #endregion

    #region Included properties Id

    public Guid? MasterId { get; set; }

    #endregion

    #region Included properties

    public Master? Master { get; set; }

    #endregion
}