using CreditOne.Framework.Batch;

namespace CreditOne.Batch.ExperianPWUpdater.Utils
{
	public static class ParameterValuesUtil
	{
		public static string CapsConnectionString
		{
			get
			{
				return ExperianPWUpdateBatchJob.Parameters[CommonParameterNames.CapsConnectionString];
			}
		}

        public static string ExperianPasswordResetUrl
        {
            get
            {
                return ExperianPWUpdateBatchJob.Parameters[ParameterKeysUtil.ExperianPasswordResetUrl];
            }
        }

        public static string PassWordCode
        {
            get
            {
                return ExperianPWUpdateBatchJob.Parameters[ParameterKeysUtil.PassWordCode];
            }
        }

        public static string UserIdCode
        {
            get
            {
                return ExperianPWUpdateBatchJob.Parameters[ParameterKeysUtil.UserIdCode];
            }
        }

        public static string BatchMode
        {
            get
            {
                return ExperianPWUpdateBatchJob.Parameters[ParameterKeysUtil.BatchMode];
            }
        }
      
        public static string MasterCopyConnectionString
        {
            get
            {
                return ExperianPWUpdateBatchJob.Parameters[ParameterKeysUtil.MasterCopyConnectionString];
            }
        }
        public static string Host
        {
            get
            {
                return ExperianPWUpdateBatchJob.Parameters[ParameterKeysUtil.Host];
            }
        }

        public static string Application
        {
            get
            {
                return ExperianPWUpdateBatchJob.Parameters[ParameterKeysUtil.Application];
            }
        }
    }
}
