namespace AppointmentApp.Test;

using Microsoft.EntityFrameworkCore;
using AppointmentApp.Models;
using AppointmentApp.Repositories;
using AppointmentApp.Interfaces;
using AppointmentApp.Contexts;
using Moq;
using AutoMapper;
using AppointmentApp.Misc;
using AppointmentApp.Services;
using AppointmentApp.Models.DTOs;

public class DoctorServiceTest
{
    private ClinicContext _context;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<ClinicContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new ClinicContext(options);

    }

    public async Task TestGetDoctorBySpeciality(string speciality)
    {
        Mock<DoctorRepository> doctorRepositoryMock = new(_context);
        Mock<SpecialityRepository> specialityRepositoryMock = new(_context);
        Mock<DoctorSpecialityRepository> doctorSpecialityRepositoryMock = new(_context);
        Mock<OtherFuncinalitiesImplementation> otherContextFunctionitiesMock = new();
        Mock<IMapper> mapperMock = new();
        Mock<UserRepository> userRepositoryMock = new(_context);

        otherContextFunctionitiesMock.Setup(ocf => ocf.GetDoctorsBySpeciality(It.IsAny<string>()))
                                     .ReturnsAsync((string speciality) => new List<DoctorsBySpecialityResponseDto>
                                     {
                                        new DoctorsBySpecialityResponseDto{
                                            Id = 1,
                                            Dname = "test",
                                            Yoe = 2
                                        }
                                     });

        IDoctorService doctorService = new DoctorService(
            doctorRepositoryMock.Object,
            specialityRepositoryMock.Object,
            doctorSpecialityRepositoryMock.Object,
            otherContextFunctionitiesMock.Object,
            mapperMock.Object,
            userRepositoryMock.Object
        );

        // Action
        var result = await doctorService.GetBySpeciality(speciality);
        Assert.That(result.Count(), Is.EqualTo(1));
    }


    [TearDown]
    public void TearDown()
    {
        _context?.Dispose();
    }
}