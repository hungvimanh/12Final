using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MForm
{
    public interface IFormRepository : IServiceScoped
    {
        //Task<List<Form>> List(Guid studentId);
        Task<Form> Create(Form form);
        Task<Form> Get(Guid Id);
        Task<Form> Update(Form form);
        Task<Form> Delete(Form form);
    }
    public class FormService : IFormRepository
    {
        private readonly IUOW UOW;
        private readonly IFormValidator FormValidator;

        public async Task<Form> Create(Form form)
        {
            form.Id = Guid.NewGuid();
            if (!await FormValidator.Create(form))
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

        public async Task<Form> Delete(Form form)
        {
            if (!await FormValidator.Delete(form))
                return form;

            try
            {
                await UOW.Begin();
                await UOW.FormRepository.Delete(form.Id);
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
            return form;
        }

        //public async Task<List<Form>> List(Guid studentId)
        //{
        //    List<Form> forms = await UOW.FormRepository.List(studentId);
        //    return forms;
        //}

        public async Task<Form> Update(Form form)
        {
            if (!await FormValidator.Update(form))
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
