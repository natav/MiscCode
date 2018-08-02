using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace winForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string myConnectionString = "Data Source=NATALYAVARS-WIN;Initial Catalog=dbAsa5;Integrated Security=True";
            string mySelectQuery = "SELECT btName, btControlFlag, btMover FROM tblBodyType ORDER BY btSort ASC;";

            using (System.Data.SqlClient.SqlConnection myConnection = new System.Data.SqlClient.SqlConnection(myConnectionString))
            {
                System.Data.SqlClient.SqlCommand myCommand = new System.Data.SqlClient.SqlCommand(mySelectQuery, myConnection);
                myConnection.Open();
                System.Data.SqlClient.SqlDataReader myReader = myCommand.ExecuteReader();

                chart1.Series["Mover?"].ChartType = SeriesChartType.Column;
                chart1.Series["Can Control"].ChartType = SeriesChartType.Column;
                //chart1.Series["Mover?"].BorderWidth = 2;
                //chart1.Series["Can Control"].BorderWidth = 2;

                //chart1.ChartAreas.Add("area1");
                //chart1.ChartAreas["area1"].AxisX.Minimum = 0;
                //chart1.ChartAreas["area1"].AxisX.Maximum = 2;
                //chart1.ChartAreas["area1"].AxisX.Interval = 1;

                chart1.Series["Mover?"].Color = Color.Coral;
                chart1.Series["Can Control"].Color = Color.Green;

                while (myReader.Read())
                {
                   chart1.Series["Mover?"].Points.AddXY(myReader["btName"], myReader["btMover"]);
                   chart1.Series["Can Control"].Points.AddXY(myReader["btName"], myReader["btControlFlag"]);
                }
            }
        }
    }
}
