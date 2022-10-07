using layihe1.Data;
using layihe1.Models;
using layihe1.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace layihe1.Services.Implementations
{
    public class BranchService : IBranchService
    {
        public Bank<Branch> branchDB;
        public BranchService()
        {
            branchDB = new Bank<Branch>();
        }
        public void Create(Branch branch)
        {
            if (branch.SoftDelete == false)
            {
                branchDB.Datas.Add(branch);
               
            }
         
        }

        public bool Delete(string name)
        {
            try
            {
                Branch brnc = branchDB.Datas.Find(x => x.Name.ToLower().Trim() == name.ToLower().Trim());
                brnc.SoftDelete = true;
                GetAll();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void GetAll()
        {
            
            foreach (var brnch in branchDB.Datas.Where(m => m.SoftDelete == false))
            {
                Console.WriteLine(brnch.Name + " " + brnch.Address + " " + brnch.Budget);
               
            }
        }

        public Branch  Get(string name)
        {
            try
            {
                Branch brnch = branchDB.Datas.Find(
                    x => x.Name.ToLower().Trim().Contains(name.ToLower().Trim()) ||
                    x.Address.ToLower().Trim().Contains(name.ToLower().Trim())
                    );
                return brnch;
            }
            catch (Exception)
            {
                Console.WriteLine("branch wasnt found");
                throw;
            }
        }


        public void Update( string data1,string data2)
        {
            try
            { 
                var e = branchDB.Datas.Where(x => x.Name.ToLower().Trim() == data1.ToLower().Trim() &&
                x.Address.ToLower().Trim() == data2.ToLower().Trim()).ToList(); // date1 = name , date2 = address

                e.ForEach(x => x.Budget = 100000);
                e.ForEach(x => x.Address = "28 may");
                Console.WriteLine(e);
                GetAll();
            }
            catch (Exception)
            {
                Console.WriteLine("pls enter correct name and address");
                throw;
            }
        }
        public void TransferEmployee(Branch brnchnamefrom, Branch brnchnameto, Employee employee)
        {
           
            brnchnamefrom.Employees.Remove(employee);
            brnchnameto.Employees.Add(employee);
        }

        public bool TransferMoney(Branch brnchnamefrom, Branch brnchnameto, int amount)
        {
            if (amount <= brnchnamefrom.Budget)
            {
                brnchnamefrom.Budget -= amount;
                brnchnameto.Budget += amount;
                return true;
            }
            else
            {
                return false;
            }
        }

        
        public void HireEmployee(string empname, string brchname)
        {
            
        }

        public void GetProfit(string branchAdress)
        {
            Branch branch = Get(branchAdress);
            int lastbudget= branch.Budget;
            foreach (var item in branch.Employees)
            {
                lastbudget  -= item.salary;
            }
            
            Console.WriteLine(lastbudget);
        }
    }
}
