using System.Text.Json;

namespace AspNet.Tutorial.Core.DataTransfers.Commons
{
    public class HttpResponseDto
    {
        public int Status { get; set; }

        public string Type { get; set; }

        public object Message { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
