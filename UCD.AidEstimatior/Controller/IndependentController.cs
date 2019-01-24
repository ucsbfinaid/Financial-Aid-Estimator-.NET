using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using Ucsb.Sa.FinAid.AidEstimation;
using Ucsb.Sa.FinAid.AidEstimation.EfcCalculation;
using Ucsb.Sa.FinAid.AidEstimation.EfcCalculation.Arguments;
using Ucsb.Sa.FinAid.AidEstimation.Utility;

namespace Web.Controllers
{
    public class IndependentController : Controller
    {
        private AppSettings AppSettings { get; set; }

        public IndependentController(IOptions<AppSettings> settings)
        {
            AppSettings = settings.Value;
        }

        /// <summary>
        /// Home page for independant students
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ViewData["Title"] = "Independent ";
            ViewData["HeaderText"] = "PLACEHOLDER Header Text";
            ViewData["Errors"] = "";
            Dictionary<string, string> FormValues = GetModelDictionary(null, null, "California", "OnCampus", null, null, null, 0, null, 0, null, 0, null, null, null, null, null, null, 0);

            return View("Index", FormValues);
        }

        /// <summary>
        /// Runs the independent estimation
        /// </summary>
        /// <param name="NumberInHousehold"></param>
        /// <param name="NumberInCollege"></param>
        /// <param name="StateOfResidency"></param>
        /// <param name="Housing"></param>
        /// <param name="StudentAge"></param>
        /// <param name="MaritalStatus"></param>
        /// <param name="StudentWorking"></param>
        /// <param name="StudentWorkIncome"></param>
        /// <param name="StudentTaxFiler"></param>
        /// <param name="StudentAgi"></param>
        /// <param name="StudentDependents"></param>
        /// <param name="StudentIncomeTax"></param>
        /// <param name="StudentUntaxedIncomeAndBenefits"></param>
        /// <param name="StudentAdditionalFinancialInfo"></param>
        /// <param name="StudentCashSavingsChecking"></param>
        /// <param name="StudentInvestmentNetWorth"></param>
        /// <param name="StudentBusinessFarmNetWorth"></param>
        /// <param name="SpouseWorking"></param>
        /// <param name="SpouseWorkIncome"></param>
        /// <returns></returns>
        [HttpPost("[controller]/[action]")]
        public IActionResult Estimate(int NumberInHousehold, int NumberInCollege, string StateOfResidency, string Housing, int StudentAge, string MaritalStatus, 
            bool StudentWorking, decimal StudentWorkIncome, bool StudentTaxFiler, decimal StudentAgi, bool StudentDependents,
            decimal StudentIncomeTax, decimal StudentUntaxedIncomeAndBenefits, decimal StudentAdditionalFinancialInfo, decimal StudentCashSavingsChecking, 
            decimal StudentInvestmentNetWorth, decimal StudentBusinessFarmNetWorth, bool SpouseWorking, decimal SpouseWorkIncome)
        {
            ViewData["Title"] = "Independent Estimate ";
            ViewData["EstimateHeaderText"] = "PLACEHOLDER Estimate Header Text";

            // Collect user input
            RawIndependentEfcCalculatorArguments rawArgs = new RawIndependentEfcCalculatorArguments();

            rawArgs.StudentAge = StudentAge.ToString();
            rawArgs.MaritalStatus = MaritalStatus;
            rawArgs.StateOfResidency = StateOfResidency;
            rawArgs.IsStudentWorking = StudentWorking.ToString();
            rawArgs.StudentWorkIncome = StudentWorkIncome.ToString();
            rawArgs.StudentAgi = StudentAgi.ToString();
            rawArgs.IsStudentTaxFiler = StudentTaxFiler.ToString();
            rawArgs.StudentIncomeTax = StudentIncomeTax.ToString();
            rawArgs.StudentUntaxedIncomeAndBenefits = StudentUntaxedIncomeAndBenefits.ToString();
            rawArgs.StudentAdditionalFinancialInfo = StudentAdditionalFinancialInfo.ToString();            
            rawArgs.StudentCashSavingsChecking = StudentCashSavingsChecking.ToString();
            rawArgs.StudentInvestmentNetWorth = StudentInvestmentNetWorth.ToString();
            rawArgs.StudentBusinessFarmNetWorth = StudentBusinessFarmNetWorth.ToString();
            rawArgs.NumberInHousehold = NumberInHousehold.ToString();
            rawArgs.NumberInCollege = NumberInCollege.ToString();
            rawArgs.IsSpouseWorking = SpouseWorking.ToString();
            rawArgs.SpouseWorkIncome = SpouseWorkIncome.ToString();
            rawArgs.HasDependents = StudentDependents.ToString();

            rawArgs.MonthsOfEnrollment = "9";
            rawArgs.IsQualifiedForSimplified = "false";

            // Validate user input
            AidEstimationValidator validator = new AidEstimationValidator();
            IndependentEfcCalculatorArguments args = validator.ValidateIndependentEfcCalculatorArguments(rawArgs);

            // If validation fails, display errors
            if (validator.Errors.Any())
            {
                string errors = "Errors found: <ul>";

                foreach (ValidationError err in validator.Errors)
                {
                    errors += "<li>" + err.Message + "</li>";
                }
                errors += "</ul>";
                ViewData["Errors"] = errors;
                ViewData["HeaderText"] = "PLACEHOLDER Header Text";

                Dictionary<string, string> FormValues = GetModelDictionary(NumberInHousehold, NumberInCollege, StateOfResidency, Housing, StudentAge, MaritalStatus,
                    StudentWorking, StudentWorkIncome, StudentTaxFiler, StudentAgi, StudentDependents, StudentIncomeTax, StudentUntaxedIncomeAndBenefits, StudentAdditionalFinancialInfo, 
                    StudentCashSavingsChecking, StudentInvestmentNetWorth, StudentBusinessFarmNetWorth, SpouseWorking, SpouseWorkIncome);
                return View("Index", FormValues);
            }
            else
            {
                ViewData["Errors"] = "";
                EfcCalculator calculator = EfcCalculatorConfigurationManager.GetEfcCalculator("1920", AppSettings.EfcCalculationConstants);
                EfcProfile profile = calculator.GetIndependentEfcProfile(args);
                CostOfAttendanceEstimator coaEstimator = CostOfAttendanceEstimatorConfigurationManager.GetCostOfAttendanceEstimator("1920", AppSettings.AidEstimationConstants);
                HousingOption ho = (HousingOption)Enum.Parse(typeof(HousingOption), Housing.ToString());
                CostOfAttendance coa = coaEstimator.GetCostOfAttendance(EducationLevel.Undergraduate, ho);

                double grantAward = 0;
                double selfHelp = Math.Max(0, AppSettings.SelfHelpConstant - profile.ExpectedFamilyContribution);
                double maxCosts = profile.ExpectedFamilyContribution + selfHelp + AppSettings.MaxLoanAmount;
                double subCosts = Math.Min(maxCosts, coa.Total);
                string AY = AppSettings.AidYear;

                if (StateOfResidency == "California")
                {
                    grantAward = coa.Total - subCosts;
                    ViewData["ShowOutOfState"] = false;
                }
                else
                {
                    grantAward = GetGrantAmount(AY, profile.ExpectedFamilyContribution, coa.Total + coa.OutOfStateFees);
                    ViewData["ShowOutOfState"] = true;
                }
                
                double parentLoan = Math.Max(0, coa.Total - grantAward - selfHelp);
                double netCosts = coa.Total - grantAward;
                double totalCOA = coa.Total;

                if (StateOfResidency != "California")
                {
                    parentLoan += coa.OutOfStateFees;
                    netCosts += coa.OutOfStateFees;
                    totalCOA += coa.OutOfStateFees;
                }

                Dictionary<string, string> EstimatedAwards = new Dictionary<string, string>();
                EstimatedAwards.Add("GrantAwards", grantAward.ToString("C0"));
                EstimatedAwards.Add("SelfHelp", selfHelp.ToString("C0"));
                EstimatedAwards.Add("ParentLoans", parentLoan.ToString("C0"));
                EstimatedAwards.Add("GrantAwardsPct", AppSettings.PercentageGrantIndependant);
                EstimatedAwards.Add("NetCost", Math.Max(0, netCosts).ToString("C0"));
                EstimatedAwards.Add("OutOfStateFee", coa.OutOfStateFees.ToString("C0"));
                EstimatedAwards.Add("TotalCOA", totalCOA.ToString("C0"));
                
                ViewData["EstimatedAwards"] = EstimatedAwards;

                var tuple = (EfcProfile: profile, CostOfAttendance: coa);
                return View(tuple);

            }

        }

