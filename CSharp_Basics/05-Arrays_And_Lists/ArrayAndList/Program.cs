using System.Text;

/*
 Note: For any of these exercises, ignore input validation unless otherwise directed. Assume the user enters values in the format that the program expects.

1- When you post a message on Facebook, depending on the number of people who like your post, Facebook displays different information.

If no one likes your post, it doesn't display anything.
If only one person likes your post, it displays: [Friend's Name] likes your post.
If two people like your post, it displays: [Friend 1] and [Friend 2] like your post.
If more than two people like your post, it displays: [Friend 1], [Friend 2] and [Number of Other People] others like your post.
Write a program and continuously ask the user to enter different names, until the user presses Enter (without supplying a name). Depending on the number of names provided, display a message based on the above pattern.

2- Write a program and ask the user to enter their name. Use an array to reverse the name and then store the result in a new string. Display the reversed name on the console.

3- Write a program and ask the user to enter 5 numbers. If a number has been previously entered, display an error message and ask the user to re-try. Once the user successfully enters 5 unique numbers, sort them and display the result on the console.

4- Write a program and ask the user to continuously enter a number or type "Quit" to exit. The list of numbers may include duplicates. Display the unique numbers that the user has entered.

5- Write a program and ask the user to supply a list of comma separated numbers (e.g 5, 1, 9, 2, 10). If the list is empty or includes less than 5 numbers, display "Invalid List" and ask the user to re-try; otherwise, display the 3 smallest numbers in the list.
 */

//DisplayInformation();
//PrintReversedUserName();
//PrintSortedNumbers();
//PrintUniqueNumbers();
PrintThreeSmallestNumbers();


///01
///
void DisplayInformation()
{
    var inputMessage = "Write the different name. Or press 'Enter' to exit";

    var friendsList = new List<string>();

    var friendName = string.Empty;

    bool isNotEmptyFriendName = false;
    do
    {
        Console.WriteLine(inputMessage);

        friendName = Console.ReadLine();

        isNotEmptyFriendName = !string.IsNullOrEmpty(friendName);

        if (isNotEmptyFriendName)
        {
            friendsList.Add(friendName);
        }
    }
    while (isNotEmptyFriendName);

    DisplayResult(friendsList);
}

void DisplayResult(List<string> friendsList)
{
    switch (friendsList.Count)
    {
        case 0:
            break;
        case 1:
            Console.WriteLine("{0}, likes your post", friendsList[0]);
            break;
        case 2:
            Console.WriteLine("{0} and {1} like your post", friendsList[0]);
            break;
        default:
            Console.WriteLine("{0}, {1}, and {2} others like your post", friendsList[0], friendsList[1], friendsList.Count - 2);
            break;
    }
}

///02
///
void PrintReversedUserName()
{
    Console.WriteLine("Write your name. It would be reversed");

    var userName = Console.ReadLine();

    if (!string.IsNullOrEmpty(userName))
    {
        string reversedName = GetReversedName(userName);

        Console.WriteLine(reversedName);
    }
}

string GetReversedName(string userName)
{
    var splittedUserName = userName.Split(' ');

    var reversedName = string.Empty;

    foreach (var partName in splittedUserName)
    {
        var sbReversedName = new StringBuilder();
        for (int i = partName.Length - 1; i >= 0; i--)
        {
            sbReversedName.Append(partName[i]);
        }

        reversedName += $"{sbReversedName} ";
    }

    return reversedName.TrimEnd();
}

///03
///
const int UniqueNumber = 5;
void PrintSortedNumbers()
{
    Console.WriteLine("Write {0} unique numbers", UniqueNumber);

    List<int> numbers = new List<int>();
    List<int> currentNumbers = new List<int>();

    do
    {
        Console.WriteLine("{0}, input other {1} numbers", PrintNumbers(numbers), UniqueNumber - numbers.Count);

        currentNumbers.Clear();
        currentNumbers.AddRange(Array.ConvertAll<string, int>(Console.ReadLine().Split(", "), x => Convert.ToInt32(x)));

        AddUniqueNumbers(numbers, currentNumbers);
    }
    while (numbers.Count < UniqueNumber);

    numbers.Sort();

    Console.WriteLine(PrintNumbers(numbers));
}

void AddUniqueNumbers(List<int> numbers, List<int> currentNumbers)
{
    var limitRange = UniqueNumber - numbers.Count;

    if (currentNumbers.Count > limitRange)
    {
        Console.WriteLine("Input numbers over limit. Numbers {0} would be deleted", GetStringNumbersOverLimit(currentNumbers, limitRange));

        currentNumbers.RemoveRange(limitRange, currentNumbers.Count - limitRange);
    }

    foreach (var cn in currentNumbers)
    {
        if (numbers.Contains(cn))
        {
            Console.WriteLine("{0} is not unique", cn);

            continue;
        }

        numbers.Add(cn);
    }
}

string GetStringNumbersOverLimit(List<int> source, int startIndex)
{
    string result = string.Empty;

    for (int i = startIndex; i < source.Count; i++)
    {
        result += source[i].ToString() + ", ";
    }

    return result.TrimEnd(' ', ',');
}

string PrintNumbers(List<int> numbers)
{
    string result = string.Empty;
    foreach (var item in numbers)
    {
        result += $"{item}, ";
    }

    return !string.IsNullOrEmpty(result) ? $"Wrote numbers {result.TrimEnd(' ', ',')}" : "Nothing wrote numbers";
}

///04
///
const string Quit = "Quit";
void PrintUniqueNumbers()
{
    bool isQuit = false;

    var listNumbers = new List<int>();

    do
    {
        Console.WriteLine("Enter a number or type \"Quit\" to exit");

        var input = Console.ReadLine();

        if (int.TryParse(input, out int number))
        {
            AddUniqueNumber(number, listNumbers);
        }
        else
        {
            isQuit = input.ToUpper() == Quit.ToUpper();
        }
    }
    while (!isQuit);

    Console.WriteLine(PrintNumbers(listNumbers));
}

void AddUniqueNumber(int number, List<int> listNumbers)
{
    if (listNumbers.Contains(number))
    {
        Console.WriteLine("{0} is not unique", number);

        return;
    }

    listNumbers.Add(number);
}
///05
///
const int NumbersAmout = 5;
void PrintThreeSmallestNumbers()
{
    var listNumbers = new List<int>();

    do
    {
        listNumbers.Clear();

        Console.WriteLine("Write a list of comma separated numbers(e.g 5, 1, 9, 2, 10)");

        var input = Console.ReadLine();

        var splitted = input?.Split(", ");

        if (splitted is null || splitted.Length < NumbersAmout)
        {
            Console.WriteLine("Invalid List");
        }

        listNumbers.AddRange(Array.ConvertAll<string, int>(splitted, x => Convert.ToInt32(x)));
    }
    while (listNumbers.Count < NumbersAmout);

    listNumbers.Sort();

    Console.WriteLine("Three smallest numbers: {0}, {1}, {2}", listNumbers[0], listNumbers[1], listNumbers[2]);
}