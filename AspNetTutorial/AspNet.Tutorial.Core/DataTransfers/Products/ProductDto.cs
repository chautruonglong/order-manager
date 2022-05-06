using System;

namespace AspNet.Tutorial.Core.DataTransfers.Products
{
    public class ProductDto
    {
        private string _image;
        private readonly string _endPoint;
        
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Image
        {
            get => $"{_endPoint ?? "http://localhost:5000"}/products/{_image}";
            set => _image = value;
        }

        public string Description { get; set; }

        public double Price { get; set; }

        public double Discount { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }

        public ProductDto()
        {
            _endPoint = Environment.GetEnvironmentVariable("ServerEndPoint");
        }
    }
}
