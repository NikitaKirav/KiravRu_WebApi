using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using KiravRu.Infrastructure;

namespace KiravRu.Infrastructure
{
    public static class ImageDrawing
    {
        const int size = 60;
        const int quality = 50;
        const string path = @"wwwroot/images/art/imageDrawing/";

        public static void OptimizationImage(string inputPath, string outputPath)
        {
            //using (var image = new Bitmap(System.Drawing.Image.FromFile(inputPath)))
            using (FileStream fileStream = new FileStream(inputPath, FileMode.Open))
            {
                var image = new Bitmap(System.Drawing.Image.FromStream(fileStream));
                int width, height;
                if (image.Width > image.Height)
                {
                    width = size;
                    height = Convert.ToInt32(image.Height * size / (double)image.Width);
                }
                else
                {
                    width = Convert.ToInt32(image.Width * size / (double)image.Height);
                    height = size;
                }
                var resized = new Bitmap(width, height);
                using (var graphics = Graphics.FromImage(resized))
                {
                    graphics.CompositingQuality = CompositingQuality.HighSpeed;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.DrawImage(image, 0, 0, width, height);
                    using (var output = File.Open(outputPath, FileMode.Create))
                    {
                        var qualityParamId = Encoder.Quality;
                        var encoderParameters = new EncoderParameters(1);
                        encoderParameters.Param[0] = new EncoderParameter(qualityParamId, quality);
                        var codec = ImageCodecInfo.GetImageDecoders()
                            .FirstOrDefault(codec => codec.FormatID == ImageFormat.Png.Guid);
                        resized.Save(output, codec, encoderParameters);
                        output.Dispose();
                        output.Close();
                    }
                }
                fileStream.Dispose();
                fileStream.Close();
            }            
        }

        public static string SaveImage(string image)
        {
            string fileNameWitPath = path + (GetLastNumberOfFile() + 1).ToString() + ".png";
            string fileNameWitPathNew = path + (GetLastNumberOfFile() + 1).ToString() + "_new.png";
            using (FileStream fs = new FileStream(fileNameWitPath, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(image);
                    bw.Write(data);
                    bw.Dispose();
                    bw.Close();
                }
                fs.Dispose();
                fs.Close();
            }
            OptimizationImage(fileNameWitPath, fileNameWitPathNew);
            RenameFileAndDelete(fileNameWitPathNew, fileNameWitPath);
            return fileNameWitPath;
        }

        /// <summary>
        /// To delete first file and rename second file
        /// </summary>
        public static void RenameFileAndDelete(string oldFileName, string newFileName)
        {
            File.Delete(newFileName); // Delete the existing file if exists
            File.Move(oldFileName, newFileName); // Rename the oldFileName into newFileName
        }

        public static int GetLastNumberOfFile()
        {
            var files = BubbleSort(GetFilesList());
            if (files.Count() == 0) { return 0; }
            int number = Convert.ToInt32(Regex.Match(files[files.Length - 1], @"^([0-9]*).png").Groups[1].Value);
            return number;
        }

        /// <summary>
        /// Get List of files in folder
        /// </summary>
        /// <returns></returns>
        public static string[] GetFilesList()
        {
            return Directory.GetFiles(path).Select(fn => Path.GetFileName(fn)).ToArray();
        }

        /// <summary>
        /// Sorted files by bubble method
        /// </summary>
        /// <param name="array"></param>
        public static string[] BubbleSort(string[] array)
        {
            if (array.Count() == 0) { return new string[0]; }
            int name, name1;
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length - 1; j++)
                {
                    name = Convert.ToInt32(Regex.Match(array[j], @"^([0-9]*)(.png)").Groups[1].Value);
                    name1 = Convert.ToInt32(Regex.Match(array[j + 1], @"^([0-9]*)(.png)").Groups[1].Value);
                    if (name > name1)
                    {
                        string t = array[j + 1];
                        array[j + 1] = array[j];
                        array[j] = t;
                    }
                }
            }
            return array;
        }
    }
}
