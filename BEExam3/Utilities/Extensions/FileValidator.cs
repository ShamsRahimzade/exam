﻿namespace BEExam3.Utilities.Extensions
{
    public static class FileValidator
    {
        public static bool CheckSize(this IFormFile file,int size)
        {
            if (file.Length<=size*1024*1024)
            {
                return true;
            }
            return false;
        }
        public static bool CheckType(this IFormFile file, string type)
        {
            if (file.ContentType.Contains(type))
            {
                return true;
            }
            return false;
        }
        public static async Task<string> CreateFile(this IFormFile file,string root,params string[] folders)
        {
            string filename=Guid.NewGuid().ToString()+file.FileName;
            for (int i = 0; i < folders.Length; i++)
            {
                root = Path.Combine(root, folders[i]);
            }
            root = Path.Combine(root, filename);
            using (FileStream stream = new FileStream(root,FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return filename;
        }
        public static void DeleteFile(this string filename, string root, params string[] folders)
        {
            for (int i = 0; i < folders.Length; i++)
            {
                root = Path.Combine(root, folders[i]);
            }
            root = Path.Combine(root, filename);
            if (File.Exists(root))
            {
                File.Delete(root);
            }
        }
    }
}
