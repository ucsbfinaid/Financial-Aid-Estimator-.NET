using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ucsb.Sa.FinAid.AidEstimation.Utility
{
    public class RawSimpleDependentEfcCalculatorArguments
    {
        public string MaritalStatus { get; set; }

        public string ParentIncome { get; set; }

        public string ParentOtherIncome { get; set; }

        public string ParentIncomeEarnedBy { get; set; }

        public string ParentIncomeTax { get; set; }

        public string ParentAssets { get; set; }

        public string StudentIncome { get; set; }

        public string StudentOtherIncome { get; set; }

        public string StudentIncomeTax { get; set; }

        public string StudentAssets { get; set; }

        public string NumberInHousehold { get; set; }

        public string NumberInCollege { get; set; }

        public string StateOfResidency { get; set; }
    }
}
