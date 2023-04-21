using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HermesVideoRental.Components
{
    public partial class Client
    {
        public DateTime LastVisitDate => Visit.ToList().Last().Date;
        public int CountOfVisits => Visit.ToList().Count;

        public List<ClientTag> Tags => App.Connection.ClientTag.Where(x => x.ClientId == Id).ToList();
    }
}
