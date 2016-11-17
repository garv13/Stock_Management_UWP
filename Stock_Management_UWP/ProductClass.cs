using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Stock_Management_UWP
{
    class ProductClass
    {
        public string Name { get; set; }

        public string Color { get; set; }

        public int Quantity { get; set; }

        public List<string> Logs { get; set; }

        public string Image { get; set; }

    }
}
