using System;
using System.ComponentModel.DataAnnotations;

namespace AspNet.Tutorial.Core.DataTransfers.Products
{
    public class ProductModificationDto
    {
        [Required(AllowEmptyStrings = false)] public string Name { get; set; }

        [Required(AllowEmptyStrings = false)] public string Image { get; set; }

        [Required(AllowEmptyStrings = false)] public string Description { get; set; }

        [Required] [Range(0, Double.MaxValue)] public double Price { get; set; }

        [Required] [Range(0, Double.MaxValue)] public double Discount { get; set; }
    }
}
