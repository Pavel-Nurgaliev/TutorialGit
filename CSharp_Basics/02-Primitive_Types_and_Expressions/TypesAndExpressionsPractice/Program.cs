try
{
    var number = "1234";

    byte b = Convert.ToByte(number);

    Console.WriteLine(b);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}