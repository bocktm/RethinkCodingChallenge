using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientsAPI.DAL;

namespace PatientsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly PatientsContext _patientsDB;

        public PatientController(PatientsContext patientsDb)
        {
            _patientsDB = patientsDb;
        }

        [Route("{id}")]
        [HttpGet]
        public Patient? Get(int id)
        {
            return _patientsDB.Patients.Where(p => p.Id == id).First();
        }

        [Route("{id}")]
        [HttpPost]
        public void Post(int id, [FromBody] Patient patient)
        {
            _patientsDB.Patients.Update(patient);
            _patientsDB.SaveChanges();
        }
    }
}
