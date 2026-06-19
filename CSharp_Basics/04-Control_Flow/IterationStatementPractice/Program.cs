/*Note: for all these exercises, ignore input validation unless otherwise directed. Assume the user enters a value in the format that the program expects. For example, if the program expects the user to enter a number, don't worry about validating if the input is a number or not. When testing your program, simply enter a number.



1- Write a program to count how many numbers between 1 and 100 are divisible by 3 with no remainder. Display the count on the console.



2- Write a program and continuously ask the user to enter a number or "ok" to exit. Calculate the sum of all the previously entered numbers and display it on the console.



3- Write a program and ask the user to enter a number. Compute the factorial of the number and print it on the console. For example, if the user enters 5, the program should calculate 5 x 4 x 3 x 2 x 1 and display it as 5! = 120.



4- Write a program that picks a random number between 1 and 10. Give the user 4 chances to guess the number. If the user guesses the number, display “You won"; otherwise, display “You lost". (To make sure the program is behaving correctly, you can display the secret number on the console first.)



5- Write a program and ask the user to enter a series of numbers separated by comma. Find the maximum of the numbers and display it on the console. For example, if the user enters “5, 3, 8, 1, 4", the program should display 8.*/

PrintDivisibleByThree();
PrintSum();
PrintFactorial();
PrintGuessedNumber();
PrintMaxValue();

void PrintDivisibleByThree()
{
    Console.WriteLine("01-Print numbers that is devisible by three");

    var count = 0;
    var divider = 3;

    for (int number = 1; number < 100; number++)
    {
        if (number % divider == 0)
        {
            count++;
        }
    }

    Console.WriteLine(count);
}

void PrintSum()
{
    Console.WriteLine("02-Print sum");

    bool isOk = false;

    while (!isOk)
    {
        Console.WriteLine("Enter the numbers (ex. '1, 2, 3, 4')");

        var input = Console.ReadLine();

        var sum = Array.ConvertAll(input.Split(", "), x => Convert.ToInt32(x)).Sum();

        Console.WriteLine("Sum is {0} .Write 'OK' to exit", sum);

        isOk = Console.ReadLine() == "OK";
    }
}

void PrintFactorial()
{
    Console.WriteLine("03 - Write the number for factorial");

    var number = Convert.ToInt32(Console.ReadLine());

    int factorial = 1;
    for (int i = 1; i <= number; i++)
    {
        factorial *= i;
    }

    Console.WriteLine(factorial);
}

void PrintGuessedNumber()
{
    Console.WriteLine("04 - Guess the number");

    var random = new Random();

    var randomNumber = random.Next(1, 10);

    var numberChances = 4;

    var userNumber = 0;
    for (int i = 0; i < numberChances; i++)
    {
        Console.WriteLine("Write the number");

        userNumber = Convert.ToInt32(Console.ReadLine());

        if (userNumber == randomNumber)
        {
            Console.WriteLine("You won");

            return;
        }
    }

    Console.WriteLine("You lost");
}

void PrintMaxValue()
{
    Console.WriteLine("05-Print numbers separated by comma");

    var splittedNumbers = Console.ReadLine()?.Split(", ");

    var values = Array.ConvertAll<string, int>(splittedNumbers, x => Convert.ToInt32(x));

    int maxValue = 0;
    foreach (var v in values)
    {
        if (v > maxValue)
        {
            maxValue = v;
        }
    }

    Console.WriteLine(maxValue);
}
