using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchitecture.Date.Entites
{
    public class Ins_Subject
    {
        [Key]
        public int InsId { get; set; }
        [Key]
        public int SubId { get; set; }
        [ForeignKey(nameof(InsId))]
        [InverseProperty("Ins_Subjects")]
        public Instructor? instructor { get; set; }
        [ForeignKey(nameof(SubId))]
        [InverseProperty("Ins_Subjects")]
        public Subjects? Subject { get; set; }

    }
}
