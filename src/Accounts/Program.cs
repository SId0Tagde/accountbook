// See https://aka.ms/new-console-template for more information
using System;
using System.Linq;
using System.Collections.Generic;

namespace Accounts
{
    public class Program{

        IList<Employee> employeeList;
	    IList<Salary> salaryList;

        public Program(){
		employeeList = new List<Employee>() { 
			new Employee(){ EmployeeID = 1, EmployeeFirstName = "Rajiv", EmployeeLastName = "Desai", Age = 49},
			new Employee(){ EmployeeID = 2, EmployeeFirstName = "Karan", EmployeeLastName = "Patel", Age = 32},
			new Employee(){ EmployeeID = 3, EmployeeFirstName = "Sujit", EmployeeLastName = "Dixit", Age = 28},
			new Employee(){ EmployeeID = 4, EmployeeFirstName = "Mahendra", EmployeeLastName = "Suri", Age = 26},
			new Employee(){ EmployeeID = 5, EmployeeFirstName = "Divya", EmployeeLastName = "Das", Age = 20},
			new Employee(){ EmployeeID = 6, EmployeeFirstName = "Ridhi", EmployeeLastName = "Shah", Age = 60},
			new Employee(){ EmployeeID = 7, EmployeeFirstName = "Dimple", EmployeeLastName = "Bhatt", Age = 53}			
		};

        salaryList = new List<Salary>() {
			new Salary(){ EmployeeID = 1, Amount = 1000, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 1, Amount = 500, Type = SalaryType.Performance},
			new Salary(){ EmployeeID = 1, Amount = 100, Type = SalaryType.Bonus},
			new Salary(){ EmployeeID = 2, Amount = 3000, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 2, Amount = 1000, Type = SalaryType.Bonus},
			new Salary(){ EmployeeID = 3, Amount = 1500, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 4, Amount = 2100, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 5, Amount = 2800, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 5, Amount = 600, Type = SalaryType.Performance},
			new Salary(){ EmployeeID = 5, Amount = 500, Type = SalaryType.Bonus},
			new Salary(){ EmployeeID = 6, Amount = 3000, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 6, Amount = 400, Type = SalaryType.Performance},
			new Salary(){ EmployeeID = 7, Amount = 4700, Type = SalaryType.Monthly}
		};
        
        }
     public static void Main(string[] args){
            
            Program program = new Program();

            program.Task1();
            program.Task2();
            program.Task3();

        }

        public void Task1()
        {   
            System.Console.WriteLine("List of employee With their salaries,in ascending order of salaries");
        
        //Group by EmployeeID on salary    
        var totalsalary = from salary in this.salaryList
                          group salary by salary.EmployeeID
                          into salarygrp
                          select new{
                            EmployeeID = salarygrp.Key,
                            Sum = salarygrp.Sum( s => s.Amount )
                            };
                    
        var account =from employee in this.employeeList
                    join salary in totalsalary
                      on employee.EmployeeID
                      equals
                      salary.EmployeeID
                    orderby salary.Sum
                    select new{
                          emp = employee,
                          salary = salary.Sum,
                      };
                      
                
        foreach (var employee in account)
        {     try{
                Console.WriteLine($" Name: {employee.emp.EmployeeFirstName} {employee.emp.EmployeeLastName}, Salary: {employee.salary}");
                Console.WriteLine();
                 }catch(Exception ex){
                   System.Console.WriteLine(ex.Message);
                                    }        
        }

        }

        public void Task2()
        {
        System.Console.WriteLine("Second oldest employee and his salary :");
        
        //query for selecting object with monthly salary
        var monthlysalary = from salary in this.salaryList
                            where salary.Type == SalaryType.Monthly 
                            select salary;

       var accounts = from salary in monthlysalary
                      join employee in this.employeeList
                             on salary.EmployeeID
                             equals
                             employee.EmployeeID
                      orderby employee.Age descending
                      select new{
                            salary,
                            employee
                               };

        foreach (var item in accounts.Take(2).Skip(1))
        {
                try{
                    Console.WriteLine($" EmployeeID: {item.employee.EmployeeID}, Name: {item.employee.EmployeeFirstName} {item.employee.EmployeeLastName}, Age: {item.employee.Age}, Salary: {item.salary.Amount}");
                   Console.WriteLine();
                }catch(Exception ex){
                            Console.WriteLine(ex.Message);
                            }
        }
        }

        public void Task3()
        {
        System.Console.WriteLine("Employees whose age is greater than 30 , printing their mean salary ");
        
        //Group by EmployeeID on salary    
     var meansalary = from salary in this.salaryList
                      group salary by salary.EmployeeID
                      into salarygrp
                      select new{
                        EmployeeID = salarygrp.Key,
                        avgamount = salarygrp.Average( s => s.Amount)
                     };

        //employee with age greater than 30
        var empGrt30 = (from employee in employeeList
                     where employee.Age > 30 
                     select employee);
        
        var accounts= from employee in empGrt30
                      join salary in meansalary
                        on employee.EmployeeID
                        equals
                        salary.EmployeeID
                      orderby employee.Age descending
                      select new{
                          emp = employee,
                          avgsalary = salary.avgamount
                      };

        foreach (var item in accounts)
        {   try{
            Console.WriteLine($" EmployeeID: {item.emp.EmployeeID}, Name: {item.emp.EmployeeFirstName} {item.emp.EmployeeLastName}, Age: {item.emp.Age}");
            Console.WriteLine($" Mean salary: {item.avgsalary:N1}");
            Console.WriteLine();
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            } 
        }   
        }

    }

            } 



