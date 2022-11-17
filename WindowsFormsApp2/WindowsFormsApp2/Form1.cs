using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Data.Entity.Migrations.Model;
using Microsoft.Office.Interop.Excel;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        List<Flat> flats;
        RealEstateEntities re = new RealEstateEntities();

        Excel.Application xlApp;
        Excel.Workbook xlWB;
        Excel.Worksheet xlSheet;

        void LoadData()
        {
            flats = re.Flat.ToList();
        }

        void CreateExcel()
        {
            try
            {
                xlApp = new Excel.Application();
                xlWB = xlApp.Workbooks.Add(Missing.Value);
                xlSheet = xlApp.ActiveSheet;
                CreateTable();

                xlApp.Visible = true;
                xlApp.UserControl = true;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + '\n' + ex.Source, "error");
                xlWB.Close(false, Missing.Value, Missing.Value);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
            }
        }

        void CreateTable()
        {
            string[] headers = new string[]
            {
                "Kód",
                "Eladó",
                "Oldal",
                "Kerület",
                "Lift",
                "Szobák száma",
                "Alapterület",
                "Ár",
                "Négyzetméter ár"
            };

            for (int i = 0; i < headers.Length; i++)
            {
                xlSheet.Cells[1,i+1] = headers[i];
            }

            object[,] values = new object[flats.Count, headers.Length];
            int counter = 0;
            foreach (var f in flats)
            {
                values[counter, 0] = f.Code;
                values[counter, 1] = f.Vendor;
                values[counter, 2] = f.Side;
                values[counter, 3] = f.District;
                values[counter, 4] = f.Elevator;
                values[counter, 5] = f.NumberOfRooms;
                values[counter, 6] = f.FloorArea;
                values[counter, 7] = f.Price;
                values[counter, 8] = "";
            //    xlSheet.Cells[counter + 2, 1] = f.Code;
            //    xlSheet.Cells[counter + 2, 2] = f.Vendor;
                counter++;
            }

              xlSheet.get_Range( GetCell(2, 1), GetCell(1 + values.GetLength(0), values.GetLength(1))).Value = values;
                      
              xlSheet.get_Range(GetCell(2, 9),     GetCell(1 + values.GetLength(0), 9)).Value2= "=1000000*"+GetCell(2, 8)+"/"+GetCell(2,7);

            Excel.Range headerRange = xlSheet.get_Range(GetCell(1, 1), GetCell(1, headers.Length));
            headerRange.Font.Bold = true;
            headerRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            headerRange.EntireColumn.AutoFit();
            headerRange.RowHeight = 40;
            headerRange.Interior.Color = Color.LightBlue;
            headerRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);

            Excel.Range allrange = (Excel.Range)xlSheet.UsedRange;
            allrange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);
            Excel.Range oszlop1 = (Excel.Range)xlSheet.get_Range(GetCell(1, 1), GetCell(values.GetLength(0), 1));
            Excel.Range oszloput = (Excel.Range)xlSheet.get_Range(GetCell(1, headers.Length), GetCell(values.GetLength(0), headers.Length));
            oszlop1.Font.Bold = true;
            oszlop1.Interior.Color = Color.LightYellow;
            oszloput.Interior.Color = Color.LightGreen;
            oszloput.NumberFormat = "0.00";





        }

        private string GetCell(int x, int y)
        {
            string ExcelCoordinate = "";
            int dividend = y;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                ExcelCoordinate = Convert.ToChar(65 + modulo).ToString() + ExcelCoordinate;
                dividend = (int)((dividend - modulo) / 26);
            }
            ExcelCoordinate += x.ToString();

            return ExcelCoordinate;
        }

        public Form1()
        {
            InitializeComponent();
            this.Visible = false;
            LoadData();
            CreateExcel();
        }
    }
}
