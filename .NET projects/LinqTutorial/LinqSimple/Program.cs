using Features;

Func<int, int> square = (x) => x * x;
Func<int, int, int> add = (int a, int b) =>
{
    var temp = a + b;
    return temp;
};//method body

/*LINQ interacts with IEnumberable<T> objects (That implements GetEnumerator by itself)*/
Employee[] developers = new Employee[]
{
    new Employee{ Id=1, Name="Scott" },
    new Employee{ Id=2, Name="Chris"},
};

List<Employee> sales = new List<Employee>
{
    new Employee{ Id=3, Name="Alex" },
};

var employees = developers.Filter(e => e.Name.Length == 5).OrderBy(e => e.Name);

foreach (var employee in employees)
{
    Console.WriteLine(employee.Name);
}