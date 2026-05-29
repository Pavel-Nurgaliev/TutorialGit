using System.Text.RegularExpressions;
using System.Globalization;
using TransformMessagesToConstants;

string en = string.Empty;
while (en != "stop")
{
    en = Console.ReadLine();

    string constant = ConverterFromMessageToConstant.ToConstantName(en) + "ErrorMessage";

    Console.WriteLine(constant);
}