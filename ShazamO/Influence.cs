using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShazamO
{
    /// <summary>
    /// Обеспечивает абстракцию конкуренции нескольких партнеров внутри 
    /// некоторой дискретной области, в зависимости от влияния партнера
    /// и допустимого отклонения границ.
    /// </summary>
    public class InfluenceArea : IEnumerable<KeyValuePair<Interval, object>>
    {
        /// <summary>
        /// Зона влияния
        /// </summary>
        int[] area;

        /// <summary>
        /// Конкуренты. Нулевой конкурент - natives.
        /// </summary>
        public List<object> Partners = new List<object>();

        List<float> influences = new List<float>();

        /// <summary>
        /// Расстояние, на котором границы влияния могут пересекаться. 
        /// Наложения считаются в пользу более весомого конкурента.
        /// </summary>
        public static int BorderDeviation = 16;

        /// <summary>
        /// Инициализирует зону влияния длиной Length
        /// </summary>
        public InfluenceArea(int Length)
        {
            area = new int[Length];
            Partners.Add(new object());
            influences.Add(0);
        }

        /// <summary>
        /// Добавить нового конкурента с претензиями на определенную зону
        /// </summary>
        public bool AddInfluence(object Partner, int Begin, int Length, float Influence)
        {
            Partners.Add(Partner);
            if (float.IsNaN(Influence)) Influence = float.MaxValue;
            influences.Add(Influence);
            int index = Partners.IndexOf(Partner);

            int[] _area = new int[Length];
            int partner = 0, lastZone = 0;
            for (int i = 0, x = Begin; i < Length; i++, x++)
            {
                if (area[x] == 0)                       // Ничейная земля
                {
                    partner = 0;
                    _area[i] = index;
                    continue;
                }
                
                if (partner != area[x])                 // Земля занята кем-то еще
                {
                    partner = area[x];
                    lastZone = 0;
                }
                bool IsPowerful = influences[partner] < Influence;      // Кто сильнее 

                if (lastZone < BorderDeviation)            // Земля слишком мала для споров
                {
                    if (IsPowerful) _area[i] = index;   // Захват зоны
                    else _area[i] = partner;            // Отдаем землю
                }
                else
                {
                    if (IsPowerful) _area[i] = index;   // Захват зоны
                    else return false;                  // Земля занята, и нам здесь не рады
                }
                lastZone++;
            }
            for (int i = 0, x = Begin; i < Length; i++, x++) area[x] = _area[i];    //Все проблемы улажены
            return true;
        }

        /// <summary>
        /// Энумератор представляет собой множество именованных интервалов, 
        /// показывающих распределенные области влияния исключая ничейные
        /// зоны.
        /// </summary>
        public IEnumerator<KeyValuePair<Interval, object>> GetEnumerator()
        {
            for (int i = 1, len = 1; i < area.Length; i++, len++)
            {
                if (area[i] != area[i - 1])
                {
                    var value = new KeyValuePair<Interval, object>(
                                        new Interval(i - len, len),
                                        Partners[area[i - 1]]);
                    len = 0;
                    if (area[i - 1] != 0) yield return value;
                }
            }

        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    /// <summary>
    /// Интервал, хранит начало и конец в виде Int32.
    /// </summary>
    public struct Interval
    {
        public int Begin;
        public int End;
        public int Length { get { return End - Begin; } }

        public Interval(int Begin, int Length)
        {
            this.Begin = Begin;
            this.End = Begin + Length;
        }
    }

    /// <summary>
    /// Совпадения. Служебный класс для поиска соответствий.
    /// </summary>
    public struct Congruence
    {
        public Signature SignLink;
        public float SignSigma;
        public float Coord;
        public float Value;

        public Congruence(PointF Coords, Signature Sign, float Sigma)
        {
            SignSigma = Sigma;
            Coord = Coords.X;
            Value = Coords.Y;
            SignLink = Sign;
        }

        public int Begin { get { return (int)Coord; } }

        public int End { get { return Begin + Length; } }

        public int Length { get { return SignLink.Data.Length; } }
    }
}
