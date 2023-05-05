using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EstoqueGrid
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                richTextBox1.LoadFile("Teste.txt", RichTextBoxStreamType.PlainText);
                Limpar();
            }
            catch (Exception exc)
            {
                richTextBox1.Text = "\n";
                richTextBox1.SaveFile("Teste.txt", RichTextBoxStreamType.PlainText);
                Limpar();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string dadosNovos = textBox1.Text + "-" + textBox2.Text + "-" + textBox3.Text + "-" + textBox4.Text + "-" + textBox6.Text + "-A";
            if (int.Parse(label8.Text) == richTextBox1.Lines.Length - 1)
            {
                //insere um novo registro
                richTextBox1.Text += dadosNovos + "\n";
            }
            else
            {
                //altera um registro existente
                string dadosAntigos = richTextBox1.Lines[int.Parse(label8.Text)];
                richTextBox1.Text = richTextBox1.Text.Replace(dadosAntigos, dadosNovos);
            }

            richTextBox1.SaveFile("Teste.txt", RichTextBoxStreamType.PlainText);

            Limpar();
        }

        private void Limpar()// cria um void para limpar
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox6.Clear();
            label8.Text = (richTextBox1.Lines.Length - 1).ToString();

            int n = richTextBox1.Lines.Length - 1;
            dataGridView1.RowCount = n;

            int linha = 0;
            for (int i = 1; i < n; i++)
            {
                string[] dados = richTextBox1.Lines[i].Split('-');
                if (dados[5] == "A")
                {
                    dataGridView1.Rows[linha].Cells[0].Value = i;
                    dataGridView1.Rows[linha].Cells[1].Value = dados[0];
                    dataGridView1.Rows[linha].Cells[2].Value = dados[1];
                    dataGridView1.Rows[linha].Cells[3].Value = dados[2];
                    dataGridView1.Rows[linha].Cells[4].Value = dados[3];
                    dataGridView1.Rows[linha].Cells[5].Value = dados[4];
                    linha++;

                }
            }
            dataGridView1.RowCount = linha;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int n = int.Parse(label8.Text);
            if (n > 0 && n < richTextBox1.Lines.Length - 1)
            {
                if (DialogResult.Yes == MessageBox.Show("Deseja realmente excluir esse registro?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    string dadosNovos = textBox1.Text + "-" + textBox2.Text + "-" + textBox3.Text + "-" + textBox4.Text + "-" + textBox5.Text + "-E";
                    string dadosAntigos = richTextBox1.Lines[int.Parse(label8.Text)];
                    richTextBox1.Text = richTextBox1.Text.Replace(dadosAntigos, dadosNovos);
                    richTextBox1.SaveFile("Texte.txt", RichTextBoxStreamType.PlainText);
                    MessageBox.Show("O registro foi excluído com sucesso!", "AVISO");
                    Limpar();
                }
                else
                {
                    MessageBox.Show("Nada foi excluído", "AVISO");
                }
            }
            else
            {
                MessageBox.Show("Registro inexistente", "AVISO");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int n = int.Parse(textBox5.Text);

            if (Convert.ToInt32(textBox5.Text) <= richTextBox1.Lines.Length - 1 && Convert.ToInt32(textBox5.Text) > 0)
            {
                string linha = richTextBox1.Lines[n];
                string[] campos = linha.Split('-');
                textBox1.Text = campos[0];
                textBox2.Text = campos[1];
                textBox3.Text = campos[2];
                textBox4.Text = campos[3];
                textBox6.Text = campos[4];
                label8.Text = n.ToString();

            }
            else
            {
                MessageBox.Show("Linha não encontrada", "Aviso");
            }
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                saveFileDialog1.FileName = openFileDialog1.FileName;
                Limpar();
            }
        }

        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void Save(object sender, CancelEventArgs e)
        {
           string caminho = saveFileDialog1.FileName;
            File.WriteAllText(caminho, richTextBox1.Text);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar != (char)Keys.Back && !char.IsSeparator(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar != (char)Keys.Back && !char.IsSeparator(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar != (char)Keys.Back && !char.IsSeparator(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar != (char)Keys.Back && !char.IsSeparator(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar != (char)Keys.Back && !char.IsSeparator(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }

        private void formatarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("você deseja formatar o Registro?", "AVISO!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                richTextBox1.ResetText();
                Limpar();   
                richTextBox1.SaveFile("Teste.txt", RichTextBoxStreamType.PlainText);
                
            }
            else
            {
                MessageBox.Show("Nada foi formatado", "Aviso");
            }
            
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("você deseja formatar o Registro?", "AVISO!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                richTextBox1.ResetText();
                Limpar();
                richTextBox1.SaveFile("Teste.txt", RichTextBoxStreamType.PlainText);

            }
            else
            {
                MessageBox.Show("Nada foi formatado", "Aviso");
            }
        }

        private void formatarToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("você deseja formatar o Registro?", "AVISO!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                richTextBox1.ResetText();
                Limpar();
                richTextBox1.SaveFile("Teste.txt", RichTextBoxStreamType.PlainText);

            }
            else
            {
                MessageBox.Show("Nada foi formatado", "Aviso");
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}