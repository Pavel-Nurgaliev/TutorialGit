using System;
using System.Collections.Generic;
using System.Linq;

public record Order(int Id, int CustomerId, DateTime CreatedAt, decimal Amount, string Currency, string City);
public record Customer(int Id, string Name, string Segment, string Country);
public record FxRate(string Currency, DateTime Date, decimal RateToUSD);
public record EventLog(DateTime Ts, string User, string Action, string Page, string SessionId);
public record Sale(string Sku, string Category, string Brand, string Region, DateTime Dt, int Qty, decimal Price);
public record Manufacturer(string Name, string Headquarters, int Year);
public record Car(string Name, string Manufacturer, int Year, int City, int Highway, double Displacement);
public record Grade(int StudentId, string Course, DateTime Dt, int Score);
public record Student(int Id, string Name, int GroupId);
public record Group(int Id, string Title);

static class Seed
{
    public static (List<Customer> customers, List<Order> orders, List<FxRate> fx,
        List<EventLog> logs, List<Sale> sales, List<Manufacturer> mfrs, List<Car> cars,
        List<Student> students, List<Group> groups, List<Grade> grades) Data()
    {
        var customers = new List<Customer> {
            new(1,"Alice","SMB","US"), new(2,"Bob","Enterprise","DE"),
            new(3,"Chandra","MidMarket","IN"), new(4,"Diana","Enterprise","US"),
            new(5,"Ethan","SMB","UA")
        };

        var orders = new List<Order> {
            new(101,1,new DateTime(2025,9,1,10,0,0), 120m,"USD","NYC"),
            new(102,1,new DateTime(2025,9,1,12,0,0), 90m,"EUR","NYC"),
            new(103,2,new DateTime(2025,9,2, 9,0,0), 1000m,"EUR","Berlin"),
            new(104,3,new DateTime(2025,9,2,10,0,0), 70000m,"INR","Mumbai"),
            new(105,3,new DateTime(2025,9,3,11,0,0), 40000m,"INR","Mumbai"),
            new(106,4,new DateTime(2025,9,3,13,0,0), 250m,"USD","SF"),
            new(107,5,new DateTime(2025,9,4, 8,0,0), 3000m,"UAH","Kyiv"),
            new(108,5,new DateTime(2025,9,4, 9,0,0),  100m,"USD","Kyiv")
        };

        var fx = new List<FxRate> {
            new("USD", new DateTime(2025,9,1), 1m),
            new("EUR", new DateTime(2025,9,1), 1.12m),
            new("EUR", new DateTime(2025,9,2), 1.11m),
            new("INR", new DateTime(2025,9,2), 0.012m),
            new("INR", new DateTime(2025,9,3), 0.0121m),
            new("UAH", new DateTime(2025,9,4), 0.026m)
        };

        var logs = new List<EventLog> {
            new(new DateTime(2025,9,1,9,0,0),"alice","view","/home","S1"),
            new(new DateTime(2025,9,1,9,1,0),"alice","click","/product/42","S1"),
            new(new DateTime(2025,9,1,9,5,0),"alice","purchase","/checkout","S1"),
            new(new DateTime(2025,9,1,10,0,0),"bob","view","/home","S2"),
            new(new DateTime(2025,9,1,10,3,0),"bob","view","/pricing","S2"),
            new(new DateTime(2025,9,1,10,5,0),"bob","logout","/","S2"),
            new(new DateTime(2025,9,1,11,0,0),"alice","view","/home","S3"),
        };

        var sales = new List<Sale> {
            new("SKU1","Phones","Acme","EU", new DateTime(2025,9,1), 10, 500),
            new("SKU2","Phones","Acme","EU", new DateTime(2025,9,2),  5, 600),
            new("SKU3","Laptops","Bold","US",new DateTime(2025,9,1),  2, 1500),
            new("SKU1","Phones","Acme","US", new DateTime(2025,9,2), 20, 480),
            new("SKU4","Laptops","Acme","EU",new DateTime(2025,9,2),  1, 2000),
            new("SKU5","Phones","Bold","APAC",new DateTime(2025,9,2),15, 520),
        };

        var mfrs = new List<Manufacturer> {
            new("Acme","Berlin",1990), new("Bold","London",1985), new("Corsa","Detroit",1979)
        };
        var cars = new List<Car> {
            new("Falcon","Acme",2018,20,30,2.0),
            new("Jet","Acme",2020,25,34,1.6),
            new("Bear","Bold",2015,18,27,3.2),
            new("Wolf","Bold",2022,22,32,2.2),
            new("Bull","Corsa",2019,17,24,5.0),
        };

        var groups = new List<Group> {
            new(1,"Math"), new(2,"Physics"), new(3,"Literature")
        };
        var students = new List<Student> {
            new(1,"Alice",1), new(2,"Bob",1), new(3,"Charlie",2),
            new(4,"Diana",3), new(5,"Ethan",2), new(6,"Fiona",1)
        };
        var grades = new List<Grade> {
            new(1,"Math", new DateTime(2025,9,1),95),
            new(2,"Math", new DateTime(2025,9,1),67),
            new(3,"Physics", new DateTime(2025,9,2),88),
            new(4,"Literature", new DateTime(2025,9,3),72),
            new(5,"Physics", new DateTime(2025,9,2),91),
            new(6,"Math", new DateTime(2025,9,1),45),
            new(1,"Physics", new DateTime(2025,9,2),78),
            new(2,"Physics", new DateTime(2025,9,2),59),
        };

        return (customers, orders, fx, logs, sales, mfrs, cars, students, groups, grades);
    }
}
