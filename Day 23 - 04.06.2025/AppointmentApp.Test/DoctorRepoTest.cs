namespace AppointmentApp.Test;

using Microsoft.EntityFrameworkCore;
using AppointmentApp.Models;
using AppointmentApp.Repositories;
using AppointmentApp.Interfaces;
using AppointmentApp.Contexts;

public class Tests
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

    [Test]
    // public async Task AddDoctorTest()
    // {
    //     // arrange
    //     var email = " test@gmail.com";
    //     var password = System.Text.Encoding.UTF8.GetBytes("test123").ToString();
    //     var user = new User
    //     {
    //         Username = email,
    //         Password = password,
    //         Role = "Doctor"
    //     };
    //     _context.Add(user);
    //     await _context.SaveChangesAsync();
    //     var doctor = new Doctor
    //     {
    //         Name = "test",
    //         YearsOfExperience = 2,
    //         Email = email
    //     };
    //     IRepository<int, Doctor> _doctorRepository = new DoctorRepository(_context);
    //     //action
    //     var result = await _doctorRepository.Add(doctor);
    //     //assert
    //     Assert.That(result, Is.Not.Null, "Doctor IS not addeed");
    //     Assert.That(result.Id, Is.EqualTo(2));
    // }

    [TestCase(1)]
    public async Task GetDoctor(int id)
    {
        // arrange
        var email = " test@gmail.com";
        var password = System.Text.Encoding.UTF8.GetBytes("test123").ToString();
        var user = new User
        {
            Username = email,
            Password = password,
            Role = "Doctor"
        };
        _context.Add(user);
        await _context.SaveChangesAsync();
        var doctor = new Doctor
        {
            Name = "test",
            YearsOfExperience = 2,
            Email = email
        };
        IRepository<int, Doctor> _doctorRepository = new DoctorRepository(_context);
         await _doctorRepository.Add(doctor);
        // action
        var result = await _doctorRepository.GetById(id);
        // Assert
        Assert.That(result.Id, Is.EqualTo(id));
    }

    

    [TearDown]
    public void Cleanup()
    {
        _context.Dispose();
    }
}