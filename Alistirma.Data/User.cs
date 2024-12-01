using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alistirma.Data
{
    [Table("TestTable", Schema = "dbo")]
    public class User
    {
        [Key]
        public long Id { get; set; }    
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
