using BindOpen.Application.Services;
using BindOpen.Extensions.Runtime;
using BindOpen.Samples.SampleA.Services.Databases;
using System;

namespace BindOpen.Samples.SampleA.Services
{
    public class TestDbRepository : BdoDbService
    {
        MyDbModel _model;

        public TestDbRepository(MyDbModel model, IBdoConnector connector) : base(connector)
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

            this.UsingDbConnection(connection =>
            {
                string query1 = Connector.BuildSqlText(_model.GetEmployeeWithCode("codeA"));
                Console.WriteLine("1- " + query1);

                string query2 = Connector.BuildSqlText(_model.UpdateEmployee("codeB", false, employee));
                Console.WriteLine("2- " + query2);

                string query3 = Connector.BuildSqlText(_model.UpsertEmployee(employee));
                Console.WriteLine("3 - " + query3);
            });
        }
    }
}