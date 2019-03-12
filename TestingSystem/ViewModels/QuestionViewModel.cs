using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestingSystem.ViewModels
{
    public class QuestionViewModel
    {
        public int QuestionID { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public int Level { get; set; }
        public int CategoryID { get; set; }
        public bool IsActive { get; set; }
    }
}