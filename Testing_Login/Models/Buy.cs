using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Testing_Login.Models
{
    public class Buy
    {

        [Key]
        public int Id { get; set; }
        public string car_Company { get; set; }
        public string car_model { get; set; }
        public int price { get; set; }
      
        public string car_condition { get; set; }
        [Required]
        public string Contact_number { get; set; }
        public string email { get; set; }
       
    }
}
