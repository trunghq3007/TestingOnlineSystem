using System;

namespace TestingSystem.DataTranferObject
{
    public class ExamPaperFilterModel
    {
        public int?  ExamPaperID { get; set; }
        public int? Time { get; set; }
        public bool? Status { get; set; }
        public int? NumberOfQuestion { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

    }
}