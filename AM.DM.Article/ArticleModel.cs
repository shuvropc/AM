using System;

//Hello
namespace AM.DM.Article
{
    public class ArticleModel
    {
        //https://www.google.com/555
        public long Id { get; set; }
        public string FileName { get; set; }
        public byte[] File { get; set; }
        public string ArticleTitle { get; set; }
        public string ArticleDescription { get; set; }
        public int? Version { get; set; }
        public bool? IsApproved { get; set; }
        public bool? IsRejected { get; set; }
        public long? ApprovedBy { get; set; }
        public long? RejectedBy { get; set; }
        public string RejectRemark { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
