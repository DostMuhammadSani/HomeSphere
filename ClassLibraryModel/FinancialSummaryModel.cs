using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryModel
{
    public class FinancialSummaryModel
    {
        public decimal CurrentMonthIncome { get; set; }
        public decimal CurrentMonthExpenses { get; set; }
        public decimal CurrentMonthNet { get; set; }
        public int UnpaidFeesCount { get; set; }
        public decimal UnpaidFeesAmount { get; set; }
        public int ActiveBudgetsCount { get; set; }
    }
}
