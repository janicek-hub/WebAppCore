using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebAppCoreData
{
    public class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name = "Type")]
        public TaskType TaskType { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }

    public enum TaskType
    {
        Code,
        Programming,
        Meeting
    }
}
