using Application.DTOs.Generic;
using System;

namespace Application.DTOs.Request
{
    public class CreateTaskRequest
    {
        public string Name { get; set; }
        public int? Points { get; set; }
        public TaskStatus Status { get; set; }
        public int? AssigenedTo { get; set; }
    }
}
