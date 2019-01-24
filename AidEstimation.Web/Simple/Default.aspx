<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Ucsb.Sa.FinAid.AidEstimation.Web.Simple.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    
    <h3>Welcome to the Financial Aid Estimator!</h3>

    <p>
        The Financial Aid Estimator provides an estimated Financial Aid Award Letter for prospective students.
        The estimated values produced by this tool are <strong>not</strong>
        the actual amounts that will be offered in your final Financial Aid Award Letter.
        All estimated values are <strong>subject to the availability of funding</strong>.
        To begin the actual Financial Aid application process, complete a <a href="http://www.fafsa.ed.gov/">FAFSA</a>.
    </p>
        
    <p>
        This estimator only produces <strong>estimated values</strong> based on the information you provide. If you provide
        incorrect information, the resulting estimated values may differ significantly from your final Financial Aid award
        letter. Furthermore, your final Financial Aid award letter can be affected by a number of factors:
    </p>
        
    <ul>
        <li>The <strong>number of units</strong> you complete during an academic quarter</li>
        <li><strong>Private scholarships</strong> from external agencies</li>
        <li><strong>Specific requirements</strong> to each type of aid</li>
    </ul>
        
    <p>
        This tool is only intended for <strong>full-time, undergraduate students</strong>. <strong>Graduate students</strong>
        should contact the Financial Aid office or their department directly for more information.
    </p>
        
    <p>
        Please choose either the <strong>Dependent</strong> or <strong>Independent</strong> estimator to continue:
    </p>
        
    <ul class="dependency-choice">
        <li class="button-wrapper">
            <a href="Dependent.aspx" class="button">
                Dependent Estimator
                <span>I am <strong>less</strong> than 24 years old</span>
            </a>
        </li>
        <li class="button-wrapper">
            <a href="Independent.aspx" class="button">
                Independent Estimator
                <span>I am <strong>at least</strong> 24 years old</span>
            </a>
        </li>
    </ul>

    <p>
        Students who are less than 24 years old may still qualify for Independent status. For more information,
        <a href="https://studentaid.ed.gov/sa/fafsa/filling-out/dependency#dependency-questions" target="_blank">view the Dependency Status criteria</a>.
    </p>

</asp:Content>
