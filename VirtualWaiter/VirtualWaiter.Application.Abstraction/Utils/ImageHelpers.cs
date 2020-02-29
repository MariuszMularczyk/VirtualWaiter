using VirtualWaiter.Resources.Shared;
using VirtualWaiter.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace VirtualWaiter.Application
{
    public static class ImageHelpers
    {
        public static string GetExtensionFileIcon(string extension)
        {
            if (extension.IsNullOrEmpty())
                return "/images/txt.svg";
            if (extension.Contains("."))
                extension = Path.GetExtension(extension);
            switch (extension)
            {
                case ".txt":
                    return "/images/txt.svg";
                case ".pdf":
                    return "/images/pdf.svg";
                case ".zip":
                    return "/images/zip.svg";
                case ".doc":
                    return "/images/doc.svg";
                case ".docx":
                    return "/images/doc.svg";
                default:
                    return "/images/txt.svg";
            }
        }

        //Overload for crop that default starts top left of the image.
        public static Image CropImage(Image Image, int Height, int Width)
        {
            return CropImage(Image.CorrectRotation(), Height, Width, 0, 0);
        }

        public static Stream CropAndResize(Stream stream, int ResizeWidth, int ResizeHeight, int CropWidth, int CropHeight, int startAtX = 0, int startAtY = 0, long jpegQuality = 90L)
        {
            Image image = Image.FromStream(stream).CorrectRotation();
            Image resultImage = CropImage(image, CropHeight, CropWidth, startAtX, startAtY);
            MemoryStream memoryStream = new MemoryStream();

            //resultImage.Save(memoryStream, resultImage.RawFormat);
            SaveImageToStreamWithFormat(ref memoryStream, resultImage);

            memoryStream.Position = 0;

            resultImage.Dispose();
            image.Dispose();

            return MyResizeImage(ResizeWidth, ResizeHeight, memoryStream, jpegQuality);
        }

        ////The crop image sub
        private static Image CropImage(Image Image, int Height, int Width, int StartAtX, int StartAtY)
        {
            Image outimage;
            MemoryStream memoryStream = null;
            try
            {
                //check the image height against our desired image height
                if (Image.Height < Height)
                {
                    Height = Image.Height;
                }

                if (Image.Width < Width)
                {
                    Width = Image.Width;
                }

                //create a bitmap window for cropping
                Bitmap bmPhoto = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
                bmPhoto.MakeTransparent();
                bmPhoto.SetResolution(72, 72);

                //create a new graphics object from our image and set properties
                Graphics grPhoto = Graphics.FromImage(bmPhoto);
                grPhoto.CompositingMode = CompositingMode.SourceOver;
                grPhoto.CompositingQuality = CompositingQuality.HighQuality;
                grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
                grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
                grPhoto.PixelOffsetMode = PixelOffsetMode.HighQuality;

                //now do the crop
                grPhoto.DrawImage(Image, new Rectangle(0, 0, Width, Height), StartAtX, StartAtY, Width, Height, GraphicsUnit.Pixel);

                // Save out to memory and get an image from it to send back out the method.
                memoryStream = new MemoryStream();
                SaveImageToStreamWithFormat(ref memoryStream, bmPhoto, Image.RawFormat);
                //bmPhoto.Save(mm, Image.RawFormat);
                Image.Dispose();
                bmPhoto.Dispose();
                grPhoto.Dispose();
                outimage = Image.FromStream(memoryStream);

                return outimage;
            }
            catch (Exception ex)
            {
                throw new Exception("Error cropping image, the error was: " + ex.Message);
            }
        }

        //Hard resize attempts to resize as close as it can to the desired size and then crops the excess
        public static Stream HardResizeImage(int Width, int Height, Stream stream)
        {
            Image Image = Image.FromStream(stream);
            int width = Image.Width;
            int height = Image.Height;
            Image resized = null;
            if (Width > Height)
            {
                resized = ResizeImage(Width, Width, Image);
            }
            else
            {
                resized = ResizeImage(Height, Height, Image);
            }
            Image output = CropImage(resized, Height, Width);
            //return the original resized image
            MemoryStream memoryStream = new MemoryStream();
            output.Save(memoryStream, ImageFormat.Jpeg);
            memoryStream.Position = 0;

            return memoryStream;
        }

        ////Image resizing
        private static Image ResizeImage(int maxWidth, int maxHeight, Image Image)
        {
            int width = Image.Width;
            int height = Image.Height;
            if (width > maxWidth || height > maxHeight)
            {
                //The flips are in here to prevent any embedded image thumbnails -- usually from cameras
                //from displaying as the thumbnail image later, in other words, we want a clean
                //resize, not a grainy one.
                Image.RotateFlip(RotateFlipType.Rotate180FlipX);
                Image.RotateFlip(RotateFlipType.Rotate180FlipX);

                float ratio = 0;
                if (width > height)
                {
                    ratio = (float)width / (float)height;
                    width = maxWidth;
                    height = Convert.ToInt32(Math.Round((float)width / ratio));
                }
                else
                {
                    ratio = (float)height / (float)width;
                    height = maxHeight;
                    width = Convert.ToInt32(Math.Round((float)height / ratio));
                }

                Bitmap bitmapImage = new Bitmap(width, height);
                bitmapImage.MakeTransparent();
                using (Graphics graphics = Graphics.FromImage(bitmapImage))
                {
                    graphics.CompositingMode = CompositingMode.SourceOver;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.DrawImage(Image, new Rectangle(0, 0, width, height));
                }

                return bitmapImage;
                //return the resized image
                //return Image.GetThumbnailImage(width, height, null, IntPtr.Zero);
            }
            //return the original resized image
            return Image;
        }

        public static Stream MyResizeImage(int Width, int Height, Stream stream, long jpegQuality = 90L)
        {
            Image Image = Image.FromStream(stream).CorrectRotation();

            double ratio = (double)Width / (double)Height;
            double imageRatio = (double)Image.Width / (double)Image.Height;
            if (ratio != imageRatio)
            {
                double ratioWidth = (double)Image.Width / (double)Width;
                double ratioHeight = (double)Image.Height / (double)Height;
                if (ratioWidth < ratioHeight)
                {
                    var width = Image.Width;
                    var height = (int)(ratioWidth * Height);
                    int heightDiff = (int)((Image.Height - (ratioWidth * Height)) / 2);
                    Image = CropImage(Image, height, width, 0, heightDiff);
                    Image = ResizeImage(Width, Height, Image);
                }
                else
                {
                    var height = Image.Height;
                    var width = (int)(ratioHeight * Width);
                    int widthDiff = (int)((Image.Width - (ratioHeight * Width)) / 2);
                    Image = CropImage(Image, height, width, widthDiff, 0);
                    Image = ResizeImage(Width, Height, Image);
                }
            }
            else
            {
                Image = ResizeImage(Width, Height, Image);
            }

            MemoryStream memoryStream = new MemoryStream();

            SaveImageToStreamWithFormat(ref memoryStream, Image, jpegQuality: jpegQuality);

            memoryStream.Position = 0;

            return memoryStream;
        }

        private static void SaveImageToStreamWithFormat(ref MemoryStream stream, Image image, ImageFormat imageFormat = null, long jpegQuality = 100L)
        {
            ImageFormat format = imageFormat == null ? image.GetImageFormat() : imageFormat.GetFromRaw();
            ImageCodecInfo imageCodecInfo = ImageCodecInfo.GetImageEncoders().FirstOrDefault(x => x.FormatID == format.Guid);

            if (imageCodecInfo == null)
            {
                image.Save(stream, ImageFormat.Bmp);
            }
            else
            {
                if (ImageFormat.Jpeg.Equals(format) && imageCodecInfo != null)
                {
                    EncoderParameters encoderParameters = new EncoderParameters(1);
                    encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, jpegQuality);
                    image.Save(stream, imageCodecInfo, encoderParameters);
                }
                else
                    image.Save(stream, format);
            }
        }

        private static ImageFormat GetImageFormat(this Image image)
        {
            if (ImageFormat.Jpeg.EqualsOr(image.RawFormat))
                return ImageFormat.Jpeg;
            else if (ImageFormat.Png.EqualsOr(image.RawFormat))
                return ImageFormat.Png;
            else if (ImageFormat.Bmp.EqualsOr(image.RawFormat))
                return ImageFormat.Bmp;
            else if (ImageFormat.Gif.EqualsOr(image.RawFormat))
                return ImageFormat.Gif;
            else if (ImageFormat.Icon.EqualsOr(image.RawFormat))
                return ImageFormat.Icon;
            else if (ImageFormat.Emf.EqualsOr(image.RawFormat))
                return ImageFormat.Emf;
            else if (ImageFormat.Exif.EqualsOr(image.RawFormat))
                return ImageFormat.Exif;
            else if (ImageFormat.MemoryBmp.EqualsOr(image.RawFormat))
                return ImageFormat.MemoryBmp;
            else if (ImageFormat.Tiff.EqualsOr(image.RawFormat))
                return ImageFormat.Tiff;
            else if (ImageFormat.Wmf.EqualsOr(image.RawFormat))
                return ImageFormat.Wmf;
            else
                return ImageFormat.Jpeg;
        }

        private static ImageFormat GetFromRaw(this ImageFormat rawFormat)
        {
            if (ImageFormat.Jpeg.Equals(rawFormat))
                return ImageFormat.Jpeg;
            else if (ImageFormat.Png.Equals(rawFormat))
                return ImageFormat.Png;
            else if (ImageFormat.Bmp.Equals(rawFormat))
                return ImageFormat.Bmp;
            else if (ImageFormat.Gif.Equals(rawFormat))
                return ImageFormat.Gif;
            else if (ImageFormat.Icon.Equals(rawFormat))
                return ImageFormat.Icon;
            else if (ImageFormat.Emf.Equals(rawFormat))
                return ImageFormat.Emf;
            else if (ImageFormat.Exif.Equals(rawFormat))
                return ImageFormat.Exif;
            else if (ImageFormat.MemoryBmp.Equals(rawFormat))
                return ImageFormat.MemoryBmp;
            else if (ImageFormat.Tiff.Equals(rawFormat))
                return ImageFormat.Tiff;
            else if (ImageFormat.Wmf.Equals(rawFormat))
                return ImageFormat.Wmf;
            else
                return ImageFormat.Jpeg;
        }

        private static Image CorrectRotation(this Image image)
        {
            //check if image contains an information about rotation
            if (image.PropertyIdList.Contains(0x0112))
            {
                int rotationValue = image.GetPropertyItem(0x0112).Value[0];
                //counter rotate/flip the image to get its intended orientation
                switch (rotationValue)
                {
                    case 1:
                        break;
                    case 2:
                        image.RotateFlip(RotateFlipType.RotateNoneFlipY);
                        break;
                    case 3:
                        image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        break;
                    case 4:
                        image.RotateFlip(RotateFlipType.Rotate180FlipY);
                        break;
                    case 5:
                        image.RotateFlip(RotateFlipType.Rotate90FlipY);
                        break;
                    case 6:
                        image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        break;
                    case 7:
                        image.RotateFlip(RotateFlipType.Rotate270FlipY);
                        break;
                    case 8:
                        image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        break;
                    default:
                        break;
                }
            }

            return image;
        }

        public static Stream ResizeFromBase64(int width, int height, string base64String)
        {
            string sanitizedBase64String = Regex.Replace(base64String, @"data:image/[a-z]+;base64,", "");
            try
            {
                byte[] byteArray = Convert.FromBase64String(sanitizedBase64String);
                Stream stream = new MemoryStream(byteArray);
                return MyResizeImage(width, height, stream);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static void CopyImageAndSave(Image image, string outputPath, ImageFormat format)
        {
            Bitmap bmPhoto = new Bitmap(image.Width, image.Height, PixelFormat.Format24bppRgb);
            bmPhoto.MakeTransparent();
            bmPhoto.SetResolution(72, 72);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.CompositingMode = CompositingMode.SourceOver;
            grPhoto.CompositingQuality = CompositingQuality.HighQuality;
            grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
            grPhoto.PixelOffsetMode = PixelOffsetMode.HighQuality;

            grPhoto.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);

            bmPhoto.Save(outputPath, format);
        }
    }
}
