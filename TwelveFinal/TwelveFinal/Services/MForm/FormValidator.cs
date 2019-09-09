using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;

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
