using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BlazorIntus2.Shared.DataModel
{
    public class Data
    {        
        public class Order
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public string? Name { get; set; }
            public string? State { get; set; }
            public ICollection<OrderWindow>? OrderWindows { get; set; }
        }        
        public class Window
        {            
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            [Required(ErrorMessage =("Name is required"))]
            public string? Name { get; set; }
            [Range(typeof(int), "1", "100", ErrorMessage = "{0} must be a number between {1} and {2}.")]
            public int QuantityOfWindows { get; set; }
            public int TotalSubElements { get; set; }
            public ICollection<SubElement>? SubElements { get; set; }
            [NotMapped]
            public bool Selected { get; set; }
        }
        
        public class SubElement
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            [Required]
            public int Element { get; set; }
            [Required(ErrorMessage = ("Name is required (Doors or Window)"))]
            public string? Type { get; set; }
            [Range(typeof(int), "1", "9999", ErrorMessage = "{0} must be a number between {1} and {2}.")]
            public int Width { get; set; }
            [Range(typeof(int), "1", "9999", ErrorMessage = "{0} must be a number between {1} and {2}.")]
            public int Height { get; set; }
            public Window? Window { get; set; }
            public int WindowId { get; set; }
        }
        
        public class OrderWindow
        {
            public int Id { get; set; }
            public int OrderId { get; set; }
            public int WindowId { get; set; } 
            public Order? Order { get; set; }
            public Window? Window { get; set; }
        }       
    }
}