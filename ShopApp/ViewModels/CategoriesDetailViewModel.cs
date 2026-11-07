using ShopApp.DataAcces;
using System.Linq;

namespace ShopApp.ViewModels;

public class CategoriesDetailViewModel : BindingUtilObjects
{
    private string _nombreCategoria = string.Empty;

    public string NombreCategoria
    {
        get { return _nombreCategoria; }
        set
        {
            if (_nombreCategoria != value)
            {
                _nombreCategoria = value;
                RaisePropertyChanged();
            }
        }
    }

    public void CargarCategoria(int id)
    {
        using var dbContext = new ShopDbContext();

        var categoria = dbContext.Categories
            .FirstOrDefault(x => x.Id == id);

        if (categoria != null)
        {
            NombreCategoria = categoria.Nombre;
        }
        else
        {
            NombreCategoria = "Categoría no encontrada";
        }
    }
}
