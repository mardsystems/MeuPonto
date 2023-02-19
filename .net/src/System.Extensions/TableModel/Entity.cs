using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.TableModel
{
    public abstract class Entity
    {
        public int? Id { get; set; }

        public DateTime? CreationDate { get; set; }

        [Timestamp]
        public byte[]? Version { get; set; }
    }
}
