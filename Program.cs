using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data;


namespace WindowsFormsApp1
{

    public class Progam
{


        static void Main(string[] args)
        {


            Contact[] data = buildDB();
            DataTable dtable = getTable(data);


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form1 ui = new Form1();
            ui.setdata(dtable);

            Application.Run(ui);



        }

        static DataTable getTable(Contact[] data)
        {

            DataTable table = new DataTable();

            table.Columns.Add(new DataColumn("Name"));
            table.Columns.Add(new DataColumn("Phone_Number"));
            table.Columns.Add(new DataColumn("Address"));

            foreach(Contact row in data)
            {
                DataRow drow = table.NewRow();
                drow["Name"] = row.name;
                drow["Phone_Number"] = row.phone_number;
                drow["Address"] = row.address;

                table.Rows.Add(drow);

            }

            return table;

    }

        static public Contact[] buildDB()
        {


            Task<string> result = GetResponseString();
            var jsonResult = result.Result;

            JObject resultobj = JObject.Parse(jsonResult);
            JArray contact = (JArray)resultobj["contacts"];

            Contact person = contact[2].ToObject<Contact>();
            JToken jperson = contact[1];


            int count = contact.Count;
            Contact[] people = new Contact[count];

            for (int i = 0; i < count; i++)
            {

                people[i] = new Contact();
                people[i].name = (string)contact[i]["name"];
                people[i].phone_number = (string)contact[i]["phone_number"];
                people[i].address = (string)contact[i]["address"];
            }

            return people;
        }



        static public async Task<string> GetResponseString()
        {
            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync("http://www.mocky.io/v2/581335f71000004204abaf83");
            var contents = await response.Content.ReadAsStringAsync();

            return contents;
        }
    }

    public class Contact
    {


        public string name { get; set; }
        public string phone_number { get; set; }
        public string address { get; set; }
    }


 }
