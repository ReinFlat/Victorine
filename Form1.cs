using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Victorine
{
    public partial class Form1 : Form
    {
        int teams_number;
        int team_now;
        string[] teamNames=new string[4];
        Control right200, right400;
        int[] mark = new int[4];
        PictureBox[] sizePic = new PictureBox[6];
        bool status = false;
        int answer300;
        Control who;
        Control chance;
        string wich;
        Point where;
        int height;
        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Enter)
            {
                if (tabControl1.SelectedIndex == 7)
                {
                    if ((textBox_write500.Visible)&&(textBox_write500.Text != "") && (textBox_write500.Text != "ВПИСАТЬ ОТВЕТ..."))
                        button_answer500_Click(button_answer500, e);
                }
                if (tabControl1.SelectedIndex == 8)
                {
                    if ((textBox_write600.Visible) && (textBox_write600.Text != "") && (textBox_write600.Text != "ВПИСАТЬ ОТВЕТ..."))
                        button_answer600_Click(button_answer600, e);
                }
                if (tabControl1.SelectedIndex == 1)
                {
                    button_next_Click(button_next, e);
                }
                e.Handled = true;
            }
        }//переключение по нажатию на энтер (для 500,600, и вкладки выбора команд)

        public void Get_points(int points)
        {
            mark[team_now] += points;
            if ((tabControl1.SelectedIndex != 5)||(answer300==3))
            {
                if (team_now == teams_number)
                    team_now = 0;
                else
                    team_now++;
                label_yourchoose.Text = ("КОМАНДА " + teamNames[team_now] + ", ВЫБЕРИТЕ ТЕМУ И НОМИНАЦИЮ");
            }
        }//метод прибавления баллов командам

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e) //Метод-обработчик отпускания мыши
        {
            status = false; //Выключаем "перемещение"
            if ((sender as PictureBox).Location.X < pictureBox_back1.Location.X + 128)
            {
                who = tabPage4.GetChildAtPoint(new Point(pictureBox_back1.Location.X + 5, pictureBox_back1.Location.Y + 250));
                if((who!=pictureBox_back1)&&(who!=pictureBox_back2) && (who != pictureBox_back3) && (who != pictureBox_back4))
                    who.Location = where;
                (sender as PictureBox).Location = new Point(pictureBox_up1.Location.X, pictureBox_up1.Location.Y + pictureBox_up1.Height + height);
                who = null;
            }
            if ((sender as PictureBox).Location.X > pictureBox_back1.Location.X + 128 && ((sender as PictureBox).Location.X < pictureBox_back2.Location.X + 128))
            {
                who = tabPage4.GetChildAtPoint(new Point(pictureBox_back2.Location.X + 5, pictureBox_back2.Location.Y + 250));
                if ((who != pictureBox_back1) && (who != pictureBox_back2) && (who != pictureBox_back3) && (who != pictureBox_back4))
                    who.Location = where;
                (sender as PictureBox).Location = new Point(pictureBox_up2.Location.X, pictureBox_up2.Location.Y + pictureBox_up2.Height + height);
                who = null;
            }
            if ((sender as PictureBox).Location.X > pictureBox_back2.Location.X + 128 && ((sender as PictureBox).Location.X < pictureBox_back3.Location.X + 128))
            {
                who = tabPage4.GetChildAtPoint(new Point(pictureBox_back3.Location.X + 5, pictureBox_back3.Location.Y + 250));
                if ((who != pictureBox_back1) && (who != pictureBox_back2) && (who != pictureBox_back3) && (who != pictureBox_back4))
                    who.Location = where;
                (sender as PictureBox).Location = new Point(pictureBox_up3.Location.X, pictureBox_up3.Location.Y + pictureBox_up3.Height + height);
                who = null;
            }
            if ((sender as PictureBox).Location.X > pictureBox_back3.Location.X + 128)
            {
                who = tabPage4.GetChildAtPoint(new Point(pictureBox_back4.Location.X + 5, pictureBox_back4.Location.Y + 250));
                if ((who != pictureBox_back1) && (who != pictureBox_back2) && (who != pictureBox_back3) && (who != pictureBox_back4))
                    who.Location = where;
                (sender as PictureBox).Location = new Point(pictureBox_up4.Location.X, pictureBox_up4.Location.Y + pictureBox_up4.Height + height);
                who = null;
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e) //Метод-обработчик задержки мыши
        {
            status = true; //Включаем "перемещение"
            where = (sender as PictureBox).Location;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e) //Метод-обработчик перемещения мыши
        {
            if (status == true) //Если "перемещение включено"
            {
                (sender as PictureBox).BringToFront();
                (sender as PictureBox).Location = new Point((Cursor.Position.X - this.Location.X - 10), (Cursor.Position.Y - this.Location.Y - 50)); 
            }
        }
        private void button1_MouseHover(object sender, EventArgs e)
        {
            if ((sender as Button).ForeColor == Color.DarkOrange)
                (sender as Button).ForeColor = Color.White;
        } // при наведении на кнопку на главной делать текст белым

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            if((sender as Button).ForeColor==Color.White)
                (sender as Button).ForeColor = Color.DarkOrange;
        }// при утере фокуса с кнопки на главной делать текст обратно ораньжевым

        private void button5_Click(object sender, EventArgs e)
        {
            sizePic[0] = pictureBox_back100; sizePic[1] = pictureBox_back200; sizePic[2] = pictureBox_back300;
            sizePic[3] = pictureBox_back400; sizePic[4] = pictureBox_back500; sizePic[5] = pictureBox_back600;
            if (this.WindowState == System.Windows.Forms.FormWindowState.Normal)
            {
                this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                for (int i = 0; i != 5; i++)
                {
                    if (sizePic[i].AccessibleName != "QUIZ__back_")
                    {
                        sizePic[i].Visible = true;
                    }
                }
            }
            else
            {
                this.WindowState = System.Windows.Forms.FormWindowState.Normal;
                for (int i = 0; i != 5; i++)
                {
                    if (sizePic[i].AccessibleName != "QUIZ__back_")
                    {
                        sizePic[i].Visible = false;
                    }
                }
            }
            button_YES300.BringToFront();
            button_NO300.BringToFront();
            button1_Round200.BringToFront(); button2_Round200.BringToFront(); button3_Round200.BringToFront(); button4_Round200.BringToFront(); 
            button1_400.BringToFront(); button2_400.BringToFront(); button3_400.BringToFront(); button4_400.BringToFront();
        }//Изменение маштаба окна

        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }//Кнопка закрытия приложения

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (teams_number==0)
                textBox_Points.Text = (teamNames[0] + " - " + mark[0]);
            if (teams_number == 1)
                textBox_Points.Text = (teamNames[0] + " - " + mark[0]+", "+ teamNames[1] + " - " + mark[1]);
            if (teams_number == 2)
                textBox_Points.Text = (teamNames[0] + " - " + mark[0] + ", " + teamNames[1] + " - " + mark[1] + ", " + teamNames[2] + " - " + mark[2]);
            if (teams_number == 3)
                textBox_Points.Text = (teamNames[0] + " - " + mark[0] + ", " + teamNames[1] + " - " + mark[1] + ", " + teamNames[2] + " - " + mark[2] + ", " + teamNames[3] + " - " + mark[3]);
        }//Вывести баллы команд при возврате на главную страницу

        private void Form1_Load(object sender, EventArgs e)
        {
            height = pictureBox_down1.Location.Y - (pictureBox_up1.Location.Y+ pictureBox_up1.Height);
            team_now = 0;
            button_Start.FlatAppearance.BorderColor = Color.FromArgb(238, 203, 171);
            richTextBox_300.SelectionAlignment = HorizontalAlignment.Center;
             textBox_write500.TextAlign = HorizontalAlignment.Center; richTextBox_right500.SelectionAlignment = HorizontalAlignment.Center;
            label_task500.TextAlign = HorizontalAlignment.Center;
            textBox_write600.TextAlign = HorizontalAlignment.Center;
            label1_200.TextAlign = HorizontalAlignment.Center; label2_200.TextAlign = HorizontalAlignment.Center;
            label3_200.TextAlign = HorizontalAlignment.Center; label4_200.TextAlign = HorizontalAlignment.Center;
            richTextBox_600.SelectionAlignment = HorizontalAlignment.Center;
            richTextBox_400.SelectionAlignment = HorizontalAlignment.Center;
            textBox_write500.Text = "ВПИСАТЬ ОТВЕТ..."; textBox_write600.Text = "ВПИСАТЬ ОТВЕТ...";//подсказка
        }//Что сделать при запуске приложения

        private void button_1team_Click(object sender, EventArgs e)
        {
            button_1team.Visible = false;
            richTextBox_1name.BringToFront();
            label_1name.BringToFront();
        }//если выбрали что команда одна

        private void button_2team_Click(object sender, EventArgs e)
        {
            button_1team.Visible = false;
            richTextBox_1name.BringToFront();
            label_1name.BringToFront();
            button_2team.Visible = false;
            richTextBox_2name.BringToFront();
            label_2name.BringToFront();
            checkBox1.Visible = true;
        }//если выбрали что команды две

        private void button_3team_Click(object sender, EventArgs e)
        {
            button_1team.Visible = false;
            richTextBox_1name.BringToFront();
            label_1name.BringToFront();
            button_2team.Visible = false;
            richTextBox_2name.BringToFront();
            label_2name.BringToFront();
            button_3team.Visible = false;
            richTextBox_3name.BringToFront();
            label_3name.BringToFront();
            checkBox1.Visible = true;
            checkBox2.Visible = true;
        }//если выбрали что команды три

        private void button_4team_Click(object sender, EventArgs e)
        {
            button_1team.Visible = false;
            richTextBox_1name.BringToFront();
            label_1name.BringToFront();
            button_2team.Visible = false;
            richTextBox_2name.BringToFront();
            label_2name.BringToFront();
            button_3team.Visible = false;
            richTextBox_3name.BringToFront();
            label_3name.BringToFront();
            button_4team.Visible = false;
            richTextBox_4name.BringToFront();
            label_4name.BringToFront();
            checkBox1.Visible = true;
            checkBox2.Visible = true;
            checkBox3.Visible = true;
        }//если выбрали что команды четыре

        private void button_Start_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }//кнопка НАЧАТЬ при запуске программы

        private void button_next_Click(object sender, EventArgs e)
        {
            if ((button_1team.Visible == false) && (richTextBox_1name.Text != ""))
            {
                teams_number = 0;
                teamNames = new string[1];
                if ((button_2team.Visible == false) && (richTextBox_2name.Text != ""))
                {
                    teams_number = 1;
                    teamNames = new string[2];
                    if ((button_3team.Visible == false) && (richTextBox_3name.Text != ""))
                    {
                        teams_number = 2;
                        teamNames = new string[3];
                        if ((button_4team.Visible == false) && (richTextBox_4name.Text != ""))
                        {
                            teams_number = 3;
                            teamNames = new string[4];
                        }
                    }
                }
            }
            if (teams_number == 0)
                teamNames[0] = richTextBox_1name.Text;
            if (teams_number == 1)
            {
                teamNames[0] = richTextBox_1name.Text; teamNames[1] = richTextBox_2name.Text;
            }
            if (teams_number == 2)
            {
                teamNames[0] = richTextBox_1name.Text; teamNames[1] = richTextBox_2name.Text; teamNames[2] = richTextBox_3name.Text;
            }
            if (teams_number == 3)
            {
                teamNames[0] = richTextBox_1name.Text; teamNames[1] = richTextBox_2name.Text; teamNames[2] = richTextBox_3name.Text; teamNames[3] = richTextBox_4name.Text;
            }

            if ((button_1team.Visible == false) && (richTextBox_1name.Text != ""))
                if (((button_2team.Visible == false) && (richTextBox_2name.Text != "")) || (button_2team.Visible == true))
                    if (((button_3team.Visible == false) && (richTextBox_3name.Text != "")) || (button_3team.Visible == true))
                        if (((button_4team.Visible == false) && (richTextBox_4name.Text != "")) || (button_4team.Visible == true))
                        {
                            team_now = 0;
                            label_yourchoose.Text = ("КОМАНДА " + teamNames[0] + ", ВЫБЕРИТЕ ТЕМУ И НОМИНАЦИЮ");
                            mark = new int[teams_number + 1];
                            tabControl1.SelectedIndex = 2;
                        }


        }//Переход на главную после выбора количества команд и ввода названий

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Visible = false;
            checkBox2.Visible = false;
            checkBox3.Visible = false;
            checkBox1.Checked = true;
            button_2team.BringToFront();
            button_2team.Visible = true;
            button_3team.BringToFront();
            button_3team.Visible = true;
            button_4team.BringToFront();
            button_4team.Visible = true;
        }//если убрали галочку на этапе выбора количества команд (одна команда)

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Visible = false;
            checkBox3.Visible = false;
            checkBox2.Checked = true;
            button_3team.BringToFront();
            button_3team.Visible = true;
            button_4team.BringToFront();
            checkBox3.Checked = true;
            button_4team.Visible = true;
        }//если убрали галочку на этапе выбора количества команд (две команды)

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            checkBox3.Visible = false;
            button_4team.BringToFront();
            button_4team.Visible = true;
        }//если убрали галочку на этапе выбора количества команд (три команды)

        private void Back_to_mainPage(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
            chance.ForeColor = Color.DarkOrange;
            chance = null;
        }//кнопка возврата с любого вопроса обратно на главную страницу

        private void button_500tomain_Click(object sender, EventArgs e)
        {
            label_theme500.Visible = true;
            label1_question500.Visible = true; label2_question500.Visible = true;
            panel_line500.Visible = true;
            label_task500.Visible = true;
            textBox_write500.Visible = true;
            pictureBox1_500.Visible = true; pictureBox2_500.Visible = true;
            button4.Visible = true;
            pictureBox_back500.Image = Properties.Resources.ALL_BACK;
            pictureBox_back500.AccessibleName = "ALL_BACK";
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back500.Visible = true;
            else
                pictureBox_back500.Visible = false;
            label_ANSWER500.Visible = false;
            button_500tomain.Visible = false;
            richTextBox_answer500.Visible = false; richTextBox_right500.Visible = false;
            textBox_write500.Text = "ВПИСАТЬ ОТВЕТ..."; //подсказка
            tabControl1.SelectedIndex = 2;
        } //после ответа на главную 500

        private void button_art500_Click(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back500.Visible = true;
            else
                pictureBox_back500.Visible = false;
            (sender as Button).ForeColor = Color.NavajoWhite;
            chance = (sender as Button);
            wich = ("ЖИВОПИСЬ");
            label_theme500.Text = ("ЖИВОПИСЬ");
            label_task500.Text = ("Кто изображён на данной картине И.Н. Никитина?");
            pictureBox1_500.Image = Properties.Resources.pictureBox1_500_zhivo;
            pictureBox2_500.Image = Properties.Resources.pictureBox2_500_zhivo;
            richTextBox_right500.Text = ("КАНЦЛЕР ГОЛОВКИН");
            richTextBox_answer500.Text = ("Иван Никитич Никитин – Портрет государственного канцлера графа Гавриила Ивановича Головкина.");
            tabControl1.SelectedIndex = 7;
        }

        private void button_inter500_Click(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back500.Visible = true;
            else
                pictureBox_back500.Visible = false;
            (sender as Button).ForeColor = Color.NavajoWhite;
            chance = (sender as Button);
            wich = ("ИНТЕРЬЕР");
            label_theme500.Text = ("ИНТЕРЬЕР");
            label_task500.Text = ("Как называется помещение, представленное на слайде?");
            pictureBox1_500.Image = Properties.Resources.pictureBox1_500_int;
            pictureBox2_500.Image = Properties.Resources.pictureBox2_500_int;
            richTextBox_right500.Text = ("ЗЕРКАЛЬНАЯ ГАЛЕРЕЯ В ВЕРСАЛЬСКОМ ДВОРЦЕ");
            richTextBox_answer500.Text = ("В галерее создаётся эффект бесконечности пространства: сады и парки Версаля, видные из высоких окон, отражаются в зеркалах, благодаря чему масштабы дворцово-паркового ансамбля кажутся более грандиозными.");
            tabControl1.SelectedIndex = 7;
        }

        private void button_arhi500_Click(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back500.Visible = true;
            else
                pictureBox_back500.Visible = false;
            (sender as Button).ForeColor = Color.NavajoWhite;
            chance = (sender as Button);
            wich = ("АРХИТЕКТУРА");
            label_theme500.Text = ("АРХИТЕКТУРА");
            label_task500.Text = ("Напишите фамилию архитектора, чей автопортрет представлен на слайде.");
            pictureBox1_500.Image = Properties.Resources.pictureBox1_500_arhi;
            pictureBox2_500.Image = Properties.Resources.pictureBox2_500_arhi;
            richTextBox_right500.Text = ("ФРАНЧЕСКО РАСТРЕЛЛИ");
            richTextBox_answer500.Text = ("Архитектор, крупнейший представитель зодчества барокко в России. (1700-1771гг.)");
            tabControl1.SelectedIndex = 7;
        }

        private void button_land500_Click(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back500.Visible = true;
            else
                pictureBox_back500.Visible = false;
            (sender as Button).ForeColor = Color.NavajoWhite;
            chance = (sender as Button);
            wich = ("ЛАНДШАФТ");
            label_theme500.Text = ("ЛАНДШАФТ");
            label_task500.Text = ("На фотографиях представлен парк времени барокко . Укажите его название.");
            pictureBox1_500.Image = Properties.Resources.pictureBox1_500_land;
            pictureBox2_500.Image = Properties.Resources.pictureBox2_500_land;
            richTextBox_right500.Text = ("ПАРК ГЕРЦОГОВ МАЛЬБОРО");
            richTextBox_answer500.Text = ("Знаменитый замок Бленхейм, никогда не играл какого-то оборонительного значения. Бленхейм изначально строился как резиденция герцога Мальборо с использованием элементов барокко, великолепным парковым ансамблем. ");
            tabControl1.SelectedIndex = 7;
        }

        private void textBox_write500_Enter(object sender, EventArgs e)
        {
            (sender as TextBox).Text = null;
            if ((sender as TextBox).Name == textBox_write500.Name)
            {
                button_answer500.Enabled = true;
                button_answer500.Visible = true;
            }
            if ((sender as TextBox).Name == textBox_write600.Name)
            {
                button_answer600.Enabled = true;
                button_answer600.Visible = true;
            }
        }

        private void button_answer500_Click(object sender, EventArgs e)
        {
            label_theme500.Visible = false;
            label1_question500.Visible = false; label2_question500.Visible = false;
            panel_line500.Visible = false;
            label_task500.Visible = false;
            textBox_write500.Visible = false;
            pictureBox1_500.Visible = false; pictureBox2_500.Visible = false;
            button_answer500.Visible = false;
            button4.Visible = false;
            pictureBox_back500.Image = Properties.Resources.QUIZ__back_;
            pictureBox_back500.Visible = true;
            pictureBox_back500.AccessibleName = "QUIZ__back_";
            label_ANSWER500.Visible = true;
            button_500tomain.Visible = true;
            richTextBox_answer500.Visible = true; richTextBox_right500.Visible = true;
            String s = richTextBox_right500.Text;
            String pattern = @textBox_write500.Text;
            if (Regex.IsMatch(s, pattern, RegexOptions.IgnoreCase))
                Get_points(500);
            else
                Get_points(0);
        } //получить ответ в номинации 500

        private void button_autor500_Click(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back500.Visible = true;
            else
                pictureBox_back500.Visible = false;
            (sender as Button).ForeColor = Color.NavajoWhite;
            chance = (sender as Button);
            wich = ("АВТОРЫ");
            label_theme500.Text = ("АВТОРЫ");
            label_task500.Text = ("Напишите фамилию художника, чей автопортрет представлен на изображении");
            pictureBox1_500.Image = Properties.Resources.pictureBox1_500_autor;
            pictureBox2_500.Image = Properties.Resources.pictureBox2_500_autor;
            richTextBox_right500.Text = ("ВАН ДЕЙК");
            richTextBox_answer500.Text = ("Антонис ван Дейк (1599 г. —1641 г.) — выдающийся фламандский художник и гравёр семнадцатого века, мастер парадного придворного портрета и сюжетов на религиозную тему.");
            tabControl1.SelectedIndex = 7;
        }

        private void button_skulp500_Click(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back500.Visible = true;
            else
                pictureBox_back500.Visible = false;
            (sender as Button).ForeColor = Color.NavajoWhite;
            chance = (sender as Button);
            wich = ("СКУЛЬПТУРА");
            label_theme500.Text = ("СКУЛЬПТУРА");
            label_task500.Text = ("Напишите название данной скульптуры авторства Лоренцо Бернини.");
            pictureBox1_500.Image = Properties.Resources.pictureBox1_500_skulp;
            pictureBox2_500.Image = Properties.Resources.pictureBox2_500_skulp;
            richTextBox_right500.Text = ("АПОЛЛОН И ДАФНА");
            richTextBox_answer500.Text = ("Антонис ван Дейк (1599 г. —1641 г.) — выдающийся фламандский художник и гравёр семнадцатого века, мастер парадного придворного портрета и сюжетов на религиозную тему.");
            tabControl1.SelectedIndex = 7;
        }

        private void textBox_write500_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        } //Все вводимые буквы заглавными

        private void button_land600_Click(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back600.Visible = true;
            else
                pictureBox_back600.Visible = false;
            (sender as Button).ForeColor = Color.NavajoWhite;
            chance = (sender as Button);
            wich = ("ЛАНДШАФТ");
            label_theme600.Text = ("ЛАНДШАФТ");
            richTextBox_600.Text = ("\n\nДостойное место в проектировании и строительстве первых садов Петербурга и его окрестностей принадлежит _______________________(1679—1719 гг.), который за два с половиной года пребывания в России успел дать целый ряд ценных проектных предложений по планировке Летнего сада, садов в Екатериненгофе, Стрельне, Петергофе и др.");
            richTextBox_right600.Text = ("ЖАН БАТИСТ АЛЕКСАНДР");
            richTextBox_answer600.Text = ("Архитектурный гений Петербурга. Он родился во Франции в 1679 г. стал одним из самых известных специалистов в градостроительстве, архитектуре, садовопарковом хозяйстве.");
            tabControl1.SelectedIndex = 8;
        }

        private void button_art600_Click(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back600.Visible = true;
            else
                pictureBox_back600.Visible = false;
            (sender as Button).ForeColor = Color.NavajoWhite;
            chance = (sender as Button);
            wich = ("ЖИВОПИСЬ");
            label_theme600.Text = ("ЖИВОПИСЬ");
            richTextBox_600.Text = ("\n\nВ творчестве трех великих живописцев второй половины XVIII века нашли наиболее полное выражение идеалы эпохи, ее эстетические, философские и религиозные представления. В короткий срок они выдвинули русское искусство, и прежде всего, портретную живопись в один ряд с лучшими произведениями западноевропейских мастеров.Назовите этих великих мастеров через запятую.");
            richTextBox_right600.Text = ("ЛЕВИЦКИЙ, РОКОТОВ, БОРОВИЦКИЙ");
            richTextBox_answer600.Text = ("Три великих портретиста: Рокотов, Левицкий, Боровиковский. Лучшие российские живописцы своего времени.");
            tabControl1.SelectedIndex = 8;
        }

        private void button_inter600_Click(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back600.Visible = true;
            else
                pictureBox_back600.Visible = false;
            (sender as Button).ForeColor = Color.NavajoWhite;
            chance = (sender as Button);
            wich = ("ИНТЕРЬЕР");
            label_theme600.Text = ("ИНТЕРЬЕР");
            richTextBox_600.Text = ("Общие характеристики стиля барокко определяются тем, что это грандиозный стиль, который подчеркивает большой масштаб и движение от прямоугольных форм к более овальным конструкциям. Стиль барокко предполагает динамику. Для придания объёма используются также текстильные обои, гипс, окрашенные деревянные панели. Такое обилие декора не было случайным. Век барокко – век мирового театра, а архитектура и интерьеры, в частности, были________");
            richTextBox_right600.Text = ("ПРОДУМАННОЙ ДЕКОРАЦИЕЙ");
            richTextBox_answer600.Text = ("Как и в любом стиле, скульптура Барокко имеет характерные черты. Архитектурные работы этой эпохи характеризуются: \nКриволинейными формами, изломами и изгибами;\nВизуальной динамичностью и пластичностью форм;\nПространственной масштабностью строений;\nКонтрастными параметрами декоративных элементов;\nОбилием декоративной отделки в экстерьере и интерьере.");
            tabControl1.SelectedIndex = 8;
        }

        private void button_arhi600_Click(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back600.Visible = true;
            else
                pictureBox_back600.Visible = false;
            (sender as Button).ForeColor = Color.NavajoWhite;
            chance = (sender as Button);
            wich = ("АРХИТЕКТУРА");
            label_theme600.Text = ("АРХИТЕКТУРА");
            richTextBox_600.Text = ("\n\n\n\n\nНапишите архитектора ансамбля площади святого Петра.");
            richTextBox_right600.Text = ("ДЖОВАННИ ЛОРЕНЦО БЕРНИНИ");
            richTextBox_answer600.Text = ("Итальянский архитектор и скульптор. Являлся видным архитектором и ведущим скульптором своего времени, считается создателем стиля барокко в скульптуре. ");
            tabControl1.SelectedIndex = 8;
        }

        private void button_answer600_Click(object sender, EventArgs e)
        {
            label_theme600.Visible = false;
            label1_question600.Visible = false; label2_question600.Visible = false;
            panel_line600.Visible = false;
            label_taskbrown600.Visible = false;
            textBox_write600.Visible = false;
            richTextBox_600.Visible = false;
            button_answer600.Visible = false;
            button7.Visible = false;
            pictureBox_back600.Image = Properties.Resources.QUIZ__back_;
            pictureBox_back600.Visible = true;
            pictureBox_back600.AccessibleName = "QUIZ__back_";
            label_ANSWER600.Visible = true;
            button_600tomain.Visible = true;
            richTextBox_answer600.Visible = true; richTextBox_right600.Visible = true;
            String s = richTextBox_right600.Text;
            String pattern = @textBox_write600.Text;
            if (Regex.IsMatch(s, pattern, RegexOptions.IgnoreCase))
                Get_points(600);
            else
                Get_points(0);
        }//получить ответ в номинации 600

        private void button_autor600_Click(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back600.Visible = true;
            else
                pictureBox_back600.Visible = false;
            (sender as Button).ForeColor = Color.NavajoWhite;
            chance = (sender as Button);
            wich = ("АВТОРЫ");
            label_theme600.Text = ("АВТОРЫ");
            richTextBox_600.Text = ("\n\n\nНазовите фамилию архитектора, который является наиболее ярким представителем Елизаветинского барокко. Именно по его проектам были созданы Зимний и Большой Петергофский дворцы в Санкт-Петербурге.");
            richTextBox_right600.Text = ("РАСТРЕЛЛИ");
            richTextBox_answer600.Text = ("Бартоломео Франческо Растрелли — один из знаменитейших русских архитекторов, которому блестяще удалось совместить барокко и классицизм с традициями древнерусского зодчества.");
            tabControl1.SelectedIndex = 8;
        }

        private void button_skulp600_Click(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back600.Visible = true;
            else
                pictureBox_back600.Visible = false;
            (sender as Button).ForeColor = Color.NavajoWhite;
            chance = (sender as Button);
            wich = ("СКУЛЬПТУРА");
            label_theme600.Text = ("СКУЛЬПТУРА");
            richTextBox_600.Text = ("\n\nХудожественный стиль одного из самых ярких периодов в истории Франции «золотого века», в котором соединились элементы Классицизма и Барокко, связан с годами правления этого короля. Основная часть французской барочной скульптуры была призвана прославить не Церковь, а французского монарха. Назовите его имя.");
            richTextBox_right600.Text = ("ЛЮДОВИК");
            richTextBox_answer600.Text = ("Этим королём был Людовик XIV. Лучшие французские скульпторы были заняты изготовлением статуй для фонтанов садов Версальского дворца и других королевских резиденций.");
            tabControl1.SelectedIndex = 8;
        }

        private void button_600tomain_Click(object sender, EventArgs e)
        {
            label_theme600.Visible = true;
            label1_question600.Visible = true; label2_question600.Visible = true;
            panel_line600.Visible = true;
            label_taskbrown600.Visible = true;
            textBox_write600.Visible = true;
            richTextBox_600.Visible = true;
            button7.Visible = true;
            pictureBox_back600.Image = Properties.Resources.ALL_BACK;
            pictureBox_back600.AccessibleName = "ALL_BACK";
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back600.Visible = true;
            else
                pictureBox_back600.Visible = false;
            label_ANSWER600.Visible = false;
            button_600tomain.Visible = false;
            richTextBox_answer600.Visible = false; richTextBox_right600.Visible = false;
            textBox_write600.Text = "ВПИСАТЬ ОТВЕТ...";
            tabControl1.SelectedIndex = 2;
        }//после ответа на главную 600

        private void button_art400_Click(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back400.Visible = true;
            else
                pictureBox_back400.Visible = false;
            (sender as Button).ForeColor = Color.NavajoWhite;
            chance = (sender as Button);
            tabControl1.SelectedIndex = 6;
            wich = ("ЖИВОПИСЬ");
            label_theme400.Text = ("ЖИВОПИСЬ");
            richTextBox_400.Text = ("В живописи голландского барокко можно выделить несколько жанров. Укажите тот, к которому относится ванитас.");
            button1_400.Text = "а) Жанр портрета. ";
            button2_400.Text = "б) Жанр пейзажа.";
            button3_400.Text = "в) Жанр натюрморта";
            button4_400.Text = "г) Бытовой жанр.";
            right400 = button3_400;
        }

        private void button_inter400_Click(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back400.Visible = true;
            else
                pictureBox_back400.Visible = false;
            (sender as Button).ForeColor = Color.NavajoWhite;
            chance = (sender as Button);
            tabControl1.SelectedIndex = 6;
            wich = ("ИНТЕРЬЕР");
            label_theme400.Text = ("ИНТЕРЬЕР");
            richTextBox_400.Text = ("Что из перечисленного характеризует интерьеры барокко?");
            button1_400.Text = "а) В интерьерах используются анфилады. " + Environment.NewLine + "Элементы «переходят» друг в друга. " + Environment.NewLine + "На потолках лепной декор, резьба и позолота";
            button2_400.Text = "б) Стиль сочетает симметрию и роскошь." + Environment.NewLine + "Стены украшались росписями, драпировали." + Environment.NewLine + "На пол выкладывали мрамор.";
            button3_400.Text = "в) Формы имеют гармоничные пропорции. " + Environment.NewLine + "Один из принципов – симметрия. " + Environment.NewLine + "Декор: полуколонны, пилястры, фрески.";
            button4_400.Text = "г) Стремление «увеличить» пространство " + Environment.NewLine + "Потолки покрыты штукатуркой / расписаны. " + Environment.NewLine + "Камины стали занимать важное место";
            right400 = button2_400;
        }

        private void button_arhi400_Click(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back400.Visible = true;
            else
                pictureBox_back400.Visible = false;
            (sender as Button).ForeColor = Color.NavajoWhite;
            chance = (sender as Button);
            tabControl1.SelectedIndex = 6;
            wich = ("АРХИТЕКТУРА");
            label_theme400.Text = ("АРХИТЕКТУРА");
            richTextBox_400.Text = ("Кто из русских императоров покровительствуя архитекторам внес большой вклад в появление и развитие так называемого «русского барокко»?");
            button1_400.Text = "а) Елизавета Петровна – " + Environment.NewLine + "так называемое «Елизаветинское барокко»";
            button2_400.Text = "б) Екатерина Великая – " + Environment.NewLine + "так называемое «Екатерининское барокко»";
            button3_400.Text = "в) Александр I – " + Environment.NewLine + "так называемое «Александровское барокко»";
            button4_400.Text = "г) Николай I – " + Environment.NewLine + "так называемое «Николаевское барокко»";
            right400 = button1_400;
        }

        private void button_land400_Click(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back400.Visible = true;
            else
                pictureBox_back400.Visible = false;
            (sender as Button).ForeColor = Color.NavajoWhite;
            chance = (sender as Button);
            tabControl1.SelectedIndex = 6;
            wich = ("ЛАНДШАФТ");
            label_theme400.Text = ("ЛАНДШАФТ");
            richTextBox_400.Text = ("Кто был первым великим садовым архитектором, создателем так называемого французского сада, характеризующегося осевым расположением, ведущим к непрерывным перспективам с пространством сада, определяемым цветочными партерами и живой изгородью, водоёмами, каналами и фонтанами. Его самые известные работы – это парк королевского дворца Версаль.");
            button1_400.Text = "а) Андре Ле Нотр. ";
            button2_400.Text = "б) Франческо Борромини.";
            button3_400.Text = "в) Жак Лемерсье.";
            button4_400.Text = "г) Франсуа Мансар.";
            right400 = button3_400;
        }

        private void button_400tomain_Click(object sender, EventArgs e)
        {
            label_theme400.Visible = true;
            label1_question400.Visible = true; label2_question400.Visible = true;
            panel_line400.Visible = true;
            label_taskbrown.Visible = true;
            button1_400.Visible = true; button2_400.Visible = true; button3_400.Visible = true; button4_400.Visible = true;
            richTextBox_400.Visible = true;
            button3.Visible = true;
            pictureBox_back400.Image = Properties.Resources.ALL_BACK;
            pictureBox_back400.AccessibleName = "ALL_BACK";
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back400.Visible = true;
            else
                pictureBox_back400.Visible = false;
            label_ANSWER400.Visible = false;
            button_400tomain.Visible = false;
            richTextBox_right400.Visible = false;
            richTextBox_answer400.Visible = false;
            tabControl1.SelectedIndex = 2;
        }//после ответа на главную 400

        private void button_autor400_Click(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back400.Visible = true;
            else
                pictureBox_back400.Visible = false;
            (sender as Button).ForeColor = Color.NavajoWhite;
            chance = (sender as Button);
            tabControl1.SelectedIndex = 6;
            wich = ("АВТОРЫ");
            label_theme400.Text = ("АВТОРЫ");
            richTextBox_400.Text = ("Питер Пауль Рубенс - фламандский живописец, один из основоположников искусства барокко и его ярчайший представитель. Ниже представлены некоторые факты из его жизни. Выберите тот, который к нему не относится.");
            button1_400.Text = "а) Занимал место главного придворного " + Environment.NewLine + "живописца правительницы Фландрии " + Environment.NewLine + "инфанты Изабеллы Австрийской.";
            button2_400.Text = "б) Увлекался точными науками и потратил " + Environment.NewLine + "множество денег на попытки " + Environment.NewLine + "создания вечного двигателя.";
            button3_400.Text = "в) Свободно говорил на шести " + Environment.NewLine + "языках и принимал активное участие " + Environment.NewLine + "политической жизни европейских стран.";
            button4_400.Text = "г) В следствие перенесённой тяжёлой" + Environment.NewLine + "болезни не мог иметь своих детей, поэтому " + Environment.NewLine + "взял под опеку нескольких сирот.";
            right400 = button4_400;
        }

        private void button_400_Click(object sender, EventArgs e)
        {
            if ((sender as Button_Round) == right400)
                Get_points(400);
            else
                Get_points(0);
            label_theme400.Visible = false;
            label1_question400.Visible = false; label2_question400.Visible = false;
            panel_line400.Visible = false;
            label_taskbrown.Visible = false;
            button1_400.Visible = false; button2_400.Visible = false; button3_400.Visible = false; button4_400.Visible = false;
            richTextBox_400.Visible = false;
            button3.Visible = false;
            pictureBox_back400.Image = Properties.Resources.QUIZ__back_;
            pictureBox_back400.Visible = true;
            pictureBox_back400.AccessibleName = "QUIZ__back_";
            label_ANSWER400.Visible = true;
            button_400tomain.Visible = true;
            richTextBox_right400.Visible = true;
            richTextBox_answer400.Visible = true;
            richTextBox_right400.Text = right400.Text;
            if (wich == "АВТОРЫ")
            {
                richTextBox_answer400.Text = ("Рубенс был женат дважды, причём во второй раз женился в пятьдесят три года на своей шестнадцатилетней натурщице. Стал отцом восьми детей, последний из которых родился уже после смерти самого художника.");
            }
            if (wich == "СКУЛЬПТУРА")
            {
                richTextBox_answer400.Text = ("Данные слова описывают скульптуру Ренессанса. Барочная скульптура ищет движение, которое подчеркивается с помощью сложных линий натяжения и имеет множество точек зрения. Эта динамика проявляется в беспокойстве персонажей и сцен, в контрасте текстур и поверхностей.");
            }
            if (wich == "ЖИВОПИСЬ")
            {
                richTextBox_answer400.Text = ("Ванитас относится к жанру натюрморт. Это жанр живописи эпохи барокко, аллегорический натюрморт, композиционным центром которого традиционно является человеческий череп.");
            }
            if (wich == "ИНТЕРЬЕР")
            {
                richTextBox_answer400.Text = ("Стиль сочетает в себе симметрию и роскошь. Стены украшались росписями,  драпировали тканью. На пол выкладывали мрамор, полудрагоценные камни, паркет из разных пород деревьев. Использовались цвета: золотой, изумрудный, терракотовый, пурпурный, синий, бежевый, белый, коричневый. Мебель была как предмет искусства.");
            }
            if (wich == "АРХИТЕКТУРА")
            {
                richTextBox_answer400.Text = ("Елизаветинское барокко — архитектурный стиль, возникший во время правления императрицы Елизаветы Петровны. Расцвет его приходится на середину XVIII века. Архитектором, бывшим самым ярким представителем стиля, стал Бартоломео Франческо Растрелли (1700-1771). В честь него елизаветинское барокко нередко называют «растреллиевским».");
            }
            if (wich == "ЛАНДШАФТ")
            {
                richTextBox_answer400.Text = ("Французский архитектор, наряду с Франсуа Мансаром и Луи Лево считается одним из создателей архитектурного стиля Людовика XIII, соединявшего принципы классицизма и элементы барокко.");
            }
        }//получить ответ в номинации 400

        private void button_skulp400_Click(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back400.Visible = true;
            else
                pictureBox_back400.Visible = false;
            (sender as Button).ForeColor = Color.NavajoWhite;
            chance = (sender as Button);
            tabControl1.SelectedIndex = 6;
            wich = ("СКУЛЬПТУРА");
            label_theme400.Text = ("СКУЛЬПТУРА");
            richTextBox_400.Text = ("Скульптуру Барокко сложно спутать с какой-либо другой. Ниже, за исключением одной, представлены её основные характеристики. Укажите ту, которая относится к другому направлению в искусстве.");
            button1_400.Text = "а) Соединение в одной композиции " + Environment.NewLine + "различных материалов.";
            button2_400.Text = "б) Композиционные схемы," + Environment.NewLine + "свободные от геометрии.";
            button3_400.Text = "в) Подчеркивание противоположных" + Environment.NewLine + "особенностей, таких как дряхлость" + Environment.NewLine + "и юность, красота и уродство.";
            button4_400.Text = "г) Интеграция в архитектуру," + Environment.NewLine + " которая обеспечивает драматическую" + Environment.NewLine + " интенсивность.";
            right400 = button2_400;
        }

        private void button_art300_Click(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back300.Visible = true;
            else
                pictureBox_back300.Visible = false;
            (sender as Button).ForeColor = Color.NavajoWhite;
            chance = (sender as Button);
            answer300 = 1;
            richTextBox_300.Text = ("\n\n\n\n\n1. Соответствуют ли данные характеристики живописи барокко: вычурность и броскость форм, торжественность? ");
            label_theme300.Text = ("ЖИВОПИСЬ");
            tabControl1.SelectedIndex = 5;
            wich = ("ЖИВОПИСЬ");
        }

        private void button_inter300_Click(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back300.Visible = true;
            else
                pictureBox_back300.Visible = false;
            (sender as Button).ForeColor = Color.NavajoWhite;
            chance = (sender as Button);
            answer300 = 1;
            richTextBox_300.Text = ("\n\n\n\n\n1. Итальянским мясом называли фрески, которые выполняли подмастерья? ");
            label_theme300.Text = ("ИНТЕРЬЕР");
            tabControl1.SelectedIndex = 5;
            wich = ("ИНТЕРЬЕР");
        }

        private void button_land300_Click(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back300.Visible = true;
            else
                pictureBox_back300.Visible = false;
            (sender as Button).ForeColor = Color.NavajoWhite;
            chance = (sender as Button);
            answer300 = 1;
            richTextBox_300.Text = ("\n\n\n\n1. Правда, что расположенный рядом с Гейдельбергом ансамбль Палатина задал тон всей барочной ландшафтной архитектуре Германии?");
            label_theme300.Text = ("ЛАНДШАФТ");
            tabControl1.SelectedIndex = 5;
            wich = ("ЛАНДШАФТ");
        }

        private void button_arhi300_Click(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back300.Visible = true;
            else
                pictureBox_back300.Visible = false;
            (sender as Button).ForeColor = Color.NavajoWhite;
            chance = (sender as Button);
            answer300 = 1;
            richTextBox_300.Text = ("\n\n\n\n1. У гениев случаются ошибки.Бернини не учел, что церковь стояла на болотистой почве.К тому же из-за сложного характера у мастера не было опытных помощников. Так ли это было? ");
            label_theme300.Text = ("АРХИТЕКТУРА");
            tabControl1.SelectedIndex = 5;
            wich = ("АРХИТЕКТУРА");
        }

        private void buttons_YESNO300_Click(object sender, EventArgs e)
        {
                label_theme300.Visible = false;
                label1_question300.Visible = false; label2_question300.Visible = false;
                panel_line300.Visible = false;
                label_taskMain.Visible = false;
                richTextBox_300.Visible = false;
                button_NO300.Visible = false; button_YES300.Visible = false;
                button1.Visible = false;
            pictureBox_back300.Image = Properties.Resources.QUIZ__back_;
            pictureBox_back300.Visible = true;
            pictureBox_back300.AccessibleName = "QUIZ__back_";
            label_ANSWER300.Visible = true;
                button1_next300.Visible = true;
                richTextBox_300answer.Visible = true;
            if (wich == "АВТОРЫ")
            {
                if (answer300 == 1)
                {
                    richTextBox_300answer.Text = ("Правильный ответ - ДА. Дело в том, что его полотна привлекали огромное внимание публики и, чтобы все остальные работы не остались незамеченными, картины Караваджо экспонировали в последнюю очередь.");
                    if ((sender as Button) == button_YES300)
                        Get_points(100);
                    else
                        Get_points(0);
                }
                if (answer300 == 2)
                {
                    richTextBox_300answer.Text = ("Правильный ответ - НЕТ. Эта фраза принадлежит известному итальянскому художнику и скульптору Джованни Лоренцо Бернини.");
                    if ((sender as Button) == button_NO300)
                        Get_points(100);
                    else
                        Get_points(0);
                }
                if (answer300 == 3)
                {
                    richTextBox_300answer.Text = ("Правильный ответ - НЕТ. Это прозвище относится к другому крупнейшему мастеру Возрождения, Микеланджело Буонарроти. Он разработал новые композиционные формы для библиотеки папы Климента VII и именно это архитектурное сооружение считается первообразом всех зданий стиля барокко.");
                    if ((sender as Button) == button_NO300)
                        Get_points(100);
                    else
                        Get_points(0);
                }
            }
            if (wich == "СКУЛЬПТУРА")
            {
                if (answer300 == 1)
                { 
                    richTextBox_300answer.Text = ("Правильный ответ - НЕТ. Основоположником барочной скульптуры является итальянский архитектор и скульптор Лоренцо Бернини.");
                    if ((sender as Button) == button_NO300)
                        Get_points(100);
                    else
                        Get_points(0);
                }
                if (answer300 == 2)
                {
                    richTextBox_300answer.Text = ("Правильный ответ - ДА. Скульптуры выполнялись из различных материалов. Например, в Испании предпочитали делать их из дерева, иногда добавляя стеклянные вставки.");
                    if ((sender as Button) == button_YES300)
                        Get_points(100);
                    else
                        Get_points(0);
                }
                if (answer300 == 3)
                {
                    richTextBox_300answer.Text = ("Правильный ответ - ДА. Религиозная тематика произведений искусства XVII столетия – сюжеты, связанные с мученичеством и религиозным воодушевлением.");
                    if ((sender as Button) == button_YES300)
                        Get_points(100);
                    else
                        Get_points(0);
                }
            }
            if (wich == "ЖИВОПИСЬ")
            {
                if (answer300 == 1)
                {
                    richTextBox_300answer.Text = ("Правильный ответ - ДА. Ренессансная ясность форм стала неактуальной. Естественность не только вышла из моды, но и стала ассоциироваться с невежеством, дикостью и примитивностью. Установление абсолютизма в Европе способствовали усилению пышности, роскошности, торжественности в искусстве.");
                    if ((sender as Button) == button_YES300)
                        Get_points(100);
                    else
                        Get_points(0);
                }
                if (answer300 == 2)
                {
                    richTextBox_300answer.Text = ("Правильный ответ - НЕТ. Заказчик отказался от картины, потому что традиционны «Смерть Марии» не изображали с печалью, ведь она воссоединилась со своим сыном. А Караваджо изобразил этот сюжет трагически, погрузив фигуры апостолов в скорбь.");
                    if ((sender as Button) == button_NO300)
                        Get_points(100);
                    else
                        Get_points(0);
                }
                if (answer300 == 3)
                {
                    richTextBox_300answer.Text = ("Правильный ответ - НЕТ. Теперь особое внимание уделялось одежде, аксессуарам, драгоценностям – как своеобразной оправе лица.Портрет демонстрировал положение человека, его заслуги перед отечеством.");
                    if ((sender as Button) == button_NO300)
                        Get_points(100);
                    else
                        Get_points(0);
                }
            }
            if (wich == "ИНТЕРЬЕР")
            {
                if (answer300 == 1)
                {
                    richTextBox_300answer.Text = ("Правильный ответ - НЕТ. Итальянское мясо – это мрамор.");
                    if ((sender as Button) == button_NO300)
                        Get_points(100);
                    else
                        Get_points(0);
                }
                if (answer300 == 2)
                {
                    richTextBox_300answer.Text = ("Правильный ответ - ДА. Церковь хотела повлиять на людей через искусство, чтобы люди вернулись к вере и католичество возродилось.");
                    if ((sender as Button) == button_YES300)
                        Get_points(100);
                    else
                        Get_points(0);
                }
                if (answer300 == 3)
                {
                    richTextBox_300answer.Text = ("Правильный ответ - НЕТ. Договор был подписан в Зеркальной галерее.");
                    if ((sender as Button) == button_NO300)
                        Get_points(100);
                    else
                        Get_points(0);
                }
            }
            if (wich == "ЛАНДШАФТ")
            {
                if (answer300 == 1)
                {
                    richTextBox_300answer.Text = ("Правильный ответ - ДА. Для осуществления этого проекта французский мастер Саломон де Коза пригласил правителя палатината — курфюрст Фридриха V.");
                    if ((sender as Button) == button_YES300)
                        Get_points(100);
                    else
                        Get_points(0);
                }
                if (answer300 == 2)
                {
                    richTextBox_300answer.Text = ("Правильный ответ - НЕТ. Не София Медичи, а Мария Медичи Королева Франции, вторая жена Генриха IV Бурбона.");
                    if ((sender as Button) == button_NO300)
                        Get_points(100);
                    else
                        Get_points(0);
                }
                if (answer300 == 3)
                {
                    richTextBox_300answer.Text = ("Правильный ответ - ДА. Сад выступал одновременно и как геометрическая форма и как зона, отведенная для растительного царства.");
                    if ((sender as Button) == button_YES300)
                        Get_points(100);
                    else
                        Get_points(0);
                }
            }
            if (wich == "АРХИТЕКТУРА")
            {
                if (answer300 == 1)
                {
                    richTextBox_300answer.Text = ("Правильный ответ - ДА. Единственным, кто мог бы ему помочь, был Борромини. Но архитекторы просто ненавидели друг друга.");
                    if ((sender as Button) == button_YES300)
                        Get_points(100);
                    else
                        Get_points(0);
                }
                if (answer300 == 2)
                {
                    richTextBox_300answer.Text = ("Правильный ответ - НЕТ. Лоренцо Бернини работал над формированием архитектурного «Вечного города» в свои 25");
                    if ((sender as Button) == button_NO300)
                        Get_points(100);
                    else
                        Get_points(0);
                }
                if (answer300 == 3)
                {
                    richTextBox_300answer.Text = ("Правильный ответ - ДА. Чертежи Растрелли для Зимнего дворца и сегодня хранятся в разных собраниях, библиотеках, а многое опубликовано.");
                    if ((sender as Button) == button_YES300)
                        Get_points(100);
                    else
                        Get_points(0);
                }
            }
        }//получить ответ в номинации 300

        private void button_skulp300_Click(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back300.Visible = true;
            else
                pictureBox_back300.Visible = false;
            (sender as Button).ForeColor = Color.NavajoWhite;
            chance = (sender as Button);
            answer300 = 1;
            richTextBox_300.Text = ("\n\n\n\n\n1. Правда ли, что создателем стиля барокко в скульптуре принято считать Микеланджело?");
            label_theme300.Text = ("СКУЛЬПТУРА");
            tabControl1.SelectedIndex = 5;
            wich = ("СКУЛЬПТУРА");
        }

        private void button1_next300_Click(object sender, EventArgs e)
        {
            label_theme300.Visible = true;
            label1_question300.Visible = true; label2_question300.Visible = true;
            panel_line300.Visible = true;
            label_taskMain.Visible = true;
            richTextBox_300.Visible = true;
            button_NO300.Visible = true; button_YES300.Visible = true;
            pictureBox_back300.Image = Properties.Resources.ALL_BACK;
            pictureBox_back300.AccessibleName = "ALL_BACK";
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back300.Visible = true;
            else
                pictureBox_back300.Visible = false;
            label_ANSWER300.Visible = false;
            button1_next300.Visible = false;
            richTextBox_300answer.Visible = false;
            answer300++;
            if (answer300 == 4)
            {
                answer300 = 0;
                button1.Visible = true;
                tabControl1.SelectedIndex = 2;
            }
            if (wich == "АВТОРЫ")
            {
                if (answer300 == 2)
                    richTextBox_300.Text = ("\n\n\n2. «Три вещи, которые необходимы для успеха в живописи и скульптуре: видеть красоту и приучать себя к этому с молодых лет, упорно работать и получать хорошие советы». Верно ли, что автором данных слов является французский творец Пьер Пюже?");
                if (answer300 == 3)
                    richTextBox_300.Text = ("\n\n\n\n\n3. Как вы считаете, действительно ли Рафаэля Санти называют «Отцом» итальянского барокко?");
            }
            if (wich == "СКУЛЬПТУРА")
            {
                if (answer300 == 2) 
                    richTextBox_300.Text = ("\n\n\n\n\n2. Верно ли, что скульптура барокко выполнялась НЕ только из мрамора?");
                if (answer300 == 3)
                    richTextBox_300.Text = ("\n\n\n\n\n3.Характеризуется ли Барокко повышенным интересом к религиозным и мистическим темам?");
            }
            if (wich == "ЖИВОПИСЬ")
            {
                if (answer300 == 2)
                    richTextBox_300.Text = ("\n\n\n\n\n2. Правда ли, что картина Караваджо «Смерть Марии» была подарена папе Павлу Пятому?");
                if (answer300 == 3)
                    richTextBox_300.Text = ("\n\n\n\n\n3.В живописи русского барокко сохранилась духовная изобразительная традиция?");
            }
            if (wich == "ИНТЕРЬЕР")
            {
                if (answer300 == 2)
                    richTextBox_300.Text = ("\n\n\n\n\n2. Действительно ли церковь была заказчиком многих сооружений, интерьеров, скульптур и т.д.?");
                if (answer300 == 3)
                    richTextBox_300.Text = ("\n\n\n3. 28 июня 1919 года в Версальском дворце под Парижем был подписан мирный договор между побежденной Германией и её победительницами — странами Антанты. Для проведения этого события символично был выбран Салон Мира.");
            }
            if (wich == "ЛАНДШАФТ")
            {
                if (answer300 == 2)
                    richTextBox_300.Text = ("\n\n\n2. В 1612 году, после смерти мужа, София Медичи приказала начать строительство Люксембургского сада в Париже. Он должен был напоминать ей о родной Италии, и за образец при его сооружении были приняты сады Боболи во Флоренции. Правдив ли этот факт?");
                if (answer300 == 3)
                    richTextBox_300.Text = ("\n\n\n\n3. Можно ли сказать, что в процессе развития барочного садово-паркового искусства сталкиваются два противоположных начала: геометрия и природа. ");
            }
            if (wich == "АРХИТЕКТУРА")
            {
                if (answer300 == 2)
                    richTextBox_300.Text = ("\n\n\n\n2. Франческо Борромини в свои 25 лет  стал знаменитостью, и уже работал над формированием архитектурного облика «Вечного города». Согласны ли вы с этим фактом ?");
                if (answer300 == 3)
                    richTextBox_300.Text = ("\n\n\n\n3. Правда, что на фасаде Зимнего дворца Франческо Бартоломео Растрелли разместил огромные окна с восхитительным рисунком обрамления наличников (22 типа)?");
            }
        }//К следующей данетке 300

        private void button_autor300_Click(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back300.Visible = true;
            else
                pictureBox_back300.Visible = false;
            (sender as Button).ForeColor = Color.NavajoWhite;
            chance = (sender as Button);
            answer300 = 1;
            richTextBox_300.Text = ("\n\n\n\n\n1. Правда ли, на выставках картины Караваджо специально накрывали сукном?");
            label_theme300.Text = ("АВТОРЫ");
            tabControl1.SelectedIndex = 5;
            wich = ("АВТОРЫ");
        }

        private void button_art200_Click(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back200.Visible = true;
            else
                pictureBox_back200.Visible = false;
            (sender as Button).ForeColor = Color.NavajoWhite;
            chance = (sender as Button);
            label_task200.Text = ("Все картины за исключением одной – работы Рубенса. Укажите, какая из работ лишняя.");
            label_theme200.Text = ("ЖИВОПИСЬ");
            pictureBox1_200.Image = Properties.Resources.pictureBox1_200_zhivo;
            label1_200.Text = ("1.Охота на львов");
            pictureBox2_200.Image = Properties.Resources.pictureBox2_200_zhivo;
            label2_200.Text = ("2.Моисей и Медный Змей");
            pictureBox3_200.Image = Properties.Resources.pictureBox3_200_zhivo;
            label3_200.Text = ("3.Оплакивание Христа");
            pictureBox4_200.Image = Properties.Resources.pictureBox4_200_zhivo;
            label4_200.Text = ("4.Борей, похищающий Орифию");
            tabControl1.SelectedIndex = 4;
            wich = ("ЖИВОПИСЬ");
            right200 = button2_Round200;
        }

        private void button_inter200_Click(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back200.Visible = true;
            else
                pictureBox_back200.Visible = false;
            (sender as Button).ForeColor = Color.NavajoWhite;
            chance = (sender as Button);
            label_task200.Text = ("Вам представлены интерьеры итальянских религиозных сооружений за исключением одного. Какой является лишним?");
            label_theme200.Text = ("ИНТЕРЬЕР");
            pictureBox1_200.Image = Properties.Resources.pictureBox1_200_int;
            label1_200.Text = ("1.собор Святого Петра, Ватикан");
            pictureBox2_200.Image = Properties.Resources.pictureBox2_200_int;
            label2_200.Text = ("2.церковь Санта-Мария-делла-Витториа");
            pictureBox3_200.Image = Properties.Resources.pictureBox3_200_int;
            label3_200.Text = ("3.Латеранская базилика, Рим");
            pictureBox4_200.Image = Properties.Resources.pictureBox4_200_int;
            label4_200.Text = ("4.Сен Поль Сен Луи, Париж");
            tabControl1.SelectedIndex = 4;
            wich = ("ИНТЕРЬЕР");
            right200 = button4_Round200;
        }

        private void button_land200_Click(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back200.Visible = true;
            else
                pictureBox_back200.Visible = false;
            (sender as Button).ForeColor = Color.NavajoWhite;
            chance = (sender as Button);
            label_task200.Text = ("Какой сад является лишним?");
            label_theme200.Text = ("ЛАНДШАФТ");
            pictureBox1_200.Image = Properties.Resources.pictureBox1_200_land;
            label1_200.Text = ("1.Версальский сад");
            pictureBox2_200.Image = Properties.Resources.pictureBox2_200_land;
            label2_200.Text = ("2.Cад Боболи");
            pictureBox3_200.Image = Properties.Resources.pictureBox3_200_land;
            label3_200.Text = ("3.Сад Дианы");
            pictureBox4_200.Image = Properties.Resources.pictureBox4_200_land;
            label4_200.Text = ("4.Сад Тюильри");
            tabControl1.SelectedIndex = 4;
            wich = ("ЛАНДШАФТ");
            right200 = button2_Round200;
        }

        private void button_arhi200_Click(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back200.Visible = true;
            else
                pictureBox_back200.Visible = false;
            (sender as Button).ForeColor = Color.NavajoWhite;
            chance = (sender as Button);
            label_task200.Text = ("Проект какого архитектурного сооружения НЕ принадлежит Франческо Бартоломео Растрелли?");
            label_theme200.Text = ("АРХИТЕКТУРА");
            pictureBox1_200.Image = Properties.Resources.pictureBox1_200_arhi;
            label1_200.Text = ("1.Екатеринский дворец");
            pictureBox2_200.Image = Properties.Resources.pictureBox2_200_arhi;
            label2_200.Text = ("2.Зимний дворец");
            pictureBox3_200.Image = Properties.Resources.pictureBox3_200_arhi;
            label3_200.Text = ("3.Собор смольного монастыря");
            pictureBox4_200.Image = Properties.Resources.pictureBox4_200_arhi;
            label4_200.Text = ("4.Николо-Богоявленский Морской собор");
            tabControl1.SelectedIndex = 4;
            wich = ("АРХИТЕКТУРА");
            right200 = button4_Round200;
        }

        private void button_200tomain_Click(object sender, EventArgs e)
        {
            button1_Round200.Visible = true; button2_Round200.Visible = true; button3_Round200.Visible = true; button4_Round200.Visible = true; 
            label4_200.Visible = true;
            label_200stack.Visible = true;
            label_queston200.Visible = true;
            label_task200.Visible = true;
            label_theme200.Visible = true;
            panel_line200.Visible = true;
            pictureBox1_200.Visible = true;
            pictureBox2_200.Visible = true;
            pictureBox3_200.Visible = true;
            pictureBox4_200.Visible = true;
            label1_200.Visible = true;
            label2_200.Visible = true;
            label3_200.Visible = true;
            label4_200.Visible = true;
            button2.Visible = true;
            pictureBox_back200.Image = Properties.Resources.ALL_BACK;
            pictureBox_back200.AccessibleName = "ALL_BACK";
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back200.Visible = true;
            else
                pictureBox_back200.Visible = false;
            label_ANSWER200.Visible = false;
            button_200tomain.Visible = false;
            richTextBox_answer200.Visible = false;
            textBox_answer200.Visible = false;
            tabControl1.SelectedIndex = 2;
        }//после ответа на главную 200

        private void button_check200_Click(object sender, EventArgs e)
        {
            if ((sender as Button_Round)==right200)
                Get_points(200);
            else
                Get_points(0);
            label4_200.Visible = false;
            label_200stack.Visible = false;
            label_queston200.Visible = false;
            label_task200.Visible = false;
            label_theme200.Visible = false;
            panel_line200.Visible = false;
            pictureBox1_200.Visible = false;pictureBox2_200.Visible = false;pictureBox3_200.Visible = false;pictureBox4_200.Visible = false;
            label1_200.Visible = false;label2_200.Visible = false;label3_200.Visible = false;label4_200.Visible = false;
            button2.Visible = false;
            pictureBox_back200.Image = Properties.Resources.QUIZ__back_;
            pictureBox_back200.Visible = true;
            pictureBox_back200.AccessibleName = "QUIZ__back_";
            label_ANSWER200.BringToFront();
            button_200tomain.BringToFront();
            label_ANSWER200.Visible = true;
            button_200tomain.Visible = true;
            richTextBox_answer200.Visible = true;
            textBox_answer200.Visible = true;
            button1_Round200.Visible = false; button2_Round200.Visible = false; button3_Round200.Visible = false; button4_Round200.Visible = false; 
            if (wich == "АВТОРЫ")
            {
                textBox_answer200.Text = label4_200.Text;
                richTextBox_answer200.Text = ("- построен в 1732—1762 годах по проекту архитектора Николо Сальви, который воспользовался наработками Бернини.");
            }
            if (wich == "СКУЛЬПТУРА")
            {
                textBox_answer200.Text = label1_200.Text;
                richTextBox_answer200.Text = ("— мраморная полуобнаженная скульптура Венеры в натуральную величину, выполненная в стиле Классицизм итальянским скульптором Антонио Кановой.");
            }
            if (wich == "ЖИВОПИСЬ")
            {
                textBox_answer200.Text = label2_200.Text;
                richTextBox_answer200.Text = ("— Картина была написана в 1620-м году. Это время завершения ученичества ван Дейка в мастерской Рубенса. Среди соучеников ходило мнение, что Антонис Ван Дейк не только не уступает учителю, но и в чем-то превосходит его.");
            }
            if (wich == "ИНТЕРЬЕР")
            {
                textBox_answer200.Text = label4_200.Text;
                richTextBox_answer200.Text = ("— Ренессанс во Франции «задел» частные замки, поэтому в церквях барокко прослеживается готика.");
            }
            if (wich == "ЛАНДШАФТ")
            {
                textBox_answer200.Text = label2_200.Text;
                richTextBox_answer200.Text = ("— Все перечисленные сады находятся во франции, за исключением одного - сада Боболи (Флорениця, Италия)");
            }
            if (wich == "АРХИТЕКТУРА")
            {
                textBox_answer200.Text = label4_200.Text;
                richTextBox_answer200.Text = ("— Все перечисленные архитектурные сооружения были спроектированы Франческо Бартоломео Растрелли, за исключением одного - Николо-Богоявленского Морского собора (архитектор - Савва Иванович Чевакинский)");
            }
        }

        private void button_autor200_Click(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back200.Visible = true;
            else
                pictureBox_back200.Visible = false;
            (sender as Button).ForeColor = Color.NavajoWhite;
            chance = (sender as Button);
            label_task200.Text = ("На слайде представлены произведения известного мастера - Лоренцо Бернини. Опеределите, какая работа принадлежит другому мастеру");
            label_theme200.Text = ("АВТОРЫ");
            pictureBox1_200.Image = Properties.Resources.pictureBox1_200_autor;
            label1_200.Text = ("1.Портрет мальчика");
            pictureBox2_200.Image = Properties.Resources.pictureBox2_200_autor;
            label2_200.Text = ("2.Аполлон и Дафна");
            pictureBox3_200.Image = Properties.Resources.pictureBox3_200_autor;
            label3_200.Text = ("3.Давид");
            pictureBox4_200.Image = Properties.Resources.pictureBox4_200_autor;
            label4_200.Text = ("4.Фонтан де Треви");
            tabControl1.SelectedIndex = 4;
            wich = ("АВТОРЫ");
            right200 = button4_Round200;
        }

        private void button_skulp200_Click(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back200.Visible = true;
            else
                pictureBox_back200.Visible = false;
            (sender as Button).ForeColor = Color.NavajoWhite;
            chance = (sender as Button);
            label_task200.Text = ("Какая из представленных на слайде женских скульптур относится не к эпохе Барокко?");
            label_theme200.Text = ("СКУЛЬПТУРА");
            pictureBox1_200.Image = Properties.Resources.pictureBox1_200_skulp;
            label1_200.Text = ("1.Венера Победительница");
            pictureBox2_200.Image = Properties.Resources.pictureBox2_200_skulp;
            label2_200.Text = ("2.Истина, открытая Временем");
            pictureBox3_200.Image = Properties.Resources.pictureBox3_200_skulp;
            label3_200.Text = ("3.Добродетельница с двумя детьми");
            pictureBox4_200.Image = Properties.Resources.pictureBox4_200_skulp;
            label4_200.Text = ("4.Святая Цецилия");
            tabControl1.SelectedIndex = 4;
            wich = ("СКУЛЬПТУРА");
            right200 = button1_Round200;
        }

        private void button_skulp100_Click(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back100.Visible = true;
            else
                pictureBox_back100.Visible = false;
            (sender as Button).ForeColor = Color.NavajoWhite;
            chance = (sender as Button);
            label_task100.Text = ("Соотнесите скульптуры с портретами авторов.");
            label_theme100.Text = ("СКУЛЬПТУРА");
            pictureBox_up1.Image = Properties.Resources.pictureBox_up1_skulp;
            pictureBox_up2.Image = Properties.Resources.pictureBox_up2_skulp;
            pictureBox_up3.Image = Properties.Resources.pictureBox_up3_skulp;
            pictureBox_up4.Image = Properties.Resources.pictureBox_up4_skulp;
            pictureBox_down1.Image = Properties.Resources.pictureBox_down1_skulp;
            pictureBox_down2.Image = Properties.Resources.pictureBox_down2_skulp;
            pictureBox_down3.Image = Properties.Resources.pictureBox_down4_skulp;
            pictureBox_down4.Image = Properties.Resources.pictureBox_down3_skulp;
            tabControl1.SelectedIndex = 3;
            wich= ("СКУЛЬПТУРА");
        }

        private void button_100tomain_Click(object sender, EventArgs e)
        {
            if((pictureBox_back1.BackColor == Color.SeaGreen)&& (pictureBox_back2.BackColor == Color.SeaGreen) && (pictureBox_back3.BackColor == Color.SeaGreen) && (pictureBox_back4.BackColor == Color.SeaGreen))
                Get_points(100);
            else
                Get_points(0);
            tabControl1.SelectedIndex = 2;
            label_100stack.Visible = true;
            label_queston100.Visible = true;
            label_task100.Visible = true;
            label_theme100.Visible = true;
            button_check100.Visible = true;
            panel_line100.Visible = true;
            button_back.Visible = true;
            pictureBox_back100.Image = Properties.Resources.ALL_BACK;
            pictureBox_back100.AccessibleName = "ALL_BACK";
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back100.Visible = true;
            else
                pictureBox_back100.Visible = false;
            label_ANSWER.Visible = false;
            button_100tomain.Visible = false;
            pictureBox_back1.BackColor = Color.FloralWhite;
            pictureBox_back2.BackColor = Color.FloralWhite;
            pictureBox_back3.BackColor = Color.FloralWhite;
            pictureBox_back4.BackColor = Color.FloralWhite;
        }//после ответа на главную 100

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back100.Visible = true;
            else
                pictureBox_back100.Visible = false;
            (sender as Button).ForeColor = Color.NavajoWhite;
            chance = (sender as Button);
            label_task100.Text = ("Соотнесите известных итальянских живописцев с названиями городов, в которых они родились.");
            label_theme100.Text = ("АВТОРЫ");
            pictureBox_up1.Image = Properties.Resources.pictureBox_up1_autor;
            pictureBox_up2.Image = Properties.Resources.pictureBox_up2_autor;
            pictureBox_up3.Image = Properties.Resources.pictureBox_up3_autor;
            pictureBox_up4.Image = Properties.Resources.pictureBox_up4_autor;
            pictureBox_down1.Image = Properties.Resources.pictureBox_down3_autor;
            pictureBox_down2.Image = Properties.Resources.pictureBox_down4_autor;
            pictureBox_down3.Image = Properties.Resources.pictureBox_down1_autor;
            pictureBox_down4.Image = Properties.Resources.pictureBox_down2_autor;
            tabControl1.SelectedIndex = 3;
            wich = ("АВТОРЫ");
        }//это если что авторы за 100

        private void button_land100_Click(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back100.Visible = true;
            else
                pictureBox_back100.Visible = false;
            (sender as Button).ForeColor = Color.NavajoWhite;
            chance = (sender as Button);
            label_task100.Text = ("Соедините фотографию сада и его название.");
            label_theme100.Text = ("ЛАНДШАФТ");
            pictureBox_up1.Image = Properties.Resources.pictureBox_up1_land;
            pictureBox_up2.Image = Properties.Resources.pictureBox_up2_land;
            pictureBox_up3.Image = Properties.Resources.pictureBox_up3_land;
            pictureBox_up4.Image = Properties.Resources.pictureBox_up4_land;
            pictureBox_down1.Image = Properties.Resources.pictureBox_down1_land;
            pictureBox_down2.Image = Properties.Resources.pictureBox_down3_land;
            pictureBox_down3.Image = Properties.Resources.pictureBox_down2_land;
            pictureBox_down4.Image = Properties.Resources.pictureBox_down4_land;
            tabControl1.SelectedIndex = 3;
            wich = ("ЛАНДШАФТ");
        }

        private void button_art100_Click(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back100.Visible = true;
            else
                pictureBox_back100.Visible = false;
            (sender as Button).ForeColor = Color.NavajoWhite;
            chance = (sender as Button);
            label_task100.Text = ("Соотнесите полотна c их авторами");
            label_theme100.Text = ("ЖИВОПИСЬ");
            pictureBox_up1.Image = Properties.Resources.pictureBox_up1_zhivo;
            pictureBox_up2.Image = Properties.Resources.pictureBox_up2_zhivo;
            pictureBox_up3.Image = Properties.Resources.pictureBox_up3_zhivo;
            pictureBox_up4.Image = Properties.Resources.pictureBox_up4_zhivo;
            pictureBox_down1.Image = Properties.Resources.pictureBox_down4_zhivo;
            pictureBox_down2.Image = Properties.Resources.pictureBox_down2_zhivo;
            pictureBox_down3.Image = Properties.Resources.pictureBox_down3_zhivo;
            pictureBox_down4.Image = Properties.Resources.pictureBox_down1_zhivo;
            tabControl1.SelectedIndex = 3;
            wich = ("ЖИВОПИСЬ");
        }

        private void button_inter100_Click(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back100.Visible = true;
            else
                pictureBox_back100.Visible = false;
            (sender as Button).ForeColor = Color.NavajoWhite;
            chance = (sender as Button);
            label_task100.Text = ("Соотнесите изображения элементов орнамента и их названия");
            label_theme100.Text = ("ИНТЕРЬЕР");
            pictureBox_up1.Image = Properties.Resources.pictureBox_up1_int;
            pictureBox_up2.Image = Properties.Resources.pictureBox_up2_int;
            pictureBox_up3.Image = Properties.Resources.pictureBox_up3_int;
            pictureBox_up4.Image = Properties.Resources.pictureBox_up4_int;
            pictureBox_down1.Image = Properties.Resources.pictureBox_down2_int;
            pictureBox_down2.Image = Properties.Resources.pictureBox_down1_int;
            pictureBox_down3.Image = Properties.Resources.pictureBox_down3_int;
            pictureBox_down4.Image = Properties.Resources.pictureBox_down4_int;
            tabControl1.SelectedIndex = 3;
            wich = ("ИНТЕРЬЕР");
        }

        private void button_arhi100_Click(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                pictureBox_back100.Visible = true;
            else
                pictureBox_back100.Visible = false;
            (sender as Button).ForeColor = Color.NavajoWhite;
            chance = (sender as Button);
            label_task100.Text = ("Соедините картинки сооружений и имена архитекторов проектирующих их.");
            label_theme100.Text = ("АРХИТЕКТУРА");
            pictureBox_up1.Image = Properties.Resources.pictureBox_up1_arhi;
            pictureBox_up2.Image = Properties.Resources.pictureBox_up2_arhi;
            pictureBox_up3.Image = Properties.Resources.pictureBox_up3_arhi;
            pictureBox_up4.Image = Properties.Resources.pictureBox_up4_arhi;
            pictureBox_down1.Image = Properties.Resources.pictureBox_down1_arhi;
            pictureBox_down2.Image = Properties.Resources.pictureBox_down2_arhi;
            pictureBox_down3.Image = Properties.Resources.pictureBox_down3_arhi;
            pictureBox_down4.Image = Properties.Resources.pictureBox_down4_arhi;
            tabControl1.SelectedIndex = 3;
            wich = ("АРХИТЕКТУРА");
        }

        private void button_check100_Click(object sender, EventArgs e)
        {
            label_100stack.Visible = false;
            label_queston100.Visible = false;
            label_task100.Visible = false;
            label_theme100.Visible = false;
            button_check100.Visible = false;
            panel_line100.Visible = false;
            button_back.Visible = false;
            pictureBox_back100.Image = Properties.Resources.QUIZ__back_;
            pictureBox_back100.Visible = true;
            pictureBox_back100.AccessibleName = "QUIZ__back_";
            label_ANSWER.BringToFront();
            button_100tomain.BringToFront();
            label_ANSWER.Visible = true;
            button_100tomain.Visible = true;
            if (wich == "АВТОРЫ")
            {
                if (pictureBox_down3.Location == new Point(pictureBox_up1.Location.X, pictureBox_up1.Location.Y + pictureBox_up1.Height + height))
                    pictureBox_back1.BackColor = Color.SeaGreen;
                if (pictureBox_down4.Location == new Point(pictureBox_up2.Location.X, pictureBox_up2.Location.Y + pictureBox_up2.Height + height))
                    pictureBox_back2.BackColor = Color.SeaGreen;
                if (pictureBox_down1.Location == new Point(pictureBox_up3.Location.X, pictureBox_up3.Location.Y + pictureBox_up3.Height + height))
                    pictureBox_back3.BackColor = Color.SeaGreen;
                if (pictureBox_down2.Location == new Point(pictureBox_up4.Location.X, pictureBox_up4.Location.Y + pictureBox_up4.Height + height))
                    pictureBox_back4.BackColor = Color.SeaGreen;
            }
            if (wich == "СКУЛЬПТУРА")
            {
                if (pictureBox_down1.Location == new Point(pictureBox_up1.Location.X, pictureBox_up1.Location.Y + pictureBox_up1.Height + height))
                    pictureBox_back1.BackColor = Color.SeaGreen;
                if (pictureBox_down2.Location == new Point(pictureBox_up2.Location.X, pictureBox_up2.Location.Y + pictureBox_up2.Height + height))
                    pictureBox_back2.BackColor = Color.SeaGreen;
                if (pictureBox_down4.Location == new Point(pictureBox_up3.Location.X, pictureBox_up3.Location.Y + pictureBox_up3.Height + height))
                    pictureBox_back3.BackColor = Color.SeaGreen;
                if (pictureBox_down3.Location == new Point(pictureBox_up4.Location.X, pictureBox_up4.Location.Y + pictureBox_up4.Height + height))
                    pictureBox_back4.BackColor = Color.SeaGreen;
            }
            if (wich == "ЛАНДШАФТ")
            {
                if (pictureBox_down1.Location == new Point(pictureBox_up1.Location.X, pictureBox_up1.Location.Y + pictureBox_up1.Height + height))
                    pictureBox_back1.BackColor = Color.SeaGreen;
                if (pictureBox_down3.Location == new Point(pictureBox_up2.Location.X, pictureBox_up2.Location.Y + pictureBox_up2.Height + height))
                    pictureBox_back2.BackColor = Color.SeaGreen;
                if (pictureBox_down2.Location == new Point(pictureBox_up3.Location.X, pictureBox_up3.Location.Y + pictureBox_up3.Height + height))
                    pictureBox_back3.BackColor = Color.SeaGreen;
                if (pictureBox_down4.Location == new Point(pictureBox_up4.Location.X, pictureBox_up4.Location.Y + pictureBox_up4.Height + height))
                    pictureBox_back4.BackColor = Color.SeaGreen;
            }
            if (wich == "ЖИВОПИСЬ")
            {
                if (pictureBox_down4.Location == new Point(pictureBox_up1.Location.X, pictureBox_up1.Location.Y + pictureBox_up1.Height + height))
                    pictureBox_back1.BackColor = Color.SeaGreen;
                if (pictureBox_down2.Location == new Point(pictureBox_up2.Location.X, pictureBox_up2.Location.Y + pictureBox_up2.Height + height))
                    pictureBox_back2.BackColor = Color.SeaGreen;
                if (pictureBox_down3.Location == new Point(pictureBox_up3.Location.X, pictureBox_up3.Location.Y + pictureBox_up3.Height + height))
                    pictureBox_back3.BackColor = Color.SeaGreen;
                if (pictureBox_down1.Location == new Point(pictureBox_up4.Location.X, pictureBox_up4.Location.Y + pictureBox_up4.Height + height))
                    pictureBox_back4.BackColor = Color.SeaGreen;
            }
            if (wich == "ИНТЕРЬЕР")
            {
                if (pictureBox_down2.Location == new Point(pictureBox_up1.Location.X, pictureBox_up1.Location.Y + pictureBox_up1.Height + height))
                    pictureBox_back1.BackColor = Color.SeaGreen;
                if (pictureBox_down1.Location == new Point(pictureBox_up2.Location.X, pictureBox_up2.Location.Y + pictureBox_up2.Height + height))
                    pictureBox_back2.BackColor = Color.SeaGreen;
                if (pictureBox_down3.Location == new Point(pictureBox_up3.Location.X, pictureBox_up3.Location.Y + pictureBox_up3.Height + height))
                    pictureBox_back3.BackColor = Color.SeaGreen;
                if (pictureBox_down4.Location == new Point(pictureBox_up4.Location.X, pictureBox_up4.Location.Y + pictureBox_up4.Height + height))
                    pictureBox_back4.BackColor = Color.SeaGreen;
            }
            if (wich == "АРХИТЕКТУРА")
            {
                if (pictureBox_down1.Location == new Point(pictureBox_up1.Location.X, pictureBox_up1.Location.Y + pictureBox_up1.Height + height))
                    pictureBox_back1.BackColor = Color.SeaGreen;
                if (pictureBox_down2.Location == new Point(pictureBox_up2.Location.X, pictureBox_up2.Location.Y + pictureBox_up2.Height + height))
                    pictureBox_back2.BackColor = Color.SeaGreen;
                if (pictureBox_down3.Location == new Point(pictureBox_up3.Location.X, pictureBox_up3.Location.Y + pictureBox_up3.Height + height))
                    pictureBox_back3.BackColor = Color.SeaGreen;
                if (pictureBox_down4.Location == new Point(pictureBox_up4.Location.X, pictureBox_up4.Location.Y + pictureBox_up4.Height + height))
                    pictureBox_back4.BackColor = Color.SeaGreen;
            }
        }//Проверить правильно ли стоят картинки 100
    }
}
