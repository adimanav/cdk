using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace substitute
{
    public class Substitute
    {
        public static void Process(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Parmeters input text file, input data file and output file missing!");
                return;
            }

            using (TextReader textReader = File.OpenText(args[0]))
            {
                using (TextReader dataReader = File.OpenText(args[1]))
                {
                    using (StreamWriter writer = new StreamWriter(args[2]))
                    {
                        var dataMap = new Dictionary<string, string>();
                        string data = dataReader.ReadToEnd();
                        var dataArr = data.Split('\n');
                        foreach (var dataEl in dataArr)
                        {
                            if (!string.IsNullOrWhiteSpace(dataEl))
                            {
                                var pair = dataEl.Split('=');
                                dataMap[pair[0].Trim().ToLower()] = pair[1].Trim();
                            }
                        }

                        string line = null;
                        bool started = false;
                        StringBuilder tmp = new StringBuilder();

                        while (null != (line = textReader.ReadLine()))
                        {
                            StringBuilder newLine = new StringBuilder();
                            foreach (var ch in line)
                            {
                                if (started)
                                {
                                    if (ch != '%')
                                        tmp.Append(ch);
                                    else
                                    {
                                        started = false;
                                        if (dataMap.ContainsKey(tmp.ToString().ToLower()))
                                            newLine.Append(dataMap[tmp.ToString().ToLower()]);
                                        tmp.Clear();
                                    }
                                }
                                else
                                {
                                    if (ch == '%')
                                        started = true;
                                    else
                                        newLine.Append(ch);
                                }
                            }
                            writer.WriteLine(newLine.ToString());
                        }
                    }
                }
            }
        }

        public static void Main(string[] args)
        {
            Process(args);
        }
    }
}
