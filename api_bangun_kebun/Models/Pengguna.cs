namespace api_bangun_kebun.Models
{
    public class Pengguna
    {
        public int? id_user { get; set; }
        public string? nama_lengkap { get; set; }
        public string? username { get; set; }
        public string? no_telepon { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public int? id_kecamatan { get; set; }
    }
}
