using System;
using System.Linq;
using Ucsb.Sa.FinAid.AidEstimation.EfcCalculation;
using Ucsb.Sa.FinAid.AidEstimation.EfcCalculation.Arguments;
using Ucsb.Sa.FinAid.AidEstimation.Utility;

namespace Ucsb.Sa.FinAid.AidEstimation.Web.Full
{
    public partial class Dependent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                // Collect user input
                RawDependentEfcCalculatorArguments rawArgs = new RawDependentEfcCalculatorArguments();

                rawArgs.OldestParentAge = inputOldestParentAge.Text;
                rawArgs.MaritalStatus = inputMaritalStatus.SelectedValue;
                rawArgs.StateOfResidency = inputStateOfResidency.SelectedValue;

                rawArgs.IsFirstParentWorking = inputFirstParentWorking.SelectedValue;
                rawArgs.FirstParentWorkIncome = inputFirstParentWorkIncome.Text;
                rawArgs.IsSecondParentWorking = inputSecondParentWorking.SelectedValue;
                rawArgs.SecondParentWorkIncome = inputSecondParentWorkIncome.Text;
                rawArgs.IsStudentWorking = inputStudentWorking.SelectedValue;
                rawArgs.StudentWorkIncome = inputStudentWorkIncome.Text;

                rawArgs.ParentAgi = inputParentAgi.Text;
                rawArgs.AreParentsTaxFilers = inputAreParentsTaxFilers.SelectedValue;
                rawArgs.ParentIncomeTax = inputParentIncomeTax.Text;
                rawArgs.ParentUntaxedIncomeAndBenefits = inputParentUntaxedIncomeAndBenefits.Text;
                rawArgs.ParentAdditionalFinancialInfo = inputParentAdditionalFinancialInfo.Text;

                rawArgs.StudentAgi = inputStudentAgi.Text;
                rawArgs.IsStudentTaxFiler = inputStudentTaxFiler.SelectedValue;
                rawArgs.StudentIncomeTax = inputStudentIncomeTax.Text;
                rawArgs.StudentUntaxedIncomeAndBenefits = inputStudentUntaxedIncomeAndBenefits.Text;
                rawArgs.StudentAdditionalFinancialInfo = inputStudentAdditionalFinancialInfo.Text;

                rawArgs.ParentCashSavingsChecking = inputParentCashSavingsChecking.Text;
                rawArgs.ParentInvestmentNetWorth = inputParentInvestmentNetWorth.Text;
                rawArgs.ParentBusinessFarmNetWorth = inputParentBusinessFarmNetWorth.Text;

                rawArgs.StudentCashSavingsChecking = inputStudentCashSavingsChecking.Text;
                rawArgs.StudentInvestmentNetWorth = inputStudentInvestmentNetWorth.Text;
                rawArgs.StudentBusinessFarmNetWorth = inputStudentBusinessFarmNetWorth.Text;

                rawArgs.NumberInHousehold = inputNumberInHousehold.Text;
                rawArgs.NumberInCollege = inputNumberInCollege.Text;

                rawArgs.MonthsOfEnrollment = "9";
                rawArgs.IsQualifiedForSimplified = "false";

                // Validate user input
                AidEstimationValidator validator = new AidEstimationValidator();
                DependentEfcCalculatorArguments args = validator.ValidateDependentEfcCalculatorArguments(rawArgs);

                // If validation fails, display errors
                if (validator.Errors.Any())
                {
                    errorList.DataSource = validator.Errors;
                    errorList.DataBind();
                    return;
                }

                // Calculate
                EfcCalculator calculator = EfcCalculatorConfigurationManager.GetEfcCalculator("1314");
                EfcProfile profile = calculator.GetDependentEfcProfile(args);

                // Display Results
                formPlaceholder.Visible = false;
                resultsPlaceholder.Visible = true;
                studentContributionOutput.Text = profile.StudentContribution.ToString();
                parentContributionOutput.Text = profile.ParentContribution.ToString();
                expectedFamilyContributionOutput.Text = profile.ExpectedFamilyContribution.ToString();
            }
        }
    }
}