/*
if/else condition statement

if(first_condition)
{
    if(another_condition) //nested if
    {
    }
}

nested if is the code smell and we should avoid if it possible

switch(variable)
{
  case value:
    code block;
    break;
  default:
    code block;
    break;
*/


using System.Net.NetworkInformation;

///1. Guard Clauses (Early Return)
///Instead of nesting your logic inside the multiple if cheks, check for invalid states first and return or throw an excetion immidiately

bool ProcessOrder(User user, Order order)
{
    if (user != null)
    {
        if (user.IsActive)
        {
            if (order.Amount > 0)
            {
                // Core processing logic here
                return true;
            }
        }
    }
    return false;
}

bool ProcessOrderSimplified(User user, Order order)
{
    if (user != null && user.IsActive && order.Amount > 0)
    {
        return true;
    }

    // Core processing logic here (Arrow code is gone!)

    return false;
}

///2. Pattern Matching and Switch Expressions
///Use pattern matching features. 

int GetAccessLevel(User user)
{
    if (user.Role == "Admin")
    {
        if (user.HasTwoFactor)
        {
            return 3;
        }
    }

    return 0;
}

int GetAccessSimplified(User user)
{
    int result = user switch
    {
        { Role: "Admin", HasTwoFactor: true } => 3,
        _ => 0,
    };

    return result;
}

/// 3. Use Dictionaries for Lookups
string GetPriority(string status)
{
    string priority;
    if (status == "Critical")
    {
        priority = "High";
    }
    else if (status == "Warning")
    {
        priority = "Medium";
    }
    else
    {
        priority = "Low";
    }

    return priority;
}

//Simplification
var statusMap = new Dictionary<string, string>()
{
    { "Critical", "High" },
    { "Warning", "Medium" },
};

string GetPrioritySimplified(string status)
{
    return statusMap[status] ?? "Low";
}

///4. Null-Conditional Operators
/// C# allows bypass nested null checks completly using the ?. and ??
string GetZipCode(Customer customer)
{
    string zipCode = "Unknown";
    if (customer != null)
    {
        if (customer.Address != null)
        {
            zipCode = customer.Address.ZipCode;
        }
    }

    return zipCode;
}

string GetZipCodeSimplified(Customer customer)
{
    string zipCode = customer?.Address?.ZipCode ?? "Unknown";
    
    return zipCode;
}