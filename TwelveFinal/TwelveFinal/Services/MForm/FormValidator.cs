﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MForm
{
    public interface IFormValidator : IServiceScoped
    {
        Task<bool> Approve(Form form);
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
            Invalid,
            IsApproved
        }

        public FormValidator(IUOW _UOW)
        {
            UOW = _UOW;
        }

        public async Task<bool> Approve(Form form)
        {
            bool IsValid = true;
            IsValid &= await IsExisted(form);
            IsValid &= await StatusIsFalse(form);
            return IsValid;
        }

        public async Task<bool> Save(Form form)
        {
            bool IsValid = true;
            IsValid &= await GraduationValidate(form);
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

        public async Task<bool> StatusIsFalse(Form form)
        {
            //Validate Trạng thái 
            //False: nếu chưa được duyệt => cho phép duyệt
            //True: đã được duyệt
            if (form.Status)
            {
                form.AddError(nameof(FormValidator), "Form", ErrorCode.IsApproved);
            }

            return form.IsValidated;
        }

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