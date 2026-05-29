using static System.Net.Mime.MediaTypeNames;

var actionName = string.Empty;

while (actionName != "stop")
{
    actionName = Console.ReadLine();

	var splitedName = actionName.Split('_');

	var result = string.Empty;
	foreach (var part in splitedName)
	{
		result+= part.Substring(0, 1).ToUpper() + part.Substring(1).ToLower();
    }

    Console.WriteLine(result);
}
