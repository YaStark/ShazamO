using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;


namespace ShazamO
{
    [AttributeUsage(AttributeTargets.All)]
    public class ShazamOptionsAttribute : Attribute
    {
        public Object DefaultValue { get; set; }

        public String Description { get; set; }

        public ShazamOptionsAttribute(object DefValue, String Info)
        {
            DefaultValue = DefValue;
            Description = Info;
        }
    }

    [DataContract]
    class Options
    {
        [DataMember,
         ShazamOptions(32, "Количество сигнатур в секунду")]
        public int Signature_SamplesPerSecond { get; set; }

        [DataMember,
         ShazamOptions(new string[] { "<<< РЕТРАНСЛЯЦИЯ 1 >>>" },
                       "Объекты лога, исключаемые из поиска")]
        public string[] LogElement_IgnoredTags { get; set; }

        [DataMember,
         ShazamOptions(new string[] {"ElemName", "EventTime", "ElemPlaySize"},
                       "Колонки в исходном лог-файле, по которым производится сериализация")]
        public string[] LogFile_RequiredFields { get; set; }

        [DataMember,
         ShazamOptions(50, "Допустимое наложение зон роликов при поиске, выраженное в сигнатурах")]
        public int InfluenceArea_BorderDeviation { get; set; }

        [DataMember,
         ShazamOptions(22050, "Частота передискретизации для построения файлов сигнатур")]
        public int Converter_PreferredSampleRate { get; set; }

        [DataMember,
         ShazamOptions(8f, "Отклонение расстояний Хемминга от среднего значения расстояний, сигнализирующее" +
                             " о возможном обнаружении совпадения роликов")]
        public float Comparing_NSigm { get; set; }

        [DataMember,
         ShazamOptions("", "Директория сохранения")]
        public string ConvertTargetDirectory { get; set; }

        [DataMember,
         ShazamOptions("", "Директория источника")]
        public string ConvertSourceDirectory { get; set; }

        [DataMember,
         ShazamOptions(true, "Сохранять рядом с испточниками")]
        public bool ConvertSaveNearWithSources { get; set; }

        public static Options Get = null;

        public static readonly Options Default = new Options(0);

        public static string FileName = "settings.ini";

        public Options() { }

        /// <summary>
        /// Конструктор заполняет объект класса значениями по умолчанию
        /// </summary>
        private Options(int K)
        {
            ForEachPropWithAttr<ShazamOptionsAttribute>(
                (propInfo, attr) => propInfo.SetValue(this, attr.DefaultValue));
        }

        /// <summary>
        /// Конструктор копирует все сериализуемые свойства, помеченные как DataMember
        /// </summary>
        public Options(Options other)
        {
            ForEachPropWithAttr<DataMemberAttribute>(
                (propInfo, attr) => propInfo.SetValue(this, propInfo.GetValue(other)));
        }

        /// <summary>
        /// Вызывает act(PropertyInfo, AttrType) для каждого свойства текущего объекта, имеющего
        /// атрибут типа AttrType
        /// </summary>
        public void ForEachPropWithAttr<AttrType>(Action<PropertyInfo, AttrType> act) where AttrType : class
        {
            Type t = typeof(Options);
            foreach (var field in t.GetProperties())
            {
                object[] attrs = field.GetCustomAttributes(typeof(ShazamOptionsAttribute), false);
                foreach (var attr in attrs)
                {
                    AttrType attribute = attr as AttrType;
                    if (attribute != null)
                    {
                        act(field, attribute);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Загружает опции в объект Get
        /// </summary>
        public static void Load()
        {
            string data = "";
            using(FileStream fstream = new FileStream(FileName, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(fstream, Encoding.Default)) data = reader.ReadToEnd();
            }
            using(MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(data)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Options));
                Get = serializer.ReadObject(stream) as Options;
            }
        }

        /// <summary>
        /// Сохраняет опции
        /// </summary>
        public static void Save()
        {
            string data = "";
            using (MemoryStream stream = new MemoryStream())
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Options));
                serializer.WriteObject(stream, Get);
                data = Encoding.UTF8.GetString(stream.GetBuffer());
            }
            using (FileStream fstream = new FileStream(FileName, FileMode.Create))
            {
                byte[] bytes = Encoding.Default.GetBytes(data);
                fstream.Write(bytes, 0, bytes.Length);
            }
        }

    }
}
