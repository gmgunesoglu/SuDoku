/*   Gökhan Güneşoğlu  (gokhangunesoglu@gmail.com)  */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuDoku
{
    public partial class Form1 : Form
    {

        SuDoku sudoku = new SuDoku();
        SuDoku temp = new SuDoku();
        int a = 0;
        
        public Form1()
        {
            InitializeComponent();
            getAdressToSuDoku(sudoku);
        }

        private void GetInputs()
        {
            int i = 0;
            foreach (TextBox tb in groupBox1.Controls.OfType<TextBox>())
            {
                if (!tb.Text.Equals(""))
                {
                    int digit = Convert.ToInt32(tb.Text);
                    sudoku.block[i / 27 % 3, i / 9 % 3].cell[i / 3 % 3, i % 3].value = digit;
                    sudoku.block[i / 27 % 3, i / 9 % 3].cell[i / 3 % 3, i % 3].odds[0] = true;
                    sudoku.block[i / 27 % 3, i / 9 % 3].hasDigit[digit] = true;
                    sudoku.Y[((i / 27 % 3) * 3 + i / 3 % 3), digit] = true;
                    sudoku.X[((i / 9 % 3) * 3 + i % 3), digit] = true;
                    listBox1.Items.Add("[" + (i / 27 % 3) + "," + (i / 9 % 3) + "][" + ((i / 3) % 3) + "," + (i % 3) + "]=" + (sudoku.block[(i / 27) % 3, (i / 9) % 3].cell[(i / 3) % 3, i % 3].value) + " added.");
                }
                //tb.Text = "" + i;
                i++;
            }
            listBox1.Items.Add("==========================================");
        }

        private void KeyPressAllow(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                //TextBox tbx = new TextBox();
                e.Handled = false;
                //TextBox tbx = groupBox1.Controls.OfType<TextBox>(SelectNextControl());
                //tbx.ResetText();
            }
            else
            {
                e.Handled = true;
                MessageBox.Show("You Can Pess Just One Digit!");
            }
        }
       
        private void showDigits(SuDoku sudoku)
        {
            listBox1.Items.Add("==Shows Digits==");
            for (int i = 0; i < 9; i++)
            {
                listBox1.Items.Add("" + sudoku.block[i / 3 % 3, i % 3].hasDigit[1] + sudoku.block[i / 3 % 3, i % 3].hasDigit[2] + sudoku.block[i / 3 % 3, i % 3].hasDigit[3] +
                sudoku.block[i / 3 % 3, i % 3].hasDigit[4] + sudoku.block[i / 3 % 3, i % 3].hasDigit[5] + sudoku.block[i / 3 % 3, i % 3].hasDigit[6] +
                sudoku.block[i / 3 % 3, i % 3].hasDigit[7] + sudoku.block[i / 3 % 3, i % 3].hasDigit[8] + sudoku.block[i / 3 % 3, i % 3].hasDigit[9]+" "+(i+1)+".block");
            }
            listBox1.Items.Add("==========================================");
        }

        private void showXY(SuDoku sudoku)
        {
            listBox1.Items.Add("====X====");
            for(int i = 0; i < 9; i++)
            {
                listBox1.Items.Add("" + sudoku.X[i, 1] + sudoku.X[i, 2] + sudoku.X[i, 3] + sudoku.X[i, 4]
                + sudoku.X[i, 5] + sudoku.X[i, 6] + sudoku.X[i, 7] + sudoku.X[i, 8] + sudoku.X[i, 9] + " " + (i + 1) + ".column");
            }
            listBox1.Items.Add("====Y====");
            for (int i = 0; i < 9; i++)
            {
                listBox1.Items.Add("" + sudoku.Y[i, 1] + sudoku.Y[i, 2] + sudoku.Y[i, 3] + sudoku.Y[i, 4]
                + sudoku.Y[i, 5] + sudoku.Y[i, 6] + sudoku.Y[i, 7] + sudoku.Y[i, 8] + sudoku.Y[i, 9] + " " + (i + 1) + ".row");
            }
            listBox1.Items.Add("==========================================");
        }

        private void getAdressToSuDoku(SuDoku newSuDoku)
        {
            for (int i = 0; i < 9; i++)
            {
                newSuDoku.block[(i / 3) % 3, i % 3] = new Block();
                for (int j = 0; j < 9; j++)
                {
                    newSuDoku.block[(i / 3) % 3, i % 3].cell[j / 3 % 3, j % 3] = new Cell();
                    newSuDoku.block[(i / 3) % 3, i % 3].hasDigit[i] = new bool();
                    newSuDoku.X[i,j] = new bool();
                    newSuDoku.Y[i,j] = new bool();
                }
                newSuDoku.X[i,9] = new bool();
                newSuDoku.Y[i,9] = new bool();
            }
        }

        private void loadOdds() //bu sağlam
        {
            for(int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    if (sudoku.block[i / 3 % 3, i % 3].cell[j / 3 % 3, j % 3].value == 0)
                    {
                        for(int digit = 1; digit < 10; digit++)
                        {
                            if (!sudoku.block[i / 3 % 3, i % 3].hasDigit[digit] && !sudoku.Y[((i /3 ) * 3 + j / 3 ), digit] && !sudoku.X[((i % 3) * 3 + j % 3), digit])
                            {
                                sudoku.block[i / 3 % 3, i % 3].cell[j / 3 % 3, j % 3].odds[digit] = true;
                                //listBox1.Items.Add("[" + (i / 3 % 3 + 1) + "," + (i % 3+1) + "] [" + (j / 3 % 3+1) + "," + (j%3+1) + "] can be" + digit);
                            }
                        }
                    }
                }
            }
        }

        private void showOdds(SuDoku sudoku)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    //if (sudoku.block[i / 3 , i % 3].cell[j / 3, j % 3].value == 0)
                    //{
                        string message = "[" + (i / 3 + 1) + "," + (i % 3 + 1) + "][" + (j / 3 + 1) + "," + (j % 3 + 1) + "] can be:";
                        for (int digit = 1; digit < 10; digit++)
                        {
                            if (sudoku.block[i / 3, i % 3].cell[j / 3, j % 3].odds[digit])
                            {
                                message = message +" "+ digit;
                            }
                        }
                        listBox1.Items.Add(message);
                   // }
                }
            }
        }

        private void showCells(SuDoku sudoku)
        {
            for(int y = 0; y < 9; y++)
            {
                string str = ">>";
                for(int x = 0; x < 9; x++)
                {
                    str = str + " " + sudoku.block[y / 3, y % 3].cell[x / 3, x % 3].value;
                }
                listBox1.Items.Add(str);
            }
        }

        private void oddsOnJustOneCellInRow(SuDoku sudoku)   //testok
        {
            for (int digit = 1; digit < 10; digit++)
            {
                for (int i = 0; i < 9; i++)
                {
                    int count = 0, jlast = 0;

                    for (int j = 0; j < 9; j++)
                    {
                        if (sudoku.block[i / 3, j / 3].cell[i % 3, j % 3].odds[digit])
                        {
                            count++;
                            jlast = j;
                        }
                    }
                    if (count == 1)
                    {
                        coverCell(sudoku,i / 3, jlast / 3, i % 3, jlast % 3, digit);
                    }
                }
            }
        }

        private void oddsOnJustOneCellInColumn(SuDoku sudoku)    //testok
        {
            for (int digit = 1; digit < 10; digit++)
            {
                for (int i = 0; i < 9; i++)
                {
                    int count = 0, jlast = 0;

                    for (int j = 0; j < 9; j++)
                    {
                        if (sudoku.block[j/3, i/3].cell[j%3,i%3].odds[digit])
                        {
                            count++;
                            jlast = j;
                        }
                    }
                    if (count == 1)
                    {
                        coverCell(sudoku,jlast / 3, i / 3, jlast % 3, i % 3, digit);
                    }
                }
            }
        }

        private void oddsOnJustOneCellInBlock(SuDoku sudoku) //test ok
        {
            for(int digit = 1; digit < 10; digit++)
            {
                for(int i = 0; i < 9; i++)         
                {
                    int count = 0, jlast=0;
                    for (int j = 0; j < 9; j++)
                    {

                        if (sudoku.block[i / 3, i % 3].cell[j / 3, j % 3].odds[digit])
                        {
                            count++;
                            jlast = j;
                        }
                    }
                    if (count == 1)
                    {
                        coverCell(sudoku,i / 3,i%3,jlast/3,jlast%3,digit);
                    }
                }
            }
        }

        private bool cellHasJustOneOdds(SuDoku sudoku)   //testok
        {
            for(int i = 0; i < 9; i++)
            {
                for(int j=0; j < 9; j++)
                {
                    int count = 0, lastDigit = 0;

                    if (sudoku.block[i / 3, i % 3].cell[j / 3, j % 3].value==0)
                    {
                        for (int digit = 1; digit < 10; digit++)
                        {
                            if (sudoku.block[i / 3, i % 3].cell[j / 3, j % 3].odds[digit])
                            {
                                count++;
                                lastDigit = digit;
                            }
                        }
                        if (count == 1)
                        {
                            coverCell(sudoku,i / 3,i%3,j/3,j%3,lastDigit);
                        }
                        if (count == 0)
                        {
                            //MessageBox.Show("Error1");
                            return false;
                        }
                    }
                    //else
                    //{
                    //    for (int digit = 1; digit < 10; digit++)
                    //    {
                    //        if (sudoku.block[i / 3, i % 3].cell[j / 3, j % 3].odds[digit])
                    //        {
                    //            //MessageBox.Show("Error2");
                    //            return false;
                    //        }
                    //        if(!sudoku.block[i / 3, i % 3].cell[j / 3, j % 3].odds[0])
                    //        {
                    //            //MessageBox.Show("Error3");
                    //            return false;
                    //        }
                    //    }
                    //}
                }
            }
            return true;
        }


        //yy row, xx column for block
        //y row, x column for cell in block
        private void coverCell(SuDoku sudoku,int yy, int xx, int y, int x, int digit) //testok
        {
            a++;
            sudoku.Y[(yy * 3 + y), digit] = true;
            sudoku.X[(xx * 3 + x), digit] = true;
            sudoku.block[yy, xx].hasDigit[digit] = true;
            sudoku.block[yy, xx].cell[y, x].value = digit;
            sudoku.block[yy, xx].cell[y, x].odds[0] = true;
            int i=0, index = yy * 27 + xx * 9 + y * 3 + x;  
            foreach(TextBox tb in groupBox1.Controls.OfType<TextBox>()) //direk nasıl seçebilirim ?
            {
                if (i == index)
                {
                    tb.Text = "" + digit;
                    break;
                }
                i++;
            }
            //removes odds(digit) in block                              for içine alsam mı daha iyi yoksa böyle mi ?
            sudoku.block[yy, xx].cell[0, 0].odds[digit] = false;
            sudoku.block[yy, xx].cell[0, 1].odds[digit] = false;
            sudoku.block[yy, xx].cell[0, 2].odds[digit] = false;
            sudoku.block[yy, xx].cell[1, 0].odds[digit] = false;
            sudoku.block[yy, xx].cell[1, 1].odds[digit] = false;
            sudoku.block[yy, xx].cell[1, 2].odds[digit] = false;
            sudoku.block[yy, xx].cell[2, 0].odds[digit] = false;
            sudoku.block[yy, xx].cell[2, 1].odds[digit] = false;
            sudoku.block[yy, xx].cell[2, 2].odds[digit] = false;
            //removes odds(digit) in row
            sudoku.block[yy, 0].cell[y, 0].odds[digit] = false;
            sudoku.block[yy, 0].cell[y, 1].odds[digit] = false;
            sudoku.block[yy, 0].cell[y, 2].odds[digit] = false;
            sudoku.block[yy, 1].cell[y, 0].odds[digit] = false;
            sudoku.block[yy, 1].cell[y, 1].odds[digit] = false;
            sudoku.block[yy, 1].cell[y, 2].odds[digit] = false;
            sudoku.block[yy, 2].cell[y, 0].odds[digit] = false;
            sudoku.block[yy, 2].cell[y, 1].odds[digit] = false;
            sudoku.block[yy, 2].cell[y, 2].odds[digit] = false;     
            //removes odds(digit) in colum
            sudoku.block[0, xx].cell[0, x].odds[digit] = false;
            sudoku.block[0, xx].cell[1, x].odds[digit] = false;
            sudoku.block[0, xx].cell[2, x].odds[digit] = false;
            sudoku.block[1, xx].cell[0, x].odds[digit] = false;
            sudoku.block[1, xx].cell[1, x].odds[digit] = false;
            sudoku.block[1, xx].cell[2, x].odds[digit] = false;
            sudoku.block[2, xx].cell[0, x].odds[digit] = false;
            sudoku.block[2, xx].cell[1, x].odds[digit] = false;
            sudoku.block[2, xx].cell[2, x].odds[digit] = false;
            //removes all odds on covered cell
            sudoku.block[yy, xx].cell[y, x].odds[1] = false;
            sudoku.block[yy, xx].cell[y, x].odds[2] = false;
            sudoku.block[yy, xx].cell[y, x].odds[3] = false;
            sudoku.block[yy, xx].cell[y, x].odds[4] = false;
            sudoku.block[yy, xx].cell[y, x].odds[5] = false;
            sudoku.block[yy, xx].cell[y, x].odds[6] = false;
            sudoku.block[yy, xx].cell[y, x].odds[7] = false;
            sudoku.block[yy, xx].cell[y, x].odds[8] = false;
            sudoku.block[yy, xx].cell[y, x].odds[9] = false;
        }
        
        private void twoOddsTwoCellInBlock(SuDoku sudoku)    //test ok
        {
            for (int i = 0; i < 9; i++)
            {
                for(int digit1 = 1; digit1 < 9; digit1++)
                {
                    if (!sudoku.block[i / 3, i % 3].hasDigit[digit1])
                    {
                        int count1 = 0;
                        for(int j = 0; j < 9; j++)
                        {
                            if (sudoku.block[i/3,i%3].cell[j/3,j%3].odds[digit1])
                            {
                                count1++;
                            }
                        }
                        if (count1 == 2)
                        {
                            for(int digit2 = digit1 + 1; digit2 < 10; digit2++)
                            {
                                if (!sudoku.block[i / 3, i % 3].hasDigit[digit2])
                                {
                                    int count2 = 0;
                                    for (int j = 0; j < 9; j++)
                                    {
                                        if (sudoku.block[i / 3, i % 3].cell[j / 3, j % 3].odds[digit1]|| sudoku.block[i / 3, i % 3].cell[j / 3, j % 3].odds[digit2])
                                        {
                                            count2++;
                                        }
                                    }
                                    if (count2 == 2)
                                    {
                                        listBox1.Items.Add("[" + (i / 3 + 1) + "," + (i % 3 + 1) + "] has 2->2 : " + digit1 + " " + digit2);
                                        for (int j = 0; j < 9; j++)
                                        {
                                            if (sudoku.block[i / 3, i % 3].cell[j / 3, j % 3].odds[digit1] || sudoku.block[i / 3, i % 3].cell[j / 3, j % 3].odds[digit2])
                                            {
                                                for (int k = 1; k < 10; k++)
                                                {
                                                    if (k != digit1 && k != digit2)
                                                    {
                                                        sudoku.block[i / 3, i % 3].cell[j / 3, j % 3].odds[k] = false;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void treeOddsTreeCellInBlock(SuDoku sudoku)  //testok
        {
            for (int i = 0; i < 9; i++)
            {
                for (int digit1 = 1; digit1 < 8; digit1++)
                {
                    if (!sudoku.block[i / 3, i % 3].hasDigit[digit1])
                    {
                        int count1 = 0;
                        for (int j = 0; j < 9; j++)
                        {
                            if (sudoku.block[i / 3, i % 3].cell[j / 3, j % 3].odds[digit1])
                            {
                                count1++;
                            }
                        }
                        if (count1 <= 3)
                        {
                            for (int digit2 = digit1 + 1; digit2 < 9; digit2++)
                            {
                                if (!sudoku.block[i / 3, i % 3].hasDigit[digit2])
                                {
                                    int count2 = 0;
                                    for (int j = 0; j < 9; j++)
                                    {
                                        if (sudoku.block[i / 3, i % 3].cell[j / 3, j % 3].odds[digit1] || sudoku.block[i / 3, i % 3].cell[j / 3, j % 3].odds[digit2])
                                        {
                                            count2++;
                                        }
                                    }
                                    if (count2 <= 3)
                                    {
                                    for(int digit3 = digit2 + 1; digit3 < 10; digit3++)
                                        {
                                            if (!sudoku.block[i / 3, i % 3].hasDigit[digit3])
                                            {
                                                int count3 = 0;
                                                for(int j = 0; j < 9; j++)
                                                {
                                                    if (sudoku.block[i / 3, i % 3].cell[j / 3, j % 3].odds[digit1] || sudoku.block[i / 3, i % 3].cell[j / 3, j % 3].odds[digit2]
                                                        || sudoku.block[i / 3, i % 3].cell[j / 3, j % 3].odds[digit3])
                                                    {
                                                        count3++;
                                                    }
                                                }
                                                if(count3 <= 3)
                                                {
                                                    listBox1.Items.Add("[" + (i / 3 + 1) + "," + (i % 3 + 1) + "] has 3->3 : " + digit1 + " " + digit2+" "+digit3);
                                                    for (int j = 0; j < 9; j++)
                                                    {
                                                        if (sudoku.block[i / 3, i % 3].cell[j / 3, j % 3].odds[digit1] || sudoku.block[i / 3, i % 3].cell[j / 3, j % 3].odds[digit2]
                                                        || sudoku.block[i / 3, i % 3].cell[j / 3, j % 3].odds[digit3])
                                                        {
                                                            for (int k = 1; k < 10; k++)
                                                            {
                                                                if (k != digit1 && k != digit2 && k != digit3)
                                                                {
                                                                    sudoku.block[i / 3, i % 3].cell[j / 3, j % 3].odds[k] = false;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void fourOddsFourCellInBlock(SuDoku sudoku)  //testok
        {
            for (int i = 0; i < 9; i++)
            {
                for (int digit1 = 1; digit1 < 7; digit1++)
                {
                    if (!sudoku.block[i / 3, i % 3].hasDigit[digit1])
                    {
                        int count1 = 0;
                        for (int j = 0; j < 9; j++)
                        {
                            if (sudoku.block[i / 3, i % 3].cell[j / 3, j % 3].odds[digit1])
                            {
                                count1++;
                            }
                        }
                        if (count1 <= 4)
                        {
                            for (int digit2 = digit1 + 1; digit2 < 8; digit2++)
                            {
                                if (!sudoku.block[i / 3, i % 3].hasDigit[digit2])
                                {
                                    int count2 = 0;
                                    for (int j = 0; j < 9; j++)
                                    {
                                        if (sudoku.block[i / 3, i % 3].cell[j / 3, j % 3].odds[digit1] || sudoku.block[i / 3, i % 3].cell[j / 3, j % 3].odds[digit2])
                                        {
                                            count2++;
                                        }
                                    }
                                    if (count2 <= 4)
                                    {
                                        for (int digit3 = digit2 + 1; digit3 < 9; digit3++)
                                        {
                                            if (!sudoku.block[i / 3, i % 3].hasDigit[digit3])
                                            {
                                                int count3 = 0;
                                                for (int j = 0; j < 9; j++)
                                                {
                                                    if (sudoku.block[i / 3, i % 3].cell[j / 3, j % 3].odds[digit1] || sudoku.block[i / 3, i % 3].cell[j / 3, j % 3].odds[digit2]
                                                        || sudoku.block[i / 3, i % 3].cell[j / 3, j % 3].odds[digit3])
                                                    {
                                                        count3++;
                                                    }
                                                }
                                                if (count3 <= 4)
                                                {
                                                    for (int digit4 = digit3 + 1; digit4 < 10; digit4++)
                                                    {
                                                        if (!sudoku.block[i / 3, i % 3].hasDigit[digit4])
                                                        {
                                                            int count4 = 0;
                                                            for(int j = 0; j < 9; j++)
                                                            {
                                                                if (sudoku.block[i / 3, i % 3].cell[j / 3, j % 3].odds[digit1] || sudoku.block[i / 3, i % 3].cell[j / 3, j % 3].odds[digit2]
                                                                    || sudoku.block[i / 3, i % 3].cell[j / 3, j % 3].odds[digit3] || sudoku.block[i / 3, i % 3].cell[j / 3, j % 3].odds[digit4])
                                                                {
                                                                    count4++;
                                                                }
                                                            }
                                                            if(count4 <= 4)
                                                            {
                                                                listBox1.Items.Add("[" + (i / 3 + 1) + "," + (i % 3 + 1) + "] has 4->4 : " + digit1 + " " + digit2 + " " + digit3 + " " + digit4);
                                                                for (int j = 0; j < 9; j++)
                                                                {
                                                                    if (sudoku.block[i / 3, i % 3].cell[j / 3, j % 3].odds[digit1] || sudoku.block[i / 3, i % 3].cell[j / 3, j % 3].odds[digit2]
                                                                    || sudoku.block[i / 3, i % 3].cell[j / 3, j % 3].odds[digit3] || sudoku.block[i / 3, i % 3].cell[j / 3, j % 3].odds[digit4])
                                                                    {
                                                                        for (int k = 1; k < 10; k++)
                                                                        {
                                                                            if (k != digit1 && k != digit2 && k != digit3 && k != digit4)
                                                                            {
                                                                                sudoku.block[i / 3, i % 3].cell[j / 3, j % 3].odds[k] = false;
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void twoOddsTwoCellInRow(SuDoku sudoku)
        {
            for (int y = 0; y < 9; y++)
            {
                for (int digit1 = 1; digit1 < 9; digit1++)
                {
                    if (!sudoku.Y[y, digit1])
                    {
                        int count1 = 0;
                        for (int x = 0; x < 9; x++)
                        {
                            if (sudoku.block[y / 3, x / 3].cell[y % 3, x % 3].odds[digit1])
                            {
                                count1++;
                            }
                        }
                        if (count1 == 2)
                        {
                            for(int digit2 = digit1 + 1; digit2 < 10; digit2++)
                            {
                                if (!sudoku.Y[y, digit2])
                                {
                                    int count2 = 0;
                                    for (int x = 0; x < 9; x++)
                                    {
                                        if (sudoku.block[y / 3, x / 3].cell[y % 3, x % 3].odds[digit1] || sudoku.block[y / 3, x / 3].cell[y % 3, x % 3].odds[digit2])
                                        {
                                            count2++;
                                        }
                                    }
                                    if (count2 == 2)
                                    {
                                        string str = "(" + (y + 1) + ".row) has 2->2 : " + digit1 + " " + digit2 + " columns:";
                                        string deleted = "|>Deleted:";
                                        for (int x = 0; x < 9; x++)
                                        {
                                            if (sudoku.block[y / 3, x / 3].cell[y % 3, x % 3].odds[digit1] || sudoku.block[y / 3, x / 3].cell[y % 3, x % 3].odds[digit2])
                                            {
                                                str = str + " " + (x + 1);
                                                for (int k = 1; k < 10; k++)
                                                {
                                                    if (k != digit1 && k != digit2)
                                                    {
                                                        sudoku.block[y / 3, x / 3].cell[y % 3, x % 3].odds[k] = false;
                                                        deleted = deleted + " " + k;
                                                    }
                                                }
                                            }
                                        }
                                        listBox1.Items.Add(str + deleted);
                                        digit1 = digit2;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void treeOddsTreeCellInRow(SuDoku sudoku)
        {
            for (int y = 0; y < 9; y++)
            {
                for (int digit1 = 1; digit1 < 8; digit1++)
                {
                    if (!sudoku.Y[y, digit1])
                    {
                        int count1 = 0;
                        for (int x = 0; x < 9; x++)
                        {
                            if (sudoku.block[y / 3, x / 3].cell[y % 3, x % 3].odds[digit1])
                            {
                                count1++;
                            }
                        }
                        if (count1 <= 3)
                        {
                            for (int digit2 = digit1 + 1; digit2 < 9; digit2++)
                            {
                                if (!sudoku.Y[y, digit2])
                                {
                                    int count2 = 0;
                                    for (int x = 0; x < 9; x++)
                                    {
                                        if (sudoku.block[y / 3, x / 3].cell[y % 3, x % 3].odds[digit1] || sudoku.block[y / 3, x / 3].cell[y % 3, x % 3].odds[digit2])
                                        {
                                            count2++;
                                        }
                                    }
                                    if (count2 <= 3)
                                    {
                                        for (int digit3 = digit2 + 1; digit3 < 10; digit3++)
                                        {
                                            if (!sudoku.Y[y, digit3])
                                            {
                                                int count3 = 0;
                                                for (int x = 0; x < 9; x++)
                                                {
                                                    if (sudoku.block[y / 3, x / 3].cell[y % 3, x % 3].odds[digit1] || sudoku.block[y / 3, x / 3].cell[y % 3, x % 3].odds[digit2]
                                                        || sudoku.block[y / 3, x / 3].cell[y % 3, x % 3].odds[digit3])
                                                    {
                                                        count3++;
                                                    }
                                                }
                                                if (count3 <= 3)
                                                {
                                                    string str= "(" + (y + 1) + ".row) deleted odds:" + digit1 + " " + digit2 + " " + digit3+" columns:";
                                                    string deleted = "|>Deleted:";
                                                    for (int x = 0; x < 9; x++)
                                                    {
                                                        if (sudoku.block[y / 3, x / 3].cell[y % 3, x % 3].odds[digit1] || sudoku.block[y / 3, x / 3].cell[y % 3, x % 3].odds[digit2]
                                                            || sudoku.block[y / 3, x / 3].cell[y % 3, x % 3].odds[digit3])
                                                        {
                                                            str = str + " " + (x + 1);
                                                            for (int k = 1; k < 10; k++)
                                                            {
                                                                if (k != digit1 && k != digit2 && k != digit3)
                                                                {
                                                                    sudoku.block[y / 3, x / 3].cell[y % 3, x % 3].odds[k] = false;
                                                                    deleted = deleted + " " + k;
                                                                }
                                                            }
                                                        }
                                                    }
                                                    listBox1.Items.Add(str+deleted);
                                                    digit1 = digit3;
                                                    digit2 = 10;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void fourOddsFourCellInRow(SuDoku sudoku)
        {
            for (int y = 0; y < 9; y++)
            {
                for (int digit1 = 1; digit1 < 7; digit1++)
                {
                    if (!sudoku.Y[y, digit1])
                    {
                        int count1 = 0;
                        for (int x = 0; x < 9; x++)
                        {
                            if (sudoku.block[y / 3, x / 3].cell[y % 3, x % 3].odds[digit1])
                            {
                                count1++;
                            }
                        }
                        if (count1 <= 4)
                        {
                            for (int digit2 = digit1 + 1; digit2 < 8; digit2++)
                            {
                                if (!sudoku.Y[y, digit2])
                                {
                                    int count2 = 0;
                                    for (int x = 0; x < 9; x++)
                                    {
                                        if (sudoku.block[y / 3, x / 3].cell[y % 3, x % 3].odds[digit1] || sudoku.block[y / 3, x / 3].cell[y % 3, x % 3].odds[digit2])
                                        {
                                            count2++;
                                        }
                                    }
                                    if (count2 <= 4)
                                    {
                                        for (int digit3 = digit2 + 1; digit3 < 9; digit3++)
                                        {
                                            if (!sudoku.Y[y, digit3])
                                            {
                                                int count3 = 0;
                                                for (int x = 0; x < 9; x++)
                                                {
                                                    if (sudoku.block[y / 3, x / 3].cell[y % 3, x % 3].odds[digit1] || sudoku.block[y / 3, x / 3].cell[y % 3, x % 3].odds[digit2]
                                                        || sudoku.block[y / 3, x / 3].cell[y % 3, x % 3].odds[digit3])
                                                    {
                                                        count3++;
                                                    }
                                                }
                                                if (count3 <= 4)
                                                {
                                                    for (int digit4 = digit3 + 1; digit4 < 10; digit4++)
                                                    {
                                                        if (!sudoku.Y[y, digit4])
                                                        {
                                                            int count4 = 0;
                                                            for (int x = 0; x < 9; x++)
                                                            {
                                                                if (sudoku.block[y / 3, x / 3].cell[y % 3, x % 3].odds[digit1] || sudoku.block[y / 3, x / 3].cell[y % 3, x % 3].odds[digit2]
                                                                    || sudoku.block[y / 3, x / 3].cell[y % 3, x % 3].odds[digit3] || sudoku.block[y / 3, x / 3].cell[y % 3, x % 3].odds[digit4])
                                                                {
                                                                    count4++;
                                                                }
                                                            }
                                                            if (count4 <= 4)
                                                            {
                                                                string str = "(" + (y+1) + ".row) has 3->3 : " + digit1 + " " + digit2 + " " + digit3+ " " + digit4 + " columns:";
                                                                string deleted = "|>Deleted:";
                                                                for (int x = 0; x < 9; x++)
                                                                {
                                                                    if (sudoku.block[y / 3, x / 3].cell[y % 3, x % 3].odds[digit1] || sudoku.block[y / 3, x / 3].cell[y % 3, x % 3].odds[digit2]
                                                                        || sudoku.block[y / 3, x / 3].cell[y % 3, x % 3].odds[digit3] || sudoku.block[y / 3, x / 3].cell[y % 3, x % 3].odds[digit4])
                                                                    {
                                                                        str = str + " " + (x + 1);
                                                                        for (int k = 1; k < 10; k++)
                                                                        {
                                                                            if (k != digit1 && k != digit2 && k != digit3 && k != digit4)
                                                                            {
                                                                                sudoku.block[y / 3, x / 3].cell[y % 3, x % 3].odds[k] = false;
                                                                                deleted = deleted + " " + k;
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                                listBox1.Items.Add(str+deleted);
                                                                digit1 = digit4;
                                                                digit2 = digit3= 10;
                                                                break;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void twoOddsTwoCellInColumn(SuDoku sudoku)
        {
            for (int y = 0; y < 9; y++)
            {
                for (int digit1 = 1; digit1 < 9; digit1++)
                {
                    if (!sudoku.X[y, digit1])
                    {
                        int count1 = 0;
                        for (int x = 0; x < 9; x++)
                        {
                            if (sudoku.block[x / 3, y / 3].cell[x % 3, y % 3].odds[digit1])
                            {
                                count1++;
                            }
                        }
                        if (count1 == 2)
                        {
                            for (int digit2 = digit1 + 1; digit2 < 10; digit2++)
                            {
                                if (!sudoku.X[y, digit2])
                                {
                                    int count2 = 0;
                                    for (int x = 0; x < 9; x++)
                                    {
                                        if (sudoku.block[x / 3, y / 3].cell[x % 3, y % 3].odds[digit1] || sudoku.block[x / 3, y / 3].cell[x % 3, y % 3].odds[digit2])
                                        {
                                            count2++;
                                        }
                                    }
                                    if (count2 == 2)
                                    {
                                        string str = "(" + (y + 1) + ".row) has 2->2 : " + digit1 + " " + digit2 + " columns:";
                                        string deleted = "|>Deleted:";
                                        for (int x = 0; x < 9; x++)
                                        {
                                            if (sudoku.block[x / 3, y / 3].cell[x % 3, y % 3].odds[digit1] || sudoku.block[x / 3, y / 3].cell[x % 3, y % 3].odds[digit2])
                                            {
                                                str = str + " " + (x + 1);
                                                for (int k = 1; k < 10; k++)
                                                {
                                                    if (k != digit1 && k != digit2)
                                                    {
                                                        sudoku.block[x / 3, y / 3].cell[x % 3, y % 3].odds[k] = false;
                                                        deleted = deleted +" "+ k;
                                                    }
                                                }
                                            }
                                        }
                                        listBox1.Items.Add(str+deleted);
                                        digit1 = digit2;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void treeOddsTreeCellInColumn(SuDoku sudoku)
        {
            for (int y = 0; y < 9; y++)
            {
                for (int digit1 = 1; digit1 < 8; digit1++)
                {
                    if (!sudoku.X[y, digit1])
                    {
                        int count1 = 0;
                        for (int x = 0; x < 9; x++)
                        {
                            if (sudoku.block[x / 3, y / 3].cell[x % 3, y % 3].odds[digit1])
                            {
                                count1++;
                            }
                        }
                        if (count1 <= 3)
                        {
                            for (int digit2 = digit1 + 1; digit2 < 9; digit2++)
                            {
                                if (!sudoku.X[y, digit2])
                                {
                                    int count2 = 0;
                                    for (int x = 0; x < 9; x++)
                                    {
                                        if (sudoku.block[x / 3, y / 3].cell[x % 3, y % 3].odds[digit1] || sudoku.block[x / 3, y / 3].cell[x % 3, y % 3].odds[digit2])
                                        {
                                            count2++;
                                        }
                                    }
                                    if (count2 <= 3)
                                    {
                                        for (int digit3 = digit2 + 1; digit3 < 10; digit3++)
                                        {
                                            if (!sudoku.X[y, digit3])
                                            {
                                                int count3 = 0;
                                                for (int x = 0; x < 9; x++)
                                                {
                                                    if (sudoku.block[x / 3, y / 3].cell[x % 3, y % 3].odds[digit1] || sudoku.block[x / 3, y / 3].cell[x % 3, y % 3].odds[digit2]
                                                        || sudoku.block[x / 3, y / 3].cell[x % 3, y % 3].odds[digit3])
                                                    {
                                                        count3++;
                                                    }
                                                }
                                                if (count3 <= 3)
                                                {
                                                    string str = "(" + (y + 1) + ".row) deleted odds:" + digit1 + " " + digit2 + " " + digit3 + " columns:";
                                                    string deleted = "|>Deleted:";
                                                    for (int x = 0; x < 9; x++)
                                                    {
                                                        if (sudoku.block[x / 3, y / 3].cell[x % 3, y % 3].odds[digit1] || sudoku.block[x / 3, y / 3].cell[x % 3, y % 3].odds[digit2]
                                                            || sudoku.block[x / 3, y / 3].cell[x % 3, y % 3].odds[digit3])
                                                        {
                                                            str = str + " " + (x + 1);
                                                            for (int k = 1; k < 10; k++)
                                                            {
                                                                if (k != digit1 && k != digit2 && k != digit3)
                                                                {
                                                                    sudoku.block[x / 3, y / 3].cell[x % 3, y % 3].odds[k] = false;
                                                                    deleted = deleted + " " + k;
                                                                }
                                                            }
                                                        }
                                                    }
                                                    listBox1.Items.Add(str + deleted);
                                                    digit1 = digit3;
                                                    digit2 = 10;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void fourOddsFourCellInColumn(SuDoku sudoku)
        {
            for (int y = 0; y < 9; y++)
            {
                for (int digit1 = 1; digit1 < 7; digit1++)
                {
                    if (!sudoku.X[y, digit1])
                    {
                        int count1 = 0;
                        for (int x = 0; x < 9; x++)
                        {
                            if (sudoku.block[x / 3, y / 3].cell[x % 3, y % 3].odds[digit1])
                            {
                                count1++;
                            }
                        }
                        if (count1 <= 4)
                        {
                            for (int digit2 = digit1 + 1; digit2 < 8; digit2++)
                            {
                                if (!sudoku.X[y, digit2])
                                {
                                    int count2 = 0;
                                    for (int x = 0; x < 9; x++)
                                    {
                                        if (sudoku.block[x / 3, y / 3].cell[x % 3, y % 3].odds[digit1] || sudoku.block[x / 3, y / 3].cell[x % 3, y % 3].odds[digit2])
                                        {
                                            count2++;
                                        }
                                    }
                                    if (count2 <= 4)
                                    {
                                        for (int digit3 = digit2 + 1; digit3 < 9; digit3++)
                                        {
                                            if (!sudoku.X[y, digit3])
                                            {
                                                int count3 = 0;
                                                for (int x = 0; x < 9; x++)
                                                {
                                                    if (sudoku.block[x / 3, y / 3].cell[x % 3, y % 3].odds[digit1] || sudoku.block[x / 3, y / 3].cell[x % 3, y % 3].odds[digit2]
                                                        || sudoku.block[x / 3, y / 3].cell[x % 3, y % 3].odds[digit3])
                                                    {
                                                        count3++;
                                                    }
                                                }
                                                if (count3 <= 4)
                                                {
                                                    for (int digit4 = digit3 + 1; digit4 < 10; digit4++)
                                                    {
                                                        if (!sudoku.X[y, digit4])
                                                        {
                                                            int count4 = 0;
                                                            for (int x = 0; x < 9; x++)
                                                            {
                                                                if (sudoku.block[x / 3, y / 3].cell[x % 3, y % 3].odds[digit1] || sudoku.block[x / 3, y / 3].cell[x % 3, y % 3].odds[digit2]
                                                                    || sudoku.block[x / 3, y / 3].cell[x % 3, y % 3].odds[digit3] || sudoku.block[x / 3, y / 3].cell[x % 3, y % 3].odds[digit4])
                                                                {
                                                                    count4++;
                                                                }
                                                            }
                                                            if (count4 <= 4)
                                                            {
                                                                string str = "(" + (y + 1) + ".row) has 3->3 : " + digit1 + " " + digit2 + " " + digit3 + " " + digit4 + " columns:";
                                                                string deleted = "|>Deleted:";
                                                                for (int x = 0; x < 9; x++)
                                                                {
                                                                    if (sudoku.block[x / 3, y / 3].cell[x % 3, y % 3].odds[digit1] || sudoku.block[x / 3, y / 3].cell[x % 3, y % 3].odds[digit2]
                                                                        || sudoku.block[x / 3, y / 3].cell[x % 3, y % 3].odds[digit3] || sudoku.block[x / 3, y / 3].cell[x % 3, y % 3].odds[digit4])
                                                                    {
                                                                        str = str + " " + (x + 1);
                                                                        for (int k = 1; k < 10; k++)
                                                                        {
                                                                            if (k != digit1 && k != digit2 && k != digit3 && k != digit4)
                                                                            {
                                                                                sudoku.block[x / 3, y / 3].cell[x % 3, y % 3].odds[k] = false;
                                                                                deleted = deleted + " " + k;
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                                listBox1.Items.Add(str+deleted);
                                                                digit1 = digit4;
                                                                digit2 = digit3 = 10;
                                                                break;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void updateOdds(SuDoku sudoku)
        {
            for(int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    for(int odds = 0; odds < 10; odds++)
                    {
                        sudoku.block[i / 3, i % 3].cell[j / 3, j % 3].odds[odds] = false;
                    }
                }
            }
            loadOdds();
        }

        private bool isThisDone(SuDoku sudok)
        {
            for(int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if (sudok.block[y / 3, y % 3].cell[x / 3, x % 3].value == 0)
                        return false;
                }
            }
            return true;
        }

        private bool start(SuDoku sudoku)
        {
            while (true)
            {
                do
                {
                    a = 0;
                    cellHasJustOneOdds(sudoku);
                    oddsOnJustOneCellInBlock(sudoku);
                    oddsOnJustOneCellInRow(sudoku);
                    oddsOnJustOneCellInColumn(sudoku);
                    twoOddsTwoCellInBlock(sudoku);
                    treeOddsTreeCellInBlock(sudoku);
                    fourOddsFourCellInBlock(sudoku);
                    twoOddsTwoCellInRow(sudoku);
                    treeOddsTreeCellInRow(sudoku);
                    fourOddsFourCellInRow(sudoku);
                    twoOddsTwoCellInColumn(sudoku);
                    treeOddsTreeCellInColumn(sudoku);
                    fourOddsFourCellInColumn(sudoku);
                    cellHasJustOneOdds(sudoku);
                    oddsOnJustOneCellInBlock(sudoku);
                    oddsOnJustOneCellInRow(sudoku);
                    oddsOnJustOneCellInColumn(sudoku);
                } while (a != 0 && cellHasJustOneOdds(sudoku));
                if (isThisDone(sudoku))                 //success
                {
                    showCells(sudoku);
                    //MessageBox.Show("done :)");
                    return true;
                }
                else if (!cellHasJustOneOdds(sudoku))   //fail
                {
                    //MessageBox.Show("fail :(");
                    return false;
                }
                else                                    //didnot finish
                {
                    //MessageBox.Show("going to space");
                    sudoku =kill(sudoku);
                }   
            }
        }

        private void saveSuDoku(SuDoku originalSuDoku, SuDoku copySuDoku)
        {
            for(int y = 0; y < 9; y++)
            {
                for(int x = 0; x < 9; x++)
                {
                    for(int odds = 0; odds < 10; odds++)
                    {
                        copySuDoku.block[y / 3, y % 3].cell[x / 3, x % 3].odds[odds] = originalSuDoku.block[y / 3, y % 3].cell[x / 3, x % 3].odds[odds];
                    }
                    copySuDoku.block[y / 3, y % 3].cell[x / 3, x % 3].value = originalSuDoku.block[y / 3, y % 3].cell[x / 3, x % 3].value;
                }
                for(int digit = 0; digit < 10; digit++)
                {
                    copySuDoku.block[y / 3, y % 3].hasDigit[digit] = originalSuDoku.block[y / 3, y % 3].hasDigit[digit];
                    copySuDoku.Y[y, digit] = originalSuDoku.Y[y, digit];
                    copySuDoku.X[y, digit] = originalSuDoku.X[y, digit];
                }
            }
        }

        private SuDoku kill(SuDoku sudoku)
        {
            SuDoku temp = new SuDoku();
            getAdressToSuDoku(temp);
            saveSuDoku(sudoku, temp);

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (temp.block[i / 3, i % 3].cell[j / 3, j % 3].value == 0)
                    {
                        for (int digit = 1; digit < 10; digit++)
                        {
                            if (temp.block[i / 3, i % 3].cell[j / 3, j % 3].odds[digit])
                            {
                                coverCell(temp, i / 3, i % 3, j / 3, j % 3, digit);
                                if (start(temp))
                                {
                                    return temp;
                                }
                                else
                                {
                                    sudoku.block[i / 3, i % 3].cell[j / 3, j % 3].odds[digit] = false;
                                    return sudoku;
                                }
                            }
                        }
                    }
                }
            }
            return sudoku;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetInputs();
            loadOdds();
            start(sudoku);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            showOdds(sudoku);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            showDigits(sudoku);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            showXY(sudoku);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            loadOdds();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            oddsOnJustOneCellInBlock(sudoku);
        }

        private void button7_Click(object sender, EventArgs e) 
        {
            cellHasJustOneOdds(sudoku);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            updateOdds(sudoku);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            oddsOnJustOneCellInColumn(sudoku);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            oddsOnJustOneCellInRow(sudoku);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            twoOddsTwoCellInBlock(sudoku);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            treeOddsTreeCellInBlock(sudoku);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            fourOddsFourCellInBlock(sudoku);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            fourOddsFourCellInRow(sudoku);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            treeOddsTreeCellInRow(sudoku);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            twoOddsTwoCellInRow(sudoku);
        }
        private void button17_Click(object sender, EventArgs e)
        {
            fourOddsFourCellInColumn(sudoku);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            treeOddsTreeCellInColumn(sudoku);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            twoOddsTwoCellInColumn(sudoku);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
