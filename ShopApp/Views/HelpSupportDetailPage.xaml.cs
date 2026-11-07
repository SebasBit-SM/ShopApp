using ShopApp.ViewModels;

namespace ShopApp.Views;

public partial class HelpSupportDetailPage : ContentPage, IQueryAttributable
{
    public HelpSupportDetailPage()
    {
        InitializeComponent();
    }

    public void ApplyQueryAttributes(
        IDictionary<string, object> query)
    {
        Title = $"Cliente: {query["id"]}";

        var clientId = int.Parse(query["id"].ToString());

        if (BindingContext is HelpSupportDetailViewModel viewModel)
        {
            viewModel.ClienteId = clientId;
        }
    }
}