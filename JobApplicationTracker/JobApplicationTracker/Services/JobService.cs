using JobApplicationTracker.Data;
using JobApplicationTracker.Enums;
using JobApplicationTracker.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplicationTracker.Services
{
	public class JobService
	{

		private readonly AppData _data;
		public JobService(AppData data)
		{
			_data = data;
		}

		public bool AddJob(Job job)
		{
			if (job == null || _data.JobsById.ContainsKey(job.Id))
				return false;

			Company? company = _data.Companies.GetById(job.CompanyId);
			if (company == null || company.Status != CompanyStatus.Approved)
				return false;

			bool IsAdded = _data.Jobs.Add(job);
			if (!IsAdded)
				return false;

			_data.JobsById[job.Id] = job;

			return true;

		}

		public Job? GetJobById(int id)
		{
			if (_data.JobsById.TryGetValue(id, out Job? job))
				return job;

			return null;

		}

		public bool UpdateJob(Job job)
		{
			if (job == null)
				return false;

			Job? ExistingJob = _data.Jobs.GetById(job.Id);
			if (ExistingJob == null)
				return false;

			bool IsUpdated = _data.Jobs.Update(job);
			if (!IsUpdated)
				return false;

			_data.JobsById[job.Id] = job;

			return true;

		}

		public bool RemoveJobById(int id)
		{
			Job? ExistingJob = _data.Jobs.GetById(id);
			if (ExistingJob == null)
				return false;

			foreach (Application application in _data.Applications.GetAll())
			{
				if (application.JobId == id)
					return false;
			}

			bool IsRemoved = _data.Jobs.RemoveById(id);
			if (!IsRemoved)
				return false;

			_data.JobsById.Remove(id);

			return true;
		}

		public List<Job> SearchByTitle(string titleKeyWord)
		{
			return _data.Jobs.Find(job =>
				job.Title.Contains(titleKeyWord, StringComparison.OrdinalIgnoreCase));
		}

		public List<Job> SearchByMinimumSalary(decimal minimumSalary)
		{
			return _data.Jobs.Find(job =>
			job.Salary >= minimumSalary);
		}

		public List<Job> SearchJobs(string titleKeyWord, decimal minimumSalary)
		{
			return _data.Jobs.Find(job =>
			job.Title.Contains(titleKeyWord, StringComparison.OrdinalIgnoreCase)
			&& job.Salary >= minimumSalary
			&& job.IsOpen);
		}

		public List<Job> SortBySalaryAscending()
		{
			return _data.Jobs.Sort((job1, job2) =>
			job1.Salary.CompareTo(job2.Salary));
		}

		public List<Job> SortBySalaryDescending()
		{
			return _data.Jobs.Sort((job1, job2) =>
			job2.Salary.CompareTo(job1.Salary));
		}

		public List<Job> SortByTitleAlphabetically()
		{
			return _data.Jobs.Sort((job1, job2) =>
			string.Compare(job1.Title, job2.Title, StringComparison.OrdinalIgnoreCase));
		}



	}
}
