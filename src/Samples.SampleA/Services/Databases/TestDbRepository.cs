using BindOpen.Framework.Application.Services;
using BindOpen.Framework.Samples.SampleA.Services.Databases;
using System;

namespace BindOpen.Framework.Samples.SampleA.Services
{
    public class TestDbRepository : BdoDbService
    {
        MyDbModel _model;

        public TestDbRepository(MyDbModel model)
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

            this.UsingConnection(connection =>
            {
                string query1 = Connector.BuildSqlText(_model.GetEmployeeWithCode("codeA"), null, false);
                Console.WriteLine("1- " + query1);

                string query2 = Connector.BuildSqlText(_model.UpdateEmployee("codeB", false, employee));
                Console.WriteLine("2- " + query2);
            });
        }
    }
}