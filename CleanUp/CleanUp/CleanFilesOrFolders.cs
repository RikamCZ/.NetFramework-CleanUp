using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CleanUp
{
    class CleanFilesOrFolders
    {
        #region Class variables
        /// <summary>
        /// This is a folder from where you wish to delete files
        /// </summary>
        private static string targetFolder;

        /// <summary>
        /// List of files which you want to delete.
        /// </summary>
        private static List<string> fileNamesList;

        #endregion

        #region Main method
        private static void Main(string[] args)
        {
            // Ask user to provice path of build
            Console.Write("Path : ");
            targetFolder = Console.ReadLine();
            if (!Directory.Exists(targetFolder))
            {
                Console.WriteLine("Specified directory does not exist!");
                return;
            }

            UserMenuSelection();

            try
            {
                if (fileNamesList != null && fileNamesList.Any())
                {
                    foreach (var file in fileNamesList)
                    {
                        // Check if file exists with its full path    
                        if (File.Exists(Path.Combine(targetFolder, file)))
                        {
                            // If file found, delete it    
                            File.Delete(Path.Combine(targetFolder, file));
                            Console.WriteLine("File deleted " + file);
                        }
                        else
                        {
                            Console.WriteLine("File not found");
                        }
                    }
                }
            }
            catch (IOException ioExp)
            {
                Console.WriteLine(ioExp.Message);
            }
            Console.ReadKey();
        }
        #endregion

        #region private method
        private static void UserMenuSelection()
        {

            fileNamesList = new List<string>();
            Console.WriteLine(@"Do you want to delete all the files with specific extension? " + Environment.NewLine + "Yes or No");
            string res = Console.ReadLine().ToLower();
            switch (res)
            {
                case "yes":
                    AddFilesToDeleteWithExtension();
                    break;
                case "no":
                    AddFilesToDeleteIntoList();
                    break;
                default:
                    break;
            }
        }

        private static void AddFilesToDeleteWithExtension()
        {
            Console.Write("Paste or Type extention or Type of the file : ");
            // files with pdb extension
            string[] FilesExtentions = Directory.GetFiles(targetFolder, "*." + Console.ReadLine());
            foreach (string file in FilesExtentions)
            {
                fileNamesList.Add(file);
            }
        }

        /// <summary>
        /// This method add files which are to be deleted and also adds files which have same extension and need to be deleted.
        /// </summary>
        private static void AddFilesToDeleteIntoList()
        {
            bool addAnotherFile = false;
            do
            {
                Console.Write("Paste or Type name of the file with extension : ");
                fileNamesList.Add(Console.ReadLine());
                Console.WriteLine(@"Do you want to delete one more file? " + Environment.NewLine + "Yes or No");
                Console.WriteLine("------------------------------------------------------");
                string res = Console.ReadLine().ToLower();
                addAnotherFile = res == "yes" ? true : false;
            } while (addAnotherFile);
        }
        #endregion
    }
}

