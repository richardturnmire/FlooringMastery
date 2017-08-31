using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;
using FlooringMastery.Models.Responses;

namespace FlooringMastery.BLL.Rules
{
    public class OrderRemovalRule
    {
        public OrderRemovalResponse Remove(Order order)
        {
            OrderRemovalResponse response = new OrderRemovalResponse
            {
                Success = false
            };

            //if (account.Type != AccountType.Premium)
            //{
            //    response.Message = "Error: A non Premium account hit the Premium Withdrawal Rule.";
            //    return response;
            //}

            //if (amount <= 0)
            //{
            //    response.Message = "Withdrawal amount must be greater than 0";
            //    return response;
            //}

            //response.OldBalance = account.Balance;
            //account.Balance -= amount;
            //if (account.Balance < -500M)
            //{
            //    response.OverdraftFees = 10M;
            //    account.Balance -= response.OverdraftFees;
            //}
            //else
            //{
            //    response.OverdraftFees = 0;
            //}
            //response.Account = account;
            //response.Amount = amount;
            //response.Success = true;

            return response;
        }
    }
}
