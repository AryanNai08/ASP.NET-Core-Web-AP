using CollegeApi.Data;

namespace CollegeApi.Repository
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAllAsync();

        Task<Student> GetByIDAsync(int id);

        Task<Student> GetByNameAsync(string name);

        Task<int> CreateAsync(Student student);

        Task<int> UpdateAsync(Student student);

        Task<bool> DeleteAsync(int id);

    }
}
