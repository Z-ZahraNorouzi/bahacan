using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Service.Security
{
    public class FileExtensions
    {
        //public T GetComplexData<T>()
        //{

        //}

        //public bool SetComplexData(string username, string[] permissions)
        //{
        //    string path = @"c:\temp\{username}\MyTest.txt";

        //    // Delete the file if it exists.
        //    if (File.Exists(path))
        //    {
        //        File.Delete(path);
        //    }

        //    using (System.IO.StreamWriter file = new System.IO.StreamWriter(System.IO.File.Create(path).Dispose()))
        //    {
        //        file.WriteLine("your text here");
        //    }

        //}

        public static void WriteToFile(string username, string Text)
        {
            //Check Whether directory exist or not if not then create it
            if (!Directory.Exists(@"c:\JStemp"))
            {
                Directory.CreateDirectory(@"c:\JStemp");
            }

            string FilePath = @$"c:\JStemp\PermissionPerUsers\{username}.txt";

            // Delete the file if it exists.
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }

            //Check Whether file exist or not if not then create it new else append on same file

            //using (StreamWriter file = new StreamWriter(File.Create(FilePath).DisposeAsync()))
            //{
            //    file.WriteLine("your text here");
            //}

            if (!File.Exists(FilePath))
            {
                File.WriteAllText(FilePath, Text);
            }
            else
            {
                Text = $"{Environment.NewLine}{Text}";
                File.AppendAllText(FilePath, Text);
            }
        }
    }

}
