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
    public class CategoryDto:IDto
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Lütfen isim giriniz!")]
        [MinLength(3, ErrorMessage = "En az 3 karakter uzunluğunda olmalıdır!")]
        [Display(Name = "Kategori Adı")]
        public string Name { get; set; }
    }
}
