using AiTopStudentStatus.Api.Models;
using Newtonsoft.Json;
using System.Runtime.Caching;

namespace AiTopStudentStatus.Api.Cache
{
    public class CacheTemp
    {
        public void SetObject(Student student)
        {
            var existingStudent = MemoryCache.Default.GetCacheItem(student.StudentId.ToString());

            if (existingStudent != null)
                MemoryCache.Default.Remove(student.StudentId.ToString());

            if (!string.IsNullOrWhiteSpace(student.ClassId))
                MemoryCache.Default.Set(student.StudentId.ToString(), student, new DateTimeOffset(DateTime.Now.AddDays(7)));
        }

        public Student[] GetValues(string classId)
        {
            var students = GetAll();

            if (!students.Any())
                return students.ToArray();

            return students.Where(x => x.ClassId == classId).ToArray();
        }

        public Student[] GetAll()
        {
            var students = new List<Student>();

            if (!MemoryCache.Default.Any())
                return students.ToArray();

            foreach (var item in MemoryCache.Default)
                students.Add(item.Value as Student);

            return students.ToArray();
        }

        public bool CheckForUpdate(string classId, string listHash)
        {
            var students = new List<Student>();
            Student[] studentsForClass = Array.Empty<Student>();

            if (MemoryCache.Default.Any())
            {
                foreach (var item in MemoryCache.Default)
                    students.Add(item.Value as Student);

                studentsForClass = students.Where(x => x.ClassId == classId).ToArray();
            }

            string s = JsonConvert.SerializeObject(studentsForClass);
            var newListHash = GetDeterministicHashCode(s);

            return !listHash.Equals(newListHash);
        }

        public bool DeleteObject(string studentId)
        {
            try
            {
                MemoryCache.Default.Remove(studentId);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool DeleteAll()
        {
            try
            {
                var cacheKeys = MemoryCache.Default.Select(kvp => kvp.Key).ToList();
                foreach (string cacheKey in cacheKeys)
                    MemoryCache.Default.Remove(cacheKey);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private string GetDeterministicHashCode(string str)
        {
            unchecked
            {
                int hash1 = (5381 << 16) + 5381;
                int hash2 = hash1;

                for (int i = 0; i < str.Length; i += 2)
                {
                    hash1 = ((hash1 << 5) + hash1) ^ str[i];
                    if (i == str.Length - 1)
                        break;
                    hash2 = ((hash2 << 5) + hash2) ^ str[i + 1];
                }

                return (hash1 + (hash2 * 1566083941)).ToString();
            }
        }
    }
}