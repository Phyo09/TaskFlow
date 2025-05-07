using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskFlow.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)] // SQLite-compatible string length
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        [Display(Name = "Completed?")]
        public bool IsComplete { get; set; }

        [ForeignKey("AspNetUsers")]
        public string? UserId { get; set; }
    }
}
