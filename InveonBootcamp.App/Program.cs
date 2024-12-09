// See https://aka.ms/new-console-template for more information
using InveonBootcamp.App;

Console.WriteLine("Hello, World!");


//Interface Kullanimi
SalaryCalculator salaryCalculator = new SalaryCalculator();
var managerSalary = salaryCalculator.Calculate(1000, new SeniorSalaryCalculate());



Console.WriteLine(managerSalary);