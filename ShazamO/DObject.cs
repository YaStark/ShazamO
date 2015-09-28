using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Drawing;

public class DObject
{
    private double[] matrix = null;
    private int sx = 0;
    private int sy = 0;

    /*** Сложение строк ***/
    private bool addLineInner(int recipient, int donor, double mult)
    {
        if (donor >= sy || recipient >= sy) return false;
        for (int i = 0; i < sx; i++)
        {
            set(i, recipient, mult * get(i, donor) + get(i, recipient));
        }
        return true; 
    }
    /*** Умножение строки на число ***/
    private bool multLineInner(int n, double mult)
    {
        if (n >= sy) return false;
        for (int i = 0; i < sx; i++)
        {
            set(i, n, get(i, n) * mult);
        }
        return true;
    }
    /*** Поменять линии местами ***/
    private bool swapLineInner(int n1, int n2)
    {
        if (n1 >= sy || n2 >= sy) return false;
        for (int i = 0; i < sx; i++)
        {
            double t = get(i, n1);
            set(i, n1, get(i, n2));
            set(i, n2, t);
        }
        return true;
    }

    public DObject() { }

    public DObject(int sizex, int sizey) 
    { 
        sx = sizex; 
        sy = sizey;
        matrix = new double[sx * sy]; 
    }

    public DObject(PointF vpoint)
    {
        sx = 3;
        sy = 1;
        matrix = new double[3];
        SetLine(0, vpoint.X, vpoint.Y, 1);
    }
    public DObject(string filepath) { fread(filepath); }
    public DObject(DObject dobj) { clone(dobj); }

    /*** getWidth() ***/
    public int getWidth() { return sx; }
    /*** getHeight() ***/
    public int getHeight() { return sy; }
    /***  Создать копию объекта ***/
    public void clone(DObject dobj)
    {
        if ((sx * sy) != (dobj.sx * dobj.sy)) matrix = new double[dobj.sx * dobj.sy];
        sx = dobj.sx;
        sy = dobj.sy;
        for (int i = 0; i < sx*sy; i++)
        {
            matrix[i] = dobj.matrix[i];
        }
    }

    /*** Запись значения value по zero-based index x, y ***/
    public bool set(int x, int y, double value)
    {
        if (x >= sx || y >= sy || x<0 || y<0) return false;
        matrix[x + sx*y] = value;
        return true;
    }

    /*** Получение значения value по zero-based index x, y ***/
    public double get(int x, int y)
    {
        if (x >= sx || y >= sy || x<0 || y<0) return 0;
        return matrix[x + sx*y]; 
    }

    /*** Создание объекта из файла ***/
    public bool fread(string path)
    {
            // Считали файл, убрали пробелы, разбили на массив построчно
        System.IO.StreamReader fs = new System.IO.StreamReader(path);
        string content = fs.ReadToEnd().Replace(".", ",");
        while (content != content.Replace("  ", " ")) content = content.Replace("  ", " ");
        while (content != content.Replace("\t", " ")) content = content.Replace("\t", " ");
        string[] strs = content.Split("\r\n".ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries);
        fs.Close();
            // Создали возвращаемый объект, заполнили поля размера по х и у
        DObject ret = new DObject();
        ret.sy = strs.Length;
        ret.sx = 0;
            // создали матрицу для хранения значений массива в строках
        string[][] ss = new string[ret.sy][];
        int i = 0;
        System.Array.ForEach<string>(strs, delegate(string str)
        {
            ss[i] = str.Split(" ".ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries);
            if (ret.sx < ss[i].Length) ret.sx = ss[i].Length;
            i++;
        });
            //Заполнили ret.matrix и вернули true
        ret = new DObject(ret.sx, ret.sy);
        for (i = 0; i < ret.sy; i++)
        {
            for (int j = 0; j < ret.sx; j++)
            {
                ret.set(j, i, double.Parse(ss[i][j]));
            }
        }
        clone(ret);
        return true;

    }

