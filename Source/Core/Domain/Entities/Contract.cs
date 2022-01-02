using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
   public class Contract
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string CustomerName { get; set; }

        [MaxLength(500)]
        public string CustomerAddress { get; set; }

        [Required]
        [MaxLength(50)]
        public string BrokerAgentName { get; set; }

        [Required]
        [MaxLength(50)]
        public string  BrokerCompanyName { get; set; }

        [Required]
        public string TotalPrice { get; set; }

        [Required]
        public DateTime? StartDate { get; set; }

        [Required]
        public DateTime?  EndDate { get; set; }



    }
}
