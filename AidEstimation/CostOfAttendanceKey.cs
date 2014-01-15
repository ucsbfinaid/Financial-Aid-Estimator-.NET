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

        public bool Equals(CostOfAttendanceKey otherKey)
        {
            return (otherKey.EducationLevel == EducationLevel && otherKey.HousingOption == HousingOption);
        }

        public override bool Equals(object obj)
        {
            CostOfAttendanceKey otherKey = obj as CostOfAttendanceKey;

            if (otherKey == null)
            {
                return false;
            }

            return Equals(otherKey);
        }

        public override int GetHashCode()
        {
            return ((int) EducationLevel) ^ ((int) HousingOption);
        }
    }
}
