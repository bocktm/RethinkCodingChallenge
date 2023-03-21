using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using PatientsAPI.Controllers;
using PatientsAPI.DAL;

namespace PatientsAPI.Tests
{
    public class PatientsControllerTests
    {
        private List<Patient> patients;
        private Mock<PatientsContext> patientsContextMock;
        private PatientsController patientsController;
        private PatientController patientController;

        [SetUp]
        public void Setup()
        {
            patients = new List<Patient>() 
            { 
                new Patient { Id = 1, FirstName = "Clark", LastName = "Kent", Birthday = new DateTime(1984,2,29), Gender = "M" },
                new Patient { Id = 2, FirstName = "Diana", LastName = "Prince", Birthday = new DateTime(1976,3,22), Gender = "F" },
                new Patient { Id = 3, FirstName = "Tony", LastName = "Stark", Birthday = new DateTime(1970,5,29), Gender = "M" },
                new Patient { Id = 4, FirstName = "Carol", LastName = "Denvers", Birthday = new DateTime(1968,4,24), Gender = "F" },
            };

            patientsContextMock = new Mock<PatientsContext>();
            patientsContextMock.Setup(x => x.Patients).ReturnsDbSet(patients);
            patientsController = new PatientsController(patientsContextMock.Object);
            patientController = new PatientController(patientsContextMock.Object);
        }

        [Test]
        public void TestGetAllPatients()
        {
            Assert.That(patientsController.Get().Count(), Is.EqualTo(4));

            Assert.Pass();
        }

        [Test]
        public void TestGetPatientsPaged()
        {
            Assert.That(patientsController.Get(0, 2, null, null).Count(), Is.EqualTo(2));

            Assert.Pass();
        }

        [Test]
        public void TestGetPatientsSortedAndPaged()
        {
            Assert.That(patientsController.Get(0, 2, "firstName", "asc").First().LastName, Is.EqualTo("Denvers"));

            Assert.Pass();
        }

        [Test]
        public void TestGetPatient()
        {
            Assert.That(patientController.Get(1).LastName, Is.EqualTo("Kent"));

            Assert.Pass();
        }
    }
}