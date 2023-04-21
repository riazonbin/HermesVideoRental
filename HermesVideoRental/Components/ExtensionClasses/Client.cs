using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HermesVideoRental.Components
{
    public partial class Client
    {
        public DateTime LastVisitDate => Visit.ToList().Count != 0 ? Visit.ToList().Last().Date : DateTime.MinValue;

        public Visibility HasVisits => Visit.ToList().Count != 0 ? Visibility.Visible: Visibility.Collapsed;

        public Visibility ZeroVisitsMessageVisibility => Visit.ToList().Count != 0 ? Visibility.Collapsed : Visibility.Visible;

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
