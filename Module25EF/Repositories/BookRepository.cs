using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module25EF.Repositories
{
    public class BookRepository
    {
        private readonly AppContext _context;
        public BookRepository(AppContext context)
        {
            _context = context;
        }
        public Book GetById(int id)//выбор объекта из БД по его идентификатору
        {
            return _context.Books.FirstOrDefault(b => b.Id == id);
        }
        public List<Book> GetAll()//выбор всех объектов
        {
            return _context.Books.ToList();
        }
        public void Add(Book book)//добавление объекта в БД
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }
        public void Delete(Book book)// удаление из БД
        {
            _context.Books.Remove(book);
            _context.SaveChanges();
        }
        public void UpdatePublishYear(int id, int publishYear)//обновление года выпуска книги (по Id)
        {
            var book = GetById(id);
            if (book != null)
            {
                book.PublishYear = publishYear;
                _context.SaveChanges();
            }
        }
        //25.5.4
        public List<Book> GetByGenreAndPublishYearRange(string genre, int startYear, int endYear)// Получать список книг определенного жанра и вышедших между определенными годами.
        {
            return _context.Books
                .Where(b => b.Genre != null && b.Genre.Name == genre && b.PublishYear >= startYear && b.PublishYear <= endYear)
                .ToList();
        }
        public int GetCountByAuthor(string authorName)
        {
            return _context.Books.Count(b => b.Author != null && b.Author.Name == authorName); // Получать количество книг определенного автора в библиотеке.
        }
        public int GetCountByGenre(string genreName)// Получать количество книг определенного жанра в библиотеке.
        {
            return _context.Books.Count(b => b.Genre != null && b.Genre.Name == genreName);
        }
        public bool HasBookByAuthorAndTitle(string authorName, string title)// Получать булевый флаг о том, есть ли книга определенного автора и с определенным названием в библиотеке.
        {
            return _context.Books.Any(b => b.Author != null && b.Author.Name == authorName && b.Title == title);
        }
        public bool IsBookIssuedToUser(int bookId, int userId)// Получать булевый флаг о том, есть ли определенная книга на руках у читателя.
        {
            return _context.Books.Any(b => b.Id == bookId && b.UserId == userId);
        }
        public int GetIssuedBookCountForUser(int userId)// Получать количество книг на руках у читателя.
        {
            return _context.Books.Count(b => b.UserId == userId);
        }
        public Book GetLatestBook()// Получение последней вышедшей книги.
        {
            return _context.Books.OrderByDescending(b => b.PublishYear).FirstOrDefault();
        }
        public List<Book> GetAllSortedByTitle()// Получение списка всех книг, отсортированного в алфавитном порядке по названию.
        {
            return _context.Books.OrderBy(b => b.Title).ToList();
        }
        public List<Book> GetAllSortedByPublishYearDescending()// Получение списка всех книг, отсортированного в порядке убывания года их выхода.
        {
            return _context.Books.OrderByDescending(b => b.PublishYear).ToList();
        }
    }
}