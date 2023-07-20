using ShoeShop.Dtos.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShoeShop.Dtos.Concrete
{
    public class StockDto
    {
        public int StockID { get; set; }

        [Display(Name = "Ürün")]
        [Required(ErrorMessage = "Boş bırakılamaz!")]
        public int ProductID { get; set; }

        [Display(Name = "Numara")]
        [Required(ErrorMessage = "Boş bırakılamaz!")]
        public int SizeName { get; set; }

        [Display(Name = "Adet")]
        [Required(ErrorMessage = "Boş bırakılamaz!")]
        public int StockNumber { get; set; }
    }
}
