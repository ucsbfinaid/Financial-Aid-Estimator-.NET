namespace Ucsb.Sa.FinAid.AidEstimation
{
    public class CostOfAttendanceKey
    {
        public EducationLevel EducationLevel { get; set; }
        public HousingOption HousingOption { get; set; }

        public CostOfAttendanceKey(EducationLevel educationLevel, HousingOption housingOption)
        {
            EducationLevel = educationLevel;
            HousingOption = housingOption;
        }
    }
}
