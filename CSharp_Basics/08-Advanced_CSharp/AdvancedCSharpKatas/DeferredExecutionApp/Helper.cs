namespace DeferredExecutionApp
{
    public static class Helper
    {
        public static IEnumerable<int> Naturals()
        {
            var number = 1;

            while (true)
            {
                yield return number;
                number++;
            }
        }

        public static IEnumerable<int> WhereEven(IEnumerable<int> src)
        {
            foreach (var item in src)
            {
                Console.WriteLine(item);

                if ((item % 2) == 0)
                {
                    yield return item;
                }
            }
        }

        /*In comments, explain the difference between `IEnumerable<T>` and `IQueryable<T>`: which runs the predicate as *compiled delegates in memory*, and which turns your lambda into an *expression tree* a provider (e.g. EF Core → SQL) translates. When does calling `.Where(...)` on an `IQueryable` hit the database vs the app?
         
         IEnumerable<T> and IQueryable<T> both supports linq, but they are used for different purposes.
        For example, lets kick of method Where. For IEnumerable<T> the method Where(IEnumerable<T> source, Func<T, bool> predicate)
        For IQueryable<T> the method Where(IQueryable<T> sourcce, Expression<Func<T, bool>> expression tree)
        Mostly, IEnumerable<T> is used to working with collections and IQueryable<T> is used to working with EF Core and databases.
        Both of them supports deferred execution. And execution itself possible when we call methods like .ToArray(), .ToList(), etc. During the query like users.Where(u=>u.Age>18), in case of using IQueriable, we don't hit the database. We only hitting database, when we try to execute our query*/
    }
}
