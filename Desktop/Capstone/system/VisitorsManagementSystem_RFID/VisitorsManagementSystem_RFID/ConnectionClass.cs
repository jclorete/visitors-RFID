using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace VisitorsManagementSystem_RFID
{
    public class ConnectionClass
    {
        public MySqlConnection connect = new MySqlConnection(@"server=localhost;user id=root;database=db_vms_rfid");
    }
}
