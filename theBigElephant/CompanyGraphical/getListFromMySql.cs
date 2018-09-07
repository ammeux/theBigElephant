using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace theBigElephant.CompanyGraphical
{
    class getListFromMySql
    {
        private List<Company> companyList = new List<Company>();
        private MySqlDataReader reader = new MySqlReader().getSqlReader();

        public List<Company> getCompanyList()
        {
            while (reader.Read())
            {
                companyList.Add(new Company()
                {
                    name = reader["company_name"].ToString(),
                    net_result = int.Parse(reader["net_result"].ToString()),
                    net_sales = int.Parse(reader["net_sales"].ToString())
                });
            }
            return (companyList);
        }
    }
}
