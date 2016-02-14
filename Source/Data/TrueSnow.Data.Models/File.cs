﻿namespace TrueSnow.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Common.Models;

    public class File : BaseModel<int>
    {
        [StringLength(255)]
        public string FileName { get; set; }

        [StringLength(100)]
        public string ContentType { get; set; }

        [Required]
        public byte[] Content { get; set; }

        [Required]
        public FileType FileType { get; set; }

        //public virtual ICollection<User> UsersCover { get; set; }

        //public virtual ICollection<User> UsersAvatar { get; set; }
    }
}
