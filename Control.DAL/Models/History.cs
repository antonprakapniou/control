namespace Control.DAL.Models;

public sealed class History:BaseModel
{
    #region Own properties

    public DateTime Created { get; set; }
    public string ? DeviceType { get; set; }
    public string ? FactoryNumber { get; set; }
    public string ? Owner { get; set; }
    public ActionEnum Action { get; set; }
    public string ? Master { get; set; }

    #endregion    
}
