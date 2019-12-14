using System;
using System.Linq;
using Ucsb.Sa.FinAid.AidEstimation.EfcCalculation;
using Ucsb.Sa.FinAid.AidEstimation.EfcCalculation.Arguments;
using Ucsb.Sa.FinAid.AidEstimation.Utility;
using System.Configuration;

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
                    validationSummary.Visible = false;
                    return;
                }
                else
                {
                    validationSummary.Visible = true;
                }

                // Calculate
                EfcCalculator calculator = EfcCalculatorConfigurationManager.GetEfcCalculator("2021");
                EfcProfile profile = calculator.GetDependentEfcProfile(args);

                // Display Results
                formPlaceholder.Visible = false;
                resultsPlaceholder.Visible = true;
                studentContributionOutput.Text = profile.StudentContribution.ToString("C0");
                parentContributionOutput.Text = profile.ParentContribution.ToString("C0");
                expectedFamilyContributionOutput.Text = profile.ExpectedFamilyContribution.ToString("C0");

                CostOfAttendanceEstimator coaEstimator = CostOfAttendanceEstimatorConfigurationManager.GetCostOfAttendanceEstimator("2021");
                CostOfAttendance coa = coaEstimator.GetCostOfAttendance(EducationLevel.Undergraduate, (HousingOption) Enum.Parse(typeof(HousingOption), inputHousing.SelectedValue));
                                
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

                percentageGrantOutput.Text = ConfigurationManager.AppSettings["PercentageGrant.Dependent.1920"];
            }
            else
            {
                //Enable client-side validators where no default value is defined
                inputOldestParentAge.Attributes.Add("onblur",
                    "ValidatorValidate(document.getElementById('" + inputOldestParentAge.ClientID + "').Validators[0]);");
                inputFirstParentWorkIncome.Attributes.Add("onblur",
                    "ValidatorValidate(document.getElementById('" + inputFirstParentWorkIncome.ClientID + "').Validators[0]);");
                inputSecondParentWorkIncome.Attributes.Add("onblur",
                    "ValidatorValidate(document.getElementById('" + inputSecondParentWorkIncome.ClientID + "').Validators[0]);");
                inputStudentWorkIncome.Attributes.Add("onblur",
                    "ValidatorValidate(document.getElementById('" + inputStudentWorkIncome.ClientID + "').Validators[0]);");

                // enable or disable validators linked to radio button lists
                inputFirstParentWorking.Items[0].Attributes.Add("onclick", "ValidatorEnable(document.getElementById('" + inputFirstParentWorkIncome.ClientID + "').Validators[0], true); ");
                inputFirstParentWorking.Items[1].Attributes.Add("onclick", "ValidatorEnable(document.getElementById('" + inputFirstParentWorkIncome.ClientID + "').Validators[0], false); ");
                inputSecondParentWorking.Items[0].Attributes.Add("onclick", "ValidatorEnable(document.getElementById('" + inputSecondParentWorkIncome.ClientID + "').Validators[0], true); ");
                inputSecondParentWorking.Items[1].Attributes.Add("onclick", "ValidatorEnable(document.getElementById('" + inputSecondParentWorkIncome.ClientID + "').Validators[0], false); ");
                inputStudentWorking.Items[0].Attributes.Add("onclick", "ValidatorEnable(document.getElementById('" + inputStudentWorkIncome.ClientID + "').Validators[0], true); ");
                inputStudentWorking.Items[1].Attributes.Add("onclick", "ValidatorEnable(document.getElementById('" + inputStudentWorkIncome.ClientID + "').Validators[0], false); ");
                inputAreParentsTaxFilers.Items[0].Attributes.Add("onclick", "ValidatorEnable(document.getElementById('" + inputParentIncomeTax.ClientID + "').Validators[0], true); ValidatorEnable(document.getElementById('" + inputParentAgi.ClientID + "').Validators[0], true);");
                inputAreParentsTaxFilers.Items[1].Attributes.Add("onclick", "ValidatorEnable(document.getElementById('" + inputParentIncomeTax.ClientID + "').Validators[0], false); ValidatorEnable(document.getElementById('" + inputParentAgi.ClientID + "').Validators[0], false);");
                inputStudentTaxFiler.Items[0].Attributes.Add("onclick", "ValidatorEnable(document.getElementById('" + inputStudentAgi.ClientID + "').Validators[0], true); ValidatorEnable(document.getElementById('" + inputStudentIncomeTax.ClientID + "').Validators[0], true);");
                inputStudentTaxFiler.Items[1].Attributes.Add("onclick", "ValidatorEnable(document.getElementById('" + inputStudentAgi.ClientID + "').Validators[0], false); ValidatorEnable(document.getElementById('" + inputStudentIncomeTax.ClientID + "').Validators[0], false);");
            }
        }

        protected void inputFirstParentWorking_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (inputFirstParentWorking.SelectedValue=="true")
            {
                valFirstParentWorkIncomeReq.Enabled = true;
                valFirstParentWorkIncomeRegExp.Enabled = true;
            }
            else
            {
                valFirstParentWorkIncomeReq.Enabled = false;
                valFirstParentWorkIncomeRegExp.Enabled = false;
            }
        }

        protected void inputSecondParentWorking_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (inputSecondParentWorking.SelectedValue == "true")
            {
                valSecondParentWorkIncomeReq.Enabled = true;
                valSecondParentWorkIncomeRegExp.Enabled = true;
            }
            else
            {
                valSecondParentWorkIncomeReq.Enabled = false;
                valSecondParentWorkIncomeRegExp.Enabled = false;
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

        protected void inputAreParentsTaxFilers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (inputAreParentsTaxFilers.SelectedValue == "true")
            {
                valParentAgiReq.Enabled = true;
                valParentAgiRegExp.Enabled = true;
                valParentIncomeTaxReq.Enabled = true;
                valParentIncomeTaxRegExp.Enabled = true;
            }
            else
            {
                valParentAgiReq.Enabled = false;
                valParentAgiRegExp.Enabled = false;
                valParentIncomeTaxReq.Enabled = false;
                valParentIncomeTaxRegExp.Enabled = false;
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
    }
}