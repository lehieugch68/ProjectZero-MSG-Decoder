using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZero_MSG_Decoder
{
    public class GameEncoding
    {
        private static Dictionary<byte, string> _CustomEncoding;
        private static Dictionary<string, string> _GameCode;
        private static Dictionary<string, string> _VietnameseEncoding;
        private static void Instance()
        {
            _CustomEncoding = new Dictionary<byte, string>();
            _CustomEncoding.Add(0, " ");
            _CustomEncoding.Add(1, "A");
            _CustomEncoding.Add(2, "B");
            _CustomEncoding.Add(3, "C");
            _CustomEncoding.Add(4, "D");
            _CustomEncoding.Add(5, "E");
            _CustomEncoding.Add(6, "F");
            _CustomEncoding.Add(7, "G");
            _CustomEncoding.Add(8, "H");
            _CustomEncoding.Add(9, "I");
            _CustomEncoding.Add(0xA, "J");
            _CustomEncoding.Add(0xB, "K");
            _CustomEncoding.Add(0xC, "L");
            _CustomEncoding.Add(0xD, "M");
            _CustomEncoding.Add(0xE, "N");
            _CustomEncoding.Add(0xF, "O");
            _CustomEncoding.Add(0x10, "P");
            _CustomEncoding.Add(0x11, "Q");
            _CustomEncoding.Add(0x12, "R");
            _CustomEncoding.Add(0x13, "S");
            _CustomEncoding.Add(0x14, "T");
            _CustomEncoding.Add(0x15, "U");
            _CustomEncoding.Add(0x16, "V");
            _CustomEncoding.Add(0x17, "W");
            _CustomEncoding.Add(0x18, "X");
            _CustomEncoding.Add(0x19, "Y");
            _CustomEncoding.Add(0x1A, "Z");
            _CustomEncoding.Add(0x1B, "a");
            _CustomEncoding.Add(0x1C, "b");
            _CustomEncoding.Add(0x1D, "c");
            _CustomEncoding.Add(0x1E, "d");
            _CustomEncoding.Add(0x1F, "e");
            _CustomEncoding.Add(0x20, "f");
            _CustomEncoding.Add(0x21, "g");
            _CustomEncoding.Add(0x22, "h");
            _CustomEncoding.Add(0x23, "i");
            _CustomEncoding.Add(0x24, "j");
            _CustomEncoding.Add(0x25, "k");
            _CustomEncoding.Add(0x26, "l");
            _CustomEncoding.Add(0x27, "m");
            _CustomEncoding.Add(0x28, "n");
            _CustomEncoding.Add(0x29, "o");
            _CustomEncoding.Add(0x2A, "p");
            _CustomEncoding.Add(0x2B, "q");
            _CustomEncoding.Add(0x2C, "r");
            _CustomEncoding.Add(0x2D, "s");
            _CustomEncoding.Add(0x2E, "t");
            _CustomEncoding.Add(0x2F, "u");
            _CustomEncoding.Add(0x30, "v");
            _CustomEncoding.Add(0x31, "w");
            _CustomEncoding.Add(0x32, "x");
            _CustomEncoding.Add(0x33, "y");
            _CustomEncoding.Add(0x34, "z");
            _CustomEncoding.Add(0x35, "０");
            _CustomEncoding.Add(0x36, "１");
            _CustomEncoding.Add(0x37, "２");
            _CustomEncoding.Add(0x38, "３");
            _CustomEncoding.Add(0x39, "４");
            _CustomEncoding.Add(0x3A, "５");
            _CustomEncoding.Add(0x3B, "６");
            _CustomEncoding.Add(0x3C, "７");
            _CustomEncoding.Add(0x3D, "８");
            _CustomEncoding.Add(0x3E, "９");
            _CustomEncoding.Add(0x3F, "0");
            _CustomEncoding.Add(0x40, "1");
            _CustomEncoding.Add(0x41, "2");
            _CustomEncoding.Add(0x42, "3");
            _CustomEncoding.Add(0x43, "4");
            _CustomEncoding.Add(0x44, "5");
            _CustomEncoding.Add(0x45, "6");
            _CustomEncoding.Add(0x46, "7");
            _CustomEncoding.Add(0x47, "8");
            _CustomEncoding.Add(0x48, "9");

            _GameCode = new Dictionary<string, string>();

            _GameCode.Add("{166}", "{Circle}");
            _GameCode.Add("{167}", "{Cross}");
            _GameCode.Add("{168}", "{Triangle}");
            _GameCode.Add("{169}", "{Square}");


            _GameCode.Add("{240}V", "\"");
            _GameCode.Add("{240}W", "'");
            _GameCode.Add("{240}X", "(");
            _GameCode.Add("{240}Y", ")");
            _GameCode.Add("{240}Z", "-");
            _GameCode.Add("{240}a", "?");
            _GameCode.Add("{240}b", "/");
            _GameCode.Add("{240}e", ";");
            _GameCode.Add("{240}f", ":");
            _GameCode.Add("{240}g", ",");
            _GameCode.Add("{240}h", ".");
            _GameCode.Add("{240}i", "!");
            _GameCode.Add("{240}j", "「");
            _GameCode.Add("{240}k", "」");
            _GameCode.Add("{240}{201}", "=");

            //_GameCode.Add("{240}p", "零");

            _GameCode.Add("{254}", "\n");
            _GameCode.Add("{255}", "{End}");
            _GameCode.Add("{250}", "{Next}");
            _GameCode.Add("{253}{130}ii", "{DarkRed}");
            _GameCode.Add("{253}xx{220}", "{DarkBlue}");
            _GameCode.Add("{253}{80}{80}{200}", "{Purple}");
            _GameCode.Add("{253}{150}{80}{80}", "{Pink}");
            _GameCode.Add("{253}{100}{100}{170}", "{LightPurple}");
            _GameCode.Add("{253}T{180}T", "{LightGreen}");
            _GameCode.Add("{253}{150}OO", "{Red}");
            _GameCode.Add("{253}T{150}T", "{Green}");
            _GameCode.Add("{253}{110}JJ", "{Crimson}");
            _GameCode.Add("{253}{128}{128}{128}", "{EndColor}");
            _GameCode.Add("{80} 1A", "{Box1}");
            _GameCode.Add("{80} {100}A", "{Box2}");

            _VietnameseEncoding = new Dictionary<string, string>();
            _VietnameseEncoding.Add("{73}", "á");
            _VietnameseEncoding.Add("{74}", "à");
            _VietnameseEncoding.Add("{75}", "ả");
            _VietnameseEncoding.Add("{76}", "ã");
            _VietnameseEncoding.Add("{77}", "ạ");
            _VietnameseEncoding.Add("{78}", "â");
            _VietnameseEncoding.Add("{79}", "ấ");
            _VietnameseEncoding.Add("{80}", "ầ");
            _VietnameseEncoding.Add("{81}", "ẩ");
            _VietnameseEncoding.Add("{82}", "ẫ");
            _VietnameseEncoding.Add("{83}", "ậ");
            _VietnameseEncoding.Add("{84}", "ă");
            _VietnameseEncoding.Add("{85}", "ắ");
            _VietnameseEncoding.Add("{86}", "ằ");
            _VietnameseEncoding.Add("{87}", "ẳ");
            _VietnameseEncoding.Add("{88}", "ẵ");
            _VietnameseEncoding.Add("{89}", "ặ");
            _VietnameseEncoding.Add("{90}", "đ");
            _VietnameseEncoding.Add("{91}", "é");
            _VietnameseEncoding.Add("{92}", "è");
            _VietnameseEncoding.Add("{93}", "ẻ");
            _VietnameseEncoding.Add("{94}", "ẽ");
            _VietnameseEncoding.Add("{95}", "ẹ");
            _VietnameseEncoding.Add("{96}", "ê");
            _VietnameseEncoding.Add("{97}", "ế");
            _VietnameseEncoding.Add("{98}", "ề");
            _VietnameseEncoding.Add("{99}", "ể");
            _VietnameseEncoding.Add("{100}", "ễ");
            _VietnameseEncoding.Add("{101}", "ệ");
            _VietnameseEncoding.Add("{102}", "í");
            _VietnameseEncoding.Add("{103}", "ì");
            _VietnameseEncoding.Add("{104}", "ỉ");
            _VietnameseEncoding.Add("{105}", "ĩ");
            _VietnameseEncoding.Add("{106}", "ị");
            _VietnameseEncoding.Add("{107}", "ó");
            _VietnameseEncoding.Add("{108}", "ò");
            _VietnameseEncoding.Add("{109}", "ỏ");
            _VietnameseEncoding.Add("{110}", "õ");
            _VietnameseEncoding.Add("{111}", "ọ");
            _VietnameseEncoding.Add("{112}", "ô");
            _VietnameseEncoding.Add("{113}", "ố");
            _VietnameseEncoding.Add("{114}", "ồ");
            _VietnameseEncoding.Add("{115}", "ổ");
            _VietnameseEncoding.Add("{116}", "ỗ");
            _VietnameseEncoding.Add("{117}", "ộ");
            _VietnameseEncoding.Add("{118}", "ơ");
            _VietnameseEncoding.Add("{119}", "ớ");
            _VietnameseEncoding.Add("{120}", "ờ");
            _VietnameseEncoding.Add("{121}", "ở");
            _VietnameseEncoding.Add("{122}", "ỡ");
            _VietnameseEncoding.Add("{123}", "ợ");
            _VietnameseEncoding.Add("{124}", "ú");
            _VietnameseEncoding.Add("{125}", "ù");
            _VietnameseEncoding.Add("{126}", "ủ");
            _VietnameseEncoding.Add("{127}", "ũ");
            _VietnameseEncoding.Add("{128}", "ụ");
            _VietnameseEncoding.Add("{129}", "ư");
            _VietnameseEncoding.Add("{130}", "ứ");
            _VietnameseEncoding.Add("{131}", "ừ");
            _VietnameseEncoding.Add("{132}", "ử");
            _VietnameseEncoding.Add("{133}", "ữ");
            _VietnameseEncoding.Add("{134}", "ự");
            _VietnameseEncoding.Add("{135}", "ý");
            _VietnameseEncoding.Add("{136}", "ỳ");
            _VietnameseEncoding.Add("{137}", "ỷ");
            _VietnameseEncoding.Add("{138}", "ỹ");
            _VietnameseEncoding.Add("{139}", "ỵ");
            _VietnameseEncoding.Add("{140}", "Á");
            _VietnameseEncoding.Add("{141}", "À");
            _VietnameseEncoding.Add("{142}", "Ả");
            _VietnameseEncoding.Add("{143}", "Ã");
            _VietnameseEncoding.Add("{144}", "Ạ");
            _VietnameseEncoding.Add("{145}", "Đ");
            _VietnameseEncoding.Add("{146}", "Ơ");
            _VietnameseEncoding.Add("{147}", "Ớ");
            _VietnameseEncoding.Add("{148}", "Ờ");
            _VietnameseEncoding.Add("{149}", "Ở");
            _VietnameseEncoding.Add("{150}", "Ỡ");
            _VietnameseEncoding.Add("{151}", "Ợ");
        }
        public static Dictionary<byte, string> GetEncoding()
        {
            if (_CustomEncoding == null) Instance();
            return _CustomEncoding;
        }
        public static Dictionary<string, string> GetGameCode()
        {
            if (_GameCode == null) Instance();
            return _GameCode;
        }
        public static Dictionary<string, string> GetVietnameseEncoding()
        {
            if (_VietnameseEncoding == null) Instance();
            return _VietnameseEncoding;
        }
        public static string GetString(byte[] raw)
        {
            string[] strs = new string[raw.Length];
            for (int i = 0; i < raw.Length; i++)
            {
                string c;
                if (GetEncoding().TryGetValue(raw[i], out c)) strs[i] = c;
                else strs[i] = $"{(char)123}{raw[i]}{(char)125}";
            }
            string str = string.Join("", strs);
            foreach (KeyValuePair<string, string> entry in GetGameCode()) str = str.Replace(entry.Key, entry.Value);
            return str;
        }
        private static byte GetKeyByValue(string value)
        {
            foreach (KeyValuePair<byte, string> entry in GetEncoding())
            {
                if (entry.Value == value) return entry.Key;
            }
            return 0;
        }
        public static byte[] GetBytes(string input)
        {
            if (_VietnameseEncoding == null) Instance();
            foreach (KeyValuePair<string, string> entry in _VietnameseEncoding)
            {
                input = input.Replace(entry.Value, entry.Key);
            }
            foreach (KeyValuePair<string, string> entry in GetGameCode())
            {
                input = input.Replace(entry.Value, entry.Key);
            }
            List<byte> result = new List<byte>();
            string[] raw = input.ToCharArray().Select(c => c.ToString()).ToArray();
            for (int i = 0; i < raw.Length; i++)
            {
                if (raw[i] != "{")
                {
                    byte b = GetKeyByValue(raw[i]);
                    result.Add(b);
                }
                else
                {
                    StringBuilder num = new StringBuilder();
                    i++;
                    while (raw[i] != "}") num.Append(raw[i++]);
                    result.Add(Convert.ToByte(int.Parse(num.ToString())));
                }
            }
            return result.ToArray();
        }
    }
}
