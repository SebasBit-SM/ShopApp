
using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using ShopApp.DataAcces;
using ShopApp.Service;
using ShopApp.Views;

namespace ShopApp.ViewModels;

public partial class HelpSupportViewModel : ViewModelGlobal
{
    // Propiedades observables

    [ObservableProperty]
    private int visitasPendientes;

    [ObservableProperty]
    private ObservableCollection<Client> clients = new();

    [ObservableProperty]
    private Client? clienteSeleccionado;


    // Servicio de navegación

    private readonly INavegacionService _navegacionService;


    // Constructor

    public HelpSupportViewModel(INavegacionService navegacionService)
    {
        _navegacionService = navegacionService;

        using var database = new ShopDbContext();

        Clients = new ObservableCollection<Client>(
            database.Clients.ToList()
        );

        PropertyChanged += HelpSupportData_PropertyChanged;
    }


    // Evento para detectar cambios en el cliente seleccionado

    private async void HelpSupportData_PropertyChanged(
        object? sender,
        PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ClienteSeleccionado)
            && ClienteSeleccionado != null)
        {
            var uri =
                $"{nameof(HelpSupportDetailPage)}?id={ClienteSeleccionado.Id}";

            await _navegacionService.GoToAsync(uri);
        }
    }
}
