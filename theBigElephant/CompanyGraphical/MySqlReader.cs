using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace theBigElephant.CompanyGraphical
{
    class MySqlReader
    {
        private Configuration.Config configFile = new Configuration.Config();
        private String connString;

        public MySqlDataReader getSqlReader()
        {
            connString = $"Server=localhost;Port=3306;Database=the_big_elephant;user=root;password={configFile.DBPWD}";
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = "SELECT results.net_result, results.net_sales, results.quarter, company.company_name FROM results INNER JOIN company ON results.companyID=company.ID";
            conn.Open();
            MySqlDataReader reader = command.ExecuteReader();
            return (reader);
        }
    }
}
