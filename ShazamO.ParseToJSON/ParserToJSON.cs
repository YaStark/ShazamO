using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using ShazamO;

namespace ShazamO.ParseToJSON
{
    [DataContract]
    public class LogElement
    {
        [DataMember]
        public string ElemName { get; set; }

        [DataMember]
        public string EventTime { get; set; }

        [DataMember]
        public string ElemPlaySize { get; set; }

        public DateTime Begin { get { return DateTime.Parse(EventTime); } }

        public static string[] IgnoredTags = null;

        public TimeSpan Length
        {
            get
            {
                try
                {
                    return TimeSpan.Parse("00:" + ElemPlaySize);
                }
                catch
                {
                    return TimeSpan.FromTicks(0);
                }
            }
        }

        public LogElement(string Name)
        {
            ElemName = Name;
        }

        public static LogElement[] DeserializeJSON(string JSON)
        {
            using (MemoryStream stream = new MemoryStream(Encoding.Unicode.GetBytes(JSON)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(LogElement[]));
                LogElement[] elems = serializer.ReadObject(stream) as LogElement[];

                List<LogElement> results = new List<LogElement>();
                foreach (var _elem in elems) if (!IgnoredTags.Contains(_elem.ElemName)) results.Add(_elem);

                return results.ToArray();
            }
        }

        public override string ToString()
        {
            return String.Format("[{0} : {1}] : {2}", Begin, Begin.Add(Length), ElemName);
        }

        public class TimeComparer : IEqualityComparer<LogElement>
        {
            public bool Equals(LogElement x, LogElement y)
            {
                return DateTime.Compare(x.Begin, y.Begin) == 0;
            }

            public int GetHashCode(LogElement obj)
            {
                return obj.EventTime.GetHashCode();
            }
        }

        public class Comparer : IEqualityComparer<LogElement>
        {
            public bool Equals(LogElement x, LogElement y)
            {
                return x.ElemName == y.ElemName;
            }

            public int GetHashCode(LogElement obj)
            {
                return obj.ElemName.GetHashCode();
            }
        }

    }

    public class ParserToJSON
    {
        /* "FIELD LIST","EventTime","Type","ElemName","ElemArtist","ElemPlaySize","ElemID","ElemClass"
         *  "DAY START","00:00.0"
         * "2015-08-29 00:00:00","ELEM_INFO","<<< РЕТРАНСЛЯЦИЯ 1 >>>","","00:01.413","0000000",""
         * */

        public static string[] RequiredFields = null;

        /// <summary>
        /// Convert fields "ElemName", "EventTime" and "ElemPlaySize".
        /// Ignores first titlename ("FIELD LIST") and all lines which length less than titles count.
        /// Add prefix to each new line.
        /// </summary>
        private static string ConvertToJSON(Stream Stream)
        {
            using (StreamReader reader = new StreamReader(Stream, Encoding.Default))
            {
                string sline = "";

                while (true)
                {
                    sline = reader.ReadLine();
                    if (sline.IndexOf("FIELD LIST") >= 0) break;
                    else if (reader.EndOfStream) throw new Exception("File doesn't contain FIELD LIST value");
                }

                List<string> Titles = sline.Remove(sline.Length - 1, 1)   // Remove first and last quotes
                                             .Remove(0, 1)
                                             .Split(new string[] { "\",\"" }, StringSplitOptions.None)
                                             .Skip(1)
                                             .ToList();

                reader.ReadLine();                                  // Skip 2nd line, it is useless for now
                
                int[] fieldsIndexes = RequiredFields.Select((item) => Titles.IndexOf(item))
                                                    .ToArray();
                // Read all lines
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("[");
                while (!reader.EndOfStream)
                {
                        // Get all fields
                    string s = reader.ReadLine();
                    string[] line = s.Remove(s.Length - 1, 1)   // Remove first and last quotes
                                     .Remove(0, 1)
                                     .Split(new string[] { "\",\"" }, StringSplitOptions.None);
                    
                    sb.Append("{");
                    foreach (var i in fieldsIndexes) sb.AppendFormat(" \"{0}\" : \"{1}\",", Titles[i], line[i]);
                    sb.Remove(sb.Length - 1, 1);
                    sb.AppendLine(" },");
                }
                sb.Remove(sb.Length - 1, 1);
                sb.AppendLine("]");
                return sb.ToString();
            }
        }

        /// <summary>
        /// Main interface to convert files to JSON
        /// </summary>
        public static string Parse(string FileName)
        {
            using (FileStream fstream = new FileStream(FileName, FileMode.Open))
            {
                return ConvertToJSON(fstream);
            }
        }
    }
}
