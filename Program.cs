using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Input directory:");
        string dir = Console.ReadLine();
        if (dir.Length == 0)
        {
            Console.WriteLine("Syntax: {0} directory_path", Path.GetFileNameWithoutExtension(Environment.GetCommandLineArgs()[0]));
            return;
        }

        if (!Directory.Exists(dir))
        {
            Console.WriteLine("Catalogue {0} does not exist.", dir);
            return;
        }

        int count = 0;
        string[] subdirectories = Directory.GetDirectories(dir, "*", SearchOption.AllDirectories);
        foreach (string subdirectory in subdirectories)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(subdirectory);
            FileSystemInfo[] subItems = directoryInfo.GetFileSystemInfos();
            bool hasOtherFoldersOrFiles = false;

            foreach (FileSystemInfo item in subItems)
            {
                if (item.Attributes.HasFlag(FileAttributes.Directory) || item.Attributes.HasFlag(FileAttributes.Normal))
                {
                    hasOtherFoldersOrFiles = true;
                    break;
                }
            }

            if (hasOtherFoldersOrFiles ||
                directoryInfo.Name.EndsWith(".{") ||
                directoryInfo.Attributes.HasFlag(FileAttributes.Directory) ||
                directoryInfo.Attributes.HasFlag(FileAttributes.ReadOnly) ||
                directoryInfo.Attributes.HasFlag(FileAttributes.Hidden))
            {
                count++;
            }
        }

        Console.WriteLine("The count of subdirectories in the directory \n{0}: {1}", dir, count);
        Console.WriteLine("program completed successfully with code 0");
        Console.ReadLine();
        return;
    }
}