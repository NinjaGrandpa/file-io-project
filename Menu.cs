using Microsoft.Toolkit.Uwp.Notifications;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Xml;

namespace file_io_project
{
    public static class Menu
    {
        public static void ListAllFiles(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string[]? files = Directory.GetFiles(path);

            if (!files.Any())
            {
                Console.WriteLine("\nThere are currently no files in the folder 'Start'\n");
            }
            else
            {
                Console.WriteLine("\nFiles:");
                Console.WriteLine(String.Join(Environment.NewLine, files));
            }
        }

        public static void ListAllCommands()
        {
            Console.WriteLine("Commands:");
            foreach (var command in Enum.GetValues(typeof(Commands)))
            {
                Console.WriteLine($"{command}");
            }
        }

        public static void CreateNewFile(string fileName, string path, string fileContent = "Hello, World!")
        {
            var filePath = $@"{path}\{fileName}";

            if (!File.Exists(filePath))
            {
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine(fileContent);
                }

                Console.WriteLine($"File '{fileName}' created.");
                return;
            }
            Console.WriteLine($"File '{fileName}' already exists");
            return;
        }

        public static void DeleteFile(string fileName, string path)
        {
            var filePath = $@"{path}\{fileName}";

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Console.WriteLine($"File '{fileName}' deleted.");
                return;
            }
            Console.WriteLine($"File '{fileName}' doesn't exist");
            return;
        }

        public static void ZipFiles(string startPath, string zipPath)
        {
            if (!Directory.Exists(startPath))
            {
                Console.WriteLine($"No directory called '{startPath}' found");
                return;
            }

            if (!Directory.GetFiles(startPath).Any())
            {
                Console.WriteLine($"No files found in folder {startPath}");
                return;
            }

            System.IO.Compression.ZipFile.CreateFromDirectory(startPath, zipPath);

            var amountOfFiles = Directory.GetFiles(startPath).Length;

            Console.WriteLine($"Zipped ({amountOfFiles}) file(s)");

            return;
        }

        public static void ExtractFiles(string zipPath, string extractPath)
        {
            if (!File.Exists(zipPath))
            {
                Console.WriteLine($"No zip called '{zipPath}' found");
                return;
            }

            System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, extractPath);

            var amountOfFiles = Directory.GetFiles(extractPath).Length;

            Console.WriteLine($"Extracted ({amountOfFiles}) file(s)");
        }

        public static void ExtractProjectToXml(string basePath)
        {
            string projectPath = @$"{basePath}\project.als";
            string extractpath = $@"{basePath}\Extract";

            string decompressedFileName = $@"{basePath}\project.xml";

            using FileStream projectFile = File.Open(projectPath, FileMode.Open);

            using FileStream outputFileStream = File.Create(decompressedFileName);

            using var decompressor = new GZipStream(projectFile, CompressionMode.Decompress);

            decompressor.CopyTo(outputFileStream);
        }

        public static void CompressXmlToGzip(string basePath)
        {
            string gzipFilePath = @$"{basePath}\streams.gz";

            var gzipFile = File.Create(gzipFilePath);

            using (GZipStream compressor = new GZipStream(gzipFile, CompressionMode.Compress))
            {
                using (XmlWriter xmlGzip = XmlWriter.Create(compressor))
                {
                    xmlGzip.WriteStartDocument();
                    xmlGzip.WriteStartElement("callsigns");
                    xmlGzip.WriteElementString("callsign", "test123dojdoj");
                }
            }

            Console.WriteLine($"{gzipFilePath} contains {new FileInfo(gzipFilePath).Length} bytes.");
            Console.WriteLine(File.ReadAllText(gzipFilePath));
            Console.WriteLine("Reading the compressed XML file:");

            gzipFile = File.Open(gzipFilePath, FileMode.Open);
            using (GZipStream decompressor = new GZipStream(gzipFile, CompressionMode.Decompress))
            {
                using (XmlReader reader = XmlReader.Create(decompressor))
                {
                    while (reader.Read())
                    {
                        if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "callsign"))
                        {
                            reader.Read();
                            Console.WriteLine(reader.Value.ToString());
                        }
                    }
                }
            }
        }
    }
}
