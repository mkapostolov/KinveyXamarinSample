using System;
using System.Threading.Tasks;
using XamarinSampleForms.Models;

namespace XamarinSampleForms.Services
{
    public interface IBooksService
    {
        Task<System.Collections.Generic.List<Book>> FindAsync(string searchText, int skip);
        Task SaveItem(Book updatedBook);
        Task DeleteItem(Book itemToDelete);
    }

    public interface IAuthService
    {
        void LogoutUser();
        bool ActiveUserExists();
        Task<string> RegisterUser(string username, string password);
        Task<string> LoginUser(string username, string password);
     }
}
