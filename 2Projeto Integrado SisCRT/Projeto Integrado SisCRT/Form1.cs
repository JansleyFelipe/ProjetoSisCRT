using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto_Integrado_SisCRT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        double calcsalarliqui, salarbruto, inss, irrf, calc13,
               mesestrabalh, feriasliqui, diasferias, valortotalferias,
               bonus, valortotaltraba, valortotalbruto, basecalculo;

        private void txtbonus_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //Ele vai tentar converter o texto para double.
                double y = double.Parse(txtbonus.Text);
            }
            catch
            {
                //Se caso nao entre no try, eh porque o usuario digitou letras, logo, o sistema vai dar uma menssagem de erro.
                if(!string.IsNullOrEmpty(txtbonus.Text))
                {
                    MessageBox.Show("Digite apenas valores numéricos", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtbonus.Text = "0";
                    txtbonus.SelectAll();
                }
            }
        }
        private void txtsalariobruto_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //Ele vai tentar converter o texto para double.
                double y = double.Parse(txtsalariobruto.Text);
            }
            catch
            {
                //Se caso nao entre no try, eh porque o usuario digitou letras, logo, o sistema vai dar uma menssagem de erro.
                if(!string.IsNullOrEmpty(txtsalariobruto.Text))
                {
                    MessageBox.Show("Digite apenas valores numéricos", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtsalariobruto.Text = "0";
                    txtsalariobruto.SelectAll();
                }
            }
        }

        private void btnsair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btncalcferias_Click(object sender, EventArgs e)
        {
            try
            {
                salarbruto = double.Parse(txtsalariobruto.Text);
                diasferias = double.Parse(txtdferias.Text);

                // Calcular o valortotalferias ((salario bruto + dias de ferias) / 30).
                valortotalferias = ((salarbruto * diasferias) / 30);

                // Conseguindo o valortotalferias, conseguimos o valor do INSS dividindo pela a aliquota.
                if (valortotalferias >= 0 && valortotalferias <= 1659.38)
                {
                    inss = valortotalferias * 0.08;
                }
                else if (valortotalferias >= 1659.39 && valortotalferias <= 2765.66)
                {
                    inss = valortotalferias * 0.09;
                }
                else if (valortotalferias >= 2765.67 && valortotalferias <= 5531.31)
                {
                    inss = valortotalferias * 0.11;
                }
                else if (valortotalferias > 5531.31)
                {
                    inss = valortotalferias - 604.44;
                }

                // Precisamos do valor da base de calculo pra podermos calcular o IRRF.
                basecalculo = valortotalferias - inss;

                if (basecalculo >= 0 && basecalculo <= 1903.98)
                {
                    irrf = 0;
                }
                else if (basecalculo >= 1903.99 && basecalculo <= 2826.65)
                {
                    irrf = (basecalculo * 0.075) - 142.80;
                }
                else if (basecalculo >= 2826.66 && basecalculo <= 3751.05)
                {
                    irrf = (basecalculo * 0.15) - 354.80;
                }
                else if (basecalculo >= 3741.06 && basecalculo <= 4664.68)
                {
                    irrf = (basecalculo * 0.225) - 636.13;
                }
                else if (basecalculo > 4664.68)
                {
                    irrf = (basecalculo * 0.275) - 869.36;
                }

                // Assim, conseguimos o resultado final das ferias liquido.
                feriasliqui = basecalculo - irrf;

                //Fazer um arrendondamento para nao dar uma dizima periodica e para que os numeros saiam com 2 casas decimais.

                inss = Math.Round(inss, 2);
                irrf = Math.Round(irrf, 2);
                basecalculo = Math.Round(basecalculo, 2);
                feriasliqui = Math.Round(feriasliqui, 2);

                txtferiasliqui.Text = feriasliqui.ToString("n2");
                txtinss.Text = inss.ToString("n2");
                txtirrf.Text = irrf.ToString("n2");
                txtferiasliqui.Text = feriasliqui.ToString("n2");
            }
            catch
            {
                //Se o usuário não preencher todos os campos ou digitar letras, vai dar uma menssagem de erro.
                if ((string.IsNullOrEmpty(txtsalariobruto.Text)) || (string.IsNullOrEmpty(txtdferias.Text)))
                {
                    MessageBox.Show("Preencha todos os campos", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                txtsalariobruto.Focus();
                txtsalarioliqui.Clear();
                txtsalariobruto.Clear();
                txtmesestrab.Clear();
                txtbonus.Clear();
                txtdferias.Clear();
                txtferiasliqui.Clear();
                txt13liqui.Clear();
                txtbasecalc.Clear();
                txtinss.Clear();
                txtirrf.Clear();
            }
        }

        private void btnlimpar_Click(object sender, EventArgs e)
        {
            //Configuração do botão limpar, que ao apertá-lo limpa todos os campos.
            txtsalarioliqui.Clear();
            txtsalariobruto.Clear();
            txtmesestrab.Clear();
            txtbonus.Clear();
            txtdferias.Clear();
            txtferiasliqui.Clear();
            txt13liqui.Clear();
            txtirrf.Clear();
            txtinss.Clear();
            txtbasecalc.Clear();
        }

        private void btncalcliqui_Click(object sender, EventArgs e)
        {
            try
            {
                salarbruto = double.Parse(txtsalariobruto.Text);
                bonus = double.Parse(txtbonus.Text);

                //Calcula o valor total bruto.
                valortotalbruto = salarbruto + bonus;

                //Com o valor do valortotalbruto, conseguimos determinar o valor do INSS (valortotalbruto * Aliquota).
                if (valortotalbruto >= 0 && valortotalbruto <= 1659.38)
                {
                    inss = valortotalbruto * 0.08;
                }
                else if (valortotalbruto >= 1659.39 && valortotalbruto <= 2765.66)
                {
                    inss = valortotalbruto * 0.09;
                }
                else if (valortotalbruto >= 2765.67 && valortotalbruto <= 5531.31)
                {
                    inss = valortotalbruto * 0.11;
                }
                else if (valortotalbruto > 5531.31)
                {
                    inss = valortotalbruto - 608.44;
                }

                //Precisamos do valor da base de calculo, para saber valor do IRRF.
                basecalculo = valortotalbruto - inss;

                if (basecalculo >= 0 && basecalculo <= 1903.98)
                {
                    irrf = 0;
                }
                else if (basecalculo >= 1903.99 && basecalculo <= 2826.65)
                {
                    irrf = (basecalculo * 0.075) - 142.80;
                }
                else if (basecalculo >= 2826.66 && basecalculo <= 3751.05)
                {
                    irrf = (basecalculo * 0.15) - 354.80;
                }
                else if (basecalculo >= 3741.06 && basecalculo <= 4664.68)
                {
                    irrf = (basecalculo * 0.225) - 636.13;
                }
                else if (basecalculo > 4664.68)
                {
                    irrf = (basecalculo * 0.275) - 869.36;
                }

                //Valor final do salário liquido.
                calcsalarliqui = basecalculo - irrf;

                //Fazer um arrendondamento para nao dar uma dizima periodica e para que os numeros saiam com 2 casas decimais.

                inss = Math.Round(inss, 2);
                irrf = Math.Round(irrf, 2);
                calcsalarliqui = Math.Round(calcsalarliqui, 2);
                basecalculo = Math.Round(basecalculo, 2);

                txtsalarioliqui.Text = calcsalarliqui.ToString("n2");
                txtinss.Text = inss.ToString("n2");
                txtirrf.Text = irrf.ToString("n2");
                txtbasecalc.Text = basecalculo.ToString("n2");


            }
            catch
            {
                //Se o usuário não preencher todos os campos vai dar uma messagem de erro.
                if ((string.IsNullOrEmpty(txtsalariobruto.Text)) || (string.IsNullOrEmpty(txtbonus.Text)))
                {
                    MessageBox.Show("Preencha todos os campos", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                txtsalariobruto.Focus();
                txtsalarioliqui.Clear();
                txtsalariobruto.Clear();
                txtmesestrab.Clear();
                txtbonus.Clear();
                txtdferias.Clear();
                txtferiasliqui.Clear();
                txt13liqui.Clear();
                txtbasecalc.Clear();
                txtinss.Clear();
                txtirrf.Clear();

            }
        }

        private void btn13liqui_Click(object sender, EventArgs e)
        {
            try
            {
                //Convertemos o valor em Texto (String), para Double.
                salarbruto = double.Parse(txtsalariobruto.Text);
                mesestrabalh = double.Parse(txtmesestrab.Text);

                //Calcula o valor total de meses trabalhados.
                valortotaltraba = (salarbruto * mesestrabalh) / 12;

                //Com o valor total de meses trabalhados, conseguimos chegar ao valor do INSS (valor total de meses trabalhados * Aliquota).
                if (valortotaltraba >= 0 && valortotaltraba <= 1659.38)
                {
                    inss = valortotaltraba * 0.08;
                }
                else if (valortotaltraba >= 1659.39 && valortotaltraba <= 2765.66)
                {
                    inss = valortotaltraba * 0.09;
                }
                else if (valortotaltraba >= 2765.67 && valortotaltraba <= 5531.31)
                {
                    inss = valortotaltraba * 0.11;
                }
                else if (valortotaltraba > 5531.31)
                {
                    inss = valortotaltraba - 608.31;
                }

                //Precisamos do valor da base de calculo, para saber valor do IRRF.
                basecalculo = valortotaltraba - inss;


                if (basecalculo >= 0 && basecalculo <= 1903.98)
                {
                    irrf = 0;
                }
                else if (basecalculo >= 1903.99 && basecalculo <= 2826.65)
                {
                    irrf = (basecalculo * 0.075) - 142.80;
                }
                else if (basecalculo >= 2826.66 && basecalculo <= 3751.05)
                {
                    irrf = (basecalculo * 0.15) - 354.80;
                }
                else if (basecalculo >= 3741.06 && basecalculo <= 4664.68)
                {
                    irrf = (basecalculo * 0.225) - 636.13;
                }
                else if (basecalculo > 4664.68)
                {
                    irrf = (basecalculo * 0.275) - 869.36;
                }

                //Resultado final do 13° liquido.
                calc13 = basecalculo - irrf;

                //Fazer um arrendondamento para nao dar uma dizima periodica e para que os numeros saiam com 2 casas decimais.

                inss = Math.Round(inss, 2);
                irrf = Math.Round(irrf, 2);
                calc13 = Math.Round(calc13, 2);
                basecalculo = Math.Round(basecalculo, 2);

                txt13liqui.Text = calc13.ToString("n2");
                txtinss.Text = inss.ToString("n2");
                txtirrf.Text = irrf.ToString("n2");
                txtbasecalc.Text = basecalculo.ToString("n2");
            }
            catch
            {   
                //Se o usuário não preencher todos os campos vai dar uma menssagem de erro.
                if ((string.IsNullOrEmpty(txtsalariobruto.Text)) || (string.IsNullOrEmpty(txtmesestrab.Text)))
                {
                    MessageBox.Show("Preencha todos os campos", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                txtsalariobruto.Focus();
                txtsalarioliqui.Clear();
                txtsalariobruto.Clear();
                txtmesestrab.Clear();
                txtbonus.Clear();
                txtdferias.Clear();
                txtferiasliqui.Clear();
                txt13liqui.Clear();
                txtbasecalc.Clear();
                txtinss.Clear();
                txtirrf.Clear();
            }
        }

        private void txtmesestrab_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //Ele vai tentar converter o texto pra int e limitara o campo de 1 a 12, caso ultrapasse o valor excedido, vai dar uma menssagem de erro.
               int x = int.Parse(txtmesestrab.Text);
                if (!(x >= 1 && x <= 12))
                {
                    MessageBox.Show("Digite apenas valores entre 1 e 12", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtmesestrab.SelectAll();
                }
            }
            catch
            {
                //Se caso nao entre no try, eh porque o usuario digitou letras, logo, o sistema vai dar uma menssagem de erro.
                if (!string.IsNullOrEmpty(txtmesestrab.Text))
                {
                    MessageBox.Show("Digite apenas valores numéricos", "Erro", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    txtmesestrab.Text = "1";
                    txtmesestrab.SelectAll();
                }
            }
        }

        private void txtdferias_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //Ele vai tentar converter o texto pra int e limitara o campo de 0 a 30, caso ultrapasse o valor excedido, vai dar uma menssagem de erro.
               int x = int.Parse(txtdferias.Text);
                if (!(x >= 0 && x <= 30))
                {
                    MessageBox.Show("Digite apenas valores entre 0 e 30", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtdferias.SelectAll();
                }                
            }
            catch
            {
                //Se caso nao entre no try, eh porque o usuario digitou letras, logo, o sistema vai dar uma menssagem de erro.
                if (!string.IsNullOrEmpty(txtdferias.Text))
                {
                    MessageBox.Show("Digite apenas valores numéricos","Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtdferias.Text = "0";
                    txtdferias.SelectAll();
                }
            }
        }
        
    }
}
