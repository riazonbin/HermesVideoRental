using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public List<Tag> UsersTags
        {
            get
            {
                var tags = new List<Tag>();

                foreach(var clientTag in Tags)
                {
                    tags.Add(clientTag.Tag);
                }
                return tags;
            }
        }
    }
}
