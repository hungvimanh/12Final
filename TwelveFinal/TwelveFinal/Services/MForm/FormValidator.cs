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
        Task<bool> Save(Form form);
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

        public async Task<bool> Save(Form form)
        {
            bool IsValid = true;
            IsValid &= await IsExisted(form);
            return IsValid;
        }

        private async Task<bool> IsExisted(Form form)
        {
            if(await UOW.FormRepository.Get(form.Id) == null)
            {
                return false;
            }
            return true;
        }
    }
}
