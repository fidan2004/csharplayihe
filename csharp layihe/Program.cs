using layihe1.Data;
using layihe1.Models;
using layihe1.Services.Implementations;
using System;
using System.Collections.Generic;

namespace layihe1
{
    public static class Program
    {

        
        public static EmployeeService employeeService = new EmployeeService();
        public static BranchService branchService = new BranchService();

        static void Main(string[] args)
        {
            

            SeedDataBase();

            Manager manager = new Manager("Fidan", "123");
            Console.WriteLine("pls enter name");
            string enteredname = Console.ReadLine();
            Console.WriteLine("pls enter password");
            string enteredpass = Console.ReadLine();
           
                if (enteredname == manager.UserName && enteredpass == manager.Password)
                {
                    Menu();
                }
                else
                {
                    Console.WriteLine("wrong username or password");
                }
        }
       
        public static void Menu()
        {
            Console.WriteLine("pls enter number 1-Branch,2-Employee");
            int mode = int.Parse(Console.ReadLine());
            switch (mode)
            {
                case 1:
                    BranchActions();
                    break;
                case 2:
                    EmployeeActions();
                    break;
               
            }
        }
        public static void BranchActions()
        {
            while (true)
            {
                Console.WriteLine("Please choose which action do you want to make:");
                Console.WriteLine(
                    "1. List all branches.\n" +
                    "2. Get a special branch.\n" +
                    "3. Create a new branch.\n" +
                    "4. Delete existing branch.\n" +
                    "5. Update a branch.\n" +
                    "6. Get Profit.\n" +
                    "7. Hire employee.\n" +
                    "8. Transfer money.\n" +
                    "9. Transfer employee.\n "
                    );

                int branchChoice= int.Parse(Console.ReadLine());


                switch (branchChoice)
                {
                    case 1:
                        branchService.GetAll();
                        break;
                    case 2:
                        Console.WriteLine("Please enter the Branch's name:");
                        string branchName = Console.ReadLine();
                        Branch brnch = branchService.Get(branchName);
                        if (brnch != null)
                        {
                            Console.WriteLine(brnch.Name + " " + brnch.Address + " " + brnch.Budget);
                        }
                        else
                        {
                            Console.WriteLine("Branch wasnt found");
                        }
                        break;
                    case 3:
                        Console.WriteLine("Enter Bank name:");
                        string newBranchName = Console.ReadLine();
                        Console.WriteLine("Enter Bank address:");
                        string newBranchAddress = Console.ReadLine();
                        Console.WriteLine("Enter Bank budget AZN :");
                        int newBranchBudget = int.Parse(Console.ReadLine());
                       
                        Branch branch = new Branch(newBranchName,newBranchAddress,newBranchBudget);
                        branchService.Create(branch);
                        branchService.GetAll();
                        break;
                    case 4:
                        Console.WriteLine("Please enter the name of branch you want to delete:");
                        string branchToDelete = Console.ReadLine();
                       if(branchService.Delete(branchToDelete) == true )
                        {
                            Console.WriteLine($"Branch has been deleted from db.");
                            branchService.GetAll();
                        }
                        else
                        {
                            Console.WriteLine($"Couldn't find branch with name");
                        }
                          
                        break;
                    case 5:
                        Console.WriteLine("Please enter the name of the branch to update:");
                        string name1 = Console.ReadLine();
                        Console.WriteLine("pls enter the address of the branch to update");
                        string address1 = Console.ReadLine();
                        branchService.Update(name1, address1);
                        Console.WriteLine("successfully ");
                        break;
                    case 6:

                        Console.WriteLine("Please enter the name of the branch to get the profit of:");
                        string brname = Console.ReadLine();
                        branchService.GetProfit(brname);
                        break;
                    case 7:
                        Console.WriteLine("Please write Branch name to hire an employee:");
                        string hiringBranchName = Console.ReadLine();
                        Branch hiringBranch = branchService.Get(hiringBranchName);
                        if (hiringBranch == null)
                        {
                            Console.WriteLine("Branch with this name  not found.");
                          
                        }
                        Console.WriteLine("Please write name OR surname of an employee from above list to hire:");
                        employeeService.GetAll();
                        string empFilter = Console.ReadLine();
                        Employee empToHire = employeeService.Get(empFilter);
                        if (empToHire == null)
                        {
                            Console.WriteLine("Wrong employee information.");
                           
                        }
                        if (empToHire.salary <= hiringBranch.Budget)
                        {
                            hiringBranch.Employees.Add(empToHire);
                            Console.WriteLine("Employee hired successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Can't hire.");
                        }
                        break;
                    case 8:
                        Console.WriteLine("Please write Branch name to transfer from:");
                        string brnchnamefrom = Console.ReadLine();
                        Branch fromBranch = branchService.Get(brnchnamefrom);
                        if (fromBranch == null)
                        {
                            Console.WriteLine("Branch not found.");
                            break;
                        }
                        Console.WriteLine("Please write Branch name to transfer to:");
                        string brnchnameto = Console.ReadLine();
                        Branch toBranch = branchService.Get(brnchnameto);
                        if (toBranch == null)
                        {
                            Console.WriteLine("Branch not found.");
                            break;
                        }
                        Console.WriteLine("Please write amount to transfer:");
                        try
                        {
                            int amount = int.Parse(Console.ReadLine());
                            bool transferResult = branchService.TransferMoney(fromBranch, toBranch, amount);
                            if (transferResult == true)
                            {
                                Console.WriteLine("Transfer succeeded");
                                branchService.GetAll();

                            }
                            else
                            {
                                Console.WriteLine("The amount is bigger than the Branch's balance. Transfer canceled.");
                            }
                        }
                        catch(Exception)
                        {
                            Console.WriteLine("pls enter correctly");
                        }
                
                        break;
                    case 9:
                        Console.WriteLine("Please write the Branch name to transfer employee from:");
                        string fromBrnchName = Console.ReadLine();
                        Branch fromBrnch = branchService.Get(fromBrnchName);
                        if (fromBrnch == null)
                        {
                            Console.WriteLine("Can't find the branch");
                            
                        }

                        Console.WriteLine("Please write name OR surname of an employee from list to transfer:");
                        string nameorsurname = Console.ReadLine();
                        Employee employeeToTransfer = employeeService.Get(nameorsurname);
                        if (employeeToTransfer == null)
                        {
                            Console.WriteLine("Wrong employee information.");
                            
                        }

                        Console.WriteLine("Please write the Branch name to transfer employee TO:");
                        string toBrnchName = Console.ReadLine();
                        Branch toBrnch = branchService.Get(toBrnchName);
                        if (toBrnch == null)    
                        {
                            Console.WriteLine("Can't find the branch.");
                         
                        }
                        branchService.TransferEmployee(fromBrnch, toBrnch, employeeToTransfer);
                        Console.WriteLine("Employee transfered successfully!");
                        break;
                }
            }
        }
        public static void EmployeeActions()
        {
            while (true)
            {
                Console.WriteLine("Please choose which action do you want to make:");
                Console.WriteLine(
                    "1. List all employees.\n" +
                    "2. Get an employee.\n" +
                    "3. Create a new employee.\n" +
                    "4. Delete existing employee.\n" +
                    "5. Update an employee.\n" 
                    
                    );

                int employeeChoice = int.Parse(Console.ReadLine());

               
                switch (employeeChoice)
                {
                    case 1:
                        employeeService.GetAll();
                        break;
                    case 2:
                        Console.WriteLine("Please enter the Employee's name OR surname:");
                        string employeeFilter = Console.ReadLine();
                        Employee employee = employeeService.Get(employeeFilter);
                        if (employee != null)
                        {
                            Console.WriteLine($"{employee.Name} {employee.surname}, {employee.profession}, {employee.salary}");
                        }
                        else
                        {
                            Console.WriteLine("Employee with this name  not found");
                        }
                        break;

                    case 3:
                        Console.WriteLine("Enter Employee name:");
                        string newEmployeeName = Console.ReadLine();
                        Console.WriteLine("Enter Employee surname:");
                        string newEmployeeSurname = Console.ReadLine();
                        Console.WriteLine("Enter Employee profession:");
                        string newEmployeeProfession = Console.ReadLine();
                        Console.WriteLine("Enter Salary ");
                        int newEmployeeSalary = int.Parse(Console.ReadLine());
                        Employee newEmployee = new Employee(newEmployeeName, newEmployeeSurname, newEmployeeSalary, newEmployeeProfession);
                        employeeService.Create(newEmployee);
                        employeeService.GetAll();
                        break;

                    case 4:
                        Console.WriteLine("Please enter the name of the employee you want to delete:");
                        string employeeNameToDelete = Console.ReadLine();
                        if( employeeService.Delete(employeeNameToDelete) == true )
                        {
                            Console.WriteLine("Employee  has been deleted from db.");
                            branchService.GetAll(); 
                        }
                        else
                        {
                            Console.WriteLine("Couldn't find branch with name ");

                        }   
                            
                        break;


                    case 5:
                        Console.WriteLine("pls enter name");
                        string name = Console.ReadLine();
                        Console.WriteLine("pls enter surname");
                        string surname = Console.ReadLine();
                        employeeService.Update(name, surname);
                        Console.WriteLine("Employee updated successfully!");
                        break;
                   
                }
               
            }
        }
        public static void SeedDataBase()
        {
           
            Employee emp1 = new Employee("Fidan", "Karimova", 2000, "Developer");
            Employee emp2 = new Employee("Jale", "Abilova", 3000, "server");
            Employee emp3 = new Employee("Omer", "Aliyev", 4000, "translator");
            Employee emp4 = new Employee("Ayxan", "Karimli", 5000, "teacher");
            Employee emp5 = new Employee("Aylin", "Priyeva", 6000, "backend");
            Employee emp6 = new Employee("Jasmin", "Suleymanli", 7000, "frontend");
            employeeService.Create(emp1);
            employeeService.Create(emp2);
            employeeService.Create(emp3);
            employeeService.Create(emp4);
            employeeService.Create(emp5);
            employeeService.Create(emp6);



            Branch br1 = new Branch("ABB", "28 may",  1000000, false);
            Branch br2 = new Branch("Yelo bank", "Targovi",  3000000, false);
            Branch br3 = new Branch("Kontakt home", "Genclik", 5000000, false);
            Branch br4 = new Branch("Leo", "Bakixanov",  2000000, false);
            Branch br5 = new Branch("Kapital bank", "Sahil", 4000000, false);
            branchService.Create(br1);
            branchService.Create(br2);
            branchService.Create(br3);
            branchService.Create(br4);
            branchService.Create(br5);

            br1.Employees.Add(emp1);
            br2.Employees.Add(emp2);
            br3.Employees.Add(emp3);
            br4.Employees.Add(emp4);
            br5.Employees.Add(emp5);

        }
       
   }
}