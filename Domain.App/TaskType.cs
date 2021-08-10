using System.Collections;
using System.Collections.Generic;
using Domain.Base;

namespace Domain.App
{
    public class TaskType : DomainEntityId
    {
        public string Name { get; set; } = default!;
        public ICollection<TaskEntry>? Tasks { get; set; }
    }
}