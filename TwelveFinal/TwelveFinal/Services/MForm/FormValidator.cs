using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MForm
{
    public interface IFormValidator
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

        }

        public FormValidator(IUOW _UOW)
        {
            UOW = _UOW;
        }

        public Task<bool> Create(Form form)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Form form)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Form form)
        {
            throw new NotImplementedException();
        }
    }
}