        /// <summary>
        /// Calculates the estimated grant amounts
        /// </summary>
        /// <param name="AY"></param>
        /// <param name="EFC"></param>
        /// <param name="COA"></param>
        /// <returns></returns>
        public double GetGrantAmount(string AY, double EFC, double COA)
        {
            // REPLACE WITH FUNCTION TO CALCULATE Grant Award
            return 0.00;
        }

        /// <summary>
        /// Creates a dictionary object from form parameters
        /// </summary>
        /// <param name="NumberInHousehold"></param>
        /// <param name="NumberInCollege"></param>
        /// <param name="StateOfResidency"></param>
        /// <param name="Housing"></param>
        /// <param name="OldestParentAge"></param>
        /// <param name="MaritalStatus"></param>
        /// <param name="FirstParentWorking"></param>
        /// <param name="FirstParentWorkIncome"></param>
        /// <param name="SecondParentWorking"></param>
        /// <param name="SecondParentWorkIncome"></param>
        /// <param name="ParentsTaxFilers"></param>
        /// <param name="ParentAgi"></param>
        /// <param name="ParentIncomeTax"></param>
        /// <param name="ParentUntaxedIncomeAndBenefits"></param>
        /// <param name="ParentAdditionalFinancialInfo"></param>
        /// <param name="ParentCashSavingsChecking"></param>
        /// <param name="ParentInvestmentNetWorth"></param>
        /// <param name="ParentBusinessFarmNetWorth"></param>
        /// <param name="StudentWorking"></param>
        /// <param name="StudentWorkIncome"></param>
        /// <param name="StudentTaxFiler"></param>
        /// <param name="StudentAgi"></param>
        /// <param name="StudentIncomeTax"></param>
        /// <param name="StudentUntaxedIncomeAndBenefits"></param>
        /// <param name="StudentAdditionalFinancialInfo"></param>
        /// <param name="StudentCashSavingsChecking"></param>
        /// <param name="StudentInvestmentNetWorth"></param>
        /// <param name="StudentBusinessFarmNetWorth"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetModelDictionary(int? NumberInHousehold, int? NumberInCollege, string StateOfResidency, string Housing, int? StudentAge, string MaritalStatus,
            bool? StudentWorking, decimal? StudentWorkIncome, bool? StudentTaxFiler, decimal? StudentAgi, bool? StudentDependents,
            decimal? StudentIncomeTax, decimal? StudentUntaxedIncomeAndBenefits, decimal? StudentAdditionalFinancialInfo, decimal? StudentCashSavingsChecking,
            decimal? StudentInvestmentNetWorth, decimal? StudentBusinessFarmNetWorth, bool? SpouseWorking, decimal? SpouseWorkIncome)
        {
            var FormValues = new Dictionary<string, string>();
            FormValues.Add("NumberInHousehold", NumberInHousehold.ToString());
            FormValues.Add("NumberInCollege", NumberInCollege.ToString());
            FormValues.Add("StateOfResidency", StateOfResidency);
            FormValues.Add("Housing", Housing);
            FormValues.Add("StudentAge", StudentAge.ToString());
            FormValues.Add("MaritalStatus", MaritalStatus);
            FormValues.Add("StudentDependents", StudentDependents.ToString().ToLower());
            FormValues.Add("SpouseWorking", SpouseWorking.ToString().ToLower());
            FormValues.Add("SpouseWorkIncome", SpouseWorkIncome.ToString());
            FormValues.Add("StudentWorking", StudentWorking.ToString().ToLower());
            FormValues.Add("StudentWorkIncome", StudentWorkIncome.ToString());
            FormValues.Add("StudentTaxFiler", StudentTaxFiler.ToString().ToLower());
            FormValues.Add("StudentAgi", StudentAgi.ToString());
            FormValues.Add("StudentIncomeTax", StudentIncomeTax.ToString());
            FormValues.Add("StudentUntaxedIncomeAndBenefits", StudentUntaxedIncomeAndBenefits.ToString());
            FormValues.Add("StudentAdditionalFinancialInfo", StudentAdditionalFinancialInfo.ToString());
            FormValues.Add("StudentCashSavingsChecking", StudentCashSavingsChecking.ToString());
            FormValues.Add("StudentInvestmentNetWorth", StudentInvestmentNetWorth.ToString());
            FormValues.Add("StudentBusinessFarmNetWorth", StudentBusinessFarmNetWorth.ToString());
            return FormValues;
        }

    }
}
