using Microsoft.AspNetCore.Mvc;

namespace ShoeShop.API.Filters.IsExist
{
    public class IsExistsAttribute : TypeFilterAttribute
    {
        public IsExistsAttribute() : base(typeof(CheckExisting))
        {
        }
    }
}
