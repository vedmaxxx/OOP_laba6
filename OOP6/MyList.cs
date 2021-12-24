﻿using System;
using System.Drawing;


namespace OOP6
{
    //базовый класс
    public class Base
    {
        protected Pen blackpen;
        protected Pen redpen;
        protected Brush br = Brushes.White;

        //состояние выделенности объекта
        protected bool Selected = false;    
        public int x, y;

        //инициализация компонентов объекта
        public virtual void initComps()
        {
            blackpen = new Pen(Color.Black);
            blackpen.Width = 1;
            redpen = new Pen(Color.Red);
            redpen.Width = 1;
        }

		public virtual char getCode()
        {
            //возврат кода
            return 'B';
        }

        //имеет реализацию далее
        public virtual bool isClicked(int x, int y, bool isCtrl, MyList mylist) 
        {
            return true;
        }

        //установка цвета кисти
        public virtual void setBrush(string color)///Blue/Brown/Yellow/Green/Purple/Red/White
        {
            switch (color)
            {
                case "Blue":
                    br = Brushes.Blue;
                    break;
                case "Brown":
                    br = Brushes.Brown;
                    break;
                case "Yellow":
                    br = Brushes.Yellow;
                    break;
                case "Green":
                    br = Brushes.Green;
                    break;
                case "Purple":
                    br = Brushes.Purple;
                    break;
                case "Red":
                    br = Brushes.Red;
                    break;
                case "White":
                    br = Brushes.White;
                    break;

                default:
                    break;
            }
        }

        //задает объекту свойство выделенности
        public virtual void setSelect(bool value)
        {
            Selected = value;
        }

        //возвращает показатель выделения объекта
        //Selected - bool
        public virtual bool getSelect()
        {
            return Selected;
        }

        //метод отрисовки без реализации
        public virtual void print(Graphics gr)
        {

        }

        //метод перемещения без реализации
        public virtual void move(int x_, int y_, int i_, int width, int height)
        {

        }

        //метод изменения размера
        public virtual void changeSize(int size,int i_, int width, int height)
        {

        }
        
        //сброс метод выделенности у всех объектов хранилища
        public virtual void refreshSelected(MyList mylist)
        {
            for (int j = 0; j < mylist.getSize(); j++)
            {
                mylist.getObj(j).Selected = false;
            }
        }

        //если не прожат CTRL, сбросить все выделенные объекты
        //и сделать текущий объект выделенным
        public virtual void toSelect(bool isCTRL, MyList mylist)
        {
            if (!isCTRL)
            {
                refreshSelected(mylist);
            }
            setSelect(true);
        }

        //удаление выделенного объекта из хранилища
        public virtual void deleteSelected(MyList list)
        {
            if (Selected) list.deleteObj(this);
        }
    }

    //класс отвечает за создание объектов, которые хранятся в переменной базового класса
    class MyBaseFactory
	{
        public MyBaseFactory() { }
		public Base createBase(Base p)
		{
			Base _base = null;
			switch (p.getCode())
			{
                case 'C':
					_base = new Circle((Circle)p);
					break;
                case 'R':
                    _base = new Rectangle((Rectangle)p);
                    break;
                case 'S':
                    _base = new Square((Square)p);
                    break;
                case 'T':
                    _base = new Triangle((Triangle)p);
                    break;
                default:
                    break;
			}
			return _base;
		} 
	}

    //хранилище
    public class MyList
    {
        public class Node
        {
            public Base base_=null;
            public Node next=null; //указатель на следующую ячейку списка

            public Node(Base _base)
            {
                MyBaseFactory factory = new MyBaseFactory();
                base_ = factory.createBase(_base);
            }

            public bool isEOL() { return Convert.ToBoolean(this == null ? 1 : 0); }
        };

        public void delete_first()
        {
            if (isEmpty()) return;

            Node temp = first;
            first =temp.next;
        }
        public void delete_last()
        {
            if (isEmpty()) return;
            if (last == first)
            {
                delete_first();
                return;
            }

            Node temp =first;
            while (temp.next != last)
            {
                temp = temp.next;
            }
            temp.next = null;
            last = temp;
        }

        public Node first=null;

        public Node last=null;

        public void add(Base _base)
        {
            Node another = new Node(_base);

            if (isEmpty())
            {
                first = another;
                last = another;
                return;
            }
            last.next = another;
            last = another;
        }
        public bool isEmpty()
        {
            return first == null;
        }
        public void deleteObj(Base _base)
        {
            if (isEmpty())
            {
                Console.WriteLine("\tХранилище пусто, удалить не удалось\n");
                return;
            }
            if (last.base_ == _base) {
                delete_last();
                Console.WriteLine("\tЭлемент удален\n");
                return;
            }
            if (first.base_ == _base) {
                delete_first();
                Console.WriteLine("\tЭлемент удален\n");
                return;
            }

            Node current = first;
            while (current.next != null && current.next.base_ != _base) {
                current = current.next;
            }
            if (current.next == null)
            {
                Console.WriteLine("\tТакого элемента нет в списке\n");
                return;
            }
            Node tmp_next = current.next;
            current.next =
                current.next.next;
                
            Console.WriteLine("\tЭлемент удален\n");
        }
        public int getSize()
        {
            if (first == null) return 0;
            Node node = first;
            int i = 1;
             
            while (node.next!=null)
            {
                i++;
                node = node.next;
            }
            return i;
        }

        public Base getObj(int i)
        {
            if (isEmpty())
            {
                Console.WriteLine("Хранилище пусто, возвращать нечего\n");
                return null;//исправить на исключение
            }
            int j = 0;
            Node current = first;
            //while (j < (i + 1) && !(current.isEOL())) {
            while (j < i && !(first.isEOL()))
            {
                current = current.next;
                j++;
            }
            // Console.WriteLine("\tОбъект передан\n");
            return (current.base_);
        }
        public Base getObjAndDelete(int i)
        {
            if (isEmpty())
            {
                Console.WriteLine("\tХранилище пусто, возвращать нечего\n");
                return null;//исправить на исключение
            }
            Base ret = getObj(i);
            Base tmp;
            MyBaseFactory factory = new MyBaseFactory();
            tmp = factory.createBase(ret);
            deleteObj(ret);
            Console.WriteLine("\tОбъект передан\n");
            return tmp;
        }
    };
};