using System;
using System.Linq;

namespace UGPangya.API.Tools
{
    public static class Utils
    {
        /// <summary>
        ///     GetFixTime - Converte DateTime para o Formato do Pangya
        /// </summary>
        public static byte[] ToPangyaDateTime(this DateTime date)
        {
            return new[]
                {
                    (ushort) date.Year,
                    (ushort) date.Month,
                    (ushort) (date.AddDays(-1).DayOfWeek + 1), //No C# não aparece dia da semana 7
                    (ushort) date.Day,
                    (ushort) date.Hour,
                    (ushort) date.Minute,
                    (ushort) date.Second,
                    (ushort) date.Millisecond
                }
                .SelectMany(v => BitConverter.GetBytes(v))
                .ToArray();
        }
    }
}