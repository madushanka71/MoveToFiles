using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string folder = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
                List<string> files = Directory.GetFiles(folder).Where(i => !(i.EndsWith(".exe")
                    || i.EndsWith(".pdb") || i.EndsWith(".config"))).ToList();

                

                if (files.Any())
                {
                    foreach (var file in files)
                    {
                        Console.WriteLine(file);
                        int index = file.LastIndexOf(@"\") + 1;
                        int length = file.Length - index;
                        string name = file.Substring(index, length);
                        Console.WriteLine(name);
                        string folderName;
                        if (name.Contains("."))
                        {
                            int dotIndex = name.IndexOf(".");
                            folderName = name.Substring(0, dotIndex);

                            Console.WriteLine(folderName);
                            string dirPath = folder + "\\" + folderName;

                            if (!Directory.Exists(dirPath))
                            {
                                Directory.CreateDirectory(dirPath);
                            }


                            string destPath = dirPath + "\\" + name;

                            File.Move(file, destPath);
                        }                        
                    }
                }
                else
                {
                    Console.WriteLine("I can't found any files in given folder");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Occured: " + ex);
                throw;
            }
            
            Console.ReadKey();
        }
    }
}
