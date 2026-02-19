using CollegeApi.Data;
using Microsoft.EntityFrameworkCore;

namespace CollegeApi.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly CollegeDBContext _dbContext;
        public StudentRepository(CollegeDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> CreateAsync(Student student)
        {
          await  _dbContext.Students.AddAsync(student);
          await  _dbContext.SaveChangesAsync();
            return student.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var studentToDelete = await _dbContext.Students.Where(student => student.Id == id).FirstOrDefaultAsync();
            if (studentToDelete == null)
            {
                throw new ArgumentNullException($"no student found with id:{id}");
            }

            _dbContext.Students.Remove(studentToDelete);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Student>> GetAllAsync()
        {
           return await _dbContext.Students.ToListAsync();
        }

        public async Task<Student> GetByIDAsync(int id)
        {
            return  await _dbContext.Students.Where(student => student.Id == id).FirstOrDefaultAsync();

           
        }

        public async Task<Student> GetByNameAsync(string name)
        {
            return  await _dbContext.Students
                 .Where(x => x.Studentname.ToLower().Contains(name.ToLower()))
                 .FirstOrDefaultAsync();
        }

        public async Task<int> UpdateAsync(Student student)
        {
          var studentToUpdate =  await _dbContext.Students.Where(s => s.Id == student.Id).FirstOrDefaultAsync();

            if (studentToUpdate == null)
            {
                throw new ArgumentException($"no student found with id:{student.Id}");
            }

            studentToUpdate.Studentname = student.Studentname;
            studentToUpdate.Email = student.Email;
            studentToUpdate.Address = student.Address;
            studentToUpdate.DOB = student.DOB;

            await _dbContext.SaveChangesAsync();

            return student.Id;

        }
    }
}
