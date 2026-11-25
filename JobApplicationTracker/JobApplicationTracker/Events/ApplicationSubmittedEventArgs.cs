
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplicationTracker.Events
{
	public sealed class ApplicationSubmittedEventArgs
	{

		public int ApplicationId { get; }

		public int JobId { get; }

		public int CandidateId { get; }

		public ApplicationSubmittedEventArgs(int applicationId, int jobId, int candidateId)
		{
			ApplicationId = applicationId;
			JobId = jobId;
			CandidateId = candidateId;
		}
	}
}
