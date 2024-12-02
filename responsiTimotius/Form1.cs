using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace responsiTimotius
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private NpgsqlConnection conn;
        string connstring = "Host=localhost; Port=5432; Username=postgres; ; Password=informatika; database=responsiTimotius";
        public DataTable dt;
        public static NpgsqlCommand cmd;
        private string sql = null;
        private DataGridViewRow r;
        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new NpgsqlConnection(connstring);
            
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            
                   if( r == null)
                {
                    MessageBox.Show("Mohon pilih baris", "Good", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            try
            {
                conn.Open();
                sql = @"select * from kr_update(:_id_karyawan,:_nama,:_id_dep)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_id_karyawan", r.Cells["_id_karyawan"].Value.ToString());
                cmd.Parameters.AddWithValue("_nama", txtNama.Text);
                cmd.Parameters.AddWithValue("_id_dep", r.Cells["_id_dep"].Value.ToString());
                if ((int)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("Data Berhasil diupdate", "wELL DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("eRROR" + ex.Message, "gAGAL iNPUT", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

           
        

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try 
            {
                conn.Open();
                dgvData.DataSource = null;

                sql = @"select * from kr_select()";
                cmd = new NpgsqlCommand(sql, conn);
                dt = new DataTable();
                NpgsqlDataReader rd = cmd.ExecuteReader();
                dt.Load(rd);
                dgvData.DataSource = dt;
                conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message, "Fail!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                sql = @"select * from kr_insert(:_nama,:_id_dep)";
                
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_nama", txtNama.Text);
                cmd.Parameters.AddWithValue("_id_dep", txtDep.Text);
                if ((int)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("Data Berhasil diinput","wELL DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("eRROR" + ex.Message,"gAGAL iNPUT", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (r == null)
            {
                MessageBox.Show("Mohon pilih baris", "Good", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            try
            {
                conn.Open();
                sql = @"select * from kr_delete(:_id_karyawan)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_id_karyawan", r.Cells["_id_karyawan"].Value.ToString());
 
                if ((int)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("Data Berhasil didelete", "wELL DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("eRROR" + ex.Message, "gAGAL Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbDepartemen_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.Open();
            sql = @"select nama_departemen from departemen";
            cmd = new NpgsqlCommand(sql, conn);
            conn.Close() ;

        }
    }
}
