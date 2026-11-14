
using ShopApp.DataAcces;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;

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


    // Base de datos SQLite

    private readonly ShopOutDbContext _outDbContext;


    // Constructor

    public HelpSupportDetailViewModel(
        ShopOutDbContext outDbContext)
    {
        _outDbContext = outDbContext;

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

        EnviarCompraCommand = new MiComando(async () =>
        {
            await EnviarCompra();
        },
        () => true);
    }


    // Comandos

    public ICommand AddCommand { get; }

    public ICommand EnviarCompraCommand { get; }


    // Guardar compras en SQLite


    private async Task EnviarCompra()
    {
        if (Compras.Count == 0)
        {
            await Shell.Current.DisplayAlert(
                "Aviso",
                "No hay productos para guardar.",
                "OK");

            return;
        }

        foreach (var item in Compras)
        {
            // Buscar el producto para obtener su precio
            var producto = Products.FirstOrDefault(
                p => p.Id == item.ProductId);

            if (producto == null)
                continue;

            // Crear el registro que se guardará en SQLite
            var compraItem = new CompraItem(
                item.ClienteId,
                item.ProductId,
                item.Cantidad,
                producto.Precio
            );

            _outDbContext.Compras.Add(compraItem);
        }

        // Guardar los cambios en SQLite
        await _outDbContext.SaveChangesAsync();

        await Shell.Current.DisplayAlert(
            "Éxito",
            "Compra guardada localmente en SQLite.",
            "OK");
    }



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
