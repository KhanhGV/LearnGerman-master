    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DE_DB_ELEANING.Config
{
    public class FormatQuestion
    {
        private const string Dot = ".";
        private const string QuestionMark = "?";
        private const string DefaultMimeType = "application/octet-stream";
        private static readonly Lazy<IDictionary<string, string>> _mappings = new Lazy<IDictionary<string, string>>(BuildMappings);
        public static IDictionary<string, string> BuildMappings()
        {
            var mappings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) {

                {"d78ba3f7-a785-47ec-b154-43cf399279da", "GiaoTiep"},
                };

            var cache = mappings.ToList(); // need ToList() to avoid modifying while still enumerating

            foreach (var mapping in cache)
            {
                if (!mappings.ContainsKey(mapping.Value))
                {
                    mappings.Add(mapping.Value, mapping.Key);
                }
            }

            return mappings;
        }
        public static bool TryGetMimeType(string str, out string mimeType)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            var indexQuestionMark = str.IndexOf(QuestionMark, StringComparison.Ordinal);
            if (indexQuestionMark != -1)
            {
                str = str.Remove(indexQuestionMark);
            }


            if (!str.StartsWith(Dot))
            {
                var index = str.LastIndexOf(Dot);
                if (index != -1 && str.Length > index + 1)
                {
                    str = str.Substring(index + 1);
                }

                str = Dot + str;
            }

            return _mappings.Value.TryGetValue(str, out mimeType);
        }


        public static string GetMimeType(string str)
        {
            return TryGetMimeType(str, out var result) ? result : DefaultMimeType;
        }

        public static string GetExtension(string mimeType, bool throwErrorIfNotFound = true)
        {
            if (mimeType == null)
            {
                throw new ArgumentNullException(nameof(mimeType));
            }

            if (mimeType.StartsWith(Dot))
            {
                throw new ArgumentException("Requested mime type is not valid: " + mimeType);
            }

            if (_mappings.Value.TryGetValue(mimeType, out string extension))
            {
                return extension;
            }

            if (throwErrorIfNotFound)
            {
                throw new ArgumentException("Requested mime type is not registered: " + mimeType);
            }

            return string.Empty;
        }
    }
}