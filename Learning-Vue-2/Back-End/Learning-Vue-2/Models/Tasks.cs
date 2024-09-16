using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Learning_Vue_2.Models
{
    public class Tasks
    {
        [Key]
        public int id {get; set;}
        public string Title { get; set;}
        public string Description { get; set;}

        [DataType(DataType.Date)]
        public string Creationdate { get; set;}
        
        [DataType(DataType.Date)]
        public string LimitDate { get; set;}
        public bool StateTask { get; set;}
        
    }
}