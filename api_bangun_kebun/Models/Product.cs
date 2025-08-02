namespace api_bangun_kebun.Models
{
    public class Product
    {
        public int? id_user { get; set; }
        public int? id_produk { get; set; }
        public string? nama_produk { get; set; }
        public string? gambar_produk { get; set; }
        public double? harga { get; set; }
        public DateTime? timestamp { get; set; }
        public int? id_jenis_produk { get; set; }
        public int? id_status_ketahanan { get; set; }
    }
}
