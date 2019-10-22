using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MUniversityAdmission
{
    public interface IUniversityAdmissionValidator : IServiceScoped
    {
        Task<bool> Check(UniversityAdmission universityAdmission);
    }
    public class UniversityAdmissionValidator : IUniversityAdmissionValidator
    {
        private IUOW UOW;
        public enum ErrorCode
        {
            NotExisted,
            Invalid
        }

        public UniversityAdmissionValidator(IUOW _UOW)
        {
            UOW = _UOW;
        }

        public async Task<bool> Check(UniversityAdmission universityAdmission)
        {
            bool IsValid = true;
            IsValid &= await ValidatePriorityType(universityAdmission);
            IsValid &= await ValidateArea(universityAdmission);
            IsValid &= await ValidateGraduateYear(universityAdmission);
            IsValid &= await ValidateTotalAdmission(universityAdmission);
            IsValid &= await ValidateAdmission(universityAdmission);
            return IsValid;
        }

        private async Task<bool> ValidatePriorityType(UniversityAdmission universityAdmission)
        {
            //Kiểm tra đối tượng ưu tiên đã được chọn chưa?
            //và đối tượng được chọn có tồn tại hay không?
            if (string.IsNullOrEmpty(universityAdmission.PriorityType))
            {
                universityAdmission.AddError(nameof(UniversityAdmissionValidator), nameof(universityAdmission.PriorityType), ErrorCode.Invalid);
                return universityAdmission.IsValidated;
            }

            PriorityTypeFilter filter = new PriorityTypeFilter
            {
                Code = new StringFilter { Equal = universityAdmission.PriorityType }
            };
            var count = await UOW.PriorityTypeRepository.Count(filter);
            if(count == 0)
            {
                universityAdmission.AddError(nameof(UniversityAdmissionValidator), nameof(universityAdmission.PriorityType), ErrorCode.NotExisted);
                return universityAdmission.IsValidated;
            }

            return universityAdmission.IsValidated;
        }

        private async Task<bool> ValidateArea(UniversityAdmission universityAdmission)
        {
            //Kiểm tra khu vực xét tuyển đã được chọn chưa?
            //và khu vực được chọn có tồn tại hay không?
            if (string.IsNullOrEmpty(universityAdmission.Area))
            {
                universityAdmission.AddError(nameof(UniversityAdmissionValidator), nameof(universityAdmission.Area), ErrorCode.Invalid);
                return universityAdmission.IsValidated;
            }

            AreaFilter filter = new AreaFilter
            {
                Name = new StringFilter { Equal = universityAdmission.Area }
            };
            var count = await UOW.AreaRepository.Count(filter);
            if (count == 0)
            {
                universityAdmission.AddError(nameof(UniversityAdmissionValidator), nameof(universityAdmission.Area), ErrorCode.NotExisted);
                return universityAdmission.IsValidated;
            }

            return universityAdmission.IsValidated;
        }

        private async Task<bool> ValidateGraduateYear(UniversityAdmission universityAdmission)
        {
            //Kiểm tra năm tốt nghiệp đã điền chính xác chưa?
            if (string.IsNullOrEmpty(universityAdmission.GraduateYear) || universityAdmission.GraduateYear.Length != 4)
            {
                universityAdmission.AddError(nameof(UniversityAdmissionValidator), nameof(universityAdmission.GraduateYear), ErrorCode.Invalid);
            }
            return universityAdmission.IsValidated;
        }

        private async Task<bool> ValidateTotalAdmission(UniversityAdmission universityAdmission)
        {
            //Kiểm tra tổng số nguyện vọng đăng ký
            if(universityAdmission.TotalAspiration != universityAdmission.FormDetails.Count || universityAdmission.TotalAspiration == null)
            {
                universityAdmission.AddError(nameof(UniversityAdmissionValidator), nameof(universityAdmission.TotalAspiration), ErrorCode.Invalid);
            }
            return universityAdmission.IsValidated;
        }

        private async Task<bool> ValidateAdmission(UniversityAdmission universityAdmission)
        {
            //Kiểm tra các nguyện vọng
            foreach (var admission in universityAdmission.FormDetails)
            {
                if(!await AdmissionValidation(admission))
                {
                    universityAdmission.AddError(nameof(UniversityAdmissionValidator), admission.MajorsCode, ErrorCode.NotExisted);
                }
            }

            bool IsValid = true;
            universityAdmission.FormDetails.ForEach(e => IsValid &= e.IsValidated);
            return IsValid;
        }

        private async Task<bool> AdmissionValidation(FormDetail formDetail)
        {
            //kiểm tra sự tồn tại của ngành học
            University_MajorsFilter filter = new University_MajorsFilter
            {
                UniversityId = formDetail.UniversityId,
                MajorsId = formDetail.MajorsId,
                SubjectGroupId = new GuidFilter { Equal = formDetail.SubjectGroupId }
            };

            var count = await UOW.University_MajorsRepository.Count(filter);
            if (count == 0)
                return false;
            return true;
        }
    }
}
