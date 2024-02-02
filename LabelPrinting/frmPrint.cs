using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using System.Configuration;

using System.Net;
using System.Net.Http;

using LabelPrinting.Models;
using Newtonsoft.Json;

namespace LabelPrinting
{
    public partial class frmPrint : Form
    {
        private string[] Args;
        private string apiString = ConfigurationManager.AppSettings["api"].ToString();
        private string labelFooter = ConfigurationManager.AppSettings["footer"].ToString();
        private string printerName = ConfigurationManager.AppSettings["printer"].ToString();
        private string pSize = ConfigurationManager.AppSettings["paper"].ToString();

        public frmPrint(string[] args)
        {
            InitializeComponent();
            this.Args = args;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printDocument();
        }

        private void printDocument()
        {
            if (this.Args[0] == "1")
            {
                this.printDrugLabel();
            }
            else
            {
                if (!(this.Args[0] == "2"))
                    return;
                this.printRegnLabel();
            }
        }

        private void printDrugLabel()
        {
            {
                PrintDocument printDocument = new PrintDocument();
                printDocument.PrinterSettings.PrinterName = printerName;
                foreach (PaperSize paperSize in printDocument.PrinterSettings.PaperSizes)
                {
                    if (paperSize.PaperName == pSize)
                    {
                        printDocument.PrinterSettings.DefaultPageSettings.PaperSize = paperSize;
                        printDocument.DefaultPageSettings.PaperSize = paperSize;
                    }
                }
                try
                {
                    MedicineLabelResponse data = new MedicineLabelResponse();
                    using (var client = new HttpClient())
                    {
                        try
                        {
                            var response = client.GetAsync(apiString + "DrugLabel").GetAwaiter().GetResult();
                            data = JsonConvert.DeserializeObject<MedicineLabelResponse>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                            if (response.StatusCode != HttpStatusCode.OK)
                            {

                            }
                            else
                            {
                                foreach(var each in data.MedicineLabels)
                                {
                                    printDocument.PrintPage += (PrintPageEventHandler)((sender, e) =>
                                    {
                                        int count = Regex.Matches(each.VsdDesc, "[\\S]+").Count;
                                        string[] strArray = each.VsdDesc.Split(' ');
                                        string str1 = "";
                                        string str2 = "";
                                        if (count <= 6)
                                        {
                                            for (int index = 0; index < count; ++index)
                                                str1 = str1 + strArray[index] + (object)' ';
                                            str2 = "";
                                        }
                                        if (count > 6)
                                        {
                                            for (int index = 0; index < count; ++index)
                                            {
                                                if (index < 6)
                                                    str1 = str1 + strArray[index] + (object)' ';
                                                else if (index >= 6)
                                                    str2 = str2 + strArray[index] + (object)' ';
                                            }
                                        }
                                        e.Graphics.DrawRectangle(new Pen(Brushes.Black), new Rectangle(0, 0, 260, 140));
                                        e.Graphics.DrawString((each.PatName) ?? "", new Font("Arial", 9f), Brushes.Black, 1f, 1f);
                                        e.Graphics.DrawString((each.MedName) ?? "", new Font("Arial", 8f), Brushes.Black, 1f, 20f);
                                        e.Graphics.DrawString(str1 ?? "", new Font("Arial", 9f), Brushes.Black, 1f, 40f);
                                        e.Graphics.DrawString(str2 ?? "", new Font("Arial", 9f), Brushes.Black, 1f, 60f);
                                        e.Graphics.DrawString("Qty:" + (each.VsdQnty) + " Keep out of children" + DateTime.Now.ToString("dd/MM/yyyy"), new Font("Arial", 8f), Brushes.Black, 1f, 80f);
                                        e.Graphics.DrawString((each.MedCatn), new Font("Arial", 9f), Brushes.Black, 1f, 100f);
                                        e.Graphics.DrawString(labelFooter, new Font("Arial", 6f), Brushes.Black, 1f, 120f);
                                    });
                                    printDocument.Print();
                                }
                            }
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                ResponseData resData = new ResponseData();
                using (var client = new HttpClient())
                {
                    try
                    {
                        var response = client.GetAsync(apiString + "DeleteDrugLabel").GetAwaiter().GetResult();
                        resData = JsonConvert.DeserializeObject<ResponseData>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                        if (response.StatusCode != HttpStatusCode.OK)
                        {

                        }
                        else
                        {
                            Console.WriteLine(resData.Message);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
        }

        private void printRegnLabel()
        {
            {
                PrintDocument printDocument = new PrintDocument();
                printDocument.PrinterSettings.PrinterName = printerName;
                foreach (PaperSize paperSize in printDocument.PrinterSettings.PaperSizes)
                {
                    if (paperSize.PaperName == pSize)
                    {
                        printDocument.PrinterSettings.DefaultPageSettings.PaperSize = paperSize;
                        printDocument.DefaultPageSettings.PaperSize = paperSize;
                    }
                }

                RegnLabelResponse data = new RegnLabelResponse();
                using (var client = new HttpClient())
                {
                    try
                    {
                        var response = client.GetAsync(apiString + "RegnLabel").GetAwaiter().GetResult();
                        data = JsonConvert.DeserializeObject<RegnLabelResponse>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                        if (response.StatusCode != HttpStatusCode.OK)
                        {

                        }
                        else
                        {
                            foreach (var each in data.RegnLabels)
                            {
                                printDocument.PrintPage += (PrintPageEventHandler)((sender, e) =>
                                {
                                    e.Graphics.DrawRectangle(new Pen(Brushes.Black), new Rectangle(0, 0, 260, 120));
                                    e.Graphics.DrawString("SPECIMAN :", new Font("Arial", 6f), Brushes.Black, 1f, 80f);
                                    e.Graphics.DrawString("NAME :" + each.PatName, new Font("Arial", 9f), Brushes.Black, 1f, 1f);
                                    e.Graphics.DrawString("IC   :" + each.PatIcno, new Font("Arial", 9f), Brushes.Black, 1f, 20f);
                                    e.Graphics.DrawString("DOB  :" + each.PatBdat.ToString("dd/MM/yyyy"), new Font("Arial", 9f), Brushes.Black, 1f, 40f);
                                    e.Graphics.DrawString("SEX  :" + each.PatGndr, new Font("Arial", 9f), Brushes.Black, 1f, 60f);
                                    e.Graphics.DrawString("AGE  :" + each.PatAged, new Font("Arial", 9f), Brushes.Black, 1f, 80f);
                                    e.Graphics.DrawString("DATE & TIME :", new Font("Arial", 9f), Brushes.Black, 1f, 100f);
                                });
                                printDocument.Print();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                ResponseData resData = new ResponseData();
                using (var client = new HttpClient())
                {
                    try
                    {
                        var response = client.GetAsync(apiString + "DeleteRegnLabel").GetAwaiter().GetResult();
                        resData = JsonConvert.DeserializeObject<ResponseData>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                        if (response.StatusCode != HttpStatusCode.OK)
                        {

                        }
                        else
                        {
                            Console.WriteLine(resData.Message);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        private void tmr_Tick(object sender, EventArgs e)
        {
            this.printDocument();
        }
    }
}
