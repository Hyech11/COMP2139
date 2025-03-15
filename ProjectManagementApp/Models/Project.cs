using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementApp.Models
{
    public class Project
    {
        [Key]
        [Column("projectid")]
        public int ProjectId { get; set; }

        [Required]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        private DateTime _startDate;
        private DateTime _endDate;

        [DataType(DataType.Date)]
        [Column("startdate")]
        public DateTime StartDate
        {
            get => _startDate;
            set => _startDate = DateTime.SpecifyKind(value, DateTimeKind.Utc); 
        }

        [DataType(DataType.Date)]
        [Column("enddate")]
        public DateTime EndDate
        {
            get => _endDate;
            set => _endDate = DateTime.SpecifyKind(value, DateTimeKind.Utc); 
        }
        
        public List<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
    }
}