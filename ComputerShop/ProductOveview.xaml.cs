using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ComputerShop
{
    /// <summary>
    /// Interaction logic for ProductOveview.xaml
    /// </summary>
    public partial class ProductOveview : UserControl
    {
        public ProductOveview()
        {
            InitializeComponent();
            updateListView();
            List<string> sortProduct = new List<string>()
            {
                "A->Z",
                "Z->A",
                "Price ↑",
                "Price ↓",
                "Instock ↑",
                "Instock ↓"
            };
            cmbProduct.ItemsSource = sortProduct;
        }
        private void updateListView()
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {

                var company = ctx.Products.Select(x => new
                {
                    Id = x.ID,
                    Name = x.Naam,
                    Purchaseprice = x.Inkoopprijs,
                    Margin = x.Marge,
                    Unit = x.Eenheid,
                    Btw = x.BTW,
                    Leverancie = x.Leverancier.Company,
                    Category = x.Categorie.CategorieNaam,
                    Instock = x.InStock,
                    Foto = x.Foto,
                    Specification = x.Specifications
                });

                productListView.SelectedValuePath = "Id";
                productListView.ItemsSource = company.ToList();
            }
        }
        private void updateFilter()
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                switch (cmbProduct.SelectedItem)
                {
                    case "A->Z":
                        var productQuery = ctx.Products.Select(x => new
                        {
                            Id = x.ID,
                            Name = x.Naam,
                            Purchaseprice = x.Inkoopprijs,
                            Margin = x.Marge,
                            Unit = x.Eenheid,
                            Btw = x.BTW,
                            Leverancie = x.Leverancier.Company,
                            Category = x.Categorie.CategorieNaam,
                            Instock = x.InStock,
                            Foto = x.Foto,
                            Specification = x.Specifications
                        }).OrderBy(x => x.Name);
                        productListView.ItemsSource = productQuery.ToList();
                        break;
                    case "Z->A":
                        var productZA = ctx.Products.Select(x => new
                        {

                            Id = x.ID,
                            Name = x.Naam,
                            Purchaseprice = x.Inkoopprijs,
                            Margin = x.Marge,
                            Unit = x.Eenheid,
                            Btw = x.BTW,
                            Leverancie = x.Leverancier.Company,
                            Category = x.Categorie.CategorieNaam,
                            Instock = x.InStock,
                            Foto = x.Foto,
                            Specification = x.Specifications
                        }).OrderByDescending(x => x.Name);
                        productListView.ItemsSource = productZA.ToList();
                        break;
                    case "Price ↑":
                        var productPric = ctx.Products.Select(x => new
                        {
                            Id = x.ID,
                            Name = x.Naam,
                            Purchaseprice = x.Inkoopprijs,
                            Margin = x.Marge,
                            Unit = x.Eenheid,
                            Btw = x.BTW,
                            Leverancie = x.Leverancier.Company,
                            Category = x.Categorie.CategorieNaam,
                            Instock = x.InStock,
                            Foto = x.Foto,
                            Specification = x.Specifications
                        }).OrderBy(x => x.Purchaseprice);
                        productListView.ItemsSource = productPric.ToList();
                        break;
                    case "Price ↓":
                         var productPric1 = ctx.Products.Select(x => new
                         {
                             Id = x.ID,
                             Name = x.Naam,
                             Purchaseprice = x.Inkoopprijs,
                             Margin = x.Marge,
                             Unit = x.Eenheid,
                             Btw = x.BTW,
                             Leverancie = x.Leverancier.Company,
                             Category = x.Categorie.CategorieNaam,
                             Instock = x.InStock,
                             Foto = x.Foto,
                             Specification = x.Specifications
                         }).OrderByDescending(x => x.Purchaseprice);
                        productListView.ItemsSource = productPric1.ToList();
                        break;
                    case "Instock ↑":
                        var productinStock = ctx.Products.Select(x => new
                        {
                            Id = x.ID,
                            Name = x.Naam,
                            Purchaseprice = x.Inkoopprijs,
                            Margin = x.Marge,
                            Unit = x.Eenheid,
                            Btw = x.BTW,
                            Leverancie = x.Leverancier.Company,
                            Category = x.Categorie.CategorieNaam,
                            Instock = x.InStock,
                            Foto = x.Foto,
                            Specification = x.Specifications
                        }).OrderBy(x => x.Instock);
                        productListView.ItemsSource = productinStock.ToList();
                        break;
                    case "Instock ↓":
                        var productinStock1 = ctx.Products.Select(x => new
                        {
                            Id = x.ID,
                            Name = x.Naam,
                            Purchaseprice = x.Inkoopprijs,
                            Margin = x.Marge,
                            Unit = x.Eenheid,
                            Btw = x.BTW,
                            Leverancie = x.Leverancier.Company,
                            Category = x.Categorie.CategorieNaam,
                            Instock = x.InStock,
                            Foto = x.Foto,
                            Specification = x.Specifications
                        }).OrderByDescending(x => x.Instock);
                        productListView.ItemsSource = productinStock1.ToList();
                        break;
                    
                }
            }
        }

        private void cmbProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateFilter();
        }
    }
}
