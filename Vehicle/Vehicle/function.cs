using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vehicle
{
    class function
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9MMSSHO;Initial Catalog=vehicle;User ID=sa;Password=12345");


        public static Boolean IsName(String str)
        {
            if (!Regex.IsMatch(str, @"^[A-Za-z]+$"))
            {
                return false;

            }
            else
            {
                return true;
            }
        }

        public static Boolean IsDigit(String str)
        {
            if (!Regex.IsMatch(str, @"^[0-9]+$"))
            {
                return false;

            }
            else
            {
                return true;
            }
        }

        public static String getNextID(String col, String table, String prefix)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9MMSSHO;Initial Catalog=vehicle;User ID=sa;Password=12345");

            int id = 0;
            String query = "select max (substring(" + col + ",3,len(" + col + "))) as id from " + table;
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                DataSet ds = new DataSet();
                sda.Fill(ds, "table");
                DataTable dt = ds.Tables["table"];
                foreach (DataRow dr in dt.Rows)
                {

                    if (dr["id"].ToString() == "")
                        id = 101;
                    else
                        id = int.Parse(dr["id"].ToString()) + 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return prefix + id;
        }

        public static void loadTableQuery(DataGridView dgv, string query)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9MMSSHO;Initial Catalog=vehicle;User ID=sa;Password=12345");
            
            try
            {

                con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                sda.Fill(dt);
                dgv.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        public static void insertQuery(string query)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9MMSSHO;Initial Catalog=vehicle;User ID=sa;Password=12345");

            try
            {

                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                sda.SelectCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        public static Boolean adddetails(String query, SqlConnection con)
        {
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(query, con);

                sda.SelectCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return false;
        }

        public static bool hasData(String query, SqlConnection con)
        {
            int count = 0;
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                DataSet ds = new DataSet();
                sda.Fill(ds, "table");
                DataTable dt = ds.Tables["table"];
                foreach (DataRow dr in dt.Rows)
                {
                    count++;
                }
                if (count > 0)
                    return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            finally
            {
                con.Close();
            }
            return false;
        }

        public static String getcol(TextBox text, String colname, String query, SqlConnection con)
        {
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                DataSet ds = new DataSet();
                sda.Fill(ds, "table");
                DataTable dt = ds.Tables["table"];
                foreach (DataRow dr in dt.Rows)
                {
                    text.Text = dr[colname].ToString();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            finally
            {
                con.Close();
            }
            return "";
        }

        public static String getVal(String colname, String query, SqlConnection con)
        {
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                DataSet ds = new DataSet();
                sda.Fill(ds, "table");
                DataTable dt = ds.Tables["table"];
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr[colname].ToString() != "")
                        return dr[colname].ToString();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            finally
            {
                con.Close();
            }
            return "";
        }

        public static void DisplayData(String quary, DataGridView dataGridView, SqlConnection con)
        {
            con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter adapt = new SqlDataAdapter(quary, con);
            adapt.Fill(dt);
            dataGridView.DataSource = dt;
            con.Close();
        }

        public static void getcount(Label text, String colname, String query, SqlConnection con)
        {
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                DataSet ds = new DataSet();
                sda.Fill(ds, "table");
                DataTable dt = ds.Tables["table"];
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr[colname].ToString() != "")
                    {
                        text.Text = dr[colname].ToString();
                    }
                    else
                    {
                        double empty = 0;
                        text.Text = empty.ToString();
                    }
                }
                con.Close();


            }

            catch (Exception ex)
            {
                Console.Write(ex);
            }
        }

        public static void executesqlquerey(string sql, SqlConnection con)
        {
            try
            {
                con.Open();
                SqlDataAdapter qry = new SqlDataAdapter(sql, con);
                qry.SelectCommand.ExecuteNonQuery();
                con.Close();

            }
            catch (SqlException ex)
            {
                

                if (ex.Number == 2627)
                {
                    MessageBox.Show("Cannot insert duplicate values.");
                }
                else
                {
                    MessageBox.Show("Error while saving data.");
                }
            }
        }

        public static void tableload(string sql, SqlConnection con, DataGridView dgb, string tablename)
        {
            try
            {
                String qry = sql;
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd;
                DataSet ds = new DataSet();
                DataView dv;
                cmd = new SqlCommand(sql, con);
                da.SelectCommand = cmd;
                da.Fill(ds, tablename);
                da.Dispose();
                cmd.Dispose();
                con.Close();
                dv = ds.Tables[0].DefaultView;
                dgb.DataSource = dv;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static String getNextID(String col, String table, String prefix, SqlConnection con)
        {
            int id = 0;
            String query = "select max (substring(" + col + ",3,len(" + col + "))) as id from " + table;
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                DataSet ds = new DataSet();
                sda.Fill(ds, "table");
                DataTable dt = ds.Tables["table"];
                foreach (DataRow dr in dt.Rows)
                {

                    if (dr["id"].ToString() == "")
                        id = 101;
                    else
                        id = int.Parse(dr["id"].ToString()) + 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return prefix + id;
        }

        

    }
}
