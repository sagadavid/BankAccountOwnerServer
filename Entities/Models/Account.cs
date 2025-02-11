﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    [Table("account")]
    public class Account
    {
        public Guid AccountId { get; set; }

        [Required(ErrorMessage = "Date created is required")]
        public DateTime DateCreated { get; set; }

        [Required(ErrorMessage = "Account type is required")]
        public string AccountType { get; set; }=string.Empty;

        [ForeignKey(nameof(Owner))]
        public Guid OwnerId { get; set; }
        public Owner? Owner { get; set; }//Navigation property

    }
}
