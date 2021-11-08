using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePieces.TeslaOnlineTest
{
    public  class ValidateLinuxCommand
    {

        public static int Validate(string[] args)
        {
            // Console.WriteLine("Sample debug output");
            if (args == null || args.Length == 0 || args.Length > 5)
            {
                return -1;
            }

            var keywords = new List<string> { "--name", "--count", "--help" };
            var normalKeywords = new List<string> { "--name", "--count" };
            var myarg = args.ToList();

            bool lastIsKeyword = false;
            bool containsHelp = false;

            for (var i = 0; i < args.Length; i++)
            {
                if (i == 0 && !keywords.Contains(args[i].ToLower()))
                {
                    return -1;
                }

                if (lastIsKeyword)
                {
                    if (args[i - 1] == "--name")
                    {
                        var len = args[i].Length;
                        if (normalKeywords.Contains(args[i].ToLower()) || len < 3 || len > 10)
                            return -1;
                    }
                    else if (args[i - 1] == "--count")
                    {
                        int num = 0;
                        int.TryParse(args[i], out num);
                        if (normalKeywords.Contains(args[i].ToLower()) || num < 10 || num > 100)
                            return -1;
                    }
                    else if (args[i - 1] == "--help")
                    {
                        if (!keywords.Contains(args[i].ToLower()))
                        {
                            return -1;
                        }
                    }
                }

                if (keywords.Contains(args[i].ToLower()))
                {
                    lastIsKeyword = true;
                }
                else
                {
                    lastIsKeyword = false;
                }

                if (args[i] == "--help")
                {
                    containsHelp = true;
                }
            }

            if (args[args.Length - 1].ToLower() == "--help")

                return 1;

            if (normalKeywords.Contains(args[args.Length - 1].ToLower()))
            {
                return -1;
            }

            if (containsHelp)
                return 1;

            return 0;
        }
    }
}
