using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MStudentService
{
    public interface IStudentValidator : IServiceScoped
    {
        Task<bool> Create(Student student);
        Task<bool> BulkInsert(List<Student> students);
        Task<bool> Update(Student student);
        Task<bool> Delete(Student student);
    }
    public class StudentValidator : IStudentValidator
    {
        private readonly IUOW UOW;
        public StudentValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public enum ErrorCode
        {

        }
        public Task<bool> BulkInsert(List<Student> students)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Create(Student student)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Student student)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Student student)
        {
            throw new NotImplementedException();
        }

        


    }
}
