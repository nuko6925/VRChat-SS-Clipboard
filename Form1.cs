namespace VRC_SS_Clipboard;

public partial class Form1 : Form
{
    public Form1()
    {
        var configPath = Path.Combine(Environment.CurrentDirectory, "config.txt");
        var path = ReadConfig(configPath);
        InitializeComponent();
        var watcher = new FileSystemWatcher();
        watcher.Path = path!;
        watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
        watcher.Filter = "*.png";
        watcher.IncludeSubdirectories = true;
        watcher.Created += file_Created;
        watcher.EnableRaisingEvents = true;
        Console.ReadLine();
    }
    
    private static void file_Created(object obj, FileSystemEventArgs e)
    {
        Thread.Sleep(1000);
        using var image = Image.FromFile(e.FullPath);
        Clipboard.SetImage(image);
        

    }
    
    private static string? ReadConfig(string configPath)
    {
        if (File.Exists(configPath))
        {
            var strArray = File.ReadAllLines(configPath);
            return strArray.Length >= 1 ? strArray[0].Trim() : null;
        }

        File.WriteAllText(configPath, $@"C:\Users\{Environment.UserName}\Pictures\VRChat");
        return $@"C:\Users\{Environment.UserName}\Pictures\VRChat";
    }
}