using layihe1.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace layihe1.Services.Interfaces
{
    public interface IBranchService : IBankService<Branch>
    {
        public void HireEmployee(string name, string brchname);
        public void GetProfit(string branchAdress);
        public bool TransferMoney(Branch brnchnamefrom, Branch brnchnameto, int amount);
        public void TransferEmployee(Branch brnchnamefrom, Branch brnchnameto, Employee employee);
    }
}