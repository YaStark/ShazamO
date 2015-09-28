using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShazamO
{
    public static class FileParser
    {
        /// <summary>
        /// Возвращает расширение из адреса файла
        /// </summary>
        public static string GetExtension(string FileName)
        {
            return Regex.Match(FileName, @"[\.]([^\.]+)$").Groups[0].Value.ToLower();
        }

        /// <summary>
        /// Извлекает имя файла из его адреса без расширения
        /// </summary>
        public static string GetFileName(string Path)
        {
            return Regex.Match(Path, @"([^\\\/]+)[\.]([^\.]+)$").Groups[1].Value;
        }
    }
}
