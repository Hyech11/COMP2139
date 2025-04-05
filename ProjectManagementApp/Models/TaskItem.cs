using System.ComponentModel.DataAnnotations;
using ProjectManagementApp.Models;

namespace ProjectManagementApp.Models
{
    public class TaskItem
    {
        public int Id { get; set; }


        public int ProjectId { get; set; }

        public Project Project { get; set; }

        [Required] public string Title { get; set; }

        public string Description { get; set; }

    }
}