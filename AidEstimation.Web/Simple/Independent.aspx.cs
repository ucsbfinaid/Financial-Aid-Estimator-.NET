using System;
using System.Linq;
using Ucsb.Sa.FinAid.AidEstimation.EfcCalculation;
using Ucsb.Sa.FinAid.AidEstimation.EfcCalculation.Arguments;
using Ucsb.Sa.FinAid.AidEstimation.Utility;

namespace Ucsb.Sa.FinAid.AidEstimation.Web.Simple
{
    public partial class Independent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                // Collect user input
                RawSimpleIndependentEfcCalculatorArguments rawArgs = new RawSimpleIndependentEfcCalculatorArguments();

                rawArgs.MaritalStatus = inputMaritalStatus.SelectedValue;
                rawArgs.StudentAge = inputStudentAge.Text;
                rawArgs.StateOfResidency = inputStateOfResidency.SelectedValue;
                rawArgs.HasDependents = inputHasDependents.SelectedValue;

                rawArgs.StudentIncome = inputStudentIncome.Text;
                rawArgs.StudentIncomeEarnedBy = inputStudentIncomeEarnedBy.SelectedValue;
                rawArgs.StudentOtherIncome = inputStudentOtherIncome.Text;
                rawArgs.StudentIncomeTax = inputStudentIncomeTax.Text;
                rawArgs.StudentAssets = inputStudentAssets.Text;

                rawArgs.NumberInCollege = inputNumberInCollege.Text;
                rawArgs.NumberInHousehold = inputNumberInHousehold.Text;

                // Validate user input
                AidEstimationValidator validator = new AidEstimationValidator();
                IndependentEfcCalculatorArguments args = validator.ValidateSimpleIndependentEfcCalculatorArguments(rawArgs);

                // If validation fails, display errors
                if (validator.Errors.Any())
                {
                    errorList.DataSource = validator.Errors;
                    errorList.DataBind();
                    return;
                }

                // Calculate
                EfcCalculator calculator = EfcCalculatorConfigurationManager.GetEfcCalculator("2021");
                EfcProfile profile = calculator.GetIndependentEfcProfile(args);

                // Display Results
                formPlaceholder.Visible = false;
                resultsPlaceholder.Visible = true;
                studentContributionOutput.Text = profile.StudentContribution.ToString();
                expectedFamilyContributionOutput.Text = profile.ExpectedFamilyContribution.ToString();
            }
        }
    }
}