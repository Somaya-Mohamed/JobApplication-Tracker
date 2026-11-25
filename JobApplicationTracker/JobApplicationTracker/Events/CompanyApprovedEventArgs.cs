using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplicationTracker.Events
{
	public sealed class CompanyApprovedEventArgs : EventArgs
	{
		public int CompanyId { get; }
		public string CompanyName { get; }

		public CompanyApprovedEventArgs(int companyId, string companyName)
		{
			CompanyId = companyId;
			CompanyName = companyName;
		}
	}
}
