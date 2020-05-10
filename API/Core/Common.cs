using Microsoft.AspNetCore.Mvc;
using API.Models;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace API.Core
{
    public class Common
    {

    }

    public class Dashboard
    {

    }

    public class CoOrdnatorDashboard
    {
        public decimal TotalSuscription { get; set; }
        public decimal TotalCollection { get; set; }
        public decimal TotalDues { get; set; }
    }

    //public class MaintenanceDashboard
    //{
    //    public decimal TotalMaintenance { get; set; }
    //    public decimal TotalCollection { get; set; }
    //    public decimal TotalDues { get; set; }
    //    public List<Maintenance> listOfMaintenance { get; set; }
    //}

    //public class CommonLogic
    //{
    //   static string keyString = "E546C8DF278CD5931069B522E695D4F2";
    //    // DB001 context = new DB001Core();

    //    public string CreateInvoiceBody(string path, Maintenance maintenance)
    //    {
    //        string body = string.Empty;
    //        var file_path = path + "\\Templates\\InvoiceTemplate.html";

    //        using (StreamReader reader = new StreamReader(file_path))
    //        {
    //            body = reader.ReadToEnd();
    //        }

    //        #region Organisation
    //        body = body.Replace("{organisation_name}", maintenance.Organisation.OrganisationName);
    //        body = body.Replace("{organisation_logo}", ""); ///
    //        body = body.Replace("{organisation_address}", maintenance.Organisation.Address);
    //        body = body.Replace("{organisation_city_zip_state}", "");  ///    
    //        body = body.Replace("{organisation_phone_fax}", maintenance.Organisation.MobileNo);
    //        body = body.Replace("{organisation_email_web}", maintenance.Organisation.OrganisationEmail + " ● " + maintenance.Organisation.Email);
    //        body = body.Replace("{payment_info1}", "Payments,");
    //        body = body.Replace("{payment_info2}", "ACCOUNT NUMBER — 123006705");
    //        body = body.Replace("{payment_info3}", " ● IBAN — US100000060345");
    //        body = body.Replace("{payment_info4}", " ● SWIFT — BOA447");
    //        body = body.Replace("{payment_info5}", "");


    //        body = body.Replace("{generated_date_label}", "Invoice Date,");
    //        body = body.Replace("{generated_date}", DateTime.Now.ToString("dd-MMM-yyyy"));
    //        body = body.Replace("{payment_status_label}", "Payment Status :");
    //        body = body.Replace("{payment_status}", maintenance.PaymentStatus.Status);
    //        body = body.Replace("{currency_label}", "* All prices are in");
    //        body = body.Replace("{currency}", "INR");
    //        #endregion

    //        #region InvoiceTo
    //        body = body.Replace("{bill_to_label}", "To,");
    //        body = body.Replace("{owner_name}", maintenance.FirstName + " " + maintenance.LastName);
    //        body = body.Replace("{owner_address}", maintenance.FlatNumber + " " + "'" + maintenance.Block.BlockName + "'");

    //        string city_district_state_pincode = maintenance.Organisation.Address;
    //        body = body.Replace("{city_district_state_pincode}", city_district_state_pincode);
    //        body = body.Replace("{owner_phone_fax}", "+91-" + maintenance.Flat.MobileNo);
    //        body = body.Replace("{owner_email}", maintenance.Flat.Email);

    //        #endregion
    //        string invoiceNo = "#" + DateTime.Now.Ticks.ToString();
    //        body = body.Replace("{invoice_title}", "Invoice");
    //        body = body.Replace("{invoice_number}", maintenance.InvoiceNumber); ///
    //        body = body.Replace("{invoice_month_year}", maintenance.Month.MonthName + " " + maintenance.Year.ToString());


    //        #region InvoiceDetail,  
    //        body = body.Replace("{item_sr}", "Sr.");
    //        body = body.Replace("{item_description_label}", "Description");
    //        body = body.Replace("{item_line_total_label}", "Total");

    //        string strItem = "";

    //        int i = 1;
    //        decimal subtotal = 0;

    //        strItem += "<tr data-iterate=" + "item" + ">";

    //        strItem += "<td>" + i + "</td>";
    //        strItem += "<td> Maintenance Amount</td>";
    //        strItem += "<td>" + maintenance.Amount + "</td>";

    //        strItem += "</tr>";

    //        subtotal = subtotal + maintenance.Amount;

    //        foreach (var item in maintenance.MaintenanceInclusion)
    //        {
    //            i++;
    //            strItem += "<tr data-iterate=" + "item" + ">";

    //            strItem += "<td>" + i + "</td>";
    //            strItem += "<td>" + item.Name + "</td>";
    //            strItem += "<td>" + item.Amount + "</td>";

    //            strItem += "</tr>";

    //            subtotal = subtotal + item.Amount;

    //        }

    //        #endregion

    //        #region Total

    //        body = body.Replace("{invoice_details}", strItem);


    //        body = body.Replace("{amount_total_label}", "Total,");
    //        body = body.Replace("{amount_total}", maintenance.TotalAmount.ToString());
    //        body = body.Replace("{terms_label}", "Terms & Notes");
    //        body = body.Replace("{terms}", "Make sure that pay the maintenance before 10th of month to avoid penalty");

    //        #endregion

    //        return body;
    //    }

    //    public FileResult ConvertHtmlToPdf(string body, string fileName)
    //    {
    //        HtmlToPdf converter = new HtmlToPdf();
    //        PdfDocument doc = converter.ConvertHtmlString(body);
    //        byte[] pdf = doc.Save();
    //        doc.Close();

    //        FileResult fileResult = new FileContentResult(pdf, "application/pdf");
    //        fileResult.FileDownloadName = fileName;
    //        return fileResult;
    //    }

    //    public static string EncryptString(string text)
    //    {
    //        var key = Encoding.UTF8.GetBytes(keyString);

    //        using (var aesAlg = Aes.Create())
    //        {
    //            using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
    //            {
    //                using (var msEncrypt = new MemoryStream())
    //                {
    //                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
    //                    using (var swEncrypt = new StreamWriter(csEncrypt))
    //                    {
    //                        swEncrypt.Write(text);
    //                    }

    //                    var iv = aesAlg.IV;

    //                    var decryptedContent = msEncrypt.ToArray();

    //                    var result = new byte[iv.Length + decryptedContent.Length];

    //                    Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
    //                    Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

    //                    return Convert.ToBase64String(result);
    //                }
    //            }
    //        }
    //    }

    //    public static string DecryptString(string cipherText)
    //    {
    //        var fullCipher = Convert.FromBase64String(cipherText);

    //        var iv = new byte[16];
    //        var cipher = new byte[16];

    //        Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
    //        Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);
    //        var key = Encoding.UTF8.GetBytes(keyString);

    //        using (var aesAlg = Aes.Create())
    //        {
    //            using (var decryptor = aesAlg.CreateDecryptor(key, iv))
    //            {
    //                string result;
    //                using (var msDecrypt = new MemoryStream(cipher))
    //                {
    //                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
    //                    {
    //                        using (var srDecrypt = new StreamReader(csDecrypt))
    //                        {
    //                            result = srDecrypt.ReadToEnd();
    //                        }
    //                    }
    //                }

    //                return result;
    //            }
    //        }
    //    }
    //}


}
