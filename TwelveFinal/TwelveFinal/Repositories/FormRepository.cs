using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories.Models;

namespace TwelveFinal.Repositories
{
    public interface IFormRepository
    {
        Task<List<Form>> List(Guid StudentId);
        Task<bool> Create(Form form);
        Task<Form> Get(Guid Id);
        Task<bool> Update(Form form);
        Task<bool> Delete(Guid Id);
    }
    public class FormRepository : IFormRepository
    {
        private readonly TFContext tFContext;
        public FormRepository(TFContext _tFContext)
        {
            tFContext = _tFContext;
        }

        public async Task<bool> Create(Form form)
        {
            FormDAO formDAO = new FormDAO
            {
                Id = form.Id,
                NumberForm = form.NumberForm,
                DepartmentCode = form.DepartmentCode,
                Date = form.Date,
                PersonalInfomartionId = form.PersonalInfomationId,
                RegisterInformationId = form.RegisterInformationId,
                GraduationInformationId = form.GraduationInformationId,
                UniversityAdmissionId = form.UniversityAdmissionId
            };

            tFContext.Form.Add(formDAO);
            await tFContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Guid Id)
        {
            await tFContext.Form.Where(b => b.Id.Equals(Id)).DeleteFromQueryAsync();
            return true;
        }

        public async Task<Form> Get(Guid Id)
        {
            Form form = await tFContext.Form.Where(f => f.Id.Equals(Id)).Select(f => new Form
            {
                Id = f.Id,
                NumberForm = f.NumberForm,
                DepartmentCode = f.DepartmentCode,
                Date = f.Date,
                PersonalInfomationId = f.PersonalInfomartionId,
                RegisterInformationId = f.RegisterInformationId,
                GraduationInformationId = f.GraduationInformationId,
                UniversityAdmissionId = f.UniversityAdmissionId,
            }).FirstOrDefaultAsync();

            return form;
        }

        public async Task<List<Form>> List(Guid StudentId)
        {
            List<Form> list = await tFContext.Form.Where(f => f.PersonalInfomartionId.Equals(StudentId)).Select(f => new Form
            {
                Id = f.Id,
                NumberForm = f.NumberForm,
                DepartmentCode = f.DepartmentCode,
                Date = f.Date,
                PersonalInfomationId = f.PersonalInfomartionId,
                RegisterInformationId = f.RegisterInformationId,
                GraduationInformationId = f.GraduationInformationId,
                UniversityAdmissionId = f.UniversityAdmissionId
            }).ToListAsync();
            return list;
        }

        public async Task<bool> Update(Form form)
        {
            await tFContext.Form.Where(f => f.Id.Equals(form.Id)).UpdateFromQueryAsync(f => new FormDAO
            {
                Id = form.Id,
                NumberForm = form.NumberForm,
                DepartmentCode = form.DepartmentCode,
                Date = form.Date,
                PersonalInfomartionId = form.PersonalInfomationId,
                RegisterInformationId = form.RegisterInformationId,
                GraduationInformationId = form.GraduationInformationId,
                UniversityAdmissionId = form.UniversityAdmissionId
            });

            return true;
        }
    }
}
