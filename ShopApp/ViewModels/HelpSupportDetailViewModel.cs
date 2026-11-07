
using ShopApp.DataAcces;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ShopApp.ViewModels;

public partial class HelpSupportDetailViewModel
    : ViewModelGlobal, IQueryAttributable
{
    // Propiedades observables

    [ObservableProperty]
    private ObservableCollection<Compra> compras = new();

    [ObservableProperty]
    private int clienteId;

    [ObservableProperty]
    private ObservableCollection<Product> products = new();

    [ObservableProperty]
    private Product? productoSeleccionado;

    [ObservableProperty]
    private int cantidad = 1;


    // Constructor

    public HelpSupportDetailViewModel()
    {
        using var database = new ShopDbContext();

        Products = new ObservableCollection<Product>(
            database.Products.ToList()
        );

        AddCommand = new MiComando(() =>
        {
            if (ProductoSeleccionado == null)
                return;

            var compra = new Compra(
                ClienteId,
                ProductoSeleccionado.Id,
                Cantidad
            );

            Compras.Add(compra);
        },
        () => true);
    }


    // Comando para agregar compras

    public ICommand AddCommand { get; }


    // Captura de parámetros de navegación

    public void ApplyQueryAttributes(
        IDictionary<string, object> query)
    {
        if (query.TryGetValue("id", out var valorId)
            && int.TryParse(valorId?.ToString(), out int id))
        {
            ClienteId = id;
        }
    }
}
