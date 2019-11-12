using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Controller.form;
using TwelveFinal.Controller.majors;
using TwelveFinal.Controller.subject_group;
using TwelveFinal.Controller.university;
using TwelveFinal.Controller.university_majors;

namespace TwelveFinal.Controller
{
    public class Root
    {
        public const string Base = "api/TF";
    }

    public class AdminRoute : Root
    {
        public const string GetForm = FormRoute.Get;
        public const string ApproveForm = FormRoute.ApproveAccept;
        public const string DeleteForm = FormRoute.Delete;

        public const string CreateMajors = MajorsRoute.Create;
        public const string GetMajors = MajorsRoute.Get;
        public const string ListMajors = MajorsRoute.List;
        public const string UpdateMajors = MajorsRoute.Update;
        public const string DeleteMajors = MajorsRoute.Delete;

        public const string CreateSubjectGroup = SubjectGroupRoute.Create;
        public const string GetSubjectGroup = SubjectGroupRoute.Get;
        public const string ListSubjectGroup = SubjectGroupRoute.List;
        public const string UpdateSubjectGroup = SubjectGroupRoute.Update;
        public const string DeleteSubjectGroup = SubjectGroupRoute.Delete;

        public const string CreateUniversity = UniversityRoute.Create;
        public const string GetUniversity = UniversityRoute.Get;
        public const string ListUniversity = UniversityRoute.List;
        public const string UpdateUniversity = UniversityRoute.Update;
        public const string DeleteUniversity = UniversityRoute.Delete;

        public const string CreateUniversity_Majors = University_MajorsRoute.Create;
        public const string GetUniversity_Majors = University_MajorsRoute.Get;
        public const string ListUniversity_Majors = University_MajorsRoute.List;
        public const string UpdateUniversity_Majors = University_MajorsRoute.Update;
        public const string DeleteUniversity_Majors = University_MajorsRoute.Delete;

        public const string CreateStudent = TwelveFinal.Controller.student.StudentRoute.Create;
        public const string ImportStudent = TwelveFinal.Controller.student.StudentRoute.Import;
        public const string GetStudent = TwelveFinal.Controller.student.StudentRoute.Get;
        public const string ListStudent = TwelveFinal.Controller.student.StudentRoute.List;
        public const string MarkInputStudent = TwelveFinal.Controller.student.StudentRoute.MarkInput;
    }

    public class StudentRoute : Root
    {

    }

    //[Authorize]
    //[Authorize(Policy = "Permission")]
    public class ApiController : ControllerBase
    {

    }
}
