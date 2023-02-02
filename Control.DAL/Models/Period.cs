namespace Control.DAL.Models;

public sealed class Period : BaseModel
{
    #region Own properties

    public string? Name { get; set; }

    public ICollection<Position>? Positions { get; set; }

    #endregion
}