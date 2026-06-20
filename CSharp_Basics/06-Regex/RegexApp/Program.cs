/*
 Regex Practice Tasks
 Implement each method in RegexHelpers.cs, then run the tests:
   dotnet test ..\RegexApp.Tests\RegexApp.Tests.csproj

 Task 1 - IsValidEmail
   Validate that a string is a properly formatted email address.
   Example: "user@example.com" → true, "invalid-email" → false

 Task 2 - ExtractNumbers
   Extract all integers from a string and return them in order.
   Example: "abc 42 def 7" → [42, 7]

 Task 3 - IsValidPhoneNumber
   Accept Russian mobile numbers in these formats only:
     +7XXXXXXXXXX  |  8XXXXXXXXXX  |  +7-XXX-XXX-XX-XX
   Example: "+79161234567" → true, "9161234567" → false

 Task 4 - ParseDate
   Parse a date in "DD.MM.YYYY" format into (day, month, year) using named groups.
   Return null for any other format.
   Example: "25.12.2024" → (25, 12, 2024)

 Task 5 - CamelToSnakeCase
   Convert a camelCase identifier to snake_case.
   Example: "myVariableName" → "my_variable_name"

 Task 6 - IsValidPassword
   Password must be at least 8 characters and contain:
     - At least one uppercase letter
     - At least one lowercase letter
     - At least one digit
     - At least one special character (!@#$%^&*...)
   Use lookaheads so the checks are independent of order.
   Example: "P@ssw0rd" → true, "abc1!23x" → false (no uppercase)
*/

// Run: dotnet test ..\RegexApp.Tests\RegexApp.Tests.csproj

Console.WriteLine("Implement the methods in RegexHelpers.cs, then run the tests.");