using CollegeApi.Data;
using Microsoft.EntityFrameworkCore;

namespace CollegeApi.Repository
{
    public class StudentRepository :CollegeRepository<Student>,  IStudentRepository
    {
        private readonly CollegeDBContext _dbContext;
        public StudentRepository(CollegeDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

         public Task<List<Student>> GetStudentsByFeesStatus(int feeStatus)
        {
            return null;
        }


    }
}
