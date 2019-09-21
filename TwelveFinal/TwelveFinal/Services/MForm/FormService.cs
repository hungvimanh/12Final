using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;
using TwelveFinal.Services.MGraduation;
using TwelveFinal.Services.MPersonal;
using TwelveFinal.Services.MRegister;
using TwelveFinal.Services.MUniversityAdmission;

namespace TwelveFinal.Services.MForm
{
    public interface IFormService : IServiceScoped
    {
        Task<Form> Save(Form form);
        Task<Form> Create(Form form);
        Task<Form> Get(Guid Id);
        Task<Form> Update(Form form);
    }
    public class FormService : IFormService
    {
        private readonly IUOW UOW;
        private readonly IFormValidator FormValidator;
        private readonly IPersonalInformationValidator PersonalInformationValidator;
        private readonly IRegisterInformationValidator RegisterInformationValidator;
        private readonly IGraduationInformationValidator GraduationInformationValidator;
        private readonly IUniversityAdmissionValidator UniversityAdmissionValidator;

        public FormService(
            IUOW UOW,
            IFormValidator FormValidator,
            IPersonalInformationValidator PersonalInformationValidator,
            IRegisterInformationValidator RegisterInformationValidator,
            IGraduationInformationValidator GraduationInformationValidator,
            IUniversityAdmissionValidator UniversityAdmissionValidator
            )
        {
            this.UOW = UOW;
            this.FormValidator = FormValidator;
            this.PersonalInformationValidator = PersonalInformationValidator;
            this.RegisterInformationValidator = RegisterInformationValidator;
            this.GraduationInformationValidator = GraduationInformationValidator;
            this.UniversityAdmissionValidator = UniversityAdmissionValidator;
        }

        public async Task<Form> Save(Form form)
        {
            if (!await FormValidator.Save(form))
                return await Create(form);
            return await Update(form);
        }

        public async Task<Form> Create(Form form)
        {
            form.Id = Guid.NewGuid();
            form.PersonalInformation.Id = form.Id;
            form.RegisterInformation.Id = form.Id;
            form.GraduationInformation.Id = form.Id;
            form.UniversityAdmission.Id = form.Id;
            if(!await PersonalInformationValidator.Check(form.PersonalInformation))
                return form;

            if (!await RegisterInformationValidator.Check(form.RegisterInformation))
                return form;

            if (!await GraduationInformationValidator.Check(form.GraduationInformation))
                return form;

            if (!await UniversityAdmissionValidator.Check(form.UniversityAdmission))
                return form;

            try
            {
                await UOW.Begin();
                await UOW.FormRepository.Create(form);
                await UOW.Commit();
                return await Get(form.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<Form> Get(Guid Id)
        {
            if (Id == Guid.Empty) return null;
            Form form = await UOW.FormRepository.Get(Id);
            form.UniversityAdmission.TotalAspiration = form.UniversityAdmission.FormDetails.Count();
            return form;
        }

        public async Task<Form> Update(Form form)
        {
            if (!await PersonalInformationValidator.Check(form.PersonalInformation))
                return form;

            if (!await RegisterInformationValidator.Check(form.RegisterInformation))
                return form;

            if (!await GraduationInformationValidator.Check(form.GraduationInformation))
                return form;

            if (!await UniversityAdmissionValidator.Check(form.UniversityAdmission))
                return form;

            try
            {
                await UOW.Begin();
                await UOW.FormRepository.Update(form);
                await UOW.Commit();
                return await Get(form.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }
    }
}
