using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HarrisBlog.Gen
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1. 根据markdown生成html");
            Console.WriteLine("2. 根据数据库生成站点需要的数据文件");
            Console.WriteLine("请选择需要执行的操作");

            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    GenMarkdown();

                    break;
                case "2":
                    GenDataFile();

                    break;
                default:
                    break;
            }

            Console.WriteLine("按任意键退出.");
            Console.ReadKey();
        }

        static void GenMarkdown()
        {

        }

        static void GenDataFile()
        {

        }
    }
}
