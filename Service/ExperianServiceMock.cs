using System;

namespace CreditOne.Batch.ExperianPWUpdater.Service
{
    class ExperianServiceMock
    {
        public string GetNewPassword()
        {
            var randomGuid = Guid.NewGuid();
            return "TestPassword" + randomGuid.ToString().Substring(0,2);
        }
    }
}
