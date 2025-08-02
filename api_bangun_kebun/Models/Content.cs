namespace api_bangun_kebun.Models
{
    public class Content
    {
        public int? id_content { get; set; }
        public int? id_user { get; set; }
        public int? id_jenis_konten { get; set; }
        public string? judul {  get; set; }
        public string? gambar_video { get; set; }
        public string? isi_deskripsi { get; set; }
        public DateTime? timestamp { get; set; }
        public int? like {  get; set; }
    }
}
