using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using BLL;
using DAL;

namespace UI
{
    public partial class InventarioApp : Form
    {
        private ProductoBLL productoBLL;
        public InventarioApp()
        {
            InitializeComponent();
            productoBLL = new ProductoBLL();
            this.CargarProductos();
        }

        public void CargarProductos()
        {
            this.dtgvProductos.Rows.Clear();
            List<Producto> productos = productoBLL.ObtenerProductos();
            foreach (Producto p in productos)
            {
                int n = dtgvProductos.Rows.Add();
                dtgvProductos.Rows[n].Cells[0].Value = p.ID;
                dtgvProductos.Rows[n].Cells[1].Value = p.Name;
                dtgvProductos.Rows[n].Cells[2].Value = p.Descp;
                dtgvProductos.Rows[n].Cells[3].Value = p.Price;
                dtgvProductos.Rows[n].Cells[4].Value = p.Qty;
            }
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                this.ValidarDatos();

                string name = txtNombre.Text.Trim();
                string desc = txtDesc.Text.Trim();
                int price = int.Parse(txtPrecio.Text.Trim());
                int qty = int.Parse(txtCantidad.Text.Trim());
                
                if (string.IsNullOrEmpty(txtId.Text))
                {
                    Producto p = new Producto(name, desc,price, qty);
                    this.productoBLL.AgregarProducto(p);
                    Limpiar();
                } else
                {
                    int id = int.Parse(txtId.Text);
                    Producto p = new Producto(id, name, desc, price, qty);
                    this.productoBLL.EditarProducto(p);
                    Limpiar();
                }
                MessageBox.Show("Operacion relizada con exito.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarProductos();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtgvProductos.SelectedRows.Count > 0)
                {
                    DataGridViewRow filaSeleccionada = dtgvProductos.SelectedRows[0];
                    int id = int.Parse(filaSeleccionada.Cells[0].Value.ToString());
                    string mensaje = $"¿Estás seguro de que deseas eliminar este producto? Esta acción no se puede deshacer.";
                    DialogResult resultado = MessageBox.Show(mensaje, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        this.productoBLL.EliminarProducto(id);
                        this.CargarProductos();
                        MessageBox.Show("El producto ha sido eliminado exitosamente.", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("No se ha seleccionado ninguna fila. Haga click en la celda vacia de la primera columna para seleccionar un producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtgvProductos.SelectedRows.Count > 0)
                {
                    DataGridViewRow filaSeleccionada = dtgvProductos.SelectedRows[0];
                    txtId.Text = filaSeleccionada.Cells[0].Value.ToString().Trim();
                    txtNombre.Text = filaSeleccionada.Cells[1].Value.ToString().Trim();
                    txtDesc.Text = filaSeleccionada.Cells[2].Value.ToString().Trim();
                    txtPrecio.Text = filaSeleccionada.Cells[3].Value.ToString().Trim();
                    txtCantidad.Text = filaSeleccionada.Cells[4].Value.ToString().Trim();
                    btnCrear.Text = "Guardar";

                }
                else
                {
                    MessageBox.Show("No se ha seleccionado ninguna fila. Haga click en la celda vacia de la primera columna para seleccionar un producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ValidarDatos()
        {
            if (string.IsNullOrEmpty(txtNombre.Text.Trim()))
            {
                throw new Exception("El nombre es obligatorio");
            }
            if (string.IsNullOrEmpty(txtDesc.Text.Trim()))
            {
                throw new Exception("La descripcion es obligatoria");
            }
            if (!int.TryParse(txtPrecio.Text.Trim(), out int price))
            {
                throw new Exception("La cantidad ingresada no es valida");
            }
            if (!int.TryParse(txtCantidad.Text.Trim(), out int qty))
            {
                throw new Exception("La cantidad ingresada no es valida");
            }
        }

        private void Limpiar()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtDesc.Clear();
            txtPrecio.Clear();
            txtCantidad.Clear();
            btnCrear.Text = "Crear";
        }
    }
}
