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
            input_text = Regex.Replace(input_text, "!{1,}\"", " "); //замена !" на пробел
            input_text = Regex.Replace(input_text, "\\?!", "."); //замена ?! на .
            //замена пунктуацию на пробелы и .
            input_text = input_text.Replace('?', '.').Replace('!', '.').Replace('\"', ' ').Replace('(', ' ').Replace(')', ' ').Replace(',', ' ').Replace('-', ' ').Replace(':', ' '); 
            input_text = input_text.ToUpper().Replace('.','\n'); //переход в верхний регистр и замена точек на \n
            input_text = Regex.Replace(input_text, "\\n\\s", "\n"); //если перед началом предложения пробел, убираем пробел
            input_text = Regex.Replace(input_text, "\\n{2,}", "\n"); //если идут несколько \n подряд, оставляем один
            input_text = new string(input_text.Where(c => !char.IsDigit(c)).Where(c => c != '\r').ToArray()); //отфильтровываем цифры и \r
            input_text = Regex.Replace(input_text, "\\u0020{2,}", " "); //если идут несколько пробелов подряд, оставляем один

            return input_text; 
        }

        static void Main(string[] args)
        {            
            //string txt=File.ReadAllText("D:\\test_text.txt");
            //File.WriteAllText("D:\\test_text_formated.txt", FormatText(txt));

            if (args.Length == 0) return;

            foreach (var item in args)
            {
                Console.WriteLine("Обработка {0}", item);
                var text = File.ReadAllText(item);
                var output_text = FormatText(text);
                File.WriteAllText(item + "_f", output_text);
            }

            Console.WriteLine("Обработано {0} файлов.", args.Length);
            Console.ReadLine();
        }
    }
}
