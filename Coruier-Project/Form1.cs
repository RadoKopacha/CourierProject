using Coruier_Project.Controler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coruier_Project
{
    public partial class Form1 : Form
    {
        private ParcelLogic controller = new ParcelLogic();

        public Form1()
        {
            InitializeComponent();
        }

        // LOAD – зарежда типовете в ComboBox и всички пратки в ListBox
  
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadTypes();
            LoadAllParcels();
        }

        // ПОМОЩНИ МЕТОДИ

        private void LoadTypes()
        {
            DataTable types = controller.GetAllTypes();
            cmbType.DisplayMember = "TypeName";
            cmbType.ValueMember = "Id";
            cmbType.DataSource = types;
        }

        private void LoadAllParcels()
        {
            listBox1.Items.Clear();
            DataTable parcels = controller.GetAllParcels();
            for (int i = 0; i < parcels.Rows.Count; i++)
            {
                listBox1.Items.Add($"{parcels.Rows[i]["Id"]} – {parcels.Rows[i]["Name"]}");
            }
        }

        private void ClearFields()
        {
            txtId.Clear();
            txtName.Clear();
            txtDescription.Clear();
            txtPrice.Clear();
            txtWeight.Clear();
            if (cmbType.Items.Count > 0)
                cmbType.SelectedIndex = 0;
        }

   
        // ADD

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string desc = txtDescription.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Моля, въведете име на пратката.", "Грешка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal price;
            if (!decimal.TryParse(txtPrice.Text.Trim(), out price))
                price = 0m;

            decimal weight;
            if (!decimal.TryParse(txtWeight.Text.Trim(), out weight))
                weight = 0m;

            int typeId = 0;
            if (cmbType.SelectedValue != null)
            {
                try { typeId = Convert.ToInt32(cmbType.SelectedValue); }
                catch { typeId = 0; }
            }

            try
            {
                controller.AddParcel(name, desc, price, weight, typeId);
                MessageBox.Show("Пратката е добавена успешно!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAllParcels();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при добавяне: " + ex.Message, "Грешка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // SELECT ALL
        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            LoadAllParcels();
            ClearFields();
        }

       
        // FIND – търси по Id

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text.Trim(), out int id))
            {
                MessageBox.Show("Моля, въведете валидно ID.", "Грешка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataTable result = controller.FindParcel(id);

                if (result.Rows.Count == 0)
                {
                    MessageBox.Show("Пратка с това ID не е намерена.", "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                txtName.Text = result.Rows[0]["Name"].ToString();
                txtDescription.Text = result.Rows[0]["Description"].ToString();
                txtPrice.Text = result.Rows[0]["Price"].ToString();
                txtWeight.Text = result.Rows[0]["Wieght"].ToString();

              
                string typeName = result.Rows[0]["TypeName"].ToString();
                DataTable types = (DataTable)cmbType.DataSource;
                for (int i = 0; i < types.Rows.Count; i++)
                {
                    if (types.Rows[i]["TypeName"].ToString() == typeName)
                    {
                        cmbType.SelectedValue = types.Rows[i]["Id"];
                        break;
                    }
                }


                listBox1.Items.Clear();
                listBox1.Items.Add($"{result.Rows[0]["Id"]} – {result.Rows[0]["Name"]}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при търсене: " + ex.Message, "Грешка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      
        // UPDATE
       
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text.Trim(), out int id))
            {
                MessageBox.Show("Моля, въведете ID на пратката за редактиране.", "Грешка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

     
            string name = txtName.Text.Trim();
            string desc = txtDescription.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Моля, въведете име на пратката.", "Грешка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal price;
            if (!decimal.TryParse(txtPrice.Text.Trim(), out price))
                price = 0m;

            decimal weight;
            if (!decimal.TryParse(txtWeight.Text.Trim(), out weight))
                weight = 0m;

            int typeId = 0;
            if (cmbType.SelectedValue != null)
            {
                try { typeId = Convert.ToInt32(cmbType.SelectedValue); }
                catch { typeId = 0; }
            }

            try
            {
                controller.UpdateParcel(id, name, desc, price, weight, typeId);
                MessageBox.Show("Пратката е обновена успешно!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAllParcels();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при обновяване: " + ex.Message, "Грешка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        // DELETE
  
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text.Trim(), out int id))
            {
                MessageBox.Show("Моля, въведете ID на пратката за изтриване.", "Грешка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                $"Сигурни ли сте, че искате да изтриете пратка с ID {id}?",
                "Потвърждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                controller.DeleteParcel(id);
                MessageBox.Show("Пратката е изтрита успешно!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAllParcels();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Грешка при изтриване: " + ex.Message, "Грешка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        // LISTBOX CLICK – клик върху пратка зарежда полетата
      
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null) return;

            string selected = listBox1.SelectedItem.ToString();
            string idStr = selected.Split('–')[0].Trim();

            if (!int.TryParse(idStr, out int id)) return;

            try
            {
                DataTable result = controller.FindParcel(id);
                if (result.Rows.Count == 0) return;

                txtId.Text = result.Rows[0]["Id"].ToString();
                txtName.Text = result.Rows[0]["Name"].ToString();
                txtDescription.Text = result.Rows[0]["Description"].ToString();
                txtPrice.Text = result.Rows[0]["Price"].ToString();
                txtWeight.Text = result.Rows[0]["Wieght"].ToString();

                string typeName = result.Rows[0]["TypeName"].ToString();
                DataTable types = (DataTable)cmbType.DataSource;
                for (int i = 0; i < types.Rows.Count; i++)
                {
                    if (types.Rows[i]["TypeName"].ToString() == typeName)
                    {
                        cmbType.SelectedValue = types.Rows[i]["Id"];
                        break;
                    }
                }
            }
            catch {  }
        }
    }
}
