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
    
    
        public class Order : IEntity
        {
            [Key]
            public int ID { get; set; }

            [Required(ErrorMessage = "Boş Bırakılamaz!")]
            [Display(Name = "Kullanıcı")]
            public int UserID { get; set; }
             public string OrderNumber { get; set; }

        [Required(ErrorMessage = "Boş Bırakılamaz!")]
            [Display(Name = "Sipariş Tarihi")]
            public DateTime OrderDate { get; set; }

            
            [Display(Name = "Toplam Fiyat")]
            public double TotalPrice { get; set; }

             public string FullName { get; set; }

        [Required(ErrorMessage = "Boş Bırakılamaz!")]
            [Display(Name = "Adres")]
            public string Address { get; set; }
            [Required(ErrorMessage = "Boş Bırakılamaz!")]
            [Display(Name = "Semt")]
            public string Semt { get; set; }
        [Required(ErrorMessage = "Boş Bırakılamaz!")]
        [Display(Name = "Sehir")]
        public string Sehir { get; set; }
        [Required(ErrorMessage = "Boş Bırakılamaz!")]
        [Display(Name = "PostaKodu")]
        public string PostaKodu { get; set; }

        public User User { get; set; }
          
        
        public List<OrderItem> OrderItems { get; set; }
        public EnumOrderState OrderState { get; set; }

        public enum EnumOrderState
        {
            waiting = 0,
            unpaid = 1,
            completed = 2
        }


    }


    
}
