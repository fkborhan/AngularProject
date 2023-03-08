using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AngularProject.Context
{
    public class dept2
    {
        [Key]
        public string deptid { get; set; }
        public string deptname { get; set; }
        public string location { get; set; }
        public IList<items2> items2 { get; set; }
    }
    public partial class items2
    {
        [Key]
        public string itemcode { get; set; }
        public string itemname { get; set; }
        [ForeignKey("dept2")]
        public string deptid { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public Nullable<decimal> cost { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public Nullable<decimal> rate { get; set; }
        public DateTime date { get; set; }
        public string picture { get; set; }
        public dept2 dept2 { get; set; }
    }
    public class DeptItemsVm
    {
        public DeptItemsVm()
        {
            this.dept2 = new dept2();
            this.items2 = new List<items2>();
            /* do nothing */
        }
        public dept2 dept2 { get; set; }
        public List<items2> items2 { get; set; }
    }

}
