using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class TaskEntry : DomainEntityId
    {
        public string Name { get; set; } = default!;
        [DisplayName("Task type")]
        public Guid TaskTypeId { get; set; }
        [DisplayName("Task type")]
        public TaskType? TaskType { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [DataType(DataType.Time)]
        public DateTime From { get; set; }
        [DataType(DataType.Time)]
        public DateTime To { get; set; }
    }
}
