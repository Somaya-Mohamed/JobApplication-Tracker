using JobApplicationTracker.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplicationTracker.Services
{
	public class NotificationService // Event Handler Methods.
	{

		// Event Handler Methods:)
		public void OnCompanyApproved(object? sender, CompanyApprovedEventArgs e)
		{
			Console.WriteLine($"Notification: Company {e.CompanyName} (ID: {e.CompanyId}) has been approved.");
		}

		public void OnApplicationSubmitted(object? sender, ApplicationSubmittedEventArgs e)
		{
			Console.WriteLine($"Notification: Candidate {e.CandidateId} submitted an application for Job {e.JobId}. Application ID: {e.ApplicationId}.");
		}

		public void OnApplicationStatusChanged(object? sender, ApplicationStatusChangedEventArgs e)
		{
			Console.WriteLine($"Notification: Application {e.ApplicationId} status changed from {e.OldStatus} to {e.NewStatus}.");
		}
	}
}
