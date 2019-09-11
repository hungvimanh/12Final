using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Repositories.Models;
using TwelveFinal.Services.MGraduation;
using Z.EntityFramework.Extensions;

namespace TwelveFinal.Repositories
{
    public interface IUOW : IServiceScoped
    {
        Task Begin();
        Task Commit();
        Task Rollback();
        IDistrictRepository DistrictRepository { get; }
        IFormRepository FormRepository { get; }
        IGraduationInformationRepository GraduationInformationRepository { get; }
        IHighSchoolRepository HighSchoolRepository { get; }
        IMajorsRepository MajorsRepository { get; }
        IProvinceRepository ProvinceRepository { get; }
        IRegisterInformationRepository RegisterInformationRepository { get; }
        IPersonalInformationRepository PersonalInformationRepository { get; }
        ITownRepository TownRepository { get; }
        IUniversity_MajorsRepository University_MajorsRepository { get; }
        IUniversityAdmissionRepository UniversityAdmissionRepository { get; }
        IUniversityRepository UniversityRepository { get; }
        IUserRepository UserRepository { get; }
    }
    public class UOW : IUOW
    { 
        private TFContext tFContext;
        public IDistrictRepository DistrictRepository { get; private set; }
        public IFormRepository FormRepository { get; private set; }
        public IGraduationInformationRepository GraduationInformationRepository { get; private set; }
        public IHighSchoolRepository HighSchoolRepository { get; private set; }
        public IMajorsRepository MajorsRepository { get; private set; }
        public IProvinceRepository ProvinceRepository { get; private set; }
        public IRegisterInformationRepository RegisterInformationRepository { get; private set; }
        public IPersonalInformationRepository PersonalInformationRepository { get; private set; }
        public ITownRepository TownRepository { get; private set; }
        public IUniversity_MajorsRepository University_MajorsRepository { get; private set; }
        public IUniversityAdmissionRepository UniversityAdmissionRepository { get; private set; }
        public IUniversityRepository UniversityRepository { get; private set; }
        public IUserRepository UserRepository { get; private set; }

        public UOW(TFContext _tFContext)
        {
            tFContext = _tFContext;
            DistrictRepository = new DistrictRepository(tFContext);
            FormRepository = new FormRepository(tFContext);
            GraduationInformationRepository = new GraduationInformationRepository(tFContext);
            HighSchoolRepository = new HighSchoolRepository(tFContext);
            MajorsRepository = new MajorsRepository(tFContext);
            ProvinceRepository = new ProvinceRepository(tFContext);
            RegisterInformationRepository = new RegisterInformationRepository(tFContext);
            PersonalInformationRepository = new PersonalInformationRepository(tFContext);
            TownRepository = new TownRepository(tFContext);
            University_MajorsRepository = new University_MajorsRepository(tFContext);
            UniversityAdmissionRepository = new UniversityAdmissionRepository(tFContext);
            UniversityRepository = new UniversityRepository(tFContext);
            UserRepository = new UserRepository(tFContext);
            EntityFrameworkManager.ContextFactory = DbContext => tFContext;
        }

        public async Task Begin()
        {
            await tFContext.Database.BeginTransactionAsync();
        }

        public Task Commit()
        {
            tFContext.Database.CommitTransaction();
            return Task.CompletedTask;
        }

        public Task Rollback()
        {
            tFContext.Database.RollbackTransaction();
            return Task.CompletedTask;
        }
    }
}
