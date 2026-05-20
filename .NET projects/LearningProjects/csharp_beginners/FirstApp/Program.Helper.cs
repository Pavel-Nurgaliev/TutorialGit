namespace FirstApp
{
    public class ProgramHelper
    {
        public static void ByRefReadonly(ref readonly string s) => Console.WriteLine(s);

        public static void ByIn(in string s) => Console.WriteLine(s);
    }
}
