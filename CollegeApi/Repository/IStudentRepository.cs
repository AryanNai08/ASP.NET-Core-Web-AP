using CollegeApi.Data;

namespace CollegeApi.Repository
{
    public interface IStudentRepository : ICollegeRepository<Student>
    {
        Task<List<Student>> GetStudentsByFeesStatus(int feeStatus);

    }
}
