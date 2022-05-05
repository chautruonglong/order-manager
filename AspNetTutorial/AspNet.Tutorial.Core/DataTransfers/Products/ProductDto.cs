using System;

namespace AspNet.Tutorial.Core.DataTransfers.Products
{
    public class ProductDto
    {
        private string _image;
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Image
        {
            get => $"https://localhost:5001/products/{_image}";
            set => _image = value;
        }

        public string Description { get; set; }

        public double Price { get; set; }

        public double Discount { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }
    }
}
