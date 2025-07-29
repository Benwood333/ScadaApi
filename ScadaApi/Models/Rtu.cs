namespace ScadaApi.Models
{
    public class Rtu
    {
        public int RtuId { get; set; }
        public string? Name { get; set; }

        public List<int>? Points { get; set; }
    }
}
