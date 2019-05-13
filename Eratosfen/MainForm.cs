using System;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

namespace Eratosfen
{
    public partial class MainForm : Form
    {
        // масив з статусами клітинок       
        int[,] arrayOfNumbersPaintedCells;
        // глобальні (для класу) змінні які будуть використоуватись
        int nextNumber = 2, indexI = 0, indexJ = 0, clickCount = 0;
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ClearButton.Enabled = false;
            dataGridView1.Enabled = false;
            dataGridView1.Select();
            // блокує форму від змін
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            dataGridView1.RowCount = 11;
            dataGridView1.ColumnCount = 14;
            // задає шрифт тексу в dataGridView1
            this.dataGridView1.DefaultCellStyle.Font = new Font("Tahoma", 15);
            // робить ширину клітинок рівну 65
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1.Columns[i].Width = 65;
            }
            arrayOfNumbersPaintedCells = new int[dataGridView1.RowCount, dataGridView1.ColumnCount];
            PrintArrayWihtNumbers();
        }

        // прибирає мигання dataGridView1
        static void SetDoubleBuffer(Control ctl, bool DoubleBuffered)
        {
            typeof(Control).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, ctl, new object[] { DoubleBuffered });
        }

        // записує числа в клітинки dataGridView1
        void PrintArrayWihtNumbers()
        {
            int z = 2;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = z;
                    z++;
                }
            }
        }

        // замальовує білим кольором всі клітинки
        void ClearAllCells()
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                    arrayOfNumbersPaintedCells[i, j] = 0;
                }
            }
        }

        // замальовує клітинки в яких прості числа
        void FillingCellsWithPrimes()
        {
            dataGridView1.ClearSelection();
            bool b = false;
            for (int i = indexI; i < dataGridView1.RowCount; i++)
            {
                for (int j = indexJ; j < dataGridView1.ColumnCount; j++)
                {
                    if (arrayOfNumbersPaintedCells[i, j] == 0 && (int)dataGridView1.Rows[i].Cells[j].Value == nextNumber)
                    {
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Red;
                        textBox2.Text += nextNumber.ToString() + "   ";
                        indexI = i;
                        indexJ = j;
                        b = true;
                        break;
                    }
                    else if (arrayOfNumbersPaintedCells[i, j] == 1 && (int)dataGridView1.Rows[i].Cells[j].Value == nextNumber)
                    {
                        indexI = i;
                        indexJ = j;
                        b = true;
                        break;
                    }
                }
                if (b)
                {
                    break;
                }
            }
            ChooseNeededIndexj();
        }

        // вибирає необідний індекс стовпчика якщо виконуються одна з трьох умов
        void ChooseNeededIndexj()
        {
            if (indexJ != 2 && indexJ == 13 && indexJ != 8)
            {
                indexJ = 1;
            }
            else if (indexJ == 2)
            {
                indexJ = 1;
            }
            else if (indexJ == 8)
            {
                indexJ = 1;
            }
        }

        // замальовує відповідні клітинки (кратні прростим числам) відповідним кольором
        void FillingCellsByColore()
        {
            dataGridView1.ClearSelection();
            for (int i = indexI; i < dataGridView1.RowCount; i++)
            {

                for (int j = indexJ; j < dataGridView1.ColumnCount; j++)
                {
                    // якщо  елемент таблиці ділиться націло на число, яке обробляється в певний момент часу, і цей елемент більший-рівний ніж це число в квадраті, і статус клітинки рівний нулю  
                    if ((int)dataGridView1.Rows[i].Cells[j].Value % nextNumber == 0 && (int)dataGridView1.Rows[i].Cells[j].Value >= nextNumber * nextNumber && arrayOfNumbersPaintedCells[i, j] == 0)
                    {
                        switch (nextNumber)
                        {
                            case 2:
                                dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.GreenYellow;
                                arrayOfNumbersPaintedCells[i, j] = 1;
                                break;
                            case 3:
                                dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Blue;
                                arrayOfNumbersPaintedCells[i, j] = 1;
                                break;
                            case 5:
                                dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Yellow;
                                arrayOfNumbersPaintedCells[i, j] = 1;
                                break;
                            case 7:
                                dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.DarkGreen;
                                arrayOfNumbersPaintedCells[i, j] = 1;
                                break;
                            case 11:
                                dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Purple;
                                arrayOfNumbersPaintedCells[i, j] = 1;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        // таймер для автоматичного показу
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (nextNumber >= 152) // якщо nextNumber більше-рівне 152 зупиняємо таймер 
            {
                timer1.Stop();
                ClearButton.Enabled = true;
                StopButton.Enabled = false;
                ContinueButton.Enabled = false;
                MessageBox.Show("Клiтинки червоного кольору - простi числа.");
            }
            if (nextNumber > 6 && nextNumber < 11)
            {
                timer1.Interval = 700;
            }
            if (nextNumber == (int)Math.Sqrt(155) + 1)
            {
                SetDoubleBuffer(dataGridView1, true);
                SetDoubleBuffer(textBox2, true);
                timer1.Interval = 70;
            }            
            if (nextNumber < (int)Math.Sqrt(155))
            {
                FillingCellsByColore();
            }
            FillingCellsWithPrimes();
            nextNumber++;
        }
    }
}

