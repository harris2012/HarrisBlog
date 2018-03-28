using HarrisBlog.Repository;
using HarrisZhang.Repository.Entity;
using HeyRed.MarkdownSharp;
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
            var xmlFilePath = @"D:\CodingWorkspace\HarrisBlog\HarrisZhang\Data\posts.xml";

            var folderPath = @"D:\CodingWorkspace\HarrisBlog\HarrisZhang\Data\posts";

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
            var xmlFilePath = @"D:\CodingWorkspace\HarrisBlog\HarrisZhang\Data\talks.xml";

            HarrisBlogDataContext context = new HarrisBlogDataContext();

            var talks = context.Talk.ToList();

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

                    postsEntityList.Add(talksEntity);
                }

                WriteToFile(xmlFilePath, postsEntityList);
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
