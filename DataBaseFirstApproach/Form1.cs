using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBaseFirstApproach
{


    public partial class Form1 : Form
    {
        ER_DBFirstApprochEntities1 db = new ER_DBFirstApprochEntities1();

        public Form1()
        {
            InitializeComponent();
            dataGridView1.DataSource = "null";
            dataGridView1.DataSource = db.students.ToList();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string search=txtSearch.Text.ToLower();

            if(search==null)
            {
                dataGridView1.DataSource = db.students.ToList();

            }

            //LINQ
            var lst = from item in db.students.ToList()
                   where item.Name.ToLower().Contains( search) ||
                   item.Email.ToLower().Contains(search) ||
                   item.Phone.ToLower().Contains(search) ||
                   item.Id.ToString().Contains(search)
                      select item;


            dataGridView1.DataSource = null;
            dataGridView1.DataSource=lst.ToList();

        }
        void ClearInputFields()
        {
            txtName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtId.Text = string.Empty;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;
            string password = txtPassword.Text;


            student std = new student
            {

                Name = name,
                Phone = phone,
                Email = email,
                Password = password

            };


            db.students.Add(std);
            db.SaveChanges();

           
            ClearInputFields();

           
            dataGridView1.DataSource = "null";
            dataGridView1.DataSource =db.students.ToList();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            int id =Convert.ToInt32( dataGridView1.Rows[rowIndex].Cells[0].Value.ToString());
            student obj =db.students.FirstOrDefault(x => x.Id == id);
            txtId.Text = obj.Id+"";
            txtName.Text = obj.Name.ToString();
            txtPhone.Text = obj.Phone.ToString();
            txtPassword.Text = obj.Password.ToString();
            txtEmail.Text = obj.Email.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtId.Text);
            student obj =db.students.FirstOrDefault(y => y.Id == id);

            db.students.Remove(obj);
            db.SaveChanges();
            ClearInputFields();

            dataGridView1.DataSource = "null";
            dataGridView1.DataSource = db.students.ToList();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtId.Text, out int id))
            {
                // Find the existing student record using the ID
                student std = db.students.FirstOrDefault(s => s.Id == id);

                if (std != null)
                {
                    // Update the student's properties with new values
                    std.Name = txtName.Text;
                    std.Email = txtEmail.Text;
                    std.Phone = txtPhone.Text;
                    std.Password = txtPassword.Text;

                    // Save changes to the database
                    try
                    {
                        db.SaveChanges();
                        MessageBox.Show("Record updated successfully.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred while updating the record: {ex.Message}");
                    }

                    // Clear input fields
                    ClearInputFields();

                    // Refresh the data grid view to show updated records
                    dataGridView1.DataSource = db.students.ToList();
                }
               
            }
            
        }
    }
}
