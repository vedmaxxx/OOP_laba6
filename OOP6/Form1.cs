using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP6
{
    public partial class Form1 : Form
    {
        MyList list;
        bool isCTRL;
        Bitmap bitmap;
        Graphics gr ;

        public Form1()
        {
            InitializeComponent();
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            gr = Graphics.FromImage(bitmap);
            
            list = new MyList();
            isCTRL = false;
            pictureBox1.Image = getBitmap();
        }

        //отрисовка объектов
        public void printStorage()                 
        {
            //если в хранилище нет объектов - нечего отрисовывать, возвращаемся
            if (list.getSize() == 0)
                return;
            //иначе отрисовываем всё
            for (int i = 0; i < list.getSize(); i++)
            {
                list.getObj(i).print(gr);
            }
        }

        //возвращает форму для отрсиосвки
        public Bitmap getBitmap()
        {
            return bitmap;
        }

        //обновление формы - отрисовка ВСЕХ объектов заново
        private void refreshSheet()
        {
            clearSheet();                       //очищает форму
            printStorage();                        //отрисовать все объекты снова
            pictureBox1.Image = getBitmap();    //снова загрузить картинку
        }

        public void clearSheet()                //очистка картинки
        {
            gr.Clear(Color.White);
        }

        //создание объекта CCircle
        public void createCCircle(int x, int y)
        {
            clearSheet();       //очищаем форму
            //создаем объект
            Circle circle = new Circle(x,y,list, pictureBox1.Width, pictureBox1.Height);
            //
            printStorage();
            pictureBox1.Image = getBitmap();
        }

        //создание объекта Rectangle
        public void createRectangle(int x,int y)
        {
            clearSheet();
            Rectangle rectangle = new Rectangle(x, y, list, pictureBox1.Width, pictureBox1.Height);
            printStorage();
            pictureBox1.Image = getBitmap();
        }

        //создание объекта Square
        public void createSquare(int x, int y)
        {
            clearSheet();
            Square square = new Square(x, y, list, pictureBox1.Width, pictureBox1.Height);
            printStorage();
            pictureBox1.Image = getBitmap();
        }

        //создание объекта Triangle
        public void createTriangle(int x,int y)
        {
            clearSheet();
            Triangle triangle = new Triangle(x, y, list, pictureBox1.Width, pictureBox1.Height);
            printStorage();
            pictureBox1.Image = getBitmap();
        }

        //при клике смотрим, мы выделяем объект или создаем объект
        public void click(object sender, MouseEventArgs e)
        {
            bool clickedOnObject = false;          //если попали на объект
            int i;
            int size = list.getSize();
            for(i = 0; i < size && (!clickedOnObject); i++)
            {
                //смотрим на объект из списка и в clickedOnObject заносим, выделен ли какой-то или нет
                switch (list.getObj(i).getCode())
                {
                    case 'C':
                        clickedOnObject = ((Circle)list.getObj(i)).isClicked(e.X, e.Y,isCTRL,list);    
                        break;
                    case 'R':
                        clickedOnObject = ((Rectangle)list.getObj(i)).isClicked(e.X, e.Y,isCTRL, list);
                        break;
                    case 'S':
                        clickedOnObject = ((Square)list.getObj(i)).isClicked(e.X, e.Y, isCTRL, list);
                        break;
                    case 'T':
                        clickedOnObject = ((Triangle)list.getObj(i)).isClicked(e.X, e.Y, isCTRL, list);
                        break;
                }
            }

            //если мы не кликнули на объект, то создаем объект класса, выбранного в listBox1
            if (!clickedOnObject)
            {
                switch (listFigures.SelectedItem.ToString())
                {
                    case "Circle":
                        createCCircle(e.X, e.Y);
                        break;
                    case "Rectangle":
                        createRectangle(e.X, e.Y);
                        break;
                    case "Square":
                        createSquare(e.X, e.Y);
                        break;
                    case "Triangle":
                        createTriangle(e.X, e.Y);
                        break;
                    default:
                        break;
                }
            }
            //если кликнули на объект, обновляем прорисовку и объект будет выделен
            else refreshSheet();
        }

        private void btnChangeColor(object sender, EventArgs e)
        {
            isCTRL = false;
            //просматриваем выбранные объекты
            //если объект выбран, то методом setBrush меняем цвет заливки
            for (int i = 0; i < list.getSize(); i++)
            {
                if (list.getObj(i).getSelect())
                {
                    list.getObj(i).setBrush(listColor.SelectedItem.ToString());
                }
            }
            //рефрешим отрисовку
            refreshSheet();
        }

        //обработка нажатия на форму
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int value = 1;
            
            if (e.Shift)                    //если прожали Shift(откат ctrl-a)
            {
                value = 10;
            }
            if (e.Control)                  //если прожали Ctrl
            {
                isCTRL = true;
            }
            else
            {
                switch (e.KeyCode)          //смотрим какие кнопки прожаты
                {
                    case Keys.Delete:       //если прожат Delete
                        //удаляем все выделенные объекты из хранилища
                        for (int j = list.getSize() - 1; j >= 0; j--)
                        {
                            //удаляем все выделенные объекты
                            list.getObj(j).deleteSelected(list);
                        }
                        //задаем первому элементу свойство выбранности Selected = true
                        list.getObj(0).setSelect(true);
                        break;
                    case Keys.Left:
                        //если нажали стрелку влево - двигаемся влево
                        for (int i = 0; i < list.getSize(); i++)
                        {
                            if (list.getObj(i).getSelect())
                            {
                                list.getObj(i).move(-value, 0, i, pictureBox1.Width, pictureBox1.Height);
                            }
                        }
                        break;
                    case Keys.Right:
                        //если нажали стрелку вправо - двигаемся вправо
                        for (int i = 0; i < list.getSize(); i++)
                        {
                            if (list.getObj(i).getSelect())
                            {
                                list.getObj(i).move(value, 0, i, pictureBox1.Width, pictureBox1.Height);
                            }
                        }
                        break;
                    case Keys.Up:
                        //если нажали стрелку вверх - двигаемся вверх
                        for (int i = 0; i < list.getSize(); i++)
                        {
                            if (list.getObj(i).getSelect())
                            {
                                list.getObj(i).move(0, -value, i, pictureBox1.Width, pictureBox1.Height);
                            }
                        }
                        break;
                    case Keys.Down:
                        //если нажали стрелку вниз - двигаемся вниз
                        for (int i = 0; i < list.getSize(); i++)
                        {
                            if (list.getObj(i).getSelect())
                            {
                                list.getObj(i).move(0, value, i, pictureBox1.Width, pictureBox1.Height/*, lists*/);
                            }
                        }
                        break;
                    case Keys.OemMinus:
                        //если нажали минус - уменьшаем размер объекта
                        for (int i = 0; i < list.getSize(); i++)
                        {
                            if (list.getObj(i).getSelect())
                            {
                                list.getObj(i).changeSize(-value, i, pictureBox1.Width, pictureBox1.Height/*, lists*/);
                            }
                        }
                        break;
                    case Keys.Oemplus:
                        //если нажали плюс - увеличиваем размер объекта
                        for (int i = 0; i < list.getSize(); i++)
                        {
                            if (list.getObj(i).getSelect())
                            {
                                list.getObj(i).changeSize(+value, i, pictureBox1.Width, pictureBox1.Height/*, lists*/);
                            }
                        }
                        break;
                    
                }
                refreshSheet();         //отрисовываем форму
                isCTRL = false;     //выключаем ctrl
            }
            
        }

        private void listColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            //при выборе цвета в списке цветов делает кнопку brush доступной
            btnBrush.Enabled = true;
        }

        private void listFigures_SelectedIndexChanged(object sender, EventArgs e)
        {
            //при выборе фигуры разблокирует форму для рисования
            pictureBox1.Enabled = true;
        }
    }
}