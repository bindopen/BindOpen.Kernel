using BindOpen.Application.Scopes;
using BindOpen.Data.Helpers.Serialization;
using BindOpen.Data.Stores;
using BindOpen.Extensions.Connectors;
using BindOpen.System.Diagnostics;
using BindOpen.Tests.Databases.Dtos;
using BindOpen.Tests.Databases.Models;
using NUnit.Framework;
using System;

namespace BindOpen.Tests.Databases.Data.Queries
{
    [TestFixture]
    public class QueriesInsertTest
    {
        TestDbModel _model;
        IBdoDbConnector _dbConnector;
        EmployeeDto _employee;

        [SetUp]
        public void Setup()
        {
            _model = GlobalVariables.AppHost.GetModel<TestDbModel>();
            _dbConnector = GlobalVariables.AppHost.CreatePostgreSqlConnector();

            _employee = new EmployeeDto()
            {
                Code = "code1",
                ContactEmail = "email@email.com",
                FisrtName = "firstName",
                LastName = "lastName",
                RegionalDirectorateCode = "FR",
                StaffNumber = "123"
            };
        }

        [Test]
        public void SimpleInsert1()
        {
            var log = new BdoLog();

            string expectedResult = @"insert into ""Mdm"".""Employee"" (""Code"",""ContactEmail"",""FisrtName"",""LastName"",""StaffNumber"") values ('code1','email@email.com','firstName','lastName','123') returning ""Mdm"".""Employee"".""EmployeeId""";

            string result = _dbConnector.CreateCommandText(_model.InsertEmployee1(_employee));

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = log.ToXml();
            }
            Assert.That(result.Equals(expectedResult, StringComparison.OrdinalIgnoreCase), "Bad script interpretation. Result was '" + xml);
        }
    }
}
