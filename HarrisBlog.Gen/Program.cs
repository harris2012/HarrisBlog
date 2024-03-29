﻿//using HarrisBlog.Repository;
using HeyRed.MarkdownSharp;
using Repository.Entity;
using Savory.Dapper;
using Savory.Rss;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace HarrisBlog.Gen
{
    class Program
    {
        static void Main(string[] args)
        {


        }

        private static string StripTagsRegex(string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty).Replace("[\r\n]", string.Empty);
        }

        static void Main_old(string[] args)
        {
            //            Console.WriteLine("1. 根据markdown生成html");
            //            Console.WriteLine("2. 根据数据库生成日志");
            //            Console.WriteLine("3. 根据数据库生成说说");
            //            Console.WriteLine("4. 更新数据库图片地址");
            //            Console.WriteLine("5. 生成RSS文件");
            //            Console.WriteLine("6. 生成说说需要的图片");
            //            Console.WriteLine("7. 测试图片裁切");
            //            Console.WriteLine("8. 处理图片");
            //            Console.WriteLine("9. 合并说说和日志");
            //            Console.Write("请选择需要执行的操作：");

            //            var input = Console.ReadLine();
            //            switch (input)
            //            {
            //                case "1":
            //                    GenMarkdown();

            //                    break;
            //                case "2":
            //                    GenDataFile();

            //                    break;
            //                case "3":
            //                    GenTalks();

            //                    break;
            //                case "4":
            //                    UpdateLocalPath();

            //                    break;
            //                case "5":
            //                    BuilderRSS();

            //                    break;
            //                case "6":
            //                    CreateImage();

            //                    break;
            //                case "9":
            //                    CombineData();

            //                    break;
            //                default:
            //                    break;
            //            }

            //            //Console.WriteLine("按任意键退出.");
            //            //Console.ReadKey();
        }

        //        static void GenMarkdown()
        //        {
        //            Markdown mark = new Markdown();

        //            HarrisBlogDataContext context = new HarrisBlogDataContext();

        //            foreach (var item in context.Post)
        //            {
        //                if (string.IsNullOrEmpty(item.MarkdownBody))
        //                {
        //                    continue;
        //                }

        //                item.HtmlBody = mark.Transform(item.MarkdownBody);
        //            }

        //            context.SubmitChanges();
        //        }

        //        static void CombineData()
        //        {
        //            var postFilePath = @"D:\CodingWorkspace\HarrisBlog\HarrisZhang\App_Data\posts.xml";

        //            var talkFilePath = @"D:\CodingWorkspace\HarrisBlog\HarrisZhang\App_Data\talks.xml";

        //            var itemFilePath = @"D:\CodingWorkspace\HarrisBlog\HarrisZhang\App_Data\items.xml";

        //            List<TheEntity> theEntityList = new List<TheEntity>();

        //            {
        //                XmlSerializer serializer = new XmlSerializer(typeof(List<PostsEntity>));

        //                var content = File.ReadAllText(postFilePath);

        //                using (StringReader reader = new StringReader(content))
        //                {
        //                    var items = (List<PostsEntity>)serializer.Deserialize(reader);

        //                    foreach (var item in items)
        //                    {
        //                        var entity = ToTheEntity(item);
        //                        theEntityList.Add(entity);
        //                    }
        //                }
        //            }

        //            {
        //                XmlSerializer serializer = new XmlSerializer(typeof(List<TalksEntity>));

        //                var content = File.ReadAllText(talkFilePath);

        //                using (StringReader reader = new StringReader(content))
        //                {
        //                    var items = (List<TalksEntity>)serializer.Deserialize(reader);

        //                    foreach (var item in items)
        //                    {
        //                        var entity = ToTheEntity(item);
        //                        theEntityList.Add(entity);
        //                    }
        //                }
        //            }

        //            WriteToFile(itemFilePath, theEntityList);
        //        }

        //        static void GenDataFile()
        //        {
        //            var xmlFilePath = @"D:\CodingWorkspace\HarrisBlog\HarrisZhang\App_Data\posts.xml";

        //            var folderPath = @"D:\CodingWorkspace\HarrisBlog\HarrisZhang\App_Data\posts";

        //            HarrisBlogDataContext context = new HarrisBlogDataContext();

        //            var posts = context.Post.ToList();

        //            {
        //                List<PostsEntity> postsEntityList = new List<PostsEntity>();
        //                foreach (var post in posts)
        //                {
        //                    PostsEntity postsEntity = new PostsEntity();
        //                    postsEntity.Ename = post.Ename;
        //                    postsEntity.Title = post.Title;
        //                    postsEntity.Summary = post.Summary;
        //                    postsEntity.PublishTime = post.CreateTime.Value;

        //                    postsEntityList.Add(postsEntity);
        //                }

        //                WriteToFile(xmlFilePath, postsEntityList);
        //            }

        //            {
        //                foreach (var post in posts)
        //                {
        //                    PostEntity postEntity = new PostEntity();

        //                    postEntity.Ename = post.Ename;
        //                    postEntity.Title = post.Title;
        //                    postEntity.Summary = post.Summary;
        //                    postEntity.PublishTime = post.CreateTime.Value;
        //                    postEntity.HtmlBody = post.HtmlBody;

        //                    var path = Path.Combine(folderPath, post.Ename + ".xml");

        //                    WriteToFile(path, postEntity);
        //                }
        //            }
        //        }

        //        static void GenTalks()
        //        {
        //            var xmlFilePath = @"D:\CodingWorkspace\HarrisBlog\HarrisZhang\App_Data\talks.xml";

        //            HarrisBlogDataContext context = new HarrisBlogDataContext();

        //            var talks = context.talk.ToList();

        //            {
        //                List<TalksEntity> postsEntityList = new List<TalksEntity>();
        //                foreach (var talk in talks)
        //                {
        //                    if (!"图文说说".Equals(talk.Category) && !"文字说说".Equals(talk.Category))
        //                    {
        //                        continue;
        //                    }

        //                    TalksEntity talksEntity = new TalksEntity();
        //                    talksEntity.Content = talk.MsgContent;
        //                    talksEntity.PublishTime = talk.CreateTime.Value;
        //                    talksEntity.ImageList = (from talkImage in context.talk_image
        //                                             join originalImage in context.original_image on talkImage.ImageId equals originalImage.ImageId
        //                                             join corpImage in context.corp_image on originalImage.ImageId equals corpImage.OriginalImageId
        //                                             where talkImage.TalkId == talk.TalkId
        //                                             select corpImage.RemoteFilePath
        //                                             ).ToList();

        //                    postsEntityList.Add(talksEntity);
        //                }

        //                WriteToFile(xmlFilePath, postsEntityList);
        //            }
        //        }

        //        static void UpdateLocalPath()
        //        {

        //            //HarrisBlogDataContext context = new HarrisBlogDataContext();

        //            //var talkImages = context.TalkImage;

        //            //var root = @"E:\LocalImage";

        //            //foreach (var talkImage in talkImages)
        //            //{
        //            //    //talkImage.RemoteFilePath = talkImage.LocalFilePath.Replace(root, "https://image.harriszhang.com").Replace(@"\", "/");

        //            //    talkImage.FakeFilePath = talkImage.LocalFilePath.Replace(root, "http://localimage.harriszhang.com").Replace(@"\", "/");

        //            //}

        //            //context.SubmitChanges();
        //        }

        //        static void BuilderRSS()
        //        {
        //            var rssFilePath = @"D:\CodingWorkspace\HarrisBlog\HarrisZhang\rss.xml";

        //            RssDocument doc = new RssDocument();

        //            HarrisBlogDataContext context = new HarrisBlogDataContext();

        //            var posts = context.Post.OrderByDescending(v => v.CreateTime).Take(10).ToList();

        //            {
        //                var channel = doc.Channel = new Channel();

        //                channel.Title = "从善如流";
        //                channel.Link = "https://www.harriszhang.com";
        //                channel.Description = "翩若惊鸿少年时，流水文思可堪记。";
        //                channel.PubDate = DateTime.Now.GetDateTimeFormats('r')[0].ToString();
        //                channel.LastBuildDate = DateTime.Now.GetDateTimeFormats('r')[0].ToString();
        //                channel.Language = Language.Chinese_Simplified;
        //                channel.Generator = "www.harriszhang.com";
        //            }

        //            {
        //                var items = doc.Items = new List<Item>();

        //                foreach (var post in posts)
        //                {
        //                    var item = new Item();

        //                    item.Title = post.Title;
        //                    item.Link = $"https://www.harriszhang.com/post/{post.Ename}";
        //                    item.Description = post.Summary;
        //                    item.Author = "从善如流";
        //                    item.PubDate = post.CreateTime.Value.GetDateTimeFormats('r')[0].ToString();

        //                    items.Add(item);
        //                }
        //            }

        //            RssBuilder builder = new RssBuilder();

        //            var content = builder.Build(doc);

        //            File.WriteAllText(rssFilePath, content);
        //        }

        //        static void CreateImage()
        //        {
        //            //    HarrisBlogDataContext context = new HarrisBlogDataContext();

        //            //    var talkImages = context.TalkImage.ToList();

        //            //    ImageProcessor processor = new ImageProcessor();

        //            //    foreach (var talkImage in talkImages)
        //            //    {
        //            //        using (FileStream inputSream = new FileStream(talkImage.LocalFilePath, FileMode.Open))
        //            //        {

        //            //        }
        //            //    }
        //        }

        //        static void WriteToFile(string path, object item)
        //        {
        //            XmlSerializer serializer = new XmlSerializer(item.GetType());

        //            using (FileStream stream = new FileStream(path, FileMode.Create))
        //            {
        //                serializer.Serialize(stream, item);
        //            }
        //        }

        //        static TheEntity ToTheEntity(PostsEntity postEntity)
        //        {
        //            TheEntity returnValue = new TheEntity();
        //            returnValue.PostType = 1;
        //            returnValue.Ename = postEntity.Ename;
        //            returnValue.Title = postEntity.Title;
        //            returnValue.Summary = postEntity.Summary;
        //            returnValue.PublishTime = postEntity.PublishTime;
        //            return returnValue;
        //        }

        //        static TheEntity ToTheEntity(TalksEntity entity)
        //        {
        //            TheEntity returnValue = new TheEntity();
        //            returnValue.PostType = 2;
        //            returnValue.Content = entity.Content;
        //            returnValue.ImageList = entity.ImageList;
        //            returnValue.PublishTime = entity.PublishTime;
        //            return returnValue;
        //        }
    }
}
