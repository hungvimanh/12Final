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
        Task<bool> IsExisted(Form form);
        Task<bool> Delete(Form form);
    }
    public class FormValidator : IFormValidator
    {
        private IUOW UOW;
        public enum ErrorCode
        {
            Duplicate,
            NotExisted,
            Invalid
        }

        public FormValidator(IUOW _UOW)
        {
            UOW = _UOW;
        }

        public async Task<bool> Save(Form form)
        {
            bool IsValid = true;
            IsValid &= await ValidatePlateOfBirth(form);
            IsValid &= await ValidateEthnic(form);
            IsValid &= await ValidateAddress(form);
            IsValid &= await ValidateHighShool(form);

            IsValid &= await ValidateClusterContest(form);
            IsValid &= await ValidateRegisterPlaceOfExam(form);

            IsValid &= await GraduationValidate(form);

            IsValid &= await ValidatePriorityType(form);
            IsValid &= await ValidateArea(form);
            IsValid &= await ValidateAspiration(form);
            IsValid &= await SequenceValidate(form.Aspirations);

            return IsValid;
        }

        public async Task<bool> Delete(Form form)
        {
            bool IsValid = true;
            IsValid &= await IsExisted(form);
            if (!IsValid)
            {
                form.AddError(nameof(FormValidator), nameof(form.Id), ErrorCode.NotExisted);
            }
            return IsValid;
        }

        public async Task<bool> IsExisted(Form form)
        {
            //Kiểm tra Form đã tồn tại hay chưa?
            if(await UOW.FormRepository.Get(form.Id) == null)
            {
                return false;
            }
            return true;
        }

        #region Validate thông tin cá nhân 
        private async Task<bool> ValidatePlateOfBirth(Form form)
        {
            //Kiểm tra nơi sinh có tồn tại?
            ProvinceFilter filter = new ProvinceFilter
            {
                Name = new StringFilter { Equal = form.PlaceOfBirth }
            };
            if (await UOW.ProvinceRepository.Count(filter) == 0)
            {
                form.AddError(nameof(FormValidator), nameof(form.PlaceOfBirth), ErrorCode.NotExisted);
            }

            return form.IsValidated;
        }

        private async Task<bool> ValidateEthnic(Form form)
        {
            //Kiểm tra dân tộc có hợp lệ hay ko?
            EthnicFilter filter = new EthnicFilter
            {
                Name = new StringFilter { Equal = form.Ethnic }
            };
            var count = await UOW.EthnicRepository.Count(filter);
            if (count == 0)
            {
                form.AddError(nameof(FormValidator), nameof(form.Ethnic), ErrorCode.NotExisted);
            }
            return form.IsValidated;
        }

        private async Task<bool> ValidateAddress(Form form)
        {
            //Kiểm tra địa chỉ
            TownFilter filter = new TownFilter
            {
                Id = new GuidFilter { Equal = form.TownId },
                Code = new StringFilter { Equal = form.TownCode }
            };
            var count = await UOW.TownRepository.Count(filter);
            if (count == 0)
            {
                form.AddError(nameof(FormValidator), nameof(form.TownId), ErrorCode.NotExisted);
                return form.IsValidated;
            }

            return form.IsValidated;
        }

        private async Task<bool> ValidateHighShool(Form personalInformation)
        {
            //Kiểm tra trường c3 có tồn tại không

            if (!await HighSchoolValidation(personalInformation.HighSchoolGrade10Code))
            {
                personalInformation.AddError(nameof(FormValidator), nameof(personalInformation.HighSchoolGrade10Code), ErrorCode.Invalid);
                return personalInformation.IsValidated;
            }

            if (!await HighSchoolValidation(personalInformation.HighSchoolGrade11Code))
            {
                personalInformation.AddError(nameof(FormValidator), nameof(personalInformation.HighSchoolGrade11Code), ErrorCode.Invalid);
                return personalInformation.IsValidated;
            }

            if (!await HighSchoolValidation(personalInformation.HighSchoolGrade12Code))
            {
                personalInformation.AddError(nameof(FormValidator), nameof(personalInformation.HighSchoolGrade12Code), ErrorCode.Invalid);
                return personalInformation.IsValidated;
            }

            return personalInformation.IsValidated;
        }
        private async Task<bool> HighSchoolValidation(string highSchoolCode)
        {
            //lọc theo Code
            HighSchoolFilter filter = new HighSchoolFilter
            {
                Code = new StringFilter { Equal = highSchoolCode }
            };

            var count = await UOW.HighSchoolRepository.Count(filter);
            if (count == 0)
                return false;
            return true;
        }

        #endregion

        #region Validate thông tin đăng kí dự thi

        private async Task<bool> ValidateClusterContest(Form form)
        {
            //Kiểm tra cụm dự thi
            var filter = new ProvinceFilter
            {
                Code = new StringFilter { Equal = form.ClusterContestCode }
            };
            var count = await UOW.ProvinceRepository.Count(filter);
            if (count == 0)
            {
                form.AddError(nameof(FormValidator), nameof(form.ClusterContestCode), ErrorCode.NotExisted);
                return form.IsValidated;
            }

            return form.IsValidated;
        }

        private async Task<bool> ValidateRegisterPlaceOfExam(Form form)
        {
            //Kiểm tra nơi đăng ký dự thi
            var filter = new HighSchoolFilter
            {
                Code = new StringFilter { Equal = form.RegisterPlaceOfExamCode }
            };
            var count = await UOW.HighSchoolRepository.Count(filter);
            if (UOW.ProvinceRepository.Get(form.RegisterPlaceOfExamId) == null)
            {
                form.AddError(nameof(FormValidator), nameof(form.RegisterPlaceOfExamCode), ErrorCode.NotExisted);
                return form.IsValidated;
            }

            return form.IsValidated;
        }
        #endregion

        #region Validate thông tin xét tốt nghiệp
        private async Task<bool> GraduationValidate(Form form)
        {
            //Kiểm tra số điểm các môn bảo lưu nếu có 
            if (form.ReserveMaths != null && !(form.ReserveMaths >= 0 && form.ReserveMaths <= 10))
            {
                form.AddError(nameof(FormValidator), nameof(form.ReserveMaths), ErrorCode.Invalid);
            }

            if (form.ReservePhysics != null && !(form.ReservePhysics >= 0 && form.ReservePhysics <= 10))
            {
                form.AddError(nameof(FormValidator), nameof(form.ReservePhysics), ErrorCode.Invalid);
            }

            if (form.ReserveChemistry != null && !(form.ReserveChemistry >= 0 && form.ReserveChemistry <= 10))
            {
                form.AddError(nameof(FormValidator), nameof(form.ReserveChemistry), ErrorCode.Invalid);
            }

            if (form.ReserveLiterature != null && !(form.ReserveLiterature >= 0 && form.ReserveLiterature <= 10))
            {
                form.AddError(nameof(FormValidator), nameof(form.ReserveLiterature), ErrorCode.Invalid);
            }

            if (form.ReserveHistory != null && !(form.ReserveHistory >= 0 && form.ReserveHistory <= 10))
            {
                form.AddError(nameof(FormValidator), nameof(form.ReserveHistory), ErrorCode.Invalid);
            }

            if (form.ReserveGeography != null && !(form.ReserveGeography >= 0 && form.ReserveGeography <= 10))
            {
                form.AddError(nameof(FormValidator), nameof(form.ReserveGeography), ErrorCode.Invalid);
            }

            if (form.ReserveBiology != null && !(form.ReserveBiology >= 0 && form.ReserveBiology <= 10))
            {
                form.AddError(nameof(FormValidator), nameof(form.ReserveBiology), ErrorCode.Invalid);
            }

            if (form.ReserveCivicEducation != null && !(form.ReserveCivicEducation >= 0 && form.ReserveCivicEducation <= 10))
            {
                form.AddError(nameof(FormValidator), nameof(form.ReserveCivicEducation), ErrorCode.Invalid);
            }

            if (form.ReserveLanguages != null && !(form.ReserveLanguages >= 0 && form.ReserveLanguages <= 10))
            {
                form.AddError(nameof(FormValidator), nameof(form.ReserveLanguages), ErrorCode.Invalid);
            }

            return form.IsValidated;
        }
        #endregion

        #region Validate thông tin xét tuyển đại học
        private async Task<bool> ValidatePriorityType(Form form)
        {
            //Kiểm tra đối tượng ưu tiên đã được chọn chưa?
            //và đối tượng được chọn có tồn tại hay không?
            PriorityTypeFilter filter = new PriorityTypeFilter
            {
                Code = new StringFilter { Equal = form.PriorityType }
            };
            var count = await UOW.PriorityTypeRepository.Count(filter);
            if (count == 0)
            {
                form.AddError(nameof(FormValidator), nameof(form.PriorityType), ErrorCode.NotExisted);
                return form.IsValidated;
            }

            return form.IsValidated;
        }

        private async Task<bool> ValidateArea(Form form)
        {
            //Kiểm tra khu vực xét tuyển đã được chọn chưa?
            //và khu vực được chọn có tồn tại hay không?
            AreaFilter filter = new AreaFilter
            {
                Name = new StringFilter { Equal = form.Area }
            };
            var count = await UOW.AreaRepository.Count(filter);
            if (count == 0)
            {
                form.AddError(nameof(FormValidator), nameof(form.Area), ErrorCode.NotExisted);
                return form.IsValidated;
            }

            return form.IsValidated;
        }

        private async Task<bool> ValidateAspiration(Form form)
        {
            //Kiểm tra các nguyện vọng
            foreach (var aspiration in form.Aspirations)
            {
                if (!await AspirationValidation(aspiration))
                {
                    form.AddError(nameof(FormValidator), aspiration.MajorsCode, ErrorCode.NotExisted);
                }
            }

            bool IsValid = true;
            form.Aspirations.ForEach(e => IsValid &= e.IsValidated);
            return IsValid;
        }

        private async Task<bool> AspirationValidation(Aspiration formDetail)
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

        private async Task<bool> SequenceValidate(List<Aspiration> aspirations)
        {
            //Kiểm tra thứ tự nguyện vọng hợp lệ
            //Sắp xếp List thứ tự nguyện vọng từ bé đến lớn
            aspirations = aspirations.OrderBy(a => a.Sequence).ToList();
            //Nguyện vọng cao nhất phải có Sequence = 1
            if (aspirations.First().Sequence != 1)
            {
                aspirations.First().AddError(nameof(FormValidator), "Sequence", ErrorCode.Invalid);
                return false;
            }

            //Sequence của các nguyện vọng sau phải hơn nguyện vọng trước 1
            var listSequence = aspirations.Select(a => a.Sequence).ToList();
            for (int i = 1; i < listSequence.Count; i++)
            {
                if (listSequence[i] != listSequence[i - 1] + 1)
                {
                    aspirations[i].AddError(nameof(FormValidator), "Sequence", ErrorCode.Invalid);
                }
            }

            bool IsValid = true;
            aspirations.ForEach(e => IsValid &= e.IsValidated);
            return IsValid;
        }
        #endregion
    }
}
