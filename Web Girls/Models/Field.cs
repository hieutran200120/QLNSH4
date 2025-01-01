using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThucTapCongNghiep.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Field
    {
        public int id { get; set; }

        [StringLength(100)]
        public string name { get; set; }

        public string type { get; set; }


        [StringLength(50)]
        public string templatecode { get; set; }

        public int? DocumentId { get; set; }

        public int height { get; set; }
        public int width { get; set; }
        public int upper { get; set; }
        public int bold { get; set; }
        public int italic { get; set; }
        public int center { get; set; }

    }
}
