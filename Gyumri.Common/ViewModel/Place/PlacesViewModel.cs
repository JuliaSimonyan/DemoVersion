using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gyumri.Common.ViewModel.Place
{
    public class PlacesViewModel
    {
        public int Id { get; set; }
        public string PlaceName { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public int SubcategoryId { get; set; }

        public IFormFile UploadedPhoto { get; set; }
    }
}
