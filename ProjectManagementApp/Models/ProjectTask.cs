using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementApp.Models
{
    public class ProjectTask
    {
        [Key]
        [Column("taskid")]
        public int TaskId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; } = false;
        
        [ForeignKey("Project")]
        [Column("projectid")]
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}