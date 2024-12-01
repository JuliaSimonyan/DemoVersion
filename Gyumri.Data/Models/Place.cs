using System.ComponentModel.DataAnnotations.Schema;

namespace Gyumri.Data.Models
{
    public class Place
    {
        public int PlaceId { get; set; }
        public string PlaceName { get; set; }
        public string Description { get; set; }
        public string Photo { get;set; }

        [ForeignKey("Subcategory")]
        public int SubcategoryId { get; set; }
        public Subcategory Subcategory { get; set; }
    }
}
