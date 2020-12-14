using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TextFormater_v1
{
    class NumsConverter
    {
        public NumsConverter()
        {

        }

        public string Convert(string text_in)
        {
            string text_out = text_in;
            Match match;
            while ((match = Regex.Match(text_out, "[0-9]{1,}", RegexOptions.Compiled)).Success)
            {
                int num = int.Parse(match.Value);
                string text_r = "";
                if (num <= 9)
                {
                    text_r = ConvertDigit(num);
                }
                else if (num <= 99)
                {
                    text_r = ConvertDozens(num);
                }
                else if (num <= 999)
                {
                    text_r = ConvertHundreds(num);
                }else if (num <= 9999)
                {
                    text_r = ConvertThousands(num);
                }

                text_out = text_out.Remove(match.Index, match.Length);
                text_out = text_out.Insert(match.Index, text_r);
            }

            return text_out;
        }

        string Convert(int num)
        {
            if (num == 0) return "";
            if (num <= 9)
            {
                return ConvertDigit(num);
            }
            else if (num <= 99)
            {
                return ConvertDozens(num);
            }
            else if (num <= 999)
            {
                return ConvertHundreds(num);
            }
            else if (num <= 9999)
            {
                return ConvertThousands(num);
            }
            else
                return "";
        }

        string ConvertDigit(int d)
        {
            switch (d)
            {
                case 0:
                    return "НОЛЬ";
                case 1:
                    return "ОДИН";
                case 2:
                    return "ДВА";
                case 3:
                    return "ТРИ";
                case 4:
                    return "ЧЕТЫРЕ";
                case 5:
                    return "ПЯТЬ";
                case 6:
                    return "ШЕСТЬ";
                case 7:
                    return "СЕМЬ";
                case 8:
                    return "ВОСЕМЬ";
                case 9:
                    return "ДЕВЯТЬ";
                default:
                    return "";
            }
        }

        string ConvertDozens(int d)
        {
            string temp_d = d.ToString();

            if (temp_d[1] == '0')
            {
                switch (temp_d[0])
                {
                    case '1':
                        return "ДЕСЯТЬ";
                    case '2':
                        return "ДВАДЦАТЬ";
                    case '3':
                        return "ТРИДЦАТЬ";
                    case '4':
                        return "СОРОК";
                    case '5':
                        return "ПЯТЬДЕСЯТ";
                    case '6':
                        return "ШЕСТЬДЕСЯТ";
                    case '7':
                        return "СЕМЬДЕСЯТ";
                    case '8':
                        return "ВОСЕМЬДЕСЯТ";
                    case '9':
                        return "ДЕВЯНОСТО";
                    default:
                        return "";
                }
            }
            else if (temp_d[0] == '1')
            {
                switch (temp_d[1])
                {
                    case '1':
                        return "ОДИНАДЦАТЬ";
                    case '2':
                        return "ДВЕНАДЦАТЬ";
                    case '3':
                        return "ТРИНАДЦАТЬ";
                    case '4':
                        return "ЧЕТЫРНАДЦАТЬ";
                    case '5':
                        return "ПЯТНАДЦАТЬ";
                    case '6':
                        return "ШЕСТНАДЦАТЬ";
                    case '7':
                        return "СЕМНАДЦАТЬ";
                    case '8':
                        return "ВОСЕМНАДЦАТЬ";
                    case '9':
                        return "ДЕВЯТНАДЦАТЬ";
                    default:
                        return "";
                }
            }
            else
            {
                return ConvertDozens((temp_d[0] - 48) * 10) + " " + ConvertDigit(temp_d[1] - 48);
            }
        }

        Dictionary<char, string> Hundreds = new Dictionary<char, string>(){
            {'1',"СТО" },
            {'2',"ДВЕСТИ" },
            {'3',"ТРИСТА" },
            {'4',"ЧЕТЫРЕСТА" },
            {'5',"ПЯТЬСОТ" },
            {'6',"ШЕСТЬСОТ" },
            {'7',"СЕМЬСОТ" },
            {'8',"ВОСЕМЬСОТ" },
            {'9',"ДЕВЯТЬСОТ" }
        };

        string ConvertHundreds(int d)
        {
            string temp_d = d.ToString();

            return Hundreds[temp_d[0]] + " " + Convert(d%100);
        }

        Dictionary<char, string> Thousands = new Dictionary<char, string>()
        {
            {'1',"ТЫСЯЧА" },
            {'2',"ДВЕ ТЫСЯЧИ" },
            {'3',"ТРИ ТЫСЯЧИ" },
            {'4',"ЧЕТЫРЕ ТЫСЯЧИ" },
            {'5',"ПЯТЬ ТЫСЯЧЬ" },
            {'6',"ШЕСТЬ ТЫСЯЧЬ" },
            {'7',"СЕМЬ ТЫСЯЧЬ" },
            {'8',"ВОСЕМЬ ТЫСЯЧЬ" },
            {'9',"ДЕВЯТЬ ТЫСЯЧЬ" }
        };

        string ConvertThousands(int d)
        {
            string temp_d = d.ToString();

            return Thousands[temp_d[0]] + " " + Convert(d%1000); 
        }
    }
}
