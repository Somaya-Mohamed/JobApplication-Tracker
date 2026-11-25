using JobApplicationTracker.Data;
using JobApplicationTracker.Enums;
using JobApplicationTracker.Events;
using JobApplicationTracker.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplicationTracker.Services
{
	public class CompanyService
	{
		private readonly AppData _data;

		public CompanyService(AppData data)
		{
			_data = data;
		}

		public event EventHandler<CompanyApprovedEventArgs>? CompanyApprovedEvent;

		public bool AddCompany(Company company)
		{
			if(company == null || string.IsNullOrWhiteSpace(company.Name))
				return false;

			return _data.Companies.Add(company);
		}

		public Company? GetCompanyById(int id)
		{
			return _data.Companies.GetById(id);
		}

		public IEnumerable<Company> GetAllCompanies()
		{
			return _data.Companies.GetAll();
		}

		public bool UpdateCompany(Company company)
		{
			if(company == null || string.IsNullOrWhiteSpace(company.Name))
				return false;

			return _data.Companies.Update(company);
		}

		public bool RemoveCompanyById(int id)
		{
			return _data.Companies.RemoveById(id);
		}

		public bool ChangeCompanyStatus(int companyId, CompanyStatus newStatus)
		{
			Company? company = _data.Companies.GetById(companyId);

			if (company == null)
				return false;

			if (company.Status == newStatus)
				return false;

			_data.CompanyStatusHistory.Push((company.Id, company.Status));

			company.Status = newStatus;
			return true;
		}

		public bool UndoLastStatusChange()
		{
			if (_data.CompanyStatusHistory.Count == 0)
				return false;

			var LastChange = _data.CompanyStatusHistory.Pop();

			Company? company = _data.Companies.GetById(LastChange.CompanyId);

			if (company == null)
				return false;

			company.Status = LastChange.OldStatus;
			return true;

		}

		public IEnumerable<Company> GetCompaniesByStatus(CompanyStatus status)
		{
			foreach (var company in _data.Companies.GetAll())
			{
				if (company.Status == status)
				{
					yield return company;
				}
			}
		}

		public bool ApproveCompany(int id)
		{
			Company? company = _data.Companies.GetById(id);

			if (company == null || company.Status != CompanyStatus.Pending)
				return false;

			bool IsApproved = ChangeCompanyStatus(id, CompanyStatus.Approved);

			if (!IsApproved)
				return false;

			CompanyApprovedEventArgs args = new CompanyApprovedEventArgs(company.Id, company.Name);
			CompanyApprovedEvent?.Invoke(this, args);

			return true;
		}

		public bool RejectCompany(int id)
		{
			return ChangeCompanyStatus(id, CompanyStatus.Rejected);
		}

		public IEnumerable<Company> GetPendingCompanies()
		{
			return GetCompaniesByStatus(CompanyStatus.Pending);
		}

		public IEnumerable<Company> GetApprovedCompanies()
		{
			return GetCompaniesByStatus(CompanyStatus.Approved);
		}

		public IEnumerable<Company> GetRejectedCompanies()
		{
			return GetCompaniesByStatus(CompanyStatus.Rejected);
		}

	}
}
