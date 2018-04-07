using HarrisBlog.Repository;
using HarrisZhang.Repository.Entity;
using HeyRed.MarkdownSharp;
using Savory.Rss;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace HarrisBlog.Gen
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1. 根据markdown生成html");
            Console.WriteLine("2. 根据数据库生成日志");
            Console.WriteLine("3. 根据数据库生成说说");
            Console.WriteLine("4. 更新数据库图片地址");
            Console.WriteLine("5. 生成RSS文件");
            Console.WriteLine("6. 生成说说需要的图片");
            Console.WriteLine("7. 测试图片裁切");
            Console.WriteLine("8. 处理图片");
            Console.Write("请选择需要执行的操作：");

            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    GenMarkdown();

                    break;
                case "2":
                    GenDataFile();

                    break;
                case "3":
                    GenTalks();

                    break;
                case "4":
                    UpdateLocalPath();

                    break;
                case "5":
                    BuilderRSS();

                    break;
                case "6":
                    CreateImage();

                    break;
                case "7":
                    TestCorpImage();

                    break;
                case "8":
                    ProcessImage();

                    break;
                default:
                    break;
            }

            //Console.WriteLine("按任意键退出.");
            //Console.ReadKey();
        }

        static void GenMarkdown()
        {
            Markdown mark = new Markdown();

            HarrisBlogDataContext context = new HarrisBlogDataContext();

            foreach (var item in context.Post)
            {
                if (string.IsNullOrEmpty(item.MarkdownBody))
                {
                    continue;
                }

                item.HtmlBody = mark.Transform(item.MarkdownBody);
            }

            context.SubmitChanges();
        }

        static void GenDataFile()
        {
            var xmlFilePath = @"D:\CodingWorkspace\HarrisBlog\HarrisZhang\App_Data\posts.xml";

            var folderPath = @"D:\CodingWorkspace\HarrisBlog\HarrisZhang\App_Data\posts";

            HarrisBlogDataContext context = new HarrisBlogDataContext();

            var posts = context.Post.ToList();

            {
                List<PostsEntity> postsEntityList = new List<PostsEntity>();
                foreach (var post in posts)
                {
                    PostsEntity postsEntity = new PostsEntity();
                    postsEntity.Ename = post.Ename;
                    postsEntity.Title = post.Title;
                    postsEntity.Summary = post.Summary;
                    postsEntity.PublishTime = post.CreateTime.Value;

                    postsEntityList.Add(postsEntity);
                }

                WriteToFile(xmlFilePath, postsEntityList);
            }

            {
                foreach (var post in posts)
                {
                    PostEntity postEntity = new PostEntity();

                    postEntity.Ename = post.Ename;
                    postEntity.Title = post.Title;
                    postEntity.Summary = post.Summary;
                    postEntity.PublishTime = post.CreateTime.Value;
                    postEntity.HtmlBody = post.HtmlBody;

                    var path = Path.Combine(folderPath, post.Ename + ".xml");

                    WriteToFile(path, postEntity);
                }
            }
        }

        static void GenTalks()
        {
            var xmlFilePath = @"D:\CodingWorkspace\HarrisBlog\HarrisZhang\App_Data\talks.xml";

            HarrisBlogDataContext context = new HarrisBlogDataContext();

            var talks = context.talk.ToList();

            {
                List<TalksEntity> postsEntityList = new List<TalksEntity>();
                foreach (var talk in talks)
                {
                    if (!"图文说说".Equals(talk.Category) && !"文字说说".Equals(talk.Category))
                    {
                        continue;
                    }

                    TalksEntity talksEntity = new TalksEntity();
                    talksEntity.Content = talk.MsgContent;
                    talksEntity.PublishTime = talk.CreateTime.Value;
                    talksEntity.ImageList = (from talkImage in context.talk_image
                                             join originalImage in context.original_image on talkImage.ImageId equals originalImage.ImageId
                                             join corpImage in context.corp_image on originalImage.ImageId equals corpImage.OriginalImageId
                                             where talkImage.TalkId == talk.TalkId
                                             select corpImage.RemoteFilePath
                                             ).ToList();

                    postsEntityList.Add(talksEntity);
                }

                WriteToFile(xmlFilePath, postsEntityList);
            }
        }



        static void ProcessImage()
        {
            HarrisBlogDataContext context = new HarrisBlogDataContext();

            ImageProcessor processor = new ImageProcessor();

            var originalImages = context.original_image.ToList();
            foreach (var originalImage in originalImages)
            {
                var talk_image = context.talk_image.FirstOrDefault(v => v.ImageId == originalImage.ImageId);
                if (talk_image == null)
                {
                    continue;
                }

                string corpImageId = Guid.NewGuid().ToString("N");
                string corpImageLocalFilePath = originalImage.LocalFilePath.Replace("LocalImage", "LocalImageCorp");

                var talk_image_s = context.talk_image.Where(v => v.TalkId == talk_image.TalkId).ToList();

                corp_image corp_image = new corp_image();
                corp_image.CorpImageId = corpImageId;
                corp_image.OriginalImageId = originalImage.ImageId;
                corp_image.LocalFilePath = corpImageLocalFilePath;
                corp_image.FileName = corpImageId + originalImage.FileExtension;
                corp_image.FileExtension = originalImage.FileExtension;
                context.corp_image.InsertOnSubmit(corp_image);

                using (FileStream stream = new FileStream(originalImage.LocalFilePath, FileMode.Open))
                {
                    using (FileStream ss = new FileStream(corpImageLocalFilePath, FileMode.Create))
                    {
                        if (talk_image_s.Count == 1)
                        {
                            using (Stream outStream = processor.ResizeByWidth(stream, 260))
                            {
                                outStream.CopyTo(ss);
                            }
                        }
                        else
                        {
                            using (Stream outStream = processor.ResizeImage(stream, 260, 200))
                            {
                                outStream.CopyTo(ss);
                            }
                        }
                    }

                }

            }

            context.SubmitChanges();
        }

        static void UpdateLocalPath()
        {

            //HarrisBlogDataContext context = new HarrisBlogDataContext();

            //var talkImages = context.TalkImage;

            //var root = @"E:\LocalImage";

            //foreach (var talkImage in talkImages)
            //{
            //    //talkImage.RemoteFilePath = talkImage.LocalFilePath.Replace(root, "https://image.harriszhang.com").Replace(@"\", "/");

            //    talkImage.FakeFilePath = talkImage.LocalFilePath.Replace(root, "http://localimage.harriszhang.com").Replace(@"\", "/");

            //}

            //context.SubmitChanges();
        }

        static void BuilderRSS()
        {
            var rssFilePath = @"D:\CodingWorkspace\HarrisBlog\HarrisZhang\rss.xml";

            RssDocument doc = new RssDocument();

            HarrisBlogDataContext context = new HarrisBlogDataContext();

            var posts = context.Post.OrderByDescending(v => v.CreateTime).Take(10).ToList();

            {
                var channel = doc.Channel = new Channel();

                channel.Title = "从善如流";
                channel.Link = "https://www.harriszhang.com";
                channel.Description = "翩若惊鸿少年时，流水文思可堪记。";
                channel.PubDate = DateTime.Now.GetDateTimeFormats('r')[0].ToString();
                channel.LastBuildDate = DateTime.Now.GetDateTimeFormats('r')[0].ToString();
                channel.Language = Language.Chinese_Simplified;
                channel.Generator = "www.harriszhang.com";
            }

            {
                var items = doc.Items = new List<Item>();

                foreach (var post in posts)
                {
                    var item = new Item();

                    item.Title = post.Title;
                    item.Link = $"https://www.harriszhang.com/post/{post.Ename}";
                    item.Description = post.Summary;
                    item.Author = "从善如流";
                    item.PubDate = post.CreateTime.Value.GetDateTimeFormats('r')[0].ToString();

                    items.Add(item);
                }
            }

            RssBuilder builder = new RssBuilder();

            var content = builder.Build(doc);

            File.WriteAllText(rssFilePath, content);
        }

        static void CreateImage()
        {
            //    HarrisBlogDataContext context = new HarrisBlogDataContext();

            //    var talkImages = context.TalkImage.ToList();

            //    ImageProcessor processor = new ImageProcessor();

            //    foreach (var talkImage in talkImages)
            //    {
            //        using (FileStream inputSream = new FileStream(talkImage.LocalFilePath, FileMode.Open))
            //        {

            //        }
            //    }
        }

        static void TestCorpImage()
        {
            ImageProcessor processor = new ImageProcessor();

            {
                var path = @"F:\tmp\5b802a795ec040bd8c0c032f6a0fd24c.jpg";
                var target = @"F:\tmp\target_1.jpg";
                var width = 400;
                var height = 300;
                using (FileStream inputStream = new FileStream(path, FileMode.Open))
                {
                    using (var stream = processor.ResizeImage(inputStream, width, height, ResizePolicy.Quick))
                    {
                        using (FileStream outputStream = new FileStream(target, FileMode.Create))
                        {
                            stream.CopyTo(outputStream);
                        }
                    }
                }
            }

            {
                var path = @"F:\tmp\5b802a795ec040bd8c0c032f6a0fd24c.jpg";
                var target = @"F:\tmp\target_2_1.jpg";
                var width = 50;
                var height = 160;
                using (FileStream inputStream = new FileStream(path, FileMode.Open))
                {
                    using (var stream = processor.ResizeByWidth(inputStream, width))
                    {
                        using (FileStream outputStream = new FileStream(target, FileMode.Create))
                        {
                            stream.CopyTo(outputStream);
                        }
                    }
                }
            }

            {
                var path = @"F:\tmp\5b802a795ec040bd8c0c032f6a0fd24c.jpg";
                var target = @"F:\tmp\target_2_2.jpg";
                var width = 500;
                var height = 50;
                using (FileStream inputStream = new FileStream(path, FileMode.Open))
                {
                    using (var stream = processor.ResizeByWidth(inputStream, width))
                    {
                        using (FileStream outputStream = new FileStream(target, FileMode.Create))
                        {
                            stream.CopyTo(outputStream);
                        }
                    }
                }
            }
        }

        static void WriteToFile(string path, object item)
        {
            XmlSerializer serializer = new XmlSerializer(item.GetType());

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(stream, item);
            }
        }
    }
}
