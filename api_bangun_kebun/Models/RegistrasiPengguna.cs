namespace api_bangun_kebun.Models
{
    public class RegistrasiPengguna
    {
        public string nama_lengkap { get; set; }
        public string no_telepon { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int id_provinsi { get; set; }
        public int id_kabupaten { get; set; }
        public int id_kecamatan { get; set; }
    }
}
