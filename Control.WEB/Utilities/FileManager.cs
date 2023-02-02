namespace Control.WEB.Utilities;

public sealed class FileManager : IFileManager
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    public string? FileName { get; private set; }

    public FileManager(IWebHostEnvironment hostEnvironment)
    {
        _webHostEnvironment = hostEnvironment;
    }

    public void Load(IFormFileCollection files, string partialPath)
    {
        var file = files[0];
        string fullPath = string.Concat(_webHostEnvironment.WebRootPath, partialPath);
        string properName = Guid.NewGuid().ToString();
        string fileExtension = Path.GetExtension(file.FileName);
        string fileName = string.Concat(properName, fileExtension);
        string fileUrl = Path.Combine(fullPath, fileName);

        using (var fileStream = new FileStream(fileUrl, FileMode.Create)) file.CopyTo(fileStream);

        FileName= fileName;
    }

    public void Delete(string fileName, string partialPath)
    {
        string fullPath = string.Concat(_webHostEnvironment.WebRootPath, partialPath);
        string fileUrl = Path.Combine(fullPath, fileName);
        if (File.Exists(fileUrl)) File.Delete(fileUrl);
    }
}
