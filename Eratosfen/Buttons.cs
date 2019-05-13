using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Eratosfen
{
    public partial class MainForm
    {
        // кнопка для автоматичного показу алгоритму
        private void AutoButton_Click(object sender, EventArgs e)
        {
            // вмикається таймер, і всі інші кнопки стають не активними, крім кнопки Stop і Continue
            timer1.Start();
            StepsButton.Enabled = false;
            AutoButton.Enabled = false;
            ClearButton.Enabled = false;
            // переносимо кнопку AutoButton_Click назад
            AutoButton.SendToBack();
        }

        // кнопка для зупинки таймера
        private void StopButton_Click(object sender, EventArgs e)
        {
            // зупиняє таймер, кнопка для очистки таблиці стає активною
            timer1.Stop();
            ClearButton.Enabled = true;
            // переносимо кнопку StopButton_Click назад
            StopButton.SendToBack();
        }

        // кнопка для продоження роботи таймера
        private void ContinueButton_Click(object sender, EventArgs e)
        {
            // запускає (продовжує) роботу таймера, кнопка для очистки таблиці стає не активною
            timer1.Start();
            ClearButton.Enabled = false;
            // переносимо кнопку StopButton_Click вперед 
            StopButton.BringToFront();
        }

        // кнопка нажавши яку зявляэться випадаюче меню
        private void ButtonInfo_Click(object sender, EventArgs e)
        {
            // якщо кількість натисків на кнопку ButtonInfo парне число показуємо випадаюче меню, інакше приховуємо
            if (clickCount % 2 == 0)
            {
                contextMenuStrip1.Show(ButtonInfo, new Point(0, ButtonInfo.Height));
            }
            else if (clickCount % 2 != 0)
            {
                contextMenuStrip1.Close();
            }
            clickCount++; // збільшуємо кількість натисків на кнопку ButtonInfo
        }

        // елемент випадаючого меню about program
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // якщо натиснутий елемент з випадаючого меню "about program", зчитуємо дані з файла "about_program.txt" і виводимо їх на  MessageBox
            string lines = File.ReadAllText("about_program.txt", Encoding.UTF8);
            MessageBox.Show(lines);
            clickCount++; // збільшуємо кількість натисків 
        }

        // елемент випадаючого меню about algorithm
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            // якщо натиснутий елемент з випадаючого меню "about algorithm", зчитуємо дані з файла "about_algorithm.txt" і виводимо їх на  MessageBox
            string lines = File.ReadAllText("about_algorithm.txt", Encoding.UTF8);
            MessageBox.Show(lines);
            clickCount++; // збільшуємо кількість натисків
        }

        // кнопка яка очищає (заммальовує) клітинки білим кольором і робить всі кнопки придатними для натискання
        private void ClearButton_Click(object sender, EventArgs e)
        {
            // очищає (заммальовує) клітинки білим кольором
            ClearAllCells();
            // обнуляємо індекси
            indexI = 0;
            indexJ = 0;
            // змінна nextNumber стає рівна двом
            nextNumber = 2;
            SetDoubleBuffer(dataGridView1, false);
            // кнопки стають активними
            StepsButton.Enabled = true;
            AutoButton.Enabled = true;
            StopButton.Enabled = true;
            ContinueButton.Enabled = true;
            AutoButton.BringToFront();
            ContinueButton.SendToBack();
            // зупиняємо таймер         
            timer1.Stop();
            // змінюємо тік таймера
            timer1.Interval = 1400;
            // очищяємо textBox-си
            textBox1.Clear();
            textBox2.Clear();
            // кнопка ClearButton стає не активною
            ClearButton.Enabled = false;
        }

        // кнопка для покрокового показу алгоритму
        private void StepsButton_Click_1(object sender, EventArgs e)
        {
            ClearButton.Enabled = true;
            StopButton.Enabled = false;
            ContinueButton.Enabled = false;
            AutoButton.Enabled = false;
            // якщо змінна nextNumber > 11, кнопка StepsButton стає не активною, і запускається робота таймера                      
            FillingCellsByColore();
            FillingCellsWithPrimes();
            // виводить номер числа яке зараз обробляється в textBox
            if (nextNumber < (int)Math.Sqrt(155))
            {
                if (arrayOfNumbersPaintedCells[indexI, indexJ] == 0 && (int)dataGridView1.Rows[indexI].Cells[indexJ].Value == nextNumber)
                {
                    textBox1.Text = nextNumber.ToString();                    
                }
            }
            else if (nextNumber == (int)Math.Sqrt(155))
            {
                StepsButton.Enabled = false;
                timer1.Start();
            }
            nextNumber++;
        }
    }
}
