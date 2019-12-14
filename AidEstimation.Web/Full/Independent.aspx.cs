using System;
using System.Configuration;
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
                EfcCalculator calculator = EfcCalculatorConfigurationManager.GetEfcCalculator("2021");
                EfcProfile profile = calculator.GetIndependentEfcProfile(args);

                // Display Results
                formPlaceholder.Visible = false;
                resultsPlaceholder.Visible = true;
                studentContributionOutput.Text = profile.StudentContribution.ToString("C0");
                expectedFamilyContributionOutput.Text = profile.ExpectedFamilyContribution.ToString("C0");

                CostOfAttendanceEstimator coaEstimator = CostOfAttendanceEstimatorConfigurationManager.GetCostOfAttendanceEstimator("2021");
                CostOfAttendance coa = coaEstimator.GetCostOfAttendance(EducationLevel.Undergraduate, (HousingOption)Enum.Parse(typeof(HousingOption), inputHousing.SelectedValue));

                tuitionFeesOutput.Text = coa.Items[0].Value.ToString("C0");
                roomBoardOutput.Text = coa.Items[1].Value.ToString("C0");
                booksSuppliesOutput.Text = coa.Items[2].Value.ToString("C0");
                otherExpensesOutput.Text = coa.Items[3].Value.ToString("C0");
                healthInsuranceOutput.Text = coa.Items[4].Value.ToString("C0");
                totalCostOutput.Text = coa.Total.ToString("C0");

                grantAwardOutput.Text = "$99,999"; // placeholder
                selfHelpAwardOutput.Text = "$99,999"; // placeholder
                familyHelpAwardOutput.Text = "$99,999"; // placeholder
                estimatedGrantOutput.Text = "$99,999"; // placeholder
                estimatedNetCostOutput.Text = "$99,999"; // placeholder

                percentageGrantOutput.Text = ConfigurationManager.AppSettings["PercentageGrant.Independent.1920"];

            }
            else
            {
                //Enable client-side validators where no default value is defined
                inputStudentAge.Attributes.Add("onblur",
                    "ValidatorValidate(document.getElementById('" + inputStudentAge.ClientID + "').Validators[0]);");
                inputSpouseWorkIncome.Attributes.Add("onblur",
                    "ValidatorValidate(document.getElementById('" + inputSpouseWorkIncome.ClientID + "').Validators[0]);");
                inputStudentWorkIncome.Attributes.Add("onblur",
                    "ValidatorValidate(document.getElementById('" + inputStudentWorkIncome.ClientID + "').Validators[0]);");

                // enable or disable validators linked to radio button lists
                inputStudentWorking.Items[0].Attributes.Add("onclick", "ValidatorEnable(document.getElementById('" + inputStudentWorkIncome.ClientID + "').Validators[0], true); ");
                inputStudentWorking.Items[1].Attributes.Add("onclick", "ValidatorEnable(document.getElementById('" + inputStudentWorkIncome.ClientID + "').Validators[0], false); ");
                inputSpouseWorking.Items[0].Attributes.Add("onclick", "ValidatorEnable(document.getElementById('" + inputSpouseWorkIncome.ClientID + "').Validators[0], true); ");
                inputSpouseWorking.Items[1].Attributes.Add("onclick", "ValidatorEnable(document.getElementById('" + inputSpouseWorkIncome.ClientID + "').Validators[0], false); ");
                inputStudentTaxFiler.Items[0].Attributes.Add("onclick", "ValidatorEnable(document.getElementById('" + inputStudentAgi.ClientID + "').Validators[0], true); ValidatorEnable(document.getElementById('" + inputStudentIncomeTax.ClientID + "').Validators[0], true);");
                inputStudentTaxFiler.Items[1].Attributes.Add("onclick", "ValidatorEnable(document.getElementById('" + inputStudentAgi.ClientID + "').Validators[0], false); ValidatorEnable(document.getElementById('" + inputStudentIncomeTax.ClientID + "').Validators[0], false);");
            }
        }
        protected void inputStudentWorking_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (inputStudentWorking.SelectedValue == "true")
            {
                valStudentWorkIncomeReq.Enabled = true;
                valStudentWorkIncomeRegExp.Enabled = true;
            }
            else
            {
                valStudentWorkIncomeReq.Enabled = false;
                valStudentWorkIncomeRegExp.Enabled = false;
            }

        }

        protected void inputStudentTaxFiler_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (inputStudentTaxFiler.SelectedValue == "true")
            {
                valStudentAgiReq.Enabled = true;
                valStudentAgiRegExp.Enabled = true;
                valStudentIncomeTaxReq.Enabled = true;
                valStudentIncomeTaxRegExp.Enabled = true;
            }
            else
            {
                valStudentAgiReq.Enabled = false;
                valStudentAgiRegExp.Enabled = false;
                valStudentIncomeTaxReq.Enabled = false;
                valStudentIncomeTaxRegExp.Enabled = false;
            }

        }

        protected void inputSpouseWorking_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (inputSpouseWorking.SelectedValue == "true")
            {
                valSpouseWorkIncomeReq.Enabled = true;
                valSpouseWorkIncomeRegExp.Enabled = true;
            }
            else
            {
                valSpouseWorkIncomeReq.Enabled = false;
                valSpouseWorkIncomeRegExp.Enabled = false;
            }
        }
    }
}