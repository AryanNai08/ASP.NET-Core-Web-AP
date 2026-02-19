using CollegeApi.Data;

namespace CollegeApi.Data.Repository
{
    public interface IStudentRepository : ICollegeRepository<Student>
    {
        Task<List<Student>> GetStudentsByFeesStatus(int feeStatus);

    }
}
