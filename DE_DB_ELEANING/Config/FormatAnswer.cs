using System;
using System.Collections.Generic;
using System.Linq;
namespace DE_DB_ELEANING.Config
{
    // Safety pig has arrived!
    //                               _
    //  _._ _…_ .-',     _.._(`))
    // '-. `     '  /-._.-'    ',/
    //    )         \            '.
    //   / _    _    |             \
    //  |  a    a    /              |
    //  \   .-.                     ;  
    //   '-('' ).-'       ,'       ;
    //      '-;           |      .'
    //         \           \    /
    //         | 7  .__  _.-\   \
    //         | |  |  “/  /`  /
    //        /,_|  |   /,_/   /
    //           /,_/      '`-'
    //
    /*@                                    /\  /\
    * @                                  /  \/  \                        —– |   | —-      |—\ |    | /–\  — |   |  —- /–\ /–\
    *  @                                /        —                        |   |   | |         |   / |    | |      |  |\  |  |    |    |
    *   \—\                          /           \                       |   |—| —-      |–/  |    |  \     |  | \ |  —-  \    \
    *    |   \————————/       /-\    \                     |   |   | |         |  \  |    |   -\   |  |  \|  |      -\   -\
    *    |                                    \-/     \                    |   |   | —-      |—/  \–/  \–/  — |   \  —- \–/ \–/
    *     \                                             ——O
    *      \                                                 /                 — |   | —-  /–\        |–\   /–\   /–\
    *       |    |                    |    |                /                   |  |\  | |    |    |       |   | |    | |
    *       |    |                    |    |—–    ——-                    |  | \ | —- |    |       |   | |    | | /-\
    *       |    |\                  /|    |     \  WWWWWW/                     |  |  \| |    |    |       |   | |    | |    |
    *       |    | \                / |    |      \——-                     — |   \ |     \–/        |–/   \–/   \–/
    *       |    |  \————–/  |    |
    *      /     |                   /     |
    *      \      \                  \      \
    *       \—–/                   \—–/
    */
    // Xin lỗi, anh hơi lười nên không viết comment cho chú
    // Nếu đoạn code này chạy đúng, tác giả của nó là DIEBOYNO1.
    // Nếu nó chạy sai, tác giả là ai… tôi không biết.
    // Nếu anh bạn đang đọc những dòng này, nghĩa là anh bạn đang làm tiếp dự án cũ của tôi.
    // Thành thật xin lỗi vì đống code… Mong trời phật phù hộ cho anh bạn.
    // Em cũng chả biết có cần hàm này không nữa, nhưng mà sợ quá nên không dám xóa
    // Tao không chịu trách nhiệm cho những dòng code dưới đây.
    // Bọn PM và Technical leader ép tao viết như thế.
    // Éo hiểu tại sao nhưng mà sửa thế này thì code nó lại chạy
    // Xóa dòng comment này đi là chương trình crash ngay tức khắc
    public class FormatAnswer
    {
        private const string Dot = ".";
        private const string QuestionMark = "?";
        private const string DefaultMimeType = "application/octet-stream";
        private static readonly Lazy<IDictionary<string, string>> _mappings = new Lazy<IDictionary<string, string>>(BuildMappings);
        public static IDictionary<string, string> BuildMappings()
        {
            var mappings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) {

                {"6999721e-334a-45c9-b545-c66e6833529f", "Một đáp án đúng "},
                {"2c96d02a-e112-4d75-b5bc-e1f0cd96c2cc", "Nhiều đáp án đúng"},
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