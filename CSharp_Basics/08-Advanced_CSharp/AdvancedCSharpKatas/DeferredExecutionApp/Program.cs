using DeferredExecutionApp;

var nat = Helper.Naturals();

var whereEven = Helper.WhereEven(nat);

whereEven.Take(5).ToArray();

Console.WriteLine("end");