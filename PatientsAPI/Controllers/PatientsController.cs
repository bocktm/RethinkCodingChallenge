using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientsAPI.DAL;
using PatientsAPI.Models;
using System.Formats.Asn1;
using System.Globalization;
using CsvHelper;
using System.Reflection.PortableExecutable;

namespace PatientsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly PatientsContext _patientsDB;

        public PatientsController(PatientsContext patientsDb)
        {
            _patientsDB = patientsDb;
        }

        [HttpGet]
        public IEnumerable<Patient> Get()
        {
            return _patientsDB.Patients.ToList();
        }

        [Route("{skip}/{take}")]
        [Route("{skip}/{take}/{sortBy}")]
        [Route("{skip}/{take}/{sortBy}/{sortDir}")]
        [HttpGet]
        public IEnumerable<Patient> Get(int skip, int take, string? sortBy, string? sortDir)
        {
            if (sortBy == null)
                return _patientsDB.Patients.Skip(skip).Take(take).ToList();
            
            var isDesc = sortDir == "desc" ? true : false;

            switch (sortBy)
            {
                case "firstName":
                    return isDesc ? _patientsDB.Patients.OrderByDescending(o => o.FirstName).Skip(skip).Take(take).ToList() : _patientsDB.Patients.OrderBy(o => o.FirstName).Skip(skip).Take(take).ToList();
                case "lastName":
                    return isDesc ? _patientsDB.Patients.OrderByDescending(o => o.LastName).Skip(skip).Take(take).ToList() : _patientsDB.Patients.OrderBy(o => o.LastName).Skip(skip).Take(take).ToList();
                case "birthday":
                    return isDesc ? _patientsDB.Patients.OrderByDescending(o => o.Birthday).Skip(skip).Take(take).ToList() : _patientsDB.Patients.OrderBy(o => o.Birthday).Skip(skip).Take(take).ToList();
                case "gender":
                    return isDesc ? _patientsDB.Patients.OrderByDescending(o => o.Gender).Skip(skip).Take(take).ToList() : _patientsDB.Patients.OrderBy(o => o.Gender).Skip(skip).Take(take).ToList();
            }

            return _patientsDB.Patients.Skip(skip).Take(take).ToList();
        }

        [HttpPost]
        public void Post()
        {
            var employeeCSVFile = Request.Form.Files[0];
            UploadPatients(employeeCSVFile);
        }

        [HttpPut]
        public void Put()
        {
            var employeeCSVFile = Request.Form.Files[0];

            Delete();

            UploadPatients(employeeCSVFile);
        }

        private void UploadPatients(IFormFile employeeCSVFile)
        {
            List<PatientFromCSV> patients = null;

            using (var stream = new MemoryStream())
            {
                using (var reader = new StreamReader(employeeCSVFile.OpenReadStream()))
                {
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        csv.Context.RegisterClassMap<PatientFromCSVMap>();
                        patients = csv.GetRecords<PatientFromCSV>().ToList();
                    }
                }
            }

            foreach (var patient in patients)
            {
                _patientsDB.Patients.Add(new Patient { FirstName = patient.FirstName, LastName = patient.LastName, Birthday = DateTime.Parse(patient.Birthday), Gender = patient.Gender });
            }
            _patientsDB.SaveChanges();
        }

        [HttpDelete]
        public void Delete()
        {
            foreach (var patient in _patientsDB.Patients) { _patientsDB.Patients.Remove(patient); }

            _patientsDB.SaveChanges();
        }
    }
}
