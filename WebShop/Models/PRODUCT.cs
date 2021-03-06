namespace WebShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PRODUCT")]
    public partial class PRODUCT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRODUCT()
        {
            CART_ITEM = new HashSet<CART_ITEM>();
        }

        [Key]
        public int product_id { get; set; }

        public int category_id { get; set; }

        public int? sale_id { get; set; }

        [StringLength(100)]
        public string name { get; set; }

        public int price { get; set; }

        [StringLength(50)]
        public string brand { get; set; }

        public int sold { get; set; }

        [Required]
        [StringLength(5)]
        public string size { get; set; }

        [Required]
        [StringLength(500)]
        public string content { get; set; }

        [StringLength(150)]
        public string image_link { get; set; }

        [StringLength(500)]
        public string image_list { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CART_ITEM> CART_ITEM { get; set; }

        public virtual CATEGORY CATEGORY { get; set; }

        public virtual SALE SALE { get; set; }
    }
}
