namespace Gyumri.Data.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public List<Subcategory> Subcategories { get; set; }
    }
}
