using Baghiut_Andreea_Lab7.Models;

namespace Baghiut_Andreea_Lab7;

public partial class ListEntryPage : ContentPage
{
    public ListEntryPage()
    {
        InitializeComponent();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        listView.ItemsSource = await App.Database.GetShopListsAsync();
    }
    async void OnShopListAddedClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ListPage
        {
            BindingContext = new ShopList()
        });
    }
    async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            await Navigation.PushAsync(new ListPage
            {
                BindingContext = e.SelectedItem as ShopList
            });
        }
    }
    async void OnDeleteItemClicked(object sender, EventArgs e)
    {
        // Obțineți produsul selectat sau implementați logica pentru a obține produsul pe care doriți să-l ștergeți
        var selectedProduct = listView.SelectedItem as ListProduct;

        if (selectedProduct != null)
        {
            // Apelați metoda de ștergere a produsului din baza de date
            await App.Database.DeleteListProductAsync(selectedProduct);

            // Actualizați sursa de date pentru a reflecta modificările
            var shopList = (ShopList)BindingContext;
            listView.ItemsSource = await App.Database.GetListProductsAsync(shopList.ID);
        }
    }
}
