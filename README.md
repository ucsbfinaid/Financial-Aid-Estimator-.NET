# Financial Aid Estimator (.NET)

Open-source .NET Financial Aid Estimator, including an implementation of the Federal Expected Family Contribution
(EFC) formula.

## Getting Started

To begin using the EFC Calculator from your web application, complete the following steps:

1. Reference `Ucsb.Sa.FinAid.AidEstimation.EfcCalculation.dll`
2. Reference `Ucsb.Sa.FinAid.AidEstimation.Utility.dll`
3. Place `EfcCalculationConstants.1314.xml` in `App_Data`
3. Add the following to the `appSettings` section in your Web.config:

```xml
<add key="EfcCalculation.Constants.1314" value="~/App_Data/EfcCalculationConstants.1314.xml"/>
```

You can now use the EFC calculator:

```csharp
EfcCalculator calculator = EfcCalculatorConfigurationManager.GetEfcCalculator("1314");

DependentEfcCalculatorArguments arguments = new DependentEfcCalculatorArguments();

arguments.MonthsOfEnrollment = 9;
arguments.NumberInHousehold = 2;
arguments.NumberInCollege = 1;

HouseholdMember mother = new HouseholdMember();
mother.IsWorking = true;
mother.WorkIncome = 123000;
arguments.FirstParent = mother;

HouseholdMember student = new HouseholdMember();
student.IsWorking = true;
student.WorkIncome = 12000;
arguments.Student = student;

EfcProfile profile = calculator.GetDependentEfcProfile(arguments);
Response.Write(profile.ExpectedFamilyContribution);
```

## Copyright

Copyright © 2013. The Regents of the University of California. All rights reserved.

## License

Licensed pursuant to the terms and conditions available for viewing at: http://opensource.org/licenses/BSD-3-Clause

## Disclaimer

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES,
INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY,
WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

By using this template, you acknowledge that you have read and agree to these terms and conditions as well as the
terms and conditions specified in the "License" section above.