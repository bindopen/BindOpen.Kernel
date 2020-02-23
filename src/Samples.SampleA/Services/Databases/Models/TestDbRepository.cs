using BindOpen.Application.Services;
using BindOpen.Data.Queries;
using BindOpen.Extensions.Connectors;
using BindOpen.System.Diagnostics;
using Samples.SampleA.Services.Databases;
using System;

namespace Samples.SampleA.Services
{
    public class TestDbRepository : BdoDbService
    {
        MyDbModel _model;

        public TestDbRepository(MyDbModel model, IBdoDbConnector connector) : base(connector)
        {
            _model = model;
        }

        public void Test()
        {
            var employee = new EmployeeDto()
            {
                Code = "code1",
                ContactEmail = "email@email.com",
                FisrtName = "firstName",
                LastName = "lastName",
                RegionalDirectorateCode = "FR",
                StaffNumber = "123"
            };

            var log = new BdoLog();
            Connector?.UsingConnection((c, l) =>
            {
                string query1 = Connector.CreateCommandText(_model.GetEmployeeWithCode("codeA"));
                Console.WriteLine("1- " + query1);

                string query2 = Connector.CreateCommandText(_model.UpdateEmployee("codeB", false, employee));
                Console.WriteLine("2- " + query2);

                string query3 = Connector.CreateCommandText(_model.UpsertEmployee(employee));
                Console.WriteLine("3 - " + query3);

                string query4 = Connector.CreateCommandText(_model.DeleteEmployee("codeC"));
                Console.WriteLine("4 - " + query4);

                string query5 = Connector.CreateCommandText(_model.InsertEmployee(employee), DbQueryParameterMode.Symboled);
                Console.WriteLine("5 - " + query5);
            }, log, false);
        }
    }
}