using System;
using System.Linq;
using Ucsb.Sa.FinAid.AidEstimation.EfcCalculation;
using Ucsb.Sa.FinAid.AidEstimation.EfcCalculation.Arguments;
using Ucsb.Sa.FinAid.AidEstimation.Utility;

namespace Ucsb.Sa.FinAid.AidEstimation.Web.Simple
{
    public partial class Dependent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                // Collect user input
                RawSimpleDependentEfcCalculatorArguments rawArgs = new RawSimpleDependentEfcCalculatorArguments();

                rawArgs.MaritalStatus = inputMaritalStatus.SelectedValue;
                rawArgs.StateOfResidency = inputStateOfResidency.SelectedValue;

                rawArgs.ParentIncome = inputParentIncome.Text;
                rawArgs.ParentOtherIncome = inputParentOtherIncome.Text;
                rawArgs.ParentIncomeEarnedBy = inputParentIncomeEarnedBy.SelectedValue;
                rawArgs.ParentIncomeTax = inputParentIncomeTax.Text;
                rawArgs.ParentAssets = inputParentAssets.Text;

                rawArgs.StudentIncome = inputStudentIncome.Text;
                rawArgs.StudentOtherIncome = inputStudentOtherIncome.Text;
                rawArgs.StudentIncomeTax = inputStudentIncomeTax.Text;
                rawArgs.StudentAssets = inputStudentAssets.Text;

                rawArgs.NumberInCollege = inputNumberInCollege.Text;
                rawArgs.NumberInHousehold = inputNumberInHousehold.Text;

                // Validate user input
                AidEstimationValidator validator = new AidEstimationValidator();
                DependentEfcCalculatorArguments args = validator.ValidateSimpleDependentEfcCalculatorArguments(rawArgs);

                // If validation fails, display errors
                if (validator.Errors.Any())
                {
                    errorList.DataSource = validator.Errors;
                    errorList.DataBind();
                    return;
                }

                // Calculate
                EfcCalculator calculator = EfcCalculatorConfigurationManager.GetEfcCalculator("2021");
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