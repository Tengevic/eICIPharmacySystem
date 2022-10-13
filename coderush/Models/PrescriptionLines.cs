namespace coderush.Models
{
    public class PrescriptionLines
    {
        public int PrescriptionLinesId { get; set; }
        public int PrescriptionId { get; set; }
        public string PrescriptionLinesName { get; set; }
        public int? ProductId { get; set; }
        public double Quantity { get; set; }
        public int OderId { get; set; }
        public string prescription { get; set; }
        public Product Product { get; set; }
        public bool sold { get; set; } = false;

    }
}