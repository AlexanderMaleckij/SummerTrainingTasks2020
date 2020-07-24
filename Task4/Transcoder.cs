using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientSide
{
    public static class Transcoder
    {
        private static readonly Dictionary<char, string> ruToEnDictionary = new Dictionary<char, string>
        {
            {'а', "a"  }, {'б', "b"  }, {'в', "v"   },
            {'г', "g"  }, {'д', "d"  }, {'е', "e"   },
            {'ё', "yo" }, {'ж', "zh" }, {'з', "z"   },
            {'и', "i"  }, {'й', "y"  }, {'к', "k"   },
            {'л', "l"  }, {'м', "m"  }, {'н', "n"   },
            {'о', "o"  }, {'п', "p"  }, {'р', "r"   },
            {'с', "s"  }, {'т', "t"  }, {'у', "u"   },
            {'ф', "f"  }, {'х', "kh" }, {'ц', "ts"  },
            {'ч', "ch" }, {'ш', "sh" }, {'щ', "sch" },
            {'ъ', "'"  }, {'ы', "y"  }, {'ь', "'"   },
            {'э', "e"  }, {'ю', "yu" }, {'я', "ya"  },
        };

        private static readonly Dictionary<char, string> enToRuDictionary = new Dictionary<char, string>
        {
             {'a', "а"  }, {'b', "б"  }, {'c', "к"   },
             {'d', "д"  }, {'e', "е"  }, {'f', "ф"   },
             {'g', "г"  }, {'h', "х"  }, {'i', "и"   },
             {'j', "ж"  }, {'k', "к"  }, {'l', "л"   },
             {'m', "м"  }, {'n', "н"  }, {'o', "о"   },
             {'p', "п"  }, {'q', "к"  }, {'r', "р"   },
             {'s', "с"  }, {'t', "т"  }, {'u', "у"   },
             {'v', "в"  }, {'w', "в"  }, {'x', "х"   },
             {'y', "у"  }, {'z', "з"  }
        };

        /// <summary>
        /// Revert russian characters to english, english to russian
        /// </summary>
        /// <param name="transcodingStr">string for transcoding</param>
        /// <returns>transcoded string</returns>
        public static string Transcode(string transcodingStr)
        {
            StringBuilder sb = new StringBuilder(transcodingStr.Length);

            foreach(char symbol in transcodingStr)
            {
                sb.Append(GetSequence(symbol));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Get transcoded character (or sequence of characters)
        /// </summary>
        /// <param name="character">character for transcoding</param>
        /// <returns>transcoded character (or sequence of characters)</returns>
        private static string GetSequence(char character)
        {
            string result = string.Empty;

            if(IsRussianLetter(character))
            {
                result = ruToEnDictionary[char.ToLower(character)];
            }
            else if(IsEnglishLetter(character))
            {
                result = enToRuDictionary[char.ToLower(character)];
            }
            else
            {
                result = character.ToString();
            }

            if (char.IsUpper(character))
            {
                return result.Replace(result[0], char.ToUpper(result[0]));
            }

            return result;
        }

        /// <summary>
        /// Detect is given letter refers to a russian language alphabet
        /// </summary>
        /// <param name="letter">letter for detection</param>
        /// <returns>is refers to a russian language</returns>
        private static bool IsRussianLetter(char letter) => ruToEnDictionary.Keys.Any(x => x == char.ToLower(letter));

        /// <summary>
        /// Detect is given letter refers to a english language alphabet
        /// </summary>
        /// <param name="letter">letter for detection</param>
        /// <returns>is refers to a english language</returns>
        private static bool IsEnglishLetter(char letter) => enToRuDictionary.Keys.Any(x => x == char.ToLower(letter));
    }
}
