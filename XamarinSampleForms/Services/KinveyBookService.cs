using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kinvey;
using XamarinSampleForms.Models;

namespace XamarinSampleForms.Services
{
    public class KinveyBookService : IBooksService
    {
        private Client kinveyClient;
        private const string booksCollection = "books";
        DataStore<Book> bookStore;

        public KinveyBookService()
        {
            kinveyClient = KinveyService.Instance().kinveyClient;
            bookStore = DataStore<Book>.Collection(booksCollection, DataStoreType.AUTO);
        }


        public async Task<List<Book>> FindAsync(string searchText, int skip)
        {
            var q = bookStore
                .OrderByDescending(x => x.ID)
                .Skip(skip)
                .Take(50);

            if (!string.IsNullOrEmpty(searchText))
                q = (System.Linq.IOrderedQueryable<XamarinSampleForms.Models.Book>)bookStore.Where(x => x.Title.StartsWith(searchText, StringComparison.Ordinal));

            try
            {
                return await bookStore.FindAsync(q);
            }
            catch (KinveyException e)
            {
                throw new System.Exception(e.Message);
            }
        }

        public async Task SaveItem(Book updatedBook)
        {
            try
            {
                var book = await bookStore.SaveAsync(updatedBook);
            }
            catch (KinveyException ke)
            {
                throw new Exception(ke.Message);
            }
        }

        public async Task DeleteItem(Book itemToDelete)
        {
            try
            {
                await bookStore.RemoveAsync(itemToDelete.ID);
            }
            catch (KinveyException ke)
            {
                throw new Exception(ke.Message);
            }
        }

    }
}
