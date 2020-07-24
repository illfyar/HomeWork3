namespace WpfTestMailSender
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Letter")]
    public partial class Letter
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_RecipientEmail { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_Message { get; set; }

        public DateTime? Date_Send { get; set; }

        public int id_SenderEmail { get; set; }

        public virtual Email RecipientEmail { get; set; }

        public virtual Email SenderEmail { get; set; }

        public virtual Message Message { get; set; }
    }
}
