using System.Collections.Generic;
using System.IO;
using BindOpen.Framework.Core.Data.Helpers.Serialization;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Diagnostics.Dto;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NUnit.Framework;

namespace BindOpen.Framework.Tests.UnitTest.System.Diagnostics
{
    [TestFixture, Order(21)]
    public class ApiLogDtoTest
    {
        private readonly string _filePath = SetupVariables.WorkingFolder + "Log.json";

        private const int _itemsNumber = 180;

        private ApiLogDto _apiResultLog = null;

        public void CreateApiLogDto()
        {
            ILog log = new Log();

            for (int i = 0; i < _itemsNumber; i++)
                log.AddError("Error" + i);
            for (int i = 0; i < _itemsNumber; i++)
                log.AddException("Exception" + i);
            for (int i = 0; i < _itemsNumber; i++)
                log.AddMessage("Message" + i);
            for (int i = 0; i < _itemsNumber; i++)
                log.AddWarning("Warning" + i);
            for (int i = 0; i < _itemsNumber; i++)
                log.AddSubLog(new Log());

            _apiResultLog = log.ToApiDto();
        }

        [Test, Order(1)]
        public void TestCreateLogDto()
        {
            if (_apiResultLog == null)
            {
                CreateApiLogDto();
            }

            Assert.That(_apiResultLog.Events.Count == _itemsNumber * 5, "Bad insertion of errors ({0} expected; {1} found)", _itemsNumber, _apiResultLog.Errors.Count);
        }

        [Test, Order(2)]
        public void TestSaveEvents()
        {
            if (_apiResultLog==null)
            {
                CreateApiLogDto();
            }

            _apiResultLog?.SaveJson(_filePath);
        }

        [Test, Order(3)]
        public void TestLoadEvents()
        {
            if (!File.Exists(_filePath))
                TestSaveEvents();

            Log log = new Log();
            _apiResultLog = JsonHelper.Load<Log>(_filePath, null, null, log);

            Assert.That(!log.HasErrorsOrExceptions(), "Error while loading log. Result was '" + log.ToXml());

            TestCreateLogDto();
        }

    }
}
