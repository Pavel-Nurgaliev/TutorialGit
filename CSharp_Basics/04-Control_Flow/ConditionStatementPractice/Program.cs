/*
Note: for all these exercises, ignore input validation unless otherwise directed. Assume the user enters a value in the format that the program expects. For example, if the program expects the user to enter a number, don't worry about validating if the input is a number or not. When testing your program, simply enter a number.



1- Write a program and ask the user to enter a number. The number should be between 1 to 10. If the user enters a valid number, display "Valid" on the console. Otherwise, display "Invalid". (This logic is used a lot in applications where values entered into input boxes need to be validated.)



2- Write a program which takes two numbers from the console and displays the maximum of the two.



3- Write a program and ask the user to enter the width and height of an image. Then tell if the image is landscape or portrait.



4- Your job is to write a program for a speed camera. For simplicity, ignore the details such as camera, sensors, etc and focus purely on the logic. Write a program that asks the user to enter the speed limit. Once set, the program asks for the speed of a car. If the user enters a value less than the speed limit, program should display Ok on the console. If the value is above the speed limit, the program should calculate the number of demerit points. For every 5km/hr above the speed limit, 1 demerit points should be incurred and displayed on the console. If the number of demerit points is above 12, the program should display License Suspended.
*/

using ConditionStatementPractice;

//PrintValidValues();
//PrintMaxValue();
//PrintImageStatus();
PrintSpeedLimit();

void PrintSpeedLimit()
{
    Console.WriteLine("04 - Write speed limit");
    var speedLimit = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("Write car speed");
    var speedCar = Convert.ToInt32(Console.ReadLine());

    if (speedCar <= speedLimit)
    {
        Console.WriteLine("Ok");

        return;
    }

    DemeritPoint point = new DemeritPoint(speedCar - speedLimit);

    Console.WriteLine("Demerit points: {0}", point.Amount);

    if (point.Amount > 12)
    {
        Console.WriteLine("License Suspended");
    }
}

void PrintValidValues()
{
    Console.WriteLine("01 - Write number");

    var number = Convert.ToInt32(Console.ReadLine());

    var message = !(number < 1 || number > 10) ? "Valid" : "Invalid";

    Console.WriteLine("01 - " + message);
}

void PrintMaxValue()
{
    Console.WriteLine("02 - Write two numbers");

    var input = Console.ReadLine();

    if (string.IsNullOrEmpty(input))
    {
        Console.WriteLine("InvalidValue");

        return;
    }

    int[] values = Array.ConvertAll<string, int>(input.Split(' '), x => Convert.ToInt32(x));

    var maxValue = values[0] > values[1] ? values[0] : values[1];

    Console.WriteLine("02 - " + maxValue);
}

void PrintImageStatus()
{
    Console.WriteLine("03 - Write two numbers (width, height)");

    var input = Console.ReadLine();

    if (string.IsNullOrEmpty(input))
    {
        Console.WriteLine("InvalidValue");

        return;
    }

    int[] values = Array.ConvertAll<string, int>(input.Split(' '), x => Convert.ToInt32(x));

    int width = values[0];
    int height = values[1];

    var message = width > height ? "Landscape" : "Portrait";

    Console.WriteLine("04 - " + message);
}