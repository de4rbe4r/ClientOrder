namespace Test_ClientOrder
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderDB")]
    public partial class OrderDB
    {
        public int Id { get; set; }

        public int? CustomerId { get; set; }

        [Required]
        [StringLength(12)]
        public string Number { get; set; }

        public int Amount { get; set; }

        public DateTime DueTime { get; set; }

        public DateTime ProcessedTime { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        public virtual CustomerDB CustomerDB { get; set; }

        public override string ToString()
        {
            return $"{Id}. {Number} ({Amount}) шт. Выполнить до {ProcessedTime.ToShortDateString()}";
        }
    }
}
