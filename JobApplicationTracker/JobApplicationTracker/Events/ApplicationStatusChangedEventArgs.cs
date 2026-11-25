using JobApplicationTracker.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplicationTracker.Events
{
	public sealed class ApplicationStatusChangedEventArgs : EventArgs
	{
		public int ApplicationId { get; }
		public ApplicationStatus OldStatus { get; }

		public ApplicationStatus NewStatus { get; }

		public ApplicationStatusChangedEventArgs( int applicationId, ApplicationStatus oldStatus, ApplicationStatus newStatus)
		{
			ApplicationId = applicationId;
			OldStatus = oldStatus;
			NewStatus = newStatus;
		}
	}
}
