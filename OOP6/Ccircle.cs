using System;
using System.Drawing;

namespace OOP6
{

    public class CCircle : Base
    {
        //радиус круга
        private int R;

        //возвращение кода класса
        public override char getCode()
        {
            return 'C';
        }

        //инициализация компонентов объекта
        public override void initComps()
        {
            base.initComps();
            R = 20;
        }

        public CCircle(int x, int y, Mylist mylist, int width, int height)
        {

            initComps();
            //проверяем - не уйдёт ли часть объекта за рамки - если есть место для объекта, создаём его
            if (((x + R < width) && (y + R < height) && (x - R > 0) && (y - R > 0)))
            {
                this.x = x;
                this.y = y;
                refreshSelected(mylist);
                Selected = true;
                mylist.add(this);
            }
        }

        public CCircle(CCircle copy)
        {
            initComps();
            x = copy.x;
            y = copy.y;
            Selected = copy.Selected;
        }
        
        //проверка на то, кликнут ли объект
        public override bool isClicked(int x,int y, bool isCtrl, Mylist mylist)
        {
            //проверка, куда кликнули
            double tmp = Math.Pow(this.x-x, 2) + Math.Pow(this.y - y, 2);
            //если попали по объекту - выделяем
            if (tmp < (R * R))
            {
                toSelect(isCtrl, mylist);
                return true;
            }
            return false;
        }
        
        //отрисовка объекта
        public void drawCircle(Graphics gr)
        {
            gr.FillEllipse(br, (x - R), (y - R), 2 * R, 2 * R);
            gr.DrawEllipse(blackpen, (x - R), (y - R), 2 * R, 2 * R);
        }
        
        //отрисовка выделения
        public void drawSelectedVert(Graphics gr)
        {
            gr.DrawEllipse(redpen, (x - R), (y - R), 2 * R, 2 * R);
        }

        //метод вывода круга на форму
        public override void print(Graphics gr)
        {
            drawCircle(gr);
            if (Selected)
            {
                drawSelectedVert(gr);
            }
        }

        //изменение положения объекта при движении
        public override void move(int x_, int y_,int i_,int width, int height)
        {
            //если мы выходим за форму, то не двигаем
            if ((x + x_+R<width)&&(y+y_+R<height)&& (x + x_-R >0)&&(y + y_ - R >0))
            {
                x += x_;
                y += y_;
            }
        }

        //изменение размера
        public override void changeSize(int size, int i_, int width, int height)
        {
            if ((x + R+size < width) && (y + size + R < height) && (x - size - R > 0) && (y -size - R > 0))
            {
                R += size;
            } 
        }
    }
}