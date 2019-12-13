using System;
using Acr.UserDialogs;
using Xamarin.Forms;
using XamarinSampleForms.Models;
using XamarinSampleForms.Services;

namespace XamarinSampleForms
{
    public partial class BookDetailViewPage : ContentPage
    {
        private Lazy<IBooksService> lazyBooksService = new Lazy<IBooksService>(() => DependencyService.Get<IBooksService>());
        private IBooksService bookService
        {
            get
            {
                return lazyBooksService.Value;
            }
        }

        public BookDetailViewPage(Book book)
        {
            if (book == null)
                throw new ArgumentNullException();

            BindingContext = book;

            InitializeComponent();
        }

        public async void Handle_SaveItem(object sender, System.EventArgs e)
        {
            var updatedBook = BindingContext as Book;

            try
            {
                await bookService.SaveItem(updatedBook);
                UserDialogs.Instance.Toast("Item saved");
                await Navigation.PopAsync();
            }
            catch (Exception error)
            {
                await DisplayAlert("Item save failure", error.Message, "OK");
            }
        }
    }
}
