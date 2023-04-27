using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module25EF.Repositories
{
    public class UserRepository
    {
        private readonly AppContext _context;

        public UserRepository(AppContext context)
        {
            _context = context;
        }

        public User GetById(int id)//выбор объекта из БД по его идентификатору
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public List<User> GetAll()// выбор всех объектов
        {
            return _context.Users.ToList();
        }

        public void Add(User user)//добавление объекта в БД
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Delete(User user)//его удаление из БД
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public void UpdateName(int id, string name)//обновление имени читателя (по Id) 
        {
            var user = GetById(id);
            if (user != null)
            {
                user.Name = name;
                _context.SaveChanges();
            }
        }
    }
}