namespace DatabaseMigration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Расширяющие методы строки для распознавания значений
    /// из строковых данных xml.
    /// </summary>
    internal static class ParseUtilities
    {
        /// <summary>
        /// Получить имя агента из строки информации об агенте.
        /// </summary>
        /// <param name="agentNameAndPhone">Строка информации об агенте, полученная из xml.</param>
        /// <returns>Имя агента, или исходная строка, если не удалось отделить имя от телефона.</returns>
        public static string GetAgentName(this string agentNameAndPhone)
        {
            return agentNameAndPhone
                .GetNameAndPhone()
                .Key
                .GetRestricredString(50);
        }

        /// <summary>
        /// Получить телефон агента из строки информации об агенте.
        /// </summary>
        /// <param name="agentNameAndPhone">Строка информации об агенте, полученная из xml.</param>
        /// <returns>Телефон агента, или "-", если телефон не найден.</returns>
        public static string GetAgentPhone(this string agentNameAndPhone)
        {
            return agentNameAndPhone
                .GetNameAndPhone()
                .Value
                .GetRestricredString(50);
        }

        /// <summary>
        /// Получить целочисленное значение цены.
        /// </summary>
        /// <param name="stringPrice">Строковое значение цены.</param>
        /// <returns>Целочисленное значение цены, или null, если преобразовать не удалось.</returns>
        public static int? GetIntegerPrice(this string stringPrice)
        {
            int intCost;
            return int.TryParse(stringPrice, out intCost)
                ? intCost
                : (int?)null;
        }

        /// <summary>
        /// Получить дату и время из строки.
        /// </summary>
        /// <param name="stringDateTime">Строка, содержащая дату и время.</param>
        /// <returns></returns>
        public static DateTime GetDateTime(this string stringDateTime)
        {
            DateTime dateTime;
            return DateTime.TryParse(stringDateTime, out dateTime)
                ? dateTime
                : DateTime.MinValue;
        }

        /// <summary>
        /// Получить имя и телефон агента из строки, содержащей и имя, и телефон.
        /// </summary>
        /// <param name="agentNameAndPhone">Строка с именем и телефоном.</param>
        /// <returns>Имя -> телефон.</returns>
        private static KeyValuePair<string, string> GetNameAndPhone(
            this string agentNameAndPhone)
        {
            agentNameAndPhone = agentNameAndPhone.Replace("+7", "8");

            List<string> spaceSplitted = 
                agentNameAndPhone.Split(
                    new [] {' ', '.' }, 
                    StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            IEnumerable<string> digitStrings = 
                spaceSplitted.Where(str => 
                    Regex.IsMatch(str, "^\\d.*\\d?$"));

            IEnumerable<string> noDigitStrings =
                spaceSplitted.Where(str =>
                    !Regex.IsMatch(str, "^\\d.*\\d?$"));

            string name = string.Join(" ", noDigitStrings);
            string phone = string.Join(" ", digitStrings);

            if (string.IsNullOrWhiteSpace(name))
            {
                name = phone;
            }

            if (string.IsNullOrWhiteSpace(phone))
            {
                phone = "-";
            }

            return new KeyValuePair<string, string>(name, phone);
        }

        /// <summary>
        /// Сократить строку до определенного количества знаков.
        /// </summary>
        /// <param name="longString">Строка с неограниченной длиной.</param>
        /// <param name="length">Необходимое количество знаков.</param>
        /// <returns>Сокращенная строка.</returns>
        private static string GetRestricredString(
            this string longString,
            int length)
        {
            if (longString.Length > length)
            {
                return longString.Substring(0, length);
            }

            return longString;
        }
    }
}
