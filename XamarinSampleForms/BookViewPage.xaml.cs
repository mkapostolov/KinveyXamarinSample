using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Xamarin.Forms;
using XamarinSampleForms.Models;
using XamarinSampleForms.Services;

namespace XamarinSampleForms
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class BookViewPage : ContentPage
    {
        public ObservableCollection<Book> _books = new ObservableCollection<Book>();
        private Lazy<IBooksService> lazyBooksService = new Lazy<IBooksService>(() => DependencyService.Get<IBooksService>());
        private IBooksService bookService
        {
            get
            {
                return lazyBooksService.Value;
            }
        }

        private bool isLoading;

        public BookViewPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {

            await LoadItems();
            listView.ItemsSource = _books;

            listView.ItemAppearing += (sender, e) =>
            {
                if (isLoading || _books.Count == 0)
                    return;

                //hit bottom!
                if (e.ItemIndex == _books.Count - 20)
                {
                    LoadItems(clear: false, skip: _books.Count);
                }
            };
        }

        public async Task LoadItems(string searchText = null, bool clear = true, int skip = 0)
        {
            isLoading = true;
            if (clear)
                _books.Clear();
            var items = await bookService.FindAsync(searchText, skip);
            foreach (var item in items)
            {
                _books.Add(item);
            }
            isLoading = false;
        }

        async void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var book = e.SelectedItem as Book;
            await Navigation.PushAsync(new BookDetailViewPage(book));
            listView.SelectedItem = null;
        }

        async void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            await LoadItems(e.NewTextValue);
        }

        async void Handle_DeleteItem(object sender, System.EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var bookToDelete = menuItem.CommandParameter as Book;

            try
            {
                await bookService.DeleteItem(bookToDelete);
                UserDialogs.Instance.Toast("Item deleted");
                _books.Remove(bookToDelete);
            }
            catch (Exception error)
            {
                await DisplayAlert("Item not deleted", error.Message, "OK");
            }
        }

        async void Handle_AddItem(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new BookDetailViewPage(new Book()));
        }

        async void Handle_Refreshing(object sender, System.EventArgs e)
        {
            await LoadItems();
            listView.EndRefresh();
        }
    }
}
