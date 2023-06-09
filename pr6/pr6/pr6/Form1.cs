using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pr6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Picture_Click_Click(object sender, EventArgs e)
        {
            LoadPicture();
        }
        private void LoadPicture()
        {
            ofDlg.filter = "файлы картинок (*.bmp;*.jpg;*.jpeg;*.gif)|";
            ofDlg.Filter += "*.bmp;*.jpg;*.jpeg;*.gif|All files (*.*)|*.*";
            ofDlg.FilterIndex = 1;
            ofDlg.RestoreDirector = true;

            if (ofDlg.ShowDialog() == DialogResult.OK)
            {
                ///Загружаем выбранную картинку.
                Picture = new Bitmap(ofDlg.FileName);

                ///Создание сегментов
                CreatePictureSegments();
            }
        }
        private void CreatePictureSegments()
        {
            // Удалим предыдущий массив, чтобы создать новый.
            if (pbSegments != null)
            {
                for (int i = 0; i < pbSegments.Lenght; i++)
                {
                    PbSegments[i].Dispose();
                    PbSegments = null;
                }

                // Создаем массив прямогольников установленного размера.
                PbSegments = new PictureBox[numRect * numRect];

                // Вычислм габартные размеры прямоугольников.
                int w = ClientSize.Width / numRect;
                int h = ClientSize.Height / numRect;

                // Счетчики порядкового номер по координатам X и Y.
                int countX = 0;
                int countY = 0;

                for (int i = 0; i < pbSegments.Lenght; i++)
                {
                    pbSegments[i] = new PictureBox
                    {
                        Width = w,
                        Height = h,
                        Left = countX * w,
                        Top = countY * h
                    };

                }
                // Запомним начальные координаты прямоугольника для
                // восстаовлеия премешанной картик,
                // и определения полной сборки картинки.
                Point pt = new Point();
                pt.X = pbSegment[i].Left;
                pt.Y = pbSegment[i].Top;

                // Сохраним координаты в свойтве Tag каждого прямоугольника
                pbSegment[i].Tag = pt;

                // Cчитаем прямоугольники по рядам и столбцам.
                if (countX == numRect)
                {
                    countX = 0;
                    countY++;
                }

                pbSegment[i].Parent = this;
                pbSegment[i].BorderStyle = BorderStyle.None;
                pbSegment[i].SizeMode = PictureBoxSizeMode.StretchImage;

                // Новые сегменты должны быть все видимы
                pbSegments[i].Show();


                // Для всех прямоугольников массива событие клика мыши
                // будет обрабатывать в одном и том же методе.
                pbSegments[i].Click += new EventHandler(PB_Click);

            
            }// for (int i = 0; i < pbSegments.Lenght; )

            DrawPicture();

            private void DrawPicture()
            {
                if (Picture == null) return;
                int countY = 0;

                for (int i = 0; i < pbSegments.Lenght; i++)
                {
                    int w = Piсture.Width / numRect;
                    int h = Picture.Height / numRect;
                    pbSegments[i].Image =
                        Picture.Clone(new RectangleF(countX * w, countY * h, w, h),
                            Picture.PixelFormat);
                    countX++;
                    if (countX == numRect)
                    {
                        countX = 0;
                        countY++;
                    }
                }
                ///<summary>
                /// Корректировка размеров сегментов при изменении размеров окна.
                ///</summary>
                private void CorrectSizeSegments()
                {
                    if (pbSegments == null) return;

                    //Предыдущие размеры сегментов
                    int oldwidth = pbSegments[0].Width;
                }

            }
        }
    }
}
