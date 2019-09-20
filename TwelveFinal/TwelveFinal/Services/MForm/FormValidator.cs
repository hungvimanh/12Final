using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MForm
{
    public interface IFormValidator : IServiceScoped
    {
        Task<bool> Create(Form form);
        Task<bool> Update(Form form);
        Task<bool> Delete(Form form);
    }
    public class FormValidator : IFormValidator
    {
        private IUOW UOW;
        public enum ErrorCode
        {
            Duplicate,
            NotExisted
        }

        public FormValidator(IUOW _UOW)
        {
            UOW = _UOW;
        }

        public async Task<bool> Create(Form form)
        {
            return true;
        }

        public async Task<bool> Delete(Form form)
        {
            bool IsValid = true;
            IsValid &= await IsExisted(form);
            return IsValid;
        }

        public async Task<bool> Update(Form form)
        {
            bool IsValid = true;
            IsValid &= await IsExisted(form);
            return IsValid;
        }

        private async Task<bool> IsExisted(Form form)
        {
            if(await UOW.FormRepository.Get(form.Id) == null)
            {
                form.AddError(nameof(FormValidator), nameof(form.Id), ErrorCode.NotExisted);
            }
            return form.IsValidated;
        }
    }
}
