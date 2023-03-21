using CsvHelper.Configuration;

namespace PatientsAPI.Models
{
    public class PatientFromCSV
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Birthday { get; set; }
        public string Gender { get; set; }
    }

    public sealed class PatientFromCSVMap : ClassMap<PatientFromCSV>
    {
        public PatientFromCSVMap()
        {
            Map(m => m.FirstName).Name("First Name");
            Map(m => m.LastName).Name("Last Name");
            Map(m => m.Birthday).Name("Birthday");
            Map(m => m.Gender).Name("Gender");
        }
    }
}
