using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoardingSystem.Common.enums
{
	public class MdStatusEnum
	{
		public string Key { get; }
		public string Value { get; }

		private MdStatusEnum(string key, string value)
		{
			Key = key;
			Value = value;
		}

		public static readonly MdStatusEnum Completed = new MdStatusEnum("Completed", "CP");
		public static readonly MdStatusEnum NotSigned = new MdStatusEnum("Not Signed", "NS");
		public static readonly MdStatusEnum PaidButNotVerified = new MdStatusEnum("Paid But Not Verified", "NV");
		public static readonly MdStatusEnum SignedButNotVerified = new MdStatusEnum("Signed but Not Verified", "NV");
		public static readonly MdStatusEnum Pending = new MdStatusEnum("Pending", "PN");
		public static readonly MdStatusEnum PaidAndVerified = new MdStatusEnum("Paid and Verified", "PV");
		public static readonly MdStatusEnum SignedandVerified = new MdStatusEnum("Signed and Verified", "SV");
	}
}
