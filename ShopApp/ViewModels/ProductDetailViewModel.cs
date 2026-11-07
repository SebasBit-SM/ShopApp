
using CommunityToolkit.Mvvm.ComponentModel;
using ShopApp.DataAcces;

namespace ShopApp.ViewModels;

public partial class ProductDetailViewModel : ViewModelGlobal
{
    // Propiedades observables

    [ObservableProperty]
    private string nombre = string.Empty;

    [ObservableProperty]
    private string descripcion = string.Empty;

    [ObservableProperty]
    private decimal precio;

    [ObservableProperty]
    private string imagen = string.Empty;


    // Método para cargar la información del producto

    public void CargarProducto(int id)
    {
        using var database = new ShopDbContext();

        var producto = database.Products
            .FirstOrDefault(x => x.Id == id);

        if (producto != null)
        {
            Nombre = producto.Nombre;
            Descripcion = producto.Descripcion;
            Precio = producto.Precio;
            Imagen = producto.Imagen;
        }
        else
        {
            Nombre = "Producto no encontrado";
            Descripcion = string.Empty;
            Precio = 0;
            Imagen = string.Empty;
        }
    }
}
