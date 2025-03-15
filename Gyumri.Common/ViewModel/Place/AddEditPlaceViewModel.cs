using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace Gyumri.Common.ViewModel.Place
{
    public class AddEditPlaceViewModel
    {
        public int Id { get; set; } 
        public string PlaceName { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public int SubcategoryId { get; set; }

        public IFormFile UploadedPhoto { get; set; }

    }

}
