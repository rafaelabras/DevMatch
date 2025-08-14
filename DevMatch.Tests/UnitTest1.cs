using DevMatch.Controllers;
using DevMatch.Dtos.User;
using DevMatch.Interfaces;
using DevMatch.Models;
using DevMatch.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevMatch.Tests;

[TestFixture]
public class UserTests
{

    private Mock<ITokenService> tokenServiceMock;


    [SetUp]
    public void Setup()
    {

        tokenServiceMock = new Mock<ITokenService>();

    } 
    [Test] 
    public async Task RegisterTest()
    {

        User user = new User
        {
            Id = "1",
            UserName = "testuser",
            Email = "teste@gmail.com"
        };


        var token = tokenServiceMock.Setup(x => x.GenerateToken(It.IsAny<User>()))
   .ReturnsAsync("fake_token");

        var teste = tokenServiceMock.Object.GenerateToken(user);

        Assert.That(teste.Result, Is.EqualTo("fake_token"));

    }

}


