using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class TaskEntry : DomainEntityId
    {
        public string Name { get; set; } = default!;
        public Guid TaskTypeId { get; set; }
        public TaskType? TaskType { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime TrackingTime { get; set; }
    }
}