
using ShopApp.DataAcces;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Linq;

namespace ShopApp.ViewModels;

public partial class ClientsDetailViewModel : ViewModelGlobal
{
    // Propiedades observables

    [ObservableProperty]
    private string nombreCliente = string.Empty;

    [ObservableProperty]
    private string direccionCliente = string.Empty;


    // Método para cargar los datos del cliente

    public void CargarCliente(int id)
    {
        using var database = new ShopDbContext();

        var cliente = database.Clients
            .FirstOrDefault(x => x.Id == id);

        if (cliente != null)
        {
            NombreCliente = cliente.Nombre;
            DireccionCliente = cliente.Direccion;
        }
        else
        {
            NombreCliente = "Cliente no encontrado";
            DireccionCliente = string.Empty;
        }
    }
}
