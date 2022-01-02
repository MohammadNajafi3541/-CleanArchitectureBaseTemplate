using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApi.DTOs
{
    public class ContractDto
    {
        public string Id { get; set; }

        public string CustomerName { get; set; }

        public string CustomerAddress { get; set; }

        public string BrokerAgentName { get; set; }

        public string BrokerCompanyName { get; set; }
        public string EmailAddress { get; set; }
         
    }
}
