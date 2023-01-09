using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CalculateRewards.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RewardsApiController : ControllerBase
    {
        [HttpPost]
        public ActionResult<Dictionary<int, Dictionary<string, int>>> CalculateRewards(CustomerTransaction[] transactions)
        {
            // Group the transactions by customer
            var transactionsByCustomer = transactions.GroupBy(t => t.CustomerId);

            // A dictionary to store the total points earned for each customer
            Dictionary<int, Dictionary<string, int>> pointsByCustomer = new Dictionary<int, Dictionary<string, int>>();

            // Iterate over the transactions by customer
            foreach (var customerTransactions in transactionsByCustomer)
            {
                // Get the customer ID
                int customerId = customerTransactions.Key;

                // A dictionary to store the total points earned for each month
                Dictionary<string, int> pointsByMonth = new Dictionary<string, int>();

                // Group the transactions by month
                var transactionsByMonth = customerTransactions.GroupBy(t => t.Date.ToString("yyyy-MM"));

                // Iterate over the transactions by month
                foreach (var monthTransactions in transactionsByMonth)
                {
                    // Get the month
                    string month = monthTransactions.Key;

                    // Calculate the total points earned for the month
                    int totalPoints = CalculateTotalPoints(monthTransactions.ToArray());

                    // Add the total points earned for the month to the dictionary
                    pointsByMonth[month] = totalPoints;
                }

                // Add the total points earned for each month to the dictionary for the customer
                pointsByCustomer[customerId] = pointsByMonth;
            }

            return pointsByCustomer;
        }

        private int CalculateTotalPoints(CustomerTransaction[] transactions)
        {
            // A variable to store the total number of points earned
            int totalPoints = 0;

            // Iterate over the array of transactions
            foreach (CustomerTransaction transaction in transactions)
            {
                int transactionPoints = CalculatePoints(transaction.Amount);
                // Add the points earned for this transaction to the total
                totalPoints += transactionPoints;
            }

            return totalPoints;
        }

        private int CalculatePoints(double price)
        {
            if (price >= 50 && price < 100)
            {
                return (int)(price - 50);
            }
            else if (price > 100)
            {
                return ((int)(2 * (price - 100) + 50));
            }
            return 0;
        }

    }
}