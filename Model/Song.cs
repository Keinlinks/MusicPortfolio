using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortafolioBack.Model
{
    public class Song
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id {  get; set; }
        public string title { get; set; }

        public int duration {  get; set; }

        public int year {  get; set; }

        public string img {  get; set; }

        public string url_data { get; set; }

        public string autor {  get; set; } 
    }
}