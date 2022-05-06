namespace coderush.Models
{
    public class PrescriptionLines
    {
        public int PrescriptionLinesId { get; set; }
        public int PrescriptionId { get; set; }
        public string PrescriptionLinesName { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int remainder { get; set; }
        public Product Product { get; set; }

    }
}