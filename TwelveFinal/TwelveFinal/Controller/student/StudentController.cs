﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Controller.DTO;
using TwelveFinal.Entities;
using TwelveFinal.Services.MStudentService;

namespace TwelveFinal.Controller.student
{
    public class StudentRoute : Root
    {
        public const string Default = Base + "/student";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Import = Default + "/import";
        public const string Get = Default + "/get";
        public const string List = Default + "/list";
        public const string MarkInput = Default + "/mark-input";
        public const string ViewMark = Default + "/view-mark";
    }
    public class StudentController : ApiController
    {
        private IStudentService StudentService;
        public StudentController(IStudentService StudentService)
        {
            this.StudentService = StudentService;
        }

        #region Create
        [Route(StudentRoute.Create), HttpPost]
        public async Task<ActionResult<RegisterDTO>> Create([FromBody] RegisterDTO registerDTO)
        {
            if (registerDTO == null) registerDTO = new RegisterDTO();

            Student student = new Student
            {
                Dob = registerDTO.Dob,
                Email = registerDTO.Email,
                Name = registerDTO.Name,
                EthnicId = registerDTO.EthnicId,
                Gender = registerDTO.Gender,
                Identify = registerDTO.Identify,
                Phone = registerDTO.Phone
            };
            student = await StudentService.Register(student);

            registerDTO = new RegisterDTO
            {
                Dob = student.Dob,
                Email = student.Email,
                Name = student.Name,
                EthnicId = student.EthnicId.Value,
                Gender = student.Gender,
                Identify = student.Identify,
                Phone = student.Phone,
                Errors = student.Errors
            };
            if (student.IsValidated)
                return registerDTO;
            else
            {
                return BadRequest(registerDTO);
            }
        }
        #endregion

        #region BulkInsert
        [Route(StudentRoute.Import), HttpPost]
        public async Task ImportExcel([FromForm]IFormFile file)
        {
            MemoryStream memoryStream = new MemoryStream();
            Request.Body.CopyTo(memoryStream);
            await this.StudentService.ImportExcel(memoryStream.ToArray());
        }
        #endregion

        #region Update
        [Route(StudentRoute.Update), HttpPost]
        public async Task<ActionResult<StudentDTO>> Update([FromBody] StudentDTO studentDTO)
        {
            if (studentDTO == null) studentDTO = new StudentDTO();

            Student student = new Student
            {
                Id = studentDTO.Id,
                Dob = studentDTO.Dob,
                Name = studentDTO.Name,
                Gender = studentDTO.Gender,
                Identify = studentDTO.Identify,
                Phone = studentDTO.Phone,
                Address = studentDTO.Address,
                EthnicId = studentDTO.EthnicId,
                HighSchoolId = studentDTO.HighSchoolId,
                PlaceOfBirth = studentDTO.PlaceOfBirth,
                ProvinceId = studentDTO.ProvinceId,
                DistrictId = studentDTO.DistrictId,
                TownId = studentDTO.TownId,
            };
            student = await StudentService.Update(student);

            studentDTO = new StudentDTO
            {
                Id = student.Id,
                Dob = student.Dob,
                Name = student.Name,
                Gender = student.Gender,
                Identify = student.Identify,
                Email = student.Email,
                Phone = student.Phone,
                Address = student.Address,
                EthnicId = student.EthnicId,
                EthnicCode = student.EthnicCode,
                EthnicName = student.EthnicName,
                HighSchoolId = student.HighSchoolId,
                HighSchoolCode = student.HighSchoolCode,
                HighSchoolName = student.HighSchoolName,
                PlaceOfBirth = student.PlaceOfBirth,
                ProvinceId = student.ProvinceId,
                ProvinceCode = student.ProvinceCode,
                ProvinceName = student.ProvinceName,
                DistrictId = student.DistrictId,
                DistrictCode = student.DistrictCode,
                DistrictName = student.DistrictName,
                TownId = student.TownId,
                TownCode = student.TownCode,
                TownName = student.TownName,
            };
            if (student.IsValidated)
                return studentDTO;
            else
            {
                return BadRequest(studentDTO);
            }
        }
        #endregion

        #region Get
        [Route(StudentRoute.Get), HttpPost]
        public async Task<StudentDTO> Get([FromBody] StudentDTO studentDTO)
        {
            if (studentDTO == null) studentDTO = new StudentDTO();
            Student student = new Student { Id = studentDTO.Id };
            student = await StudentService.Get(student.Id);

            return student == null ? null : new StudentDTO()
            {
                Id = student.Id,
                Address = student.Address,
                Dob = student.Dob,
                Gender = student.Gender,
                Email = student.Email,
                Identify = student.Identify,
                PlaceOfBirth = student.PlaceOfBirth,
                Name = student.Name,
                Phone = student.Phone,
                EthnicId = student.EthnicId,
                EthnicName = student.EthnicName,
                EthnicCode = student.EthnicCode,
                HighSchoolId = student.HighSchoolId,
                HighSchoolName = student.HighSchoolName,
                HighSchoolCode = student.HighSchoolCode,
                TownId = student.TownId,
                TownCode = student.TownCode,
                TownName = student.TownName,
                DistrictId = student.DistrictId,
                DistrictCode = student.DistrictCode,
                DistrictName = student.DistrictName,
                ProvinceId = student.ProvinceId,
                ProvinceCode = student.ProvinceCode,
                ProvinceName = student.ProvinceName,
            };
        }
        #endregion

        #region List
        [Route(StudentRoute.List), HttpPost]
        public async Task<List<StudentDTO>> List([FromForm] StudentFilterDTO studentFilterDTO)
        {
            StudentFilter studentFilter = new StudentFilter
            {
                Id = studentFilterDTO.Id,
                Identify = studentFilterDTO.Identify,
                Name = studentFilterDTO.Name,
                ProvinceId = studentFilterDTO.ProvinceId,
                HighSchoolId = studentFilterDTO.HighSchoolId,
                Gender = studentFilterDTO.Gender,
                Dob = studentFilterDTO.Dob,
                Status = studentFilterDTO.Status,
                Skip = studentFilterDTO.Skip,
                Take = studentFilterDTO.Take
            };

            var students = await StudentService.List(studentFilter);
            if (students == null) return new List<StudentDTO>();
            return students.Select(s => new StudentDTO
            {
                Id = s.Id,
                Address = s.Address,
                Dob = s.Dob,
                Gender = s.Gender,
                Email = s.Email,
                Identify = s.Identify,
                PlaceOfBirth = s.PlaceOfBirth,
                Name = s.Name,
                Phone = s.Phone,
                EthnicId = s.EthnicId,
                EthnicName = s.EthnicName,
                EthnicCode = s.EthnicCode,
                HighSchoolId = s.HighSchoolId,
                HighSchoolName = s.HighSchoolName,
                HighSchoolCode = s.HighSchoolCode,
                TownId = s.TownId,
                TownCode = s.TownCode,
                TownName = s.TownName,
                DistrictId = s.DistrictId,
                DistrictCode = s.DistrictCode,
                DistrictName = s.DistrictName,
                ProvinceId = s.ProvinceId,
                ProvinceCode = s.ProvinceCode,
                ProvinceName = s.ProvinceName,
            }).ToList();
        }
        #endregion

        #region Mark Input
        [Route(StudentRoute.MarkInput), HttpPost]
        public async Task<ActionResult<MarkDTO>> MarkInput([FromBody] MarkDTO markInputDTO)
        {
            if (markInputDTO == null) markInputDTO = new MarkDTO();

            Student student = new Student
            {
                Id = markInputDTO.StudentId,
                Biology = markInputDTO.Biology,
                Chemistry = markInputDTO.Chemistry,
                CivicEducation = markInputDTO.CivicEducation,
                Geography = markInputDTO.Geography,
                History = markInputDTO.History,
                Languages = markInputDTO.Languages,
                Literature = markInputDTO.Literature,
                Maths = markInputDTO.Maths,
                Physics = markInputDTO.Physics,
            };

            student = await StudentService.Update(student);

            markInputDTO = new MarkDTO
            {
                StudentId = student.Id,
                Identify = student.Identify,
                Email = student.Email,
                Biology = student.Biology,
                Chemistry = student.Chemistry,
                CivicEducation = student.CivicEducation,
                Geography = student.Geography,
                History = student.History,
                Languages = student.Languages,
                Literature = student.Literature,
                Maths = student.Maths,
                Physics = student.Physics,
                Errors = student.Errors
            };

            if (student.IsValidated)
            {
                return Ok(markInputDTO);
            }
            else
            {
                return BadRequest(markInputDTO);
            }
        }
        #endregion

        //#region View Mark
        //[Route(StudentRoute.ViewMark), HttpPost]
        //public async Task<MarkDTO> ViewMark([FromBody] StudentDTO studentDTO)
        //{
        //    if (studentDTO == null) studentDTO = new StudentDTO();

        //}
        //#endregion
    }
}
