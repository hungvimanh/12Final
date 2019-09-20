using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MSubjectGroup
{
    public interface ISubjectGroupService : IServiceScoped
    {
        Task<SubjectGroup> Create(SubjectGroup SubjectGroup);
        Task<SubjectGroup> Get(Guid Id);
        Task<List<SubjectGroup>> List(SubjectGroupFilter SubjectGroupFilter);
        Task<SubjectGroup> Update(SubjectGroup SubjectGroup);
        Task<SubjectGroup> Delete(SubjectGroup SubjectGroup);
    }
    public class SubjectGroupService : ISubjectGroupService
    {
        private readonly IUOW UOW;
        private readonly ISubjectGroupValidator SubjectGroupValidator;

        public SubjectGroupService(
            IUOW UOW,
            ISubjectGroupValidator SubjectGroupValidator
            )
        {
            this.UOW = UOW;
            this.SubjectGroupValidator = SubjectGroupValidator;
        }

        public async Task<SubjectGroup> Create(SubjectGroup SubjectGroup)
        {
            SubjectGroup.Id = Guid.NewGuid();
            if (!await SubjectGroupValidator.Create(SubjectGroup))
                return SubjectGroup;

            try
            {
                await UOW.Begin();
                await UOW.SubjectGroupRepository.Create(SubjectGroup);
                await UOW.Commit();
                return await Get(SubjectGroup.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<SubjectGroup> Delete(SubjectGroup SubjectGroup)
        {
            if (!await SubjectGroupValidator.Delete(SubjectGroup))
                return SubjectGroup;

            try
            {
                await UOW.Begin();
                await UOW.SubjectGroupRepository.Delete(SubjectGroup.Id);
                await UOW.Commit();
                return await Get(SubjectGroup.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<SubjectGroup> Get(Guid Id)
        {
            if (Id == Guid.Empty) return null;
            SubjectGroup SubjectGroup = await UOW.SubjectGroupRepository.Get(Id);
            return SubjectGroup;
        }

        public async Task<List<SubjectGroup>> List(SubjectGroupFilter SubjectGroupFilter)
        {
            return await UOW.SubjectGroupRepository.List(SubjectGroupFilter);
        }

        public async Task<SubjectGroup> Update(SubjectGroup SubjectGroup)
        {
            if (!await SubjectGroupValidator.Update(SubjectGroup))
                return SubjectGroup;

            try
            {
                await UOW.Begin();
                await UOW.SubjectGroupRepository.Update(SubjectGroup);
                await UOW.Commit();
                return await Get(SubjectGroup.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }
    }
}
