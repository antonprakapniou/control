namespace Control.WEB.Interfaces;

public interface IFileManager
{
    public string? FileName { get; }
    public void Load(IFormFileCollection files, string partialPath);
    public void Delete(string fileName, string partialPath);
}