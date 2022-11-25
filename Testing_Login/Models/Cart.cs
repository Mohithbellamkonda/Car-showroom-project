using System.ComponentModel.DataAnnotations;

namespace Testing_Login.Models
{
    public class Cart
    {

        [Key]
        public int Id { get; set; }
        public string car_Company { get; set; }
        public string car_model { get; set; }
        public int price { get; set; }
        public string car_condition { get; set; }
        public string Contact_number { get; set; }
        public string  email { get; set; }
        public int item_Id { get; set; }

        public static implicit operator Cart(List<Cart> v)
        {
            throw new NotImplementedException();
        }
    }
}
