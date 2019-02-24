using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Style;
using System;
using System.Drawing;
using System.IO;
using System.Text;

namespace EpplusStudyConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateExcel();
            CreateCharset();
            Console.ReadKey();
        }

        /// <summary>
        /// 创建Excel
        /// </summary>
        public static void CreateExcel()
        {
            FileInfo newFile = new FileInfo(@"e:\test.xlsx");
            if (newFile.Exists)
            {
                newFile.Delete();
                newFile = new FileInfo(@"e:\test.xlsx");
            }
            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("test");
                worksheet.Cells[1, 1].Value = "名称";
                worksheet.Cells[1, 2].Value = "价格";
                worksheet.Cells[1, 3].Value = "销量";

                worksheet.Cells[2, 1].Value = "大米";
                worksheet.Cells[2, 2].Value = 56;
                worksheet.Cells[2, 3].Value = 100;

                worksheet.Cells[3, 1].Value = "玉米";
                worksheet.Cells[3, 2].Value = 45;
                worksheet.Cells[3, 3].Value = 150;

                worksheet.Cells[4, 1].Value = "小米";
                worksheet.Cells[4, 2].Value = 38;
                worksheet.Cells[4, 3].Value = 130;

                worksheet.Cells[5, 1].Value = "糯米";
                worksheet.Cells[5, 2].Value = 22;
                worksheet.Cells[5, 3].Value = 200;
                #region 样式
                //worksheet.Cells["D2:D7"].Formula = "B2*C2";  //d2 = b2*c2; d3 = b3*c3; 
                //worksheet.Cells[6, 2, 6, 4].Formula = string.Format("SUBTOTAL(9,{0})", new ExcelAddress(2, 2, 5, 2).Address); //6,2 到 6,4 求和
                //worksheet.Cells[5, 3].Style.Numberformat.Format = "#,##0.00";//这是保留两位小数 5，3 保留了两位小数，没有小数，用0补足。
                //worksheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//水平居中
                //worksheet.Cells[1, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;//垂直居中
                //worksheet.Cells[1, 4, 1, 5].Merge = true;//合并单元格
                //worksheet.Cells.Style.WrapText = true;//自动换行
                //worksheet.Cells[1, 1].Style.Font.Bold = true;//字体为粗体
                //worksheet.Cells[1, 1].Style.Font.Color.SetColor(Color.Red);//字体颜色
                //worksheet.Cells[1, 1].Style.Font.Name = "微软雅黑";//字体
                //worksheet.Cells[1, 1].Style.Font.Size = 12;//字体大小
                //worksheet.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                //worksheet.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(128, 128, 128));//设置单元格背景色
                //worksheet.Cells[1, 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.FromArgb(255, 191, 191));//设置单元格所有边框
                //worksheet.Cells[1, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;//单独设置单元格底部边框样式和颜色（上下左右均可分开设置）
                //worksheet.Cells[1, 1].Style.Border.Bottom.Color.SetColor(Color.FromArgb(255, 102, 191));
                //worksheet.Cells.Style.ShrinkToFit = true;//单元格自动适应大小
                //worksheet.Row(1).Height = 15;//设置行高
                //worksheet.Row(1).CustomHeight = true;//自动调整行高
                //worksheet.Column(1).Width = 15;//设置列宽
                //worksheet.View.ShowGridLines = false;//去掉sheet的网格线
                //worksheet.Cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
                //worksheet.Cells.Style.Fill.BackgroundColor.SetColor(Color.Red);//设置背景色
                //worksheet.BackgroundImage.Image = Image.FromFile(@"05.jpg");//设置背景图片 wps中无法实现
                //ExcelPicture picture = worksheet.Drawings.AddPicture("logo", Image.FromFile(@"05.jpg"));//插入图片
                //picture.SetPosition(100, 100);//设置图片的位置
                //picture.SetSize(100, 100);//设置图片的大小
                //ExcelShape shape = worksheet.Drawings.AddShape("shape", eShapeStyle.Rect);//插入形状
                //shape.Font.Color = Color.Red;//设置形状的字体颜色
                //shape.Font.Size = 15;//字体大小
                //shape.Font.Bold = true;//字体粗细
                //shape.Fill.Style = eFillStyle.NoFill;//设置形状的填充样式
                //shape.Border.Fill.Style = eFillStyle.NoFill;//边框样式
                //shape.SetPosition(200, 300);//形状的位置
                //shape.SetSize(80, 30);//形状的大小
                //shape.Text = "test";//形状的内容
                //ExcelPicture picture = worksheet.Drawings.AddPicture("logo", Image.FromFile(@"05.jpg"), new ExcelHyperLink("http:\\www.baidu.com", UriKind.Relative));
                //worksheet.Cells[1, 1].Hyperlink = new ExcelHyperLink("http:\\www.baidu.com", UriKind.Relative);
                //worksheet.Hidden = eWorkSheetHidden.Hidden;//隐藏sheet
                //worksheet.Column(1).Hidden = true;//隐藏某一列
                //worksheet.Row(1).Hidden = true;//隐藏某一行
                #endregion
                package.Save();
            }
        }

        ///<summary>
        /// 生成图表
        /// </summary>
        public static void CreateCharset()
        {
            FileInfo newFile = new FileInfo(@"e:\test.xlsx");
            if (newFile.Exists)
            {
                newFile.Delete();
                newFile = new FileInfo(@"e:\test.xlsx");
            }
            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("test");

                worksheet.Cells.Style.WrapText = true;
                worksheet.View.ShowGridLines = false;//去掉sheet的网格线

                worksheet.Cells[1, 1].Value = "名称";
                worksheet.Cells[1, 2].Value = "价格";
                worksheet.Cells[1, 3].Value = "销量";

                worksheet.Cells[2, 1].Value = "大米";
                worksheet.Cells[2, 2].Value = 56;
                worksheet.Cells[2, 3].Value = 100;

                worksheet.Cells[3, 1].Value = "玉米";
                worksheet.Cells[3, 2].Value = 45;
                worksheet.Cells[3, 3].Value = 150;

                worksheet.Cells[4, 1].Value = "小米";
                worksheet.Cells[4, 2].Value = 38;
                worksheet.Cells[4, 3].Value = 130;

                worksheet.Cells[5, 1].Value = "糯米";
                worksheet.Cells[5, 2].Value = 22;
                worksheet.Cells[5, 3].Value = 200;

                using (ExcelRange range = worksheet.Cells[1, 1, 5, 3])
                {
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                }

                using (ExcelRange range = worksheet.Cells[1, 1, 1, 3])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Font.Color.SetColor(Color.White);
                    range.Style.Font.Name = "微软雅黑";
                    range.Style.Font.Size = 12;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(128, 128, 128));
                }

                worksheet.Cells[1, 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.FromArgb(191, 191, 191));
                worksheet.Cells[1, 2].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.FromArgb(191, 191, 191));
                worksheet.Cells[1, 3].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.FromArgb(191, 191, 191));

                worksheet.Cells[2, 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.FromArgb(191, 191, 191));
                worksheet.Cells[2, 2].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.FromArgb(191, 191, 191));
                worksheet.Cells[2, 3].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.FromArgb(191, 191, 191));

                worksheet.Cells[3, 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.FromArgb(191, 191, 191));
                worksheet.Cells[3, 2].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.FromArgb(191, 191, 191));
                worksheet.Cells[3, 3].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.FromArgb(191, 191, 191));

                worksheet.Cells[4, 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.FromArgb(191, 191, 191));
                worksheet.Cells[4, 2].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.FromArgb(191, 191, 191));
                worksheet.Cells[4, 3].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.FromArgb(191, 191, 191));

                worksheet.Cells[5, 1].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.FromArgb(191, 191, 191));
                worksheet.Cells[5, 2].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.FromArgb(191, 191, 191));
                worksheet.Cells[5, 3].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.FromArgb(191, 191, 191));

                ExcelChart chart = worksheet.Drawings.AddChart("chart", eChartType.ColumnClustered);

                ExcelChartSerie serie = chart.Series.Add(worksheet.Cells[2, 3, 5, 3], worksheet.Cells[2, 1, 5, 1]);
                serie.HeaderAddress = worksheet.Cells[1, 3];

                chart.SetPosition(150, 10);
                chart.SetSize(500, 300);
                chart.Title.Text = "销量走势";
                chart.Title.Font.Color = Color.FromArgb(89, 89, 89);
                chart.Title.Font.Size = 15;
                chart.Title.Font.Bold = true;
                chart.Style = eChartStyle.Style15;
                chart.Legend.Border.LineStyle = eLineStyle.Solid;
                chart.Legend.Border.Fill.Color = Color.FromArgb(217, 217, 217);

                package.Save();
            }
        }

        ///<summary>
        ///一些其他设置
        /// </summary>
        public static void CreateOther()
        {
            FileInfo newFile = new FileInfo(@"e:\test.xlsx");
            if (newFile.Exists)
            {
                newFile.Delete();
                newFile = new FileInfo(@"e:\test.xlsx");
            }
            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("test");
                //嵌入VBA代码
                worksheet.CodeModule.Name = "sheet";
                worksheet.CodeModule.Code = File.ReadAllText(@"VBA-Code\vba.txt", Encoding.Default);
                //Excel加密和锁定
                worksheet.Protection.IsProtected = true;//设置是否进行锁定
                worksheet.Protection.SetPassword("yk");//设置密码
                worksheet.Protection.AllowAutoFilter = false;//下面是一些锁定时权限的设置
                worksheet.Protection.AllowDeleteColumns = false;
                worksheet.Protection.AllowDeleteRows = false;
                worksheet.Protection.AllowEditScenarios = false;
                worksheet.Protection.AllowEditObject = false;
                worksheet.Protection.AllowFormatCells = false;
                worksheet.Protection.AllowFormatColumns = false;
                worksheet.Protection.AllowFormatRows = false;
                worksheet.Protection.AllowInsertColumns = false;
                worksheet.Protection.AllowInsertHyperlinks = false;
                worksheet.Protection.AllowInsertRows = false;
                worksheet.Protection.AllowPivotTables = false;
                worksheet.Protection.AllowSelectLockedCells = false;
                worksheet.Protection.AllowSelectUnlockedCells = false;
                worksheet.Protection.AllowSort = false;
                //下拉框
                var val = worksheet.DataValidations.AddListValidation(worksheet.Cells[7, 8].Address);//设置下拉框显示的数据区域
                val.Formula.ExcelFormula = "=parameter";//数据区域的名称
                val.Prompt = "下拉选择参数";//下拉提示
                val.ShowInputMessage = true;//显示提示内容
            }
        }
    }
}
