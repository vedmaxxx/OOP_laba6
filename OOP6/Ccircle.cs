using System;
using System.Drawing;

namespace OOP6
{

    public class CCircle : Base
    {
        private int R;

        public override char getCode()
        {
            return 'C';
        }
        public override void initComps()
        {
            base.initComps();
            R = 20;
        }
        public CCircle(int x, int y, Mylist mylist, int width, int height)
        {

            initComps();
            //Проверяем не уйдёт ли часть объекта за рамки если есть место для объекта создаём
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
        
        public override bool isClicked(int x,int y, bool isCtrl, Mylist mylist)
        {
            double tmp = Math.Pow(this.x-x, 2) + Math.Pow(this.y - y, 2);
            if (tmp < (R * R))
            {
                toSelect(isCtrl, mylist);
                return true;
            }
            return false;
        }
        
        
        public void drawCircle(Graphics gr)//Вывод просто вершины(круга)
        {
            gr.FillEllipse(br, (x - R), (y - R), 2 * R, 2 * R);
            gr.DrawEllipse(blackpen, (x - R), (y - R), 2 * R, 2 * R);
        }
        
        public void drawSelectedVert(Graphics gr)//Внутренний вывод выбранной
        {
            gr.DrawEllipse(redpen, (x - R), (y - R), 2 * R, 2 * R);
        }

        public override void print(Graphics gr)//Вывод круга
        {
            drawCircle(gr);
            if (Selected)
            {
                drawSelectedVert(gr);
            }
        }

        public override void move(int x_, int y_,int i_,int width, int height)
        {
            //Проверяем не выйдем ли мы за границу Бокса
            if ((x + x_+R<width)&&(y+y_+R<height)&& (x + x_-R >0)&&(y + y_ - R >0))
            {
                x += x_;
                y += y_;
            }
        }

        public override void changeSize(int size, int i_, int width, int height)
        {
            if ((x + R+size < width) && (y + size + R < height) && (x - size - R > 0) && (y -size - R > 0))
            {
                R += size;
            } 
        }
    }
}
