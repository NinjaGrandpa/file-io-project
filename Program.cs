using file_io_project;


internal class Program
{
    private static void Main(string[] args)
    {
        string basePath = @"C:\Users\MaxJenslöv-NET22GBG\Documents\GitHub\file-io-project";

        string startPath = $@"{basePath}\Start";
        string zipPath = $@"{basePath}\Result.zip";
        string extractPath = $@"{basePath}\Extract";

        if (!Directory.Exists(startPath)) Directory.CreateDirectory(startPath);

        if (!Directory.Exists(basePath)) Directory.CreateDirectory(extractPath); 

        Console.WriteLine("Welcome to my File IO Project\n");

        Menu.ListAllCommands();

        while (true)
        {
            Console.WriteLine("\nInput Command:");
            string menuInput = Console.ReadLine();

            menuInput = string.Concat(menuInput[0].ToString().ToUpper(), menuInput.AsSpan(1));

            string fileName;

            switch (menuInput)
            {
                case nameof(Commands.List):
                    Menu.ListAllFiles(startPath);
                    break;

                case nameof(Commands.Help):
                    Menu.ListAllCommands();
                    break;

                case nameof(Commands.Create):
                    Console.WriteLine("\nFileName:");
                    fileName = Console.ReadLine();

                    Console.WriteLine("\nText:");
                    var content = Console.ReadLine();

                    Menu.CreateNewFile(fileName, startPath, content);
                    break;

                case nameof(Commands.Clear):
                    Console.Clear();
                    break;

                case nameof(Commands.Delete):
                    Console.WriteLine("File to delete:");
                    fileName = Console.ReadLine();

                    Menu.DeleteFile(fileName, startPath);
                    break;

                case nameof(Commands.Zip):
                    Console.WriteLine("\nZipping files in 'Start Folder'");
                    Menu.ZipFiles(startPath, zipPath);
                    break;

                case nameof(Commands.Extract):
                    Console.WriteLine("\nExtracting files from 'Result.zip'");
                    Menu.ExtractFiles(zipPath, extractPath);
                    break;

                case nameof(Commands.Exit):
                    Console.WriteLine("Are you sure you want to exit? y/n");
                    if (Console.ReadLine() == "y")
                    {
                        Console.WriteLine("Bye!");
                        Environment.Exit(0);
                        break;
                    }
                    break;

                default:
                    Console.WriteLine("No such command");
                    break;
            }
        }
    }
}
