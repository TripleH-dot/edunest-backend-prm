using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    [Table("Tutors")]
    public class Tutor
    {
        [Key]
        public int TutorId { get; set; }

        public int UserId { get; set; }

        [MaxLength(1000)]
        public string Bio { get; set; } = string.Empty;

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual ICollection<TutorSubject> TutorSubjects { get; set; } = new List<TutorSubject>();
        public virtual ICollection<Availability> Availabilities { get; set; } = new List<Availability>();
    }
}
