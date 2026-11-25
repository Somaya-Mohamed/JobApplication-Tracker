using JobApplicationTracker.Data;
using JobApplicationTracker.Enums;
using JobApplicationTracker.Events;
using JobApplicationTracker.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplicationTracker.Services
{
	public class ApplicationService
	{
		private readonly AppData _data;

		public ApplicationService(AppData data)
		{
			_data = data;
		}

		public event EventHandler<ApplicationSubmittedEventArgs>? ApplicationSubmittedEvent;
		public event EventHandler<ApplicationStatusChangedEventArgs>? ApplicationStatusChangedEvent;

		public bool ApplyForJob(int jobId, int candidateId)
		{
			Candidate? candidate = _data.Candidates.GetById(candidateId);

			Job? job = _data.JobsById.ContainsKey(jobId) ? _data.JobsById[jobId] : null;

			if (candidate == null || job == null || !job.IsOpen)
				return false;

			Company? company = _data.Companies.GetById(job.CompanyId);

			if (company == null || company.Status != CompanyStatus.Approved)
				return false;

			var applicationKey = (JobId: jobId, CandidateId: candidateId);
			//if (_data.AppliedJobs.Contains(applicationKey))
			if (!_data.AppliedJobs.Add(applicationKey)) //M4 — Duplicate Application Protection ALREADY DONE :))
				return false;

			Application application = new Application
			{
				Id = GetNextApplicationId(),
				JobId = jobId,
				CandidateId = candidateId,
				ApplicationDate = DateTime.Now,
				Status = ApplicationStatus.Submitted
			};

			bool IsAdded = _data.Applications.Add(application);
			if (!IsAdded)
			{
				_data.AppliedJobs.Remove(applicationKey);
				return false;

			}

			//_data.AppliedJobs.Add(applicationKey);

			ApplicationSubmittedEventArgs args = new ApplicationSubmittedEventArgs(application.Id, application.JobId, application.CandidateId);
			ApplicationSubmittedEvent?.Invoke(this, args);

			return true;


		}
		
		private int GetNextApplicationId()
		{
			int maxId = 0;
			foreach (var application in _data.Applications.GetAll())
			{
				if (application.Id > maxId)
					maxId = application.Id;
			}

			return maxId + 1;
		}

		public bool ChangeApplicationStatus(int applicationId, ApplicationStatus newStatus)
		{
			Application? application = _data.Applications.GetById(applicationId);

			if (application == null || application.Status == newStatus)
				return false;

			ApplicationStatus oldStatus = application.Status;

			application.Status = newStatus;

			bool IsUpdated = _data.Applications.Update(application);

			if (!IsUpdated)
			{
				application.Status = oldStatus;
				return false;
			}

			ApplicationStatusChangedEventArgs args = new ApplicationStatusChangedEventArgs(application.Id, oldStatus, newStatus);
			ApplicationStatusChangedEvent?.Invoke(this, args);

			return true;

		}

		public bool RemoveApplicationById(int id)
		{
			Application? application = _data.Applications.GetById(id);

			if (application == null)
				return false;

			bool IsRemoved = _data.Applications.RemoveById(id);

			if (!IsRemoved)
				return false;

			_data.AppliedJobs.Remove((application.JobId, application.CandidateId));

			return true;

		}

	}
}
