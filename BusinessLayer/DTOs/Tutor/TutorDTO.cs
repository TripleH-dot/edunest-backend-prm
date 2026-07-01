using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs.Tutor
{
    public class TutorResponseDTO
    {
        public int TutorId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Bio { get; set; }
    }

    public class UpdateTutorDTO
    {
        public string? Bio { get; set; }
        public string? Phone { get; set; }
        public string? Name { get; set; }
    }

}
