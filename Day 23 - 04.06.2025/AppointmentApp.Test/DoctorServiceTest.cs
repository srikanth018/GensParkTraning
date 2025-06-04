using NUnit.Framework;
using Moq;
using AppointmentApp.Services;
using AppointmentApp.Interfaces;
using AppointmentApp.Models;
using AppointmentApp.Models.DTOs;
using AutoMapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;

namespace AppointmentApp.Test
{
    public class DoctorServiceTests
    {
        private Mock<IRepository<int, Doctor>> _mockDoctorRepo;
        private Mock<IRepository<int, Speciality>> _mockSpecialityRepo;
        private Mock<IRepository<int, DoctorSpeciality>> _mockDoctorSpecialityRepo;
        private Mock<IRepository<string, User>> _mockUserRepo;
        private Mock<IMapper> _mockMapper;
        private Mock<IOtherContextFunctionities> _mockOtherContextFunc;

        private DoctorService _doctorService;

        [SetUp]
        public void Setup()
        {
            _mockDoctorRepo = new Mock<IRepository<int, Doctor>>();
            _mockSpecialityRepo = new Mock<IRepository<int, Speciality>>();
            _mockDoctorSpecialityRepo = new Mock<IRepository<int, DoctorSpeciality>>();
            _mockUserRepo = new Mock<IRepository<string, User>>();
            _mockMapper = new Mock<IMapper>();
            _mockOtherContextFunc = new Mock<IOtherContextFunctionities>();

            _doctorService = new DoctorService(
                _mockDoctorRepo.Object,
                _mockSpecialityRepo.Object,
                _mockDoctorSpecialityRepo.Object,
                _mockOtherContextFunc.Object,
                _mockMapper.Object,
                _mockUserRepo.Object);
        }

        [Test]
        public async Task Add_WhenCalled_AddsUserAndDoctor()
        {
            // Arrange
            var addRequest = new AddDoctorRequest
            {
                Name = "Test Doctor",
                Password = "pass123",
                PhoneNumber = "12345",
                Specialities = new List<AddSpecialityRequest>()
            };

            var fakeUser = new User { Username = "Test Doctor", Password = "hashedpass", Role = "Doctor" };
            var fakeDoctor = new Doctor { Id = 1, Name = "Test Doctor" };

            _mockMapper
                .Setup(m => m.Map<AddDoctorRequest, User>(It.IsAny<AddDoctorRequest>()))
                .Returns(fakeUser);

            _mockUserRepo
                .Setup(r => r.Add(It.IsAny<User>()))
                .ReturnsAsync(fakeUser);

            
            _mockDoctorRepo
                .Setup(r => r.Add(It.IsAny<Doctor>()))
                .ReturnsAsync(fakeDoctor);

            // Act
            var result = await _doctorService.Add(addRequest);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(fakeDoctor.Id, result.Id);
            Assert.AreEqual(fakeDoctor.Name, result.Name);

            _mockUserRepo.Verify(r => r.Add(It.IsAny<User>()), Times.Once);
            _mockDoctorRepo.Verify(r => r.Add(It.IsAny<Doctor>()), Times.Once);
        }

        [Test]
        public void GetById_InvalidId_ThrowsArgumentException()
        {
            Assert.ThrowsAsync<ArgumentException>(() => _doctorService.GetById(0));
            Assert.ThrowsAsync<ArgumentException>(() => _doctorService.GetById(-1));
        }

        [Test]
        public async Task GetByName_WhenCalled_ReturnsFilteredDoctors()
        {
            // Arrange
            var doctors = new List<Doctor>
            {
                new Doctor { Id = 1, Name = "John Smith" },
                new Doctor { Id = 2, Name = "Jane Doe" },
                new Doctor { Id = 3, Name = "Johnny Appleseed" }
            }.AsEnumerable();

            _mockDoctorRepo.Setup(r => r.GetAll()).ReturnsAsync(doctors);

            // Act
            var result = await _doctorService.GetByName("John");

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.IsTrue(result.All(d => d.Name.Contains("John", StringComparison.OrdinalIgnoreCase)));
        }
    }
}
