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
                string query1a = Connector.CreateCommandText(_model.GetEmployeeWithCode1("codeA"));
                Console.WriteLine("1a- " + query1a);

                string query1b = Connector.CreateCommandText(_model.GetEmployeeWithCode1("codeA"));
                Console.WriteLine("1b- " + query1b);

                string query2 = Connector.CreateCommandText(_model.UpdateEmployee("codeB", false, employee));
                Console.WriteLine("2- " + query2);

                string query3 = Connector.CreateCommandText(_model.UpsertEmployee(employee));
                Console.WriteLine("3 - " + query3);

                string query4 = Connector.CreateCommandText(_model.DeleteEmployee1("codeC"));
                Console.WriteLine("4 - " + query4);

                string query5 = Connector.CreateCommandText(_model.InsertEmployee(employee), DbQueryParameterMode.Symboled);
                Console.WriteLine("5 - " + query5);

                string query6 = Connector.CreateCommandText(_model.ListEmployees());
                Console.WriteLine("6- " + query6);

                // Insert into ... select ....

                // select tuple

            }, log, false);
        }
    }
}