using System;
using System.Linq;
using Ucsb.Sa.FinAid.AidEstimation.EfcCalculation;
using Ucsb.Sa.FinAid.AidEstimation.EfcCalculation.Arguments;
using Ucsb.Sa.FinAid.AidEstimation.Utility;

namespace Ucsb.Sa.FinAid.AidEstimation.Web.Full
{
    public partial class Independent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                // Collect user input
                RawIndependentEfcCalculatorArguments rawArgs = new RawIndependentEfcCalculatorArguments();

                rawArgs.StudentAge = inputStudentAge.Text;
                rawArgs.MaritalStatus = inputMaritalStatus.SelectedValue;
                rawArgs.StateOfResidency = inputStateOfResidency.SelectedValue;

                rawArgs.IsStudentWorking = inputStudentWorking.SelectedValue;
                rawArgs.StudentWorkIncome = inputStudentWorkIncome.Text;
                rawArgs.IsSpouseWorking = inputSpouseWorking.SelectedValue;
                rawArgs.SpouseWorkIncome = inputSpouseWorkIncome.Text;

                rawArgs.StudentAgi = inputStudentAgi.Text;
                rawArgs.IsStudentTaxFiler = inputStudentTaxFiler.SelectedValue;
                rawArgs.StudentIncomeTax = inputStudentIncomeTax.Text;
                rawArgs.StudentUntaxedIncomeAndBenefits = inputStudentUntaxedIncomeAndBenefits.Text;
                rawArgs.StudentAdditionalFinancialInfo = inputStudentAdditionalFinancialInfo.Text;

                rawArgs.StudentCashSavingsChecking = inputStudentCashSavingsChecking.Text;
                rawArgs.StudentInvestmentNetWorth = inputStudentInvestmentNetWorth.Text;
                rawArgs.StudentBusinessFarmNetWorth = inputStudentBusinessFarmNetWorth.Text;

                rawArgs.HasDependents = inputHasDependents.SelectedValue;
                rawArgs.NumberInHousehold = inputNumberInHousehold.Text;
                rawArgs.NumberInCollege = inputNumberInCollege.Text;

                rawArgs.MonthsOfEnrollment = "9";
                rawArgs.IsQualifiedForSimplified = "false";

                // Validate user input
                AidEstimationValidator validator = new AidEstimationValidator();
                IndependentEfcCalculatorArguments args = validator.ValidateIndependentEfcCalculatorArguments(rawArgs);

                // If validation fails, display errors
                if (validator.Errors.Any())
                {
                    errorList.DataSource = validator.Errors;
                    errorList.DataBind();
                    return;
                }

                // Calculate
                EfcCalculator calculator = EfcCalculatorConfigurationManager.GetEfcCalculator("1415");
                EfcProfile profile = calculator.GetIndependentEfcProfile(args);

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