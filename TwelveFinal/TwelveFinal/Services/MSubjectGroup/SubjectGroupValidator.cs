using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MSubjectGroup
{
    public interface ISubjectGroupValidator : IServiceScoped
    {
        Task<bool> Create(SubjectGroup subjectGroup);
        Task<bool> Update(SubjectGroup subjectGroup);
        Task<bool> Delete(SubjectGroup subjectGroup);
    }
    public class SubjectGroupValidator : ISubjectGroupValidator
    {
        private IUOW UOW;
        public enum ErrorCode
        {

        }

        public SubjectGroupValidator(IUOW _UOW)
        {
            UOW = _UOW;
        }

        public Task<bool> Create(SubjectGroup SubjectGroup)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(SubjectGroup SubjectGroup)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(SubjectGroup SubjectGroup)
        {
            throw new NotImplementedException();
        }
    }
}
