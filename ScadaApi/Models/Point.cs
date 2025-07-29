
namespace ScadaApi.Models
{
    public class Point
    {
        public int PointId { get; set; }

        public int RtuId { get; set; }

        public string? Name { get; set; }

        public int Value { get; set; }

        public DateTime? TimeStamp { get; set; }
    }
}
