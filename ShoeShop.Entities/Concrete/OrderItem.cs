using ShoeShop.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShoeShop.Entities.Concrete
{
    public class OrderItem : IEntity
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Boş Bırakılamaz!")]
        [Display(Name = "Sipariş")]
        public int OrderID { get; set; }

        [Required(ErrorMessage = "Boş Bırakılamaz!")]
        [Display(Name = "Ürün")]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Boş Bırakılamaz!")]
        [Display(Name = "Adet")]
        public int Quantity { get; set; }

        [Display(Name = "Fiyat")]
        public double Price { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
