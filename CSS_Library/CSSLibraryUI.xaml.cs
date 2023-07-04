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
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Windows;
using Autodesk.AutoCAD.Windows.ToolPalette;
using CSS_Library;
namespace CSS_Library
{
    /// <summary>
    /// Interaction logic for CSSLibraryUI.xaml
    /// </summary>
    /// 
    
    public partial class CSSLibraryUI : UserControl
    {
       
        public CSSLibraryUI()
        {
            InitializeComponent();
            var products = GetProducts();
            if (products.Count > 0)
                ListViewProduct.ItemsSource = products;
        }
        private List<Product> GetProducts()
        {
            
            return new List<Product>()
            {
                new Product("Detail 1",100,"D:\\4.PERSONAL\\5.CODING\\6.1CSS_Cad_Library\\CSS_CAD LIB\\CSS_Library\\DataImage\\TYP. FPS COATING - JOIST.png"),
                new Product("Detail 1",100,"D:\\4.PERSONAL\\5.CODING\\6.1CSS_Cad_Library\\CSS_CAD LIB\\CSS_Library\\DataImage\\TYP. FPS COATING - SLAB.png"),
                new Product("Detail 1",100,"D:\\4.PERSONAL\\5.CODING\\5.CSS_Library_Cad\\Database\\ANCHOR DETAIL\\Image\\ANCHOR DETAIL - 1.png"),
                new Product("Detail 1",100,"D:\\4.PERSONAL\\5.CODING\\5.CSS_Library_Cad\\Database\\ANCHOR DETAIL\\Image\\ANCHOR DETAIL - 2.png"),
                new Product("Detail 1",100,"D:\\4.PERSONAL\\5.CODING\\5.CSS_Library_Cad\\Database\\ANCHOR DETAIL\\Image\\ANCHOR DETAIL - 3.png"),
                new Product("Detail 1",100,"D:\\4.PERSONAL\\5.CODING\\5.CSS_Library_Cad\\Database\\ANCHOR DETAIL\\Image\\ANCHOR DETAIL - 4.png"),
                new Product("Detail 1",100,"D:\\4.PERSONAL\\5.CODING\\5.CSS_Library_Cad\\Database\\ANCHOR DETAIL\\Image\\ANCHOR DETAIL - 5.png"),
                new Product("Detail 1",100,"D:\\4.PERSONAL\\5.CODING\\5.CSS_Library_Cad\\Database\\ANCHOR DETAIL\\Image\\ANCHOR DETAIL - 6.png"),
                new Product("Detail 1",100,"D:\\4.PERSONAL\\5.CODING\\5.CSS_Library_Cad\\Database\\ANCHOR DETAIL\\Image\\ANCHOR DETAIL - 7.png"),
                new Product("Detail 1",100,"D:\\4.PERSONAL\\5.CODING\\5.CSS_Library_Cad\\Database\\ANCHOR DETAIL\\Image\\ANCHOR DETAIL - 8.png"),
                new Product("Detail 1",100,"D:\\4.PERSONAL\\5.CODING\\5.CSS_Library_Cad\\Database\\ANCHOR DETAIL\\Image\\ANCHOR DETAIL - 9.png"),
                new Product("Detail 1",100,"D:\\4.PERSONAL\\5.CODING\\5.CSS_Library_Cad\\Database\\ANCHOR DETAIL\\Image\\ANCHOR DETAIL - 9A.png"),
                new Product("Detail 1",100,"D:\\4.PERSONAL\\5.CODING\\5.CSS_Library_Cad\\Database\\ANCHOR DETAIL\\Image\\ANCHOR DETAIL - 9B.png"),
            };
        }
        C1 fhuy = new C1();
        private void InsertDetail_Click(object sender, MouseButtonEventArgs e)
        {
            C1 fhuy = new C1();
            string path = "D:\\4.PERSONAL\\5.CODING\\6.1CSS_Cad_Library\\CSS_CAD LIB\\CSS_Library\\DataImage\\TYP. FPS COATING - JOIST.dwg";
            string block_name = "TYP. FPS COATING - JOIST";
            fhuy.insert_block(path, block_name);
        }

    }


}