    /*** Умножение на матрицу dobj ***/
    public DObject Mult(DObject dobj)
    {
        if ((sx == 0) || (sx != dobj.sy)) return null;
        DObject ret = new DObject(dobj.sx, sy);
        for (int i = 0; i < dobj.sx; i++)
        {
            for (int j = 0; j < sy; j++)
            {
                double r = 0;
                for (int k = 0; k < sx; k++)
                {
                    r += get(k, j) * dobj.get(i, k);
                }
                ret.set(i, j, r);
            }
        }
        return ret;
    }
    /*** Приведение матрицы вида {X,Y,Z} к виду {X/Z, Y/Z, 1} ***/
    public bool Normalize()
    {
        try
        {
            DObject ret = new DObject(this);
            for (int i = 0; i < sy; i++)
            {
                double n = get(sx - 1, i);
                if (n == 1) continue;
                else ret.set(sx - 1, i, 1);
                for (int j = 0; j < sx - 1; j++) ret.set(j, i, get(j, i) / n);
            }
            clone(ret);
            return true;
        }
        catch
        {
            return false;
        }
    }
    /*** Запись строки матрицы ***/
    public DObject SetLine(int n, params double[] pars)
    {
        try
        {
            if (n >= sy) return null;
            for (int i = 0; i < sx; i++)
            {
                double v = 1;
                if (i < pars.Length) v = pars[i];
                set(i, n, v);
            }
            return this;
        }
        catch
        {
            return null;
        }
    }

    /*** Сложение двух матриц ***/
    public DObject Add(DObject dobj)
    {
        if (sx != dobj.sx || sy != dobj.sy) return null;
        DObject ret = new DObject(sx, sy);
        for (int i = 0; i < matrix.Length; i++)
        {
            ret.matrix[i] = matrix[i] + dobj.matrix[i];
        }
        return ret;
    }

    /*** Сложение строки с номером n и строки, представленной в параметрах ***/
    public void AddLine(int n, params double[] pars)
    {
        if (pars.Length != sx) return;
        for (int i = 0; i < sx; i++)
        {
            matrix[n * sx + i] += pars[i];
        }
    }
    
    /// <summary>
    /// Инициализация матрицы как линейной со значением value
    /// </summary>
    public void SetToLinear(double value)
    {
        if (sx != sy) return;
        matrix.Initialize();
        for (int i = 0; i < sx; i++)
        {
            set(i, i, value);
        }
    }

    /// <summary>
    /// Подсчет определителя
    /// </summary>
    public double Det()
    {
        if (sx != sy) return 0;
        double ret = 1;
        for (int i = 0; i < sy; i++)
        {
            int k = i;
            while (get(i, i) == 0)
            {
                if (!swapLineInner(i, k++)) return 0;
                else ret *= -1;
            }
            ret *= get(i, i);
            multLineInner(i, 1 / get(i, i));
            for (int j = 0; j < sy; j++)
            {
                if (i == j) continue;
                addLineInner(j, i, -get(i, j));
            }
        }
        return ret;
    }

    /// <summary>
    /// Получение обратной матрицы
    /// </summary>
    public DObject Invert()
    {
        if (sx != sy) return null;
        DObject ret = new DObject(sx, sy);
        for (int i = 0; i < sx; i++)
        {
            DObject dmx = new DObject(sx + 1, sy);
            for (int i0 = 0; i0 < sy; i0++)
            {
                for (int j0 = 0; j0 < sx; j0++) dmx.set(j0, i0, get(j0, i0));
                dmx.set(sx, i0, (i == i0 ? 1 : 0));
            }
            DObject res = dmx.SLAU();
            for (int j = 0; j < sy; j++) ret.set(i, j, res.get(j, 0));
        }
        return ret;
    }
    
    /// <summary>
    /// Транспонирование
    /// </summary>
    public DObject Transpon()
    {
        DObject ret = new DObject(sy, sx);
        for (int i = 0; i < sx; i++)
        {
            for (int j = 0; j < sy; j++)
            {
                ret.set(j, i, get(i, j));
            }
        }
        return ret;
    }

    /// <summary>
    /// Решение системы линейных алгебраических уравнений
    /// </summary>
    public DObject SLAU()
    {
        if (sx != sy + 1) return null;
        DObject ret = new DObject(sy, 1); 
        for (int i = 0; i < sy; i++)
        {
            int k = i;
            while (get(i, i) == 0)
            {
                if (!swapLineInner(i, k++)) return null;
            }
            multLineInner(i, 1/get(i,i));
            for (int j = 0; j < sy; j++)
            {
                if (i == j) continue;
                addLineInner(j, i, -get(i, j));
            }
        }
        for (int i = 0; i < sx; i++) ret.set(i, 0, get(sx-1, i));
        return ret;
    }

    /*** Приведение к строке ***/
    override public string ToString()
    {
        string ret = "";
        for (int i = 0; i < sy; i++)
        {
            for (int j = 0; j < sx; j++)
            {
                ret += String.Format("{0, 5:N2} ", get(j, i));
            }
            ret += "\r\n";
        }
        return ret;
    }
}