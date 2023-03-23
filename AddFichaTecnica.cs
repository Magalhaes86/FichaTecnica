using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SQLite;

using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Pdfa;
using iText.Layout.Borders;
using iText.Kernel.Colors;

namespace FichaTecnica
{
    public partial class AddFichaTecnica : Form
    {


        SQLiteConnection con = new SQLiteConnection("Data Source=C:\\FichaTecnicaSoft\\BaseDados\\FichaTecnica.sqlite; Version=3;");
        SQLiteCommand cmd;
        SQLiteDataAdapter adapt;
        
        int ID = 0;


        private static SQLiteConnection sqliteConnection;

        public AddFichaTecnica()
        {

            InitializeComponent();
        }

        int indexRow;
        private void LimpaDados()
        {
  
            textmaquina.Text = "";
            textqtd.Text = "";
            textcompSChanfro.Text = "";
            textCompCChanfro.Text = "";
            textferro.Text = "";
            textFinal.Text = "";
            comboBoxColas.Text = "";
            textCompound.Text = ""; ;

        }



    private void ImprimePDF()
        {
            {
                try
                {
                    List<AddFichaTecnica> orders = new List<AddFichaTecnica>();

                    orders.Add(new AddFichaTecnica { textcodcliente = textcodcliente });
                 //   orders.Add(new AddFichaTecnica { productId = 7561, product = "Logitech", price = 799, qty = 2 });
              


                    string filePath = @"C:\FichaTecnicaSoft\PDFResources\sRGB_CS_profile.icm";
                    string fontFile = @"C:\FichaTecnicaSoft\PDFResources\FreeSans.ttf";
                    string fileName = @"C:\FichaTecnicaSoft\FicheirosPDF\FichaN " + DateTime.Now.ToString("" + txtST_ID.Text+ " - " + "ddMMyyyy") + ".pdf";
                 //   string fileName = @"C:\FichaTecnicaSoft\FicheirosPDF\FichaN " + DateTime.Now.ToString("-" + txtST_ID.Text + " " + "ddMMyyyy") + ".pdf";

                    PdfADocument pdf = new PdfADocument(
                        new PdfWriter(fileName),
                        PdfAConformanceLevel.PDF_A_1B,
                        new PdfOutputIntent("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1",
                        new FileStream(filePath, FileMode.Open, FileAccess.Read)));
                   

                    PdfFont font = PdfFontFactory.CreateFont(fontFile, PdfEncodings.WINANSI,
                   PdfFontFactory.EmbeddingStrategy.FORCE_EMBEDDED);


                    Document document = new Document(pdf);

                    document.SetFont(font);


                    //  Paragraph header = new Paragraph("Ficha tecnica").SetTextAlignment(TextAlignment.CENTER).SetFontSize(20);
                    Paragraph Header1 = new Paragraph("Indurrolos").SetBold().SetTextAlignment(TextAlignment.LEFT).SetFontSize(20);
                    Paragraph header2 = new Paragraph("Ficha Técnica Nº" + txtST_ID.Text.ToString()).SetBold().SetTextAlignment(TextAlignment.RIGHT).SetFontSize(14);
                    Paragraph header3 = new Paragraph("Data:" + dtpDate.Text.ToString()).SetTextAlignment(TextAlignment.RIGHT).SetFontSize(12);

                    //     document.Add(header);
                    document.Add(Header1);
                    document.Add(header2);
                    document.Add(header3);
                    //  Paragraph subheader = new Paragraph("Observações: " + textobs.Text).SetTextAlignment(TextAlignment.CENTER).SetFontSize(15);
                    //  document.Add(subheader);

                    LineSeparator ls = new LineSeparator(new SolidLine());
                    document.Add(ls);

                    //   Paragraph sellerDetail = new Paragraph("Seller Company").SetTextAlignment(TextAlignment.LEFT);

                    //   document.Add(sellerDetail);

                    Paragraph customerAddress1 = new Paragraph("Nome: " + textNomeCliente.Text.ToString()).SetBold().SetTextAlignment(TextAlignment.LEFT).SetFontSize(18);
             //       Paragraph customerHeader = new Paragraph("Cod.Cliente: "+ textcodcliente.Text.ToString()).SetBold().SetTextAlignment(TextAlignment.LEFT).SetFontSize(12);


                    Paragraph CodClienteEcodsage = new Paragraph(" Cod.Cliente: " + textcodcliente.Text + " /  Cod.Sage: " + textcodsage.Text.ToString()).SetBold().SetTextAlignment(TextAlignment.LEFT).SetFontSize(12);

                    //     Paragraph customerAddress2 = new Paragraph("Mumbai 400054, Maharashtra India").SetTextAlignment(TextAlignment.RIGHT);

                    document.Add(customerAddress1);
             //       document.Add(customerHeader);

                    document.Add(CodClienteEcodsage);


                    //     Paragraph orderNo = new Paragraph("Ficha Tecnica Nº"+ txtST_ID.Text.ToString()).SetBold().SetTextAlignment(TextAlignment.LEFT);
                    Paragraph fichaobsespaco = new Paragraph("   ").SetTextAlignment(TextAlignment.LEFT);


                    Paragraph fichaobs = new Paragraph("Observações: " + textobs.Text.ToString()).SetBold().SetTextAlignment(TextAlignment.LEFT).SetFontSize(18);

                    //     document.Add(invoiceNo);

                    document.Add(fichaobsespaco);


                    Table table = new Table(8, true);

                    table.SetFontSize(8);
                    table.SetAutoLayout();
                   
                    //  table.setBorder(new SolidBorder(borderWidth));


                    Cell headerMaquina = new Cell(1, 1).SetBorder(new SolidBorder(ColorConstants.BLACK, 0.8f)).SetTextAlignment(TextAlignment.LEFT).SetFontSize(13).Add(new Paragraph("Máquina / Referência"));
                    Cell headerQtd = new Cell(1, 1).SetBorder(new SolidBorder(ColorConstants.BLACK, 0.8f)).SetTextAlignment(TextAlignment.CENTER).SetFontSize(11).Add(new Paragraph("Qtd"));
                    Cell headercompSChanfro = new Cell(1, 1).SetBorder(new SolidBorder(ColorConstants.BLACK, 0.8f)).SetBold().SetTextAlignment(TextAlignment.CENTER).SetFontSize(13).Add(new Paragraph("Comp Total"));
                    Cell headercompCChanfro = new Cell(1, 1).SetBorder(new SolidBorder(ColorConstants.BLACK, 0.8f)).SetTextAlignment(TextAlignment.CENTER).SetFontSize(13).Add(new Paragraph("Comp Chanfro"));
                    Cell headerFerro = new Cell(1, 1).SetBorder(new SolidBorder(ColorConstants.BLACK, 0.8f)).SetTextAlignment(TextAlignment.CENTER).SetFontSize(13).Add(new Paragraph("Ferro"));
                    Cell headerFinal = new Cell(1, 1).SetBorder(new SolidBorder(ColorConstants.BLACK, 0.8f)).SetBold().SetTextAlignment(TextAlignment.CENTER).SetFontSize(13).Add(new Paragraph("Final"));
                    Cell headerColas = new Cell(1, 1).SetBorder(new SolidBorder(ColorConstants.BLACK, 0.8f)).SetTextAlignment(TextAlignment.CENTER).SetFontSize(11).Add(new Paragraph("Colas"));
                    Cell headerCompound = new Cell(1, 1).SetBorder(new SolidBorder(ColorConstants.BLACK, 0.8f)).SetTextAlignment(TextAlignment.LEFT).SetFontSize(13).Add(new Paragraph("Compound"));



                    //         .SetBold().SetTextAlignment(TextAlignment.LEFT).SetFontSize(18)

                 

                    table.AddCell(headerMaquina); 
                    table.AddCell(headerQtd);
                    table.AddCell(headercompSChanfro);
                    table.AddCell(headercompCChanfro);
                    table.AddCell(headerFerro);
                    table.AddCell(headerFinal);
                    table.AddCell(headerColas);
                    table.AddCell(headerCompound);


                    // foreach (DataGridViewRow row in DataGridView1.Rows)
                    for (int z = 0; z < DataGridView1.Rows.Count; z++)
                    {
                       


                     

                        //       Cell product = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph(DataGridView1.Rows[z].Cells[0].Value.ToString()));
                        Cell Maquina = new Cell(1, 1).SetBorderBottom(new SolidBorder(ColorConstants.LIGHT_GRAY, 0.5f)).SetBorderRight(new SolidBorder(ColorConstants.LIGHT_GRAY, 0.5f)).SetBorderLeft(new SolidBorder(ColorConstants.BLACK, 0.8f)).SetTextAlignment(TextAlignment.LEFT).SetFontSize(13).Add(new Paragraph(DataGridView1.Rows[z].Cells[0].Value.ToString()));
                          Cell QTD = new Cell(1, 1).SetBorderBottom(new SolidBorder(ColorConstants.LIGHT_GRAY, 0.5f)).SetBorderRight(new SolidBorder(ColorConstants.LIGHT_GRAY, 0.5f)).SetTextAlignment(TextAlignment.CENTER).SetFontSize(11).Add(new Paragraph(DataGridView1.Rows[z].Cells[1].Value.ToString()));
                        Cell compSChanfro = new Cell(1, 1).SetBorderBottom(new SolidBorder(ColorConstants.LIGHT_GRAY, 0.5f)).SetBorderRight(new SolidBorder(ColorConstants.LIGHT_GRAY, 0.5f)).SetBold().SetTextAlignment(TextAlignment.CENTER).SetFontSize(13).Add(new Paragraph(DataGridView1.Rows[z].Cells[2].Value.ToString()));
                        Cell compCChanfro = new Cell(1, 1).SetBorderBottom(new SolidBorder(ColorConstants.LIGHT_GRAY, 0.5f)).SetBorderRight(new SolidBorder(ColorConstants.LIGHT_GRAY, 0.5f)).SetTextAlignment(TextAlignment.CENTER).SetFontSize(13).Add(new Paragraph(DataGridView1.Rows[z].Cells[3].Value.ToString()));
                        Cell Ferro = new Cell(1, 1).SetBorderBottom(new SolidBorder(ColorConstants.LIGHT_GRAY, 0.5f)).SetBorderRight(new SolidBorder(ColorConstants.LIGHT_GRAY, 0.5f)).SetTextAlignment(TextAlignment.CENTER).SetFontSize(13).Add(new Paragraph(DataGridView1.Rows[z].Cells[4].Value.ToString()));
                       Cell Final = new Cell(1, 1).SetBorderBottom(new SolidBorder(ColorConstants.LIGHT_GRAY, 0.5f)).SetBorderRight(new SolidBorder(ColorConstants.LIGHT_GRAY, 0.5f)).SetBold().SetTextAlignment(TextAlignment.CENTER).SetFontSize(13).Add(new Paragraph(DataGridView1.Rows[z].Cells[5].Value.ToString()));
                      Cell Colas = new Cell(1, 1).SetBorderBottom(new SolidBorder(ColorConstants.LIGHT_GRAY, 0.5f)).SetBorderRight(new SolidBorder(ColorConstants.LIGHT_GRAY, 0.5f)).SetTextAlignment(TextAlignment.CENTER).SetFontSize(11).Add(new Paragraph(DataGridView1.Rows[z].Cells[6].Value.ToString()));
                   Cell Compound = new Cell(1, 1).SetBorderBottom(new SolidBorder(ColorConstants.LIGHT_GRAY, 0.5f)).SetBorderRight(new SolidBorder(ColorConstants.BLACK, 0.8f)).SetTextAlignment(TextAlignment.LEFT).SetFontSize(13).Add(new Paragraph(DataGridView1.Rows[z].Cells[7].Value.ToString()));



                        //   cell.setBorder(new SolidBorder(borderWidth));



                        //  table.AddCell(Maquina);
                        table.AddCell(Maquina);
                        table.AddCell(QTD);
                            table.AddCell(compSChanfro);
                            table.AddCell(compCChanfro);
                            table.AddCell(Ferro);
                            table.AddCell(Final);
                        table.AddCell(Colas);
                          table.AddCell(Compound);

                    }
                    document.Add(table);
                    table.Flush();
                    table.Complete();


                    document.Add(fichaobsespaco);
                    document.Add(fichaobsespaco);
                    document.Add(fichaobs);





                    document.Close();

                      
                    System.Diagnostics.Process.Start(fileName);
                }
                catch (Exception ex)
                {

                }

            }
        }

        private void GuardaPDF2()
        {
            {
                
                    try
                    {
                        List<AddFichaTecnica> orders = new List<AddFichaTecnica>();

                        orders.Add(new AddFichaTecnica { textcodcliente = textcodcliente });
                        //   orders.Add(new AddFichaTecnica { productId = 7561, product = "Logitech", price = 799, qty = 2 });
                


                        string filePath = @"C:\FichaTecnicaSoft\PDFResources\sRGB_CS_profile.icm";
                        string fontFile = @"C:\FichaTecnicaSoft\PDFResources\FreeSans.ttf";
                        string fileName = @"C:\FichaTecnicaSoft\FicheirosPDF\FichaN " + DateTime.Now.ToString("" + txtST_ID.Text + " - " + "ddMMyyyy") + ".pdf";
                        //   string fileName = @"C:\FichaTecnicaSoft\FicheirosPDF\FichaN " + DateTime.Now.ToString("-" + txtST_ID.Text + " " + "ddMMyyyy") + ".pdf";

                     

                        PdfADocument pdf = new PdfADocument(
                            new PdfWriter(fileName),
                            PdfAConformanceLevel.PDF_A_1B,
                            new PdfOutputIntent("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1",
                            new FileStream(filePath, FileMode.Open, FileAccess.Read)));


                        PdfFont font = PdfFontFactory.CreateFont(fontFile, PdfEncodings.WINANSI,
                       PdfFontFactory.EmbeddingStrategy.FORCE_EMBEDDED);
                        Document document = new Document(pdf);

                        document.SetFont(font);


                        //  Paragraph header = new Paragraph("Ficha tecnica").SetTextAlignment(TextAlignment.CENTER).SetFontSize(20);
                        Paragraph Header1 = new Paragraph("Indurrolos").SetBold().SetTextAlignment(TextAlignment.LEFT).SetFontSize(20);
                        Paragraph header2 = new Paragraph("Ficha Técnica Nº" + txtST_ID.Text.ToString()).SetBold().SetTextAlignment(TextAlignment.RIGHT).SetFontSize(14);
                        Paragraph header3 = new Paragraph("Data:" + dtpDate.Text.ToString()).SetTextAlignment(TextAlignment.RIGHT).SetFontSize(12);

                        //     document.Add(header);
                        document.Add(Header1);
                        document.Add(header2);
                        document.Add(header3);
                        //  Paragraph subheader = new Paragraph("Observações: " + textobs.Text).SetTextAlignment(TextAlignment.CENTER).SetFontSize(15);
                        //  document.Add(subheader);

                        LineSeparator ls = new LineSeparator(new SolidLine());
                        document.Add(ls);


                        //     Paragraph sellerHeader = new Paragraph("Indurrolos").SetBold().SetTextAlignment(TextAlignment.LEFT);
                        //   Paragraph sellerDetail = new Paragraph("Seller Company").SetTextAlignment(TextAlignment.LEFT);

                        //     document.Add(sellerHeader);
                        //   document.Add(sellerDetail);


                        Paragraph customerAddress1 = new Paragraph("Nome: " + textNomeCliente.Text.ToString()).SetBold().SetTextAlignment(TextAlignment.LEFT).SetFontSize(18);
                        //       Paragraph customerHeader = new Paragraph("Cod.Cliente: "+ textcodcliente.Text.ToString()).SetBold().SetTextAlignment(TextAlignment.LEFT).SetFontSize(12);
                   

                        Paragraph CodClienteEcodsage = new Paragraph(" Cod.Cliente: " + textcodcliente.Text + " /  Cod.Sage: " + textcodsage.Text.ToString()).SetBold().SetTextAlignment(TextAlignment.LEFT).SetFontSize(12);

                        //     Paragraph customerAddress2 = new Paragraph("Mumbai 400054, Maharashtra India").SetTextAlignment(TextAlignment.RIGHT);

                        document.Add(customerAddress1);
                        //       document.Add(customerHeader);
                

                        document.Add(CodClienteEcodsage);




                        //     Paragraph orderNo = new Paragraph("Ficha Tecnica Nº"+ txtST_ID.Text.ToString()).SetBold().SetTextAlignment(TextAlignment.LEFT);
                        Paragraph fichaobsespaco = new Paragraph("   ").SetTextAlignment(TextAlignment.LEFT);
                     


                        Paragraph fichaobs = new Paragraph("Observações: " + textobs.Text.ToString()).SetBold().SetTextAlignment(TextAlignment.LEFT).SetFontSize(18);

                        //     document.Add(invoiceNo);
                   

                        document.Add(fichaobsespaco);


                        Table table = new Table(8, true);

                        table.SetFontSize(8);
                        table.SetAutoLayout();

                    Cell headerMaquina = new Cell(1, 1).SetBorder(new SolidBorder(ColorConstants.BLACK, 0.8f)).SetTextAlignment(TextAlignment.LEFT).SetFontSize(13).Add(new Paragraph("Máquina / Referência"));
                    Cell headerQtd = new Cell(1, 1).SetBorder(new SolidBorder(ColorConstants.BLACK, 0.8f)).SetTextAlignment(TextAlignment.CENTER).SetFontSize(11).Add(new Paragraph("Qtd"));
                    Cell headercompSChanfro = new Cell(1, 1).SetBorder(new SolidBorder(ColorConstants.BLACK, 0.8f)).SetBold().SetTextAlignment(TextAlignment.CENTER).SetFontSize(13).Add(new Paragraph("Comp Total"));
                    Cell headercompCChanfro = new Cell(1, 1).SetBorder(new SolidBorder(ColorConstants.BLACK, 0.8f)).SetTextAlignment(TextAlignment.CENTER).SetFontSize(13).Add(new Paragraph("Comp Chanfro"));
                    Cell headerFerro = new Cell(1, 1).SetBorder(new SolidBorder(ColorConstants.BLACK, 0.8f)).SetTextAlignment(TextAlignment.CENTER).SetFontSize(13).Add(new Paragraph("Ferro"));
                    Cell headerFinal = new Cell(1, 1).SetBorder(new SolidBorder(ColorConstants.BLACK, 0.8f)).SetBold().SetTextAlignment(TextAlignment.CENTER).SetFontSize(13).Add(new Paragraph("Final"));
                    Cell headerColas = new Cell(1, 1).SetBorder(new SolidBorder(ColorConstants.BLACK, 0.8f)).SetTextAlignment(TextAlignment.CENTER).SetFontSize(11).Add(new Paragraph("Colas"));
                    Cell headerCompound = new Cell(1, 1).SetBorder(new SolidBorder(ColorConstants.BLACK, 0.8f)).SetTextAlignment(TextAlignment.LEFT).SetFontSize(13).Add(new Paragraph("Compound"));



                    //         .SetBold().SetTextAlignment(TextAlignment.LEFT).SetFontSize(18)



                    table.AddCell(headerMaquina);
                    table.AddCell(headerQtd);
                    table.AddCell(headercompSChanfro);
                    table.AddCell(headercompCChanfro);
                    table.AddCell(headerFerro);
                    table.AddCell(headerFinal);
                    table.AddCell(headerColas);
                    table.AddCell(headerCompound);


                    // foreach (DataGridViewRow row in DataGridView1.Rows)
                    for (int z = 0; z < DataGridView1.Rows.Count; z++)
                    {





                        //       Cell product = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph(DataGridView1.Rows[z].Cells[0].Value.ToString()));
                        Cell Maquina = new Cell(1, 1).SetBorderBottom(new SolidBorder(ColorConstants.LIGHT_GRAY, 0.5f)).SetBorderRight(new SolidBorder(ColorConstants.LIGHT_GRAY, 0.5f)).SetBorderLeft(new SolidBorder(ColorConstants.BLACK, 0.8f)).SetTextAlignment(TextAlignment.LEFT).SetFontSize(13).Add(new Paragraph(DataGridView1.Rows[z].Cells[0].Value.ToString()));
                        Cell QTD = new Cell(1, 1).SetBorderBottom(new SolidBorder(ColorConstants.LIGHT_GRAY, 0.5f)).SetBorderRight(new SolidBorder(ColorConstants.LIGHT_GRAY, 0.5f)).SetTextAlignment(TextAlignment.CENTER).SetFontSize(11).Add(new Paragraph(DataGridView1.Rows[z].Cells[1].Value.ToString()));
                        Cell compSChanfro = new Cell(1, 1).SetBorderBottom(new SolidBorder(ColorConstants.LIGHT_GRAY, 0.5f)).SetBorderRight(new SolidBorder(ColorConstants.LIGHT_GRAY, 0.5f)).SetBold().SetTextAlignment(TextAlignment.CENTER).SetFontSize(13).Add(new Paragraph(DataGridView1.Rows[z].Cells[2].Value.ToString()));
                        Cell compCChanfro = new Cell(1, 1).SetBorderBottom(new SolidBorder(ColorConstants.LIGHT_GRAY, 0.5f)).SetBorderRight(new SolidBorder(ColorConstants.LIGHT_GRAY, 0.5f)).SetTextAlignment(TextAlignment.CENTER).SetFontSize(13).Add(new Paragraph(DataGridView1.Rows[z].Cells[3].Value.ToString()));
                        Cell Ferro = new Cell(1, 1).SetBorderBottom(new SolidBorder(ColorConstants.LIGHT_GRAY, 0.5f)).SetBorderRight(new SolidBorder(ColorConstants.LIGHT_GRAY, 0.5f)).SetTextAlignment(TextAlignment.CENTER).SetFontSize(13).Add(new Paragraph(DataGridView1.Rows[z].Cells[4].Value.ToString()));
                        Cell Final = new Cell(1, 1).SetBorderBottom(new SolidBorder(ColorConstants.LIGHT_GRAY, 0.5f)).SetBorderRight(new SolidBorder(ColorConstants.LIGHT_GRAY, 0.5f)).SetBold().SetTextAlignment(TextAlignment.CENTER).SetFontSize(13).Add(new Paragraph(DataGridView1.Rows[z].Cells[5].Value.ToString()));
                        Cell Colas = new Cell(1, 1).SetBorderBottom(new SolidBorder(ColorConstants.LIGHT_GRAY, 0.5f)).SetBorderRight(new SolidBorder(ColorConstants.LIGHT_GRAY, 0.5f)).SetTextAlignment(TextAlignment.CENTER).SetFontSize(11).Add(new Paragraph(DataGridView1.Rows[z].Cells[6].Value.ToString()));
                        Cell Compound = new Cell(1, 1).SetBorderBottom(new SolidBorder(ColorConstants.LIGHT_GRAY, 0.5f)).SetBorderRight(new SolidBorder(ColorConstants.BLACK, 0.8f)).SetTextAlignment(TextAlignment.LEFT).SetFontSize(13).Add(new Paragraph(DataGridView1.Rows[z].Cells[7].Value.ToString()));



                        table.AddCell(Maquina);
                            table.AddCell(QTD);
                            table.AddCell(compSChanfro);
                            table.AddCell(compCChanfro);
                            table.AddCell(Ferro);
                            table.AddCell(Final);
                            table.AddCell(Colas);
                            table.AddCell(Compound);

                        }
                        document.Add(table);
                        table.Flush();
                        table.Complete();


                        document.Add(fichaobsespaco);
                        document.Add(fichaobsespaco);
                        document.Add(fichaobs);





                        document.Close();

                        //  }
                        //   System.Diagnostics.Process.Start(fileName);
                    }
                catch (Exception ex)
                {

                }

            }
        }



        private void ADDFICHA2()
        {
            
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    //   cmd.CommandText = "INSERT INTO Clientes(id,CodSage, Nome, email ) values (@id,@CodSage, @nome, @email)";
                    cmd.CommandText = "INSERT INTO FichaTecnica(CodCliente,CodClienteSage,NomeCliente,tlmcliente,NumeroEncomenda,Data,Observacoes) values (@CodCliente,@CodClienteSage, @NomeCliente,@tlmcliente,@NumeroEncomenda,@Data,@Observacoes)";
                    //   cmd.CommandText = "INSERT INTO FichaTecnica(CodCliente,CodClienteSage, NomeCliente) values (@CodCliente,@CodClienteSage, @NomeCliente)";
                    cmd.Parameters.AddWithValue("@CodCliente", textcodcliente.Text);
                    cmd.Parameters.AddWithValue("@CodClienteSage", textcodsage.Text);
                    cmd.Parameters.AddWithValue("@NomeCliente", textNomeCliente.Text);
                    cmd.Parameters.AddWithValue("@tlmcliente", tbtlm.Text);
                    //tbtlm

                    cmd.Parameters.AddWithValue("@NumeroEncomenda", textencomenda.Text);
                    cmd.Parameters.AddWithValue("@Data", dtpDate.Text);
                    cmd.Parameters.AddWithValue("@Observacoes", textobs.Text);


                    cmd.ExecuteNonQuery();
                }
              //  MessageBox.Show("Dados Guardados ");

            }
        }


        private static SQLiteConnection DbConnection()
        {
            sqliteConnection = new SQLiteConnection("Data Source=C:\\FichaTecnicaSoft\\BaseDados\\FichaTecnica.sqlite; Version=3;");
            sqliteConnection.Open();
            return sqliteConnection;
        }



        SQLiteDataAdapter da = null;
        DataTable dt = new DataTable();

        
        private void NewId2()
        {

            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();


            using (var cmd = DbConnection().CreateCommand())
            {
                LimpaDados();
                cmd.CommandText = "SELECT MAX(Cod) + 1 as Id FROM FichaTecnica";
                da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                da.Fill(dt);
                txtST_ID.Text = cmd.ExecuteScalar().ToString();

                if (txtST_ID.Text.Length == 0)
                {
                    txtST_ID.Text = "1";
                }

            }
            }

        void cleardata()
        {

            textcodcliente.Clear();
            textcodsage.Clear();
            textNomeCliente.Clear();
            tbtlm.Clear();
            textencomenda.Clear();

            textobs.Clear();

            DataGridView1.DataSource = null;
            DataGridView1.Rows.Clear();
            DataGridView1.Refresh();

        }

        private void Label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button4_Click(object sender, EventArgs e)
        {

            listclientes frmlistcliente = new listclientes();
            frmlistcliente.ShowDialog();
           
        }


        private void saveFicha()
        {
            if (textcodcliente.Text == "")
            {
                MessageBox.Show("Inserir Codigo Cliente", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textcodcliente.Focus();
                return;
            }
            if (textcodsage.Text == "")
            {
                MessageBox.Show("Inserir Cod Cliente sage", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textcodsage.Focus();
                return;


            }
            if (textNomeCliente.Text == "")
            {
                MessageBox.Show("Inserir Nome Cliente", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textNomeCliente.Focus();
                return;

            }
            try
            {



                AddFichaTecnica fxt = new AddFichaTecnica();
                fxt.textcodcliente.Text = textcodcliente.Text;
                fxt.textcodsage.Text = textcodsage.Text;
                fxt.textNomeCliente.Text = textNomeCliente.Text;
                fxt.tbtlm.Text = tbtlm.Text;
                
                DalHelper.Add2(fxt);
               

                LimpaDados();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {


            if (textcodcliente.Text == "")
            {
                MessageBox.Show("Inserir Codigo Cliente", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textcodcliente.Focus();
                return;
            }
            if (textcodsage.Text == "")
            {
                MessageBox.Show("Inserir Cod Cliente sage", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textcodsage.Focus();
                return;


            }
            if (textNomeCliente.Text == "")
            {
                MessageBox.Show("Inserir Nome Cliente", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textNomeCliente.Focus();
                return;

            }
            if (DataGridView1 == null || DataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Inserir Dados na listagem", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textmaquina.Focus();
                return;

            }
            try
            {

          

                AddFichaTecnica fxt = new AddFichaTecnica();
                fxt.textcodcliente.Text = textcodcliente.Text;
                fxt.textcodsage.Text = textcodsage.Text;
                fxt.textNomeCliente.Text = textNomeCliente.Text;
                fxt.tbtlm.Text = tbtlm.Text;
               

                ADDFICHA2();
                AddLinhasFicha();

                btnUpdate.Enabled = true;
                btnSave.Enabled = false;
                add2.Enabled = true;

                
                LimpaDados();


               DialogResult dialogResult = MessageBox.Show("Deseja Imprimir ?", "Imprimir Documento", MessageBoxButtons.YesNo);
               if (dialogResult == DialogResult.Yes)
              {

                   ImprimePDF();

              }
               else if (dialogResult == DialogResult.No)
              {


                    GuardaPDF2();

                }

                

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }

          
                }

        private void btnNew_Click(object sender, EventArgs e)
        {
            //   NewId();
            cleardata();

            NewId2();
            Abtndicionar.Enabled = true;
            btnSave.Enabled = true;
          
   
        }

          
        private void button2_Click(object sender, EventArgs e)
        {
          
        }


        private void AddLinhasFicha()
        {
            using (var cmd = DbConnection().CreateCommand())
                for (int z = 0; z < DataGridView1.Rows.Count; z++)
                {
                    foreach (DataGridViewRow row in DataGridView1.Rows)

                   cmd.CommandText = "INSERT INTO LinhasFichaTecnica(NumeroDocumento, Maquina, Quantidade, CompraSChanfro, CompraCChamfro, Ferro, Final, Colas, Compound) values(@NumeroDocumento, @Maquina, @Quantidade, @CompraSChanfro, @CompraCChamfro, @Ferro, @Final, @Colas, @Compound)";

                    cmd.Parameters.AddWithValue("@NumeroDocumento", txtST_ID.Text);
                    cmd.Parameters.AddWithValue("@Maquina", DataGridView1.Rows[z].Cells[0].Value.ToString());
                    cmd.Parameters.AddWithValue("@Quantidade", DataGridView1.Rows[z].Cells[1].Value.ToString());
                    cmd.Parameters.AddWithValue("@CompraSChanfro", DataGridView1.Rows[z].Cells[2].Value.ToString());
                    cmd.Parameters.AddWithValue("@CompraCChamfro", DataGridView1.Rows[z].Cells[3].Value.ToString());
                    cmd.Parameters.AddWithValue("@Ferro", DataGridView1.Rows[z].Cells[4].Value.ToString());
                    cmd.Parameters.AddWithValue("@Final", DataGridView1.Rows[z].Cells[5].Value.ToString());
                    cmd.Parameters.AddWithValue("@Colas", DataGridView1.Rows[z].Cells[6].Value.ToString());
                    cmd.Parameters.AddWithValue("@Compound", DataGridView1.Rows[z].Cells[7].Value.ToString());

                    cmd.ExecuteNonQuery();

                }
            MessageBox.Show("Dados Guardados ");

        }


        private void Button1_Click_1(object sender, EventArgs e)
        {

            Updatelinhasdatagrid();

            }

       

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (textcodcliente.Text == "")
            {
                MessageBox.Show("Inserir Codigo Cliente", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textcodcliente.Focus();
                return;
            }
            if (textcodsage.Text == "")
            {
                MessageBox.Show("Inserir Cod Cliente sage", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textcodsage.Focus();
                return;


            }
            if (textNomeCliente.Text == "")
            {
                MessageBox.Show("Inserir Nome Cliente", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textNomeCliente.Focus();
                return;

            }
            if (DataGridView1 == null || DataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Inserir Dados na listagem", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textmaquina.Focus();
                return;

            }
            try
            {


                Updatecabecalho();
                Updatelinhasdatagrid();
                LimpaDados();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }


           

        }


        public void Updatecabecalho()
        {

            cmd = new SQLiteCommand("update FichaTecnica set NumeroEncomenda=@NumeroEncomenda,Observacoes=@Observacoes where Cod=@id", con);
            con.Open();
            cmd.Parameters.AddWithValue("@id", txtST_ID.Text);

            cmd.Parameters.AddWithValue("@NumeroEncomenda", textencomenda.Text);
            cmd.Parameters.AddWithValue("@Observacoes", textobs.Text);
            cmd.ExecuteNonQuery();
         //   MessageBox.Show("Atualizado com sucesso");

        }


        public void Updatelinhasdatagrid()
        {

            DataGridViewRow row = DataGridView1.Rows[indexRow];

            cmd = new SQLiteCommand("update LinhasFichaTecnica set Maquina=@Maquina,Quantidade=@Quantidade,CompraSChanfro=@CompraSChanfro, " +
                "CompraCChamfro =@CompraCChamfro, Ferro = @Ferro ,  Final = @Final ,Colas = @Colas , Compound = @Compound where NumeroDocumento = @NumeroDocumento", con);
           // con.Open();
            cmd.Parameters.AddWithValue("@NumeroDocumento", txtST_ID.Text);

            cmd.Parameters.AddWithValue("@Maquina", row.Cells[0].Value.ToString());
            cmd.Parameters.AddWithValue("@Quantidade", row.Cells[1].Value.ToString());
            cmd.Parameters.AddWithValue("@CompraSChanfro", row.Cells[2].Value.ToString());
            cmd.Parameters.AddWithValue("@CompraCChamfro", row.Cells[3].Value.ToString());

            cmd.Parameters.AddWithValue("@Ferro", row.Cells[4].Value.ToString());
            cmd.Parameters.AddWithValue("@Final", row.Cells[5].Value.ToString());
            cmd.Parameters.AddWithValue("@Colas", row.Cells[6].Value.ToString());
            cmd.Parameters.AddWithValue("@Compound", row.Cells[7].Value.ToString());

            cmd.ExecuteNonQuery();
            MessageBox.Show("Atualizado com sucesso");
       

            con.Close();
        }



        private void AddFichaTecnica_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtST_ID.Text))
            {
                btnSave.Enabled = false;
                btnUpdate.Enabled = false;
            
                Abtndicionar.Enabled = false;
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.DataGridView1.Rows[e.RowIndex];
                textmaquina.Text = row.Cells[0].Value.ToString();
                textqtd.Text = row.Cells[1].Value.ToString();
                textcompSChanfro.Text = row.Cells[2].Value.ToString();
                textCompCChanfro.Text = row.Cells[3].Value.ToString();
                textferro.Text = row.Cells[4].Value.ToString();
                textFinal.Text = row.Cells[3].Value.ToString();
                comboBoxColas.Text = row.Cells[4].Value.ToString();
                textCompound.Text = row.Cells[4].Value.ToString();

            }
        }
        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {

        }


   

        public void UpdateCabecalho()
        {

            cmd = new SQLiteCommand("update FichaTecnica set NumeroEncomenda=@NumeroEncomenda,Observacoes=@Observacoes where Cod=@id", con);
            con.Open();
            cmd.Parameters.AddWithValue("@id", txtST_ID.Text);

            cmd.Parameters.AddWithValue("@NumeroEncomenda", textencomenda.Text);
            cmd.Parameters.AddWithValue("@Observacoes", textobs.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Atualizado com sucesso");


        }



        private void add2_Click(object sender, EventArgs e)
        {
            ImprimePDF();
        }

        private void DataGridView1_DoubleClick(object sender, EventArgs e)
        {
 
    }



        public void ObterDadosCabecalho()
        {
            string constr = @"Data Source=C:\\FichaTecnicaSoft\\BaseDados\\FichaTecnica.sqlite; Version=3;";
            using (SQLiteConnection con = new SQLiteConnection(constr))
            {

               // cmd = new SQLiteCommand("update FichaTecnica set NumeroEncomenda=@NumeroEncomenda,Observacoes=@Observacoes where Cod '" + txtFilter.Text + "'", con);
                using (SQLiteCommand cmd = new SQLiteCommand("SELECT Cod,CodCliente, CodClienteSage,NomeCliente,NumeroEncomenda,Data,Observacoes FROM FichaTecnica WHERE Cod = '" + txtST_ID.Text + "'"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (SQLiteDataReader sdr = cmd.ExecuteReader())
                    {
                        sdr.Read();
                        txtST_ID.Text = sdr["Cod"].ToString();
                        textcodcliente.Text = sdr["CodCliente"].ToString();
                        textcodsage.Text = sdr["CodClienteSage"].ToString();
                        textNomeCliente.Text = sdr["NomeCliente"].ToString();
                        textencomenda.Text = sdr["NumeroEncomenda"].ToString();
                        dtpDate.Text = sdr["Data"].ToString();
                        textobs.Text = sdr["Observacoes"].ToString();
                    }
                    con.Close();
                }
            }
        }


        public void ObterDadosLinhas()
        {
            string constr = @"Data Source=C:\\FichaTecnicaSoft\\BaseDados\\FichaTecnica.sqlite; Version=3;";
            using (SQLiteConnection con = new SQLiteConnection(constr))
            {
                try
                {
                    con.Open();
                    if (con.State == ConnectionState.Open)
                    {
                       // MessageBox.Show("Connected to database");
                        dt = new DataTable();
                        // adapter = new SQLiteDataAdapter("SELECT * FROM Clientes", conn);Data

                      //  adapt = new SQLiteDataAdapter("SELECT LinhasFichaTecnica.Linha,LinhasFichaTecnica.NumeroDocumento,LinhasFichaTecnica.Maquina,LinhasFichaTecnica.Quantidade,LinhasFichaTecnica.CompraSChanfro  ,LinhasFichaTecnica.CompraCChamfro,LinhasFichaTecnica.Ferro       ,LinhasFichaTecnica.Final,LinhasFichaTecnica.Colas ,LinhasFichaTecnica.Compound FROM LinhasFichaTecnica  WHERE NumeroDocumento = '" + txtST_ID.Text + "'", con);
                       
                        adapt = new SQLiteDataAdapter("SELECT Maquina,Quantidade,CompraSChanfro,CompraCChamfro,Ferro,Final,Colas ,Compound FROM LinhasFichaTecnica  WHERE NumeroDocumento = '" + txtST_ID.Text + "'", con);
                        //Linha
                    
                        adapt.Fill(dt);
                        DataGridView1.DataSource = dt;

                        // LinhasFichaTecnica
                       
                      
                    //   DataGridView1.Columns[0].HeaderText = "Linha";
                       // DataGridView1.Columns[1].HeaderText = "NumeroDocumento";
                        DataGridView1.Columns[0].HeaderText = "Maquina";
                        DataGridView1.Columns[1].HeaderText = "Quantidade";

                        DataGridView1.Columns[2].HeaderText = "CompraSChanfro";
                        DataGridView1.Columns[3].HeaderText = "CompraCChamfro";
                        DataGridView1.Columns[4].HeaderText = "Ferro";


                        DataGridView1.Columns[5].HeaderText = "Final";
                        DataGridView1.Columns[6].HeaderText = "Colas";
                        DataGridView1.Columns[7].HeaderText = "Compound";




                        con.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        private void Addtodatagrid()
        {
        
            try
            {
                if (textmaquina.Text == "")
                {
                    MessageBox.Show("Inserir Máquina", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textmaquina.Focus();
                    return;
                }
                if (textqtd.Text == "")
                {
                    MessageBox.Show("Inserir Quantidade", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textqtd.Focus();
                    return;


                }
                if (textcompSChanfro.Text == "")
                {
                    MessageBox.Show("Inserir Comp S/Chanfro", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textcompSChanfro.Focus();
                    return;

                }
                if (textCompCChanfro.Text == "")
                {
                    MessageBox.Show("Inserir Comp C/Chanfro", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textCompCChanfro.Focus();
                    return;

                }
                if (textferro.Text == "")
                {
                    MessageBox.Show("inserir Ferro", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textferro.Focus();
                    return;


                }
                if (textFinal.Text == "")
                {
                    MessageBox.Show("Inserir Final", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textFinal.Focus();
                    return;

                }



                if (comboBoxColas.Text == "")
                {
                    MessageBox.Show("Inserir Colas", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    comboBoxColas.Focus();
                    return;

                }

                if (textCompound.Text == "")
                {
                    MessageBox.Show("Inserir Compound.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textCompound.Focus();
                    return;
                  
                }

             //   LimpaDados();

                if (DataGridView1.Rows.Count == 0)
                {
                    DataGridView1.Rows.Add(textmaquina.Text, textqtd.Text, textcompSChanfro.Text, textCompCChanfro.Text, textferro.Text, textFinal.Text, comboBoxColas.Text, textCompound.Text);
        
                    return;
                }
                
                foreach (DataGridViewRow row in DataGridView1.Rows)
                {
                    if (row.Cells[1].Value.ToString().Trim() == textmaquina.Text)
                    {
                        row.Cells[2].Value = textqtd.Text;
                        row.Cells[3].Value = textcompSChanfro.Text;
                        row.Cells[4].Value = textCompCChanfro.Text;
                        row.Cells[5].Value = textferro.Text;
                        row.Cells[6].Value = textFinal.Text;
                        row.Cells[7].Value = comboBoxColas.Text;
                        row.Cells[8].Value = textCompound.Text;

                       // LimpaDados();

                        return;

                    }
                   
                }

                DataGridView1.Rows.Add(textmaquina.Text, textqtd.Text, textcompSChanfro.Text, textCompCChanfro.Text, textferro.Text, textFinal.Text, comboBoxColas.Text, textCompound.Text);
            
            }

            catch (Exception ex)

            {
                Console.Write("Error info:" + ex.Message);
                //    MessageBox.Show("algo pendente para fazer!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }





        private void Addtodatagrid2()
        {


            if (textmaquina.Text == "")
            {

                textmaquina.Text =".";

            }

            try
            {
               


                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(DataGridView1);
            

                newRow.Cells[0].Value = textmaquina.Text;
                newRow.Cells[1].Value = textqtd.Text;
                newRow.Cells[2].Value = textcompSChanfro.Text;
                newRow.Cells[3].Value = textCompCChanfro.Text;
                newRow.Cells[4].Value = textferro.Text;
                newRow.Cells[5].Value = textFinal.Text;
                newRow.Cells[6].Value = comboBoxColas.Text;
                newRow.Cells[7].Value = textCompound.Text;


                DataGridView1.Rows.Add(newRow);
            }
            catch { }
      
       
        }





        private void btnAdd_Click(object sender, EventArgs e)
        {
            Addtodatagrid();
        //  LimpaDados();


        }




        public void editline2()
        {
            DialogResult dialogResult = MessageBox.Show("Pretende editar a linha selecionada?", "Editar Linha", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
              

               // indexRow = e.RowIndex; // Obter o indice da linha selecionada
                DataGridViewRow row = DataGridView1.Rows[indexRow];

                textmaquina.Text = row.Cells[0].Value.ToString();
                textqtd.Text = row.Cells[1].Value.ToString();
                textcompSChanfro.Text = row.Cells[2].Value.ToString();
                textCompCChanfro.Text = row.Cells[3].Value.ToString();
                textferro.Text = row.Cells[4].Value.ToString();
                textFinal.Text = row.Cells[5].Value.ToString();
                comboBoxColas.Text = row.Cells[6].Value.ToString();
                textCompound.Text = row.Cells[7].Value.ToString();
            }
            else if (dialogResult == DialogResult.No)
            {
                //caso pretenda fazer outra coisa qualuqer.
                textmaquina.Focus();
            }
        }




        private void bteditline_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Pretende editar a linha selecionada?", "Editar Linha", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
               
               
                btnUpdate.Enabled = true;
             
                //   indexRow = e.RowIndex; // Obter o indice da linha selecionada
                DataGridViewRow row = DataGridView1.Rows[indexRow];

                textmaquina.Text = row.Cells[0].Value.ToString();
                textqtd.Text = row.Cells[1].Value.ToString();
                textcompSChanfro.Text = row.Cells[2].Value.ToString();
                textCompCChanfro.Text = row.Cells[3].Value.ToString();
                textferro.Text = row.Cells[4].Value.ToString();
                textFinal.Text = row.Cells[5].Value.ToString();
                comboBoxColas.Text = row.Cells[6].Value.ToString();
                textCompound.Text = row.Cells[7].Value.ToString();
            }
            else if (dialogResult == DialogResult.No)
            {
                //caso pretenda fazer outra coisa qualuqer.
                textmaquina.Focus();
            }
        }


        private void UpdateLinhas()
        {

            try
            {
                if (textmaquina.Text == "")
                {
                    MessageBox.Show("Inserir Máquina", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textmaquina.Focus();
                    return;
                }
                if (textqtd.Text == "")
                {
                    MessageBox.Show("Inserir Quantidade", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textqtd.Focus();
                    return;


                }
                if (textcompSChanfro.Text == "")
                {
                    MessageBox.Show("Inserir Comp S/Chanfro", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textcompSChanfro.Focus();
                    return;

                }
                if (textCompCChanfro.Text == "")
                {
                    MessageBox.Show("Inserir Comp C/Chanfro", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textCompCChanfro.Focus();
                    return;

                }
                if (textferro.Text == "")
                {
                    MessageBox.Show("inserir Ferro", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textferro.Focus();
                    return;


                }
                if (textFinal.Text == "")
                {
                    MessageBox.Show("Inserir Final", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textFinal.Focus();
                    return;

                }



                if (comboBoxColas.Text == "")
                {
                    MessageBox.Show("Inserir Colas", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    comboBoxColas.Focus();
                    return;

                }

                if (textCompound.Text == "")
                {
                    MessageBox.Show("Inserir Compound.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textCompound.Focus();
                    return;

                }


                if (DataGridView1.Rows.Count == 0)
                {
                    DataGridView1.Rows.Add(textmaquina.Text, textqtd.Text, textcompSChanfro.Text, textCompCChanfro.Text, textferro.Text, textFinal.Text, comboBoxColas.Text, textCompound.Text);

             
                    return;
                   
                }
                // foreach (DataGridViewRow r in this.DataGridView1.Rows)
                DataGridViewRow newDataRow = DataGridView1.Rows[indexRow];
                newDataRow.Cells[0].Value = textmaquina.Text;
                newDataRow.Cells[1].Value = textqtd.Text;
                newDataRow.Cells[2].Value = textcompSChanfro.Text;
                newDataRow.Cells[3].Value = textCompCChanfro.Text;

                newDataRow.Cells[4].Value = textferro.Text;
                newDataRow.Cells[5].Value = textFinal.Text;
                newDataRow.Cells[6].Value = comboBoxColas.Text;
                newDataRow.Cells[7].Value = textCompound.Text;


           //     LimpaDados();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }

        }

        int selectedRow;

        private void UpdateLinhasSemValidacao()
        {


            selectedRow = DataGridView1.CurrentCell.RowIndex;
            DataGridViewRow newDataRow = DataGridView1.Rows[selectedRow];
            newDataRow.Cells[0].Value = textmaquina.Text;
            newDataRow.Cells[1].Value = textqtd.Text;
            newDataRow.Cells[2].Value = textcompSChanfro.Text;
            newDataRow.Cells[3].Value = textCompCChanfro.Text;

            newDataRow.Cells[4].Value = textferro.Text;
            newDataRow.Cells[5].Value = textFinal.Text;
            newDataRow.Cells[6].Value = comboBoxColas.Text;
            newDataRow.Cells[7].Value = textCompound.Text;

            textmaquina.Text = "";
            textqtd.Text = "";
            textcompSChanfro.Text = "";
            textCompCChanfro.Text = "";
            textferro.Text = "";
            textFinal.Text = "";
            comboBoxColas.Text = "";
            textCompound.Text = ""; ;

            this.DataGridView1.Update();
            this.DataGridView1.Refresh();

        }


   

     
        private void button3_Click_1(object sender, EventArgs e)
        {
            Listagemdefichas frmlistfichatecnica = new Listagemdefichas();
            frmlistfichatecnica.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(txtST_ID.Text))
            {
                MessageBox.Show("Pesquise primeiro o numero da Ficha", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                button3.Focus();
            }
            else

            ObterDadosCabecalho();
            ObterDadosLinhas();
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            label4.Visible = true;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            label4.Visible = false;
        }

        private void button5_MouseHover(object sender, EventArgs e)
        {
            label6.Visible = true;
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            label6.Visible = false;
        }

        private void Button4_MouseHover(object sender, EventArgs e)
        {
            label7.Visible = true;
        }

        private void Button4_MouseLeave(object sender, EventArgs e)
        {
            label7.Visible = false;
        }

     

       
       

        private void btnClose_MouseHover(object sender, EventArgs e)
        {
            label8.Visible = true;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            label8.Visible = false;
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {

            start fstart = (start)Application.OpenForms["start"];

            this.Hide();
            //   fstart.ShowDialog();
            this.Close();
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            label9.Visible = true;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            label9.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LimpaDados();
            textmaquina.Focus();
        }



        private void RemoveLinhasFicha()
        {



            SQLiteConnection con = new SQLiteConnection("Data Source = C:\\FichaTecnicaSoft\\BaseDados\\FichaTecnica.sqlite; Version = 3;");

            string sqlStatement = "DELETE FROM LinhasFichaTecnica WHERE NumeroDocumento= @id";



            // string sqlStatement = "DELETE FROM LinhasFichaTecnica WHERE NumeroDocumento =  txtST_ID.Text";

            try
            {
                con.Open();
                SQLiteCommand cmd = new SQLiteCommand(sqlStatement, con);
                cmd.Parameters.AddWithValue("@id", txtST_ID.Text);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

            }
            finally
            {
                con.Close();




            }
        }

        private void Removecabficha()
        {


            SQLiteConnection con = new SQLiteConnection("Data Source=C:\\FichaTecnicaSoft\\BaseDados\\FichaTecnica.sqlite; Version=3;");

            //     string sqlStatement = "DELETE FROM FichaTecnica WHERE Cod = 19";

            string sqlStatement = "DELETE FROM FichaTecnica WHERE Cod=@id";

            try
            {
                con.Open();
                SQLiteCommand cmd = new SQLiteCommand(sqlStatement, con);
                cmd.Parameters.AddWithValue("@id", txtST_ID.Text);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

            }
            finally
            {
                con.Close();
                MessageBox.Show("Dados Eliminados com Sucesso", "Eliminados dados!!", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }

        }



        private void button2_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtST_ID.Text))
            {
                MessageBox.Show("Escolha na lista de documentos, o documento que pretende anular");
                return;
            }

            DialogResult dialogResult = MessageBox.Show("Deseja ELIMINAR o Documento ?", "Eliminar Documento", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {

                RemoveLinhasFicha();
                Removecabficha();


                LimpaDados();
                this.Controls.Clear();
                this.InitializeComponent();
                Abtndicionar.Enabled = false;


            }

            else if (dialogResult == DialogResult.No)
                {
                    //caso pretenda fazer outra coisa qualuqer.
                    textmaquina.Focus();
                }
            }






        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            Addtodatagrid2();
        }


  
  

        private void button5_Click_2(object sender, EventArgs e)
        {
            UpdateLinhasSemValidacao();


        }

      

        private void Abtndicionar_Click(object sender, EventArgs e)
        {
            if (textcodcliente.Text == "")
            {
                MessageBox.Show("Tem de selecionar um cliente", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Button4.Focus();
                return;
            }

            try
            {
                Addtodatagrid2();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }

          
           
        }

        private void button5_Click_3(object sender, EventArgs e)
        {
            UpdateLinhasSemValidacao();
            button5.Enabled = false;
            button6.Enabled = false;
            button8.Enabled = false;
            Abtndicionar.Enabled = true;

        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
    }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
          
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
          
        }

        private void DataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            button7.Enabled = true;
     
        }

        private void button6_Click(object sender, EventArgs e)
        {
            {
                DialogResult dialogResult = MessageBox.Show("Deseja ELIMINAR a Linha selecionada?", "Eliminar Linha", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {

                    int rowIndex = DataGridView1.CurrentCell.RowIndex;
                    DataGridView1.Rows.RemoveAt(rowIndex);
                    LimpaDados();
              //      btnRemove.Visible = false;
              //      btnupdatelinha.Visible = false;
             //       btnAdd.Visible = true;
                  

                    textmaquina.Focus();
                    button6.Visible = false;
                    button8.Enabled = false;
                    button5.Enabled = false;
                    Abtndicionar.Enabled = true;
                    button7.Enabled = false;
                }
                else if (dialogResult == DialogResult.No)
                {
                    //caso pretenda fazer outra coisa qualuqer.
                    textmaquina.Focus();
                    button6.Visible = false;
                    LimpaDados();
                    button8.Enabled = false;
                    Abtndicionar.Enabled = true;
                    button5.Enabled = false;

                }
            }
    }

        private void bteditline_Click_1(object sender, EventArgs e)
        {


        
    }

        private void button7_Click(object sender, EventArgs e)
        {

       
                DialogResult dialogResult = MessageBox.Show("Deseja Editar a Linha selecionada?", "Editar Linha", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {



                DataGridViewCell cell = null;
                foreach (DataGridViewCell selectedCell in DataGridView1.SelectedCells)
                {
                    cell = selectedCell;
                    break;
                }
                if (cell != null)
                {
                    DataGridViewRow row = cell.OwningRow;


                    textmaquina.Text = row.Cells[0].Value.ToString();
                    textqtd.Text = row.Cells[1].Value.ToString();
                    textcompSChanfro.Text = row.Cells[2].Value.ToString();
                    textCompCChanfro.Text = row.Cells[3].Value.ToString();
                    textferro.Text = row.Cells[4].Value.ToString();
                    textFinal.Text = row.Cells[5].Value.ToString();
                    comboBoxColas.Text = row.Cells[6].Value.ToString();
                    textCompound.Text = row.Cells[7].Value.ToString();
                    button7.Enabled = false;
                    button8.Enabled = true;
                    button6.Visible = true;
                    Abtndicionar.Enabled = false;
                    button5.Enabled = true;
                    button6.Enabled = true;





                }

        
            }
                else if (dialogResult == DialogResult.No)
                {
                    //caso pretenda fazer outra coisa qualuqer.
                    textmaquina.Focus();
                button7.Enabled = false;
            }
            }

        private void DataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
       
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Deseja Cancelar a Edição da linha?", "Cancelar Edição", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                LimpaDados();
                this.DataGridView1.Refresh();
                button8.Enabled = false;

                Abtndicionar.Enabled = true;
                button5.Enabled = false;
                button6.Enabled = false;



            }
            else if (dialogResult == DialogResult.No)
            {
                //caso pretenda fazer outra coisa qualuqer.
                textmaquina.Focus();
            }
        }

        private void btnupdatelinha_Click(object sender, EventArgs e)
        {

        }
    }
    }
















