namespace Control.BLL.ViewModels;

public class HistoryVM:BaseVM
{
    #region Own properties

    public DateTime Created { get; set; }
    public string? DeviceType { get; set; }
    public string? FactoryNumber { get; set; }
    public string? Owner { get; set; }
    public string? Action { get; set; }
    public string? Master { get; set; }
    public string? History { get => $"[{Created}] '{DeviceType}' with number '{FactoryNumber}' from '{Owner}' was {Action} by '{Master}'"; }

    #endregion    
}
