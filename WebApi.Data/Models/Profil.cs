using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Data.Models
{
    public class Profil
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "NIK harus di isi")]
        public string? NIK { get; set; }
        [Required(ErrorMessage = "Nama harus di isi")]
        public string? Nama { get; set; }
        [Required(ErrorMessage = "Alamat harus di isi")]
        public string? Alamat { get; set; }
        public string? StatusPerkawinan { get; set; }
    }
}