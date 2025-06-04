using NUnit.Framework;
using Moq;
using AppointmentApp.Services;
using AppointmentApp.Interfaces;
using AppointmentApp.Models;
using AppointmentApp.Models.DTOs;
using AutoMapper;
using System.Threading.Tasks;
using System;

namespace AppointmentApp.Test
{
    public class PatientServiceTests
    {
        private Mock<IRepository<int, Patient>> _mockPatientRepo;
        private Mock<IRepository<string, User>> _mockUserRepo;
        private Mock<IMapper> _mockMapper;
        private PatientService _patientService;

        [SetUp]
        public void Setup()
        {
            _mockPatientRepo = new Mock<IRepository<int, Patient>>();
            _mockUserRepo = new Mock<IRepository<string, User>>();
            _mockMapper = new Mock<IMapper>();

            _patientService = new PatientService(_mockPatientRepo.Object, null, _mockMapper.Object, _mockUserRepo.Object);
        }

        [Test]
        public async Task Add_WhenCalled_AddsUserAndPatient()
        {
            // Arrange
            var patientRequest = new AddPatientRequest
            {
                Name = "Patient Test",
                Password = "patientpass",
                PhoneNumber = "98765",
                Email = "patient@test.com"
            };

            var fakeUser = new User { Username = patientRequest.Email, Password = "hashedpass", Role = "Patient" };
            var fakePatient = new Patient { Id = 1, Name = patientRequest.Name, Email = patientRequest.Email };

            _mockMapper
                .Setup(m => m.Map<AddPatientRequest, User>(It.IsAny<AddPatientRequest>()))
                .Returns(fakeUser);

            _mockUserRepo
                .Setup(r => r.Add(It.IsAny<User>()))
                .ReturnsAsync(fakeUser);

            _mockPatientRepo
                .Setup(r => r.Add(It.IsAny<Patient>()))
                .ReturnsAsync(fakePatient);

            // Act
            var result = await _patientService.Add(patientRequest);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(fakePatient.Id, result.Id);
            Assert.AreEqual(fakePatient.Name, result.Name);
            Assert.AreEqual(fakePatient.Email, result.Email);

            _mockUserRepo.Verify(r => r.Add(It.IsAny<User>()), Times.Once);
            _mockPatientRepo.Verify(r => r.Add(It.IsAny<Patient>()), Times.Once);
        }

        [Test]
        public void Add_NullRequest_ThrowsArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _patientService.Add(null));
        }
    }
}
