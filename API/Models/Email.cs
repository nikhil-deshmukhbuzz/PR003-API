using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Email
    {
        [Key]
        public long EmailID { get; set; }
        public string ProductCode { get; set; }
        public string CustomerCode { get; set; }
        public string To { get; set; }
        public string CC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public byte[] Attachement { get; set; }
        public byte[] Attachement1 { get; set; }
        public byte[] Attachement2 { get; set; }
        public string AttachementName { get; set; }
        public string Attachement1Name { get; set; }
        public string Attachement2Name { get; set; }
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
        public int Attempt { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsSent { get; set; }
    }
}
