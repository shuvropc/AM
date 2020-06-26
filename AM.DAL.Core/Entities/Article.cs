using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AM.DAL.Core.Entities
{
    [Table("Article", Schema = "Article")]
    public class Article : BaseEntity
    {
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
    }
}
