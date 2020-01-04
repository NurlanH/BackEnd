using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;

namespace DevFormAz.Extentions
{
    public static class İmageControl
    {
        public static bool CheckImageType(HttpPostedFileBase Image)
        {
            return Image.ContentType == "image/jpg" || Image.ContentType == "image/jpeg" || Image.ContentType == "image/png" || Image.ContentType == "image/gif";

        }

        public static bool CheckImageSize(HttpPostedFileBase Image, int mb)
        {
            return Image.ContentLength / 1024 / 1024 < mb;
        }

        public static string SaveImage(string folder, HttpPostedFileBase Image)
        {
            string filename = DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + Path.GetFileName(Image.FileName);

            string path = Path.Combine(folder, filename);

            Image.SaveAs(path);

            return filename;
        }

        public static bool DeleteImage(string folder, string filename)
        {
            string pathToImage = Path.Combine(folder, filename);

            if (File.Exists(pathToImage))
            {
                File.Delete(pathToImage);
                return true;
            }

            return false;
        }
    }
}