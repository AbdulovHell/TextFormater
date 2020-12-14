using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TextFormater_v1
{
    class Program
    {
        static string FormatText(string input_text)
        {
            NumsConverter numsConverter = new NumsConverter();

            input_text = Regex.Replace(input_text, "!{1,}\"", " "); //замена !" на пробел
            input_text = Regex.Replace(input_text, "\\?!", "."); //замена ?! на .
            //замена пунктуацию на пробелы и .
            input_text = input_text.Replace('?', '.').Replace('!', '.').Replace('\"', ' ').Replace('(', ' ').Replace(')', ' ').Replace(',', ' ').Replace('-', ' ').Replace(':', ' ');
            input_text = input_text.ToUpper().Replace('.', '\n'); //переход в верхний регистр и замена точек на \n
            input_text = numsConverter.Convert(input_text); //преобразование чисел 0-9999 в текстовый вид
            input_text = new string(input_text.Where(c => !char.IsDigit(c)).Where(c => c != '\r').ToArray()); //отфильтровываем оставшиеся цифры и \r
            input_text = Regex.Replace(input_text, "\\u0020{2,}", " "); //если идут несколько пробелов подряд, оставляем один
            input_text = Regex.Replace(input_text, "\\n\\u0020", "\n"); //если перед началом предложения пробел, убираем пробел
            input_text = Regex.Replace(input_text, "\\n{2,}", "\n"); //если идут несколько \n подряд, оставляем один

            return input_text;
        }

        static string AddPrefix(string path)
        {
            int offset = path.Reverse().TakeWhile(c => c != '.').Count() + 1;
            return path.Insert(path.Length - offset, "_formated");
        }

        static void Main(string[] args)
        {
            //var test=AddPrefix("dummy_file.12.txt");

            //string txt=File.ReadAllText("D:\\nums.txt");
            //File.WriteAllText("D:\\nums_formated.txt", FormatText(txt));

            if (args.Length == 0) return;

            foreach (var item in args)
            {
                Console.WriteLine("Обработка {0}", item);
                var text = File.ReadAllText(item);
                var output_text = FormatText(text);
                File.WriteAllText(AddPrefix(item), output_text);
            }

            Console.WriteLine("Обработано {0} файлов.", args.Length);
            Console.ReadLine();
        }
    }
}
