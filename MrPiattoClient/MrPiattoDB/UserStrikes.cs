using System;
using System.Collections.Generic;

namespace MrPiattoClient.MrPiattoDB
{
    public partial class UserStrikes
    {
        public int IduserStrikes { get; set; }
        public int Iduser { get; set; }
        public DateTime? Date { get; set; }
        public string Reason { get; set; }

        public virtual User IduserNavigation { get; set; }
    }
}