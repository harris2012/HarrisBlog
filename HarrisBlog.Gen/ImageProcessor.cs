using ImageMagick;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarrisBlog.Gen
{
    public class ImageProcessor
    {
        //public Stream Watermark(Stream stream)
        //{
        //    MemoryStream outStream = new MemoryStream();

        //    using (MagickImage water = new MagickImage(@"D:\PsWorkspace\savory - 2015\savory_128_64_water_1.png"))
        //    {
        //        using (MagickImage image = new MagickImage(stream))
        //        {
        //            image.Composite(water, Gravity.Southeast, CompositeOperator.Atop);

        //            image.Interlace = Interlace.Line;

        //            image.Write(outStream);
        //        }
        //    }

        //    outStream.Seek(0, SeekOrigin.Begin);

        //    return outStream;
        //}

        /// <summary>
        /// 使用指定尺寸裁切一张图片
        /// </summary>
        /// <param name="stream">原始图片数据流</param>
        /// <param name="width">目标宽度</param>
        /// <param name="height">目标高度</param>
        /// <returns>裁剪后的图片数据流</returns>
        public Stream ResizeImage(Stream stream, int width, int height)
        {
            return ResizeImage(stream, width, height, ResizePolicy.Auto);
        }

        /// <summary>
        /// 使用指定尺寸裁切一张图片。该方法会保持原始纵横比，但是会剪掉多余的宽或者高
        /// 支持jpg png
        /// </summary>
        /// <param name="stream">原始图片数据流</param>
        /// <param name="targetWidth">目标宽度</param>
        /// <param name="targetHeight">目标高度</param>
        /// <param name="policy">
        /// <see cref="Ranta.WechatManager.Core.ResizePolicy"/>
        /// </param>
        /// <returns>裁剪后的图片数据流</returns>
        public Stream ResizeImage(Stream stream, int targetWidth, int targetHeight, ResizePolicy policy)
        {
            MemoryStream outStream = new MemoryStream();

            using (MagickImage originalImage = new MagickImage(stream))
            {
                using (MemoryStream jpgStream = new MemoryStream())
                {
                    originalImage.Write(jpgStream, MagickFormat.Jpg);

                    jpgStream.Seek(0, SeekOrigin.Begin);

                    using (MagickImage jpgImage = new MagickImage(jpgStream))
                    {
                        jpgImage.Interlace = Interlace.Line;

                        int actualWidth, actualHeight;
                        {
                            int possibleHeight = jpgImage.Width * targetHeight / targetWidth;
                            int possibleWidth = jpgImage.Height * targetWidth / targetHeight;

                            if (possibleWidth == targetWidth)
                            {
                                actualWidth = possibleWidth;
                                actualHeight = possibleHeight;
                            }
                            else if (possibleWidth < jpgImage.Width)
                            {
                                actualWidth = possibleWidth;
                                actualHeight = jpgImage.Height;
                            }
                            else
                            {
                                actualWidth = jpgImage.Width;
                                actualHeight = possibleHeight;
                            }
                        }

                        MagickGeometry size = new MagickGeometry(actualWidth, actualHeight);

                        jpgImage.Crop(size.Width, size.Height, Gravity.Center);

                        var targetSize = new MagickGeometry(targetWidth, targetHeight);
                        targetSize.IgnoreAspectRatio = true;

                        switch (policy)
                        {
                            case ResizePolicy.Quick:
                                jpgImage.AdaptiveResize(targetSize);

                                break;
                            case ResizePolicy.HighQuality:
                                jpgImage.Resize(targetSize);

                                break;
                            case ResizePolicy.Auto:
                            default:
                                if ((Math.Abs((size.Width - targetWidth) / (float)size.Width) <= 0.5) &&
                                    (Math.Abs((size.Height - targetHeight) / (float)size.Height) <= 0.5))
                                {
                                    jpgImage.AdaptiveResize(targetSize);
                                }
                                else
                                {
                                    jpgImage.Resize(targetSize);
                                }

                                break;
                        }

                        jpgImage.Write(outStream);

                        outStream.Seek(0, SeekOrigin.Begin);
                    }
                }
            }

            return outStream;
        }

        /// <summary>
        /// 保持纵横比，根据宽度调整宽度
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public Stream ResizeByWidth(Stream stream, int targetWidth)
        {
            MemoryStream outStream = new MemoryStream();

            using (MagickImage originalImage = new MagickImage(stream))
            {
                using (MemoryStream jpgStream = new MemoryStream())
                {
                    originalImage.Write(jpgStream, MagickFormat.Jpg);

                    jpgStream.Seek(0, SeekOrigin.Begin);

                    using (MagickImage jpgImage = new MagickImage(jpgStream))
                    {
                        jpgImage.Interlace = Interlace.Line;

                        var targetSize = new MagickGeometry(targetWidth, originalImage.Height);

                        jpgImage.AdaptiveResize(targetSize);
                        //jpgImage.Resize(targetSize);

                        jpgImage.Write(outStream);

                        outStream.Seek(0, SeekOrigin.Begin);
                    }
                }
            }

            return outStream;
        }
    }
}
