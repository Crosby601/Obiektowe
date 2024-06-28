using System;
using System.Linq;

public class Vector
{
    private double[] współrzędne;
    public Vector(byte wymiar)
    {
        if (wymiar < 1)
            throw new ArgumentException("Wymiar musi być większy niż 0.");
        współrzędne = new double[wymiar];
    }

    public Vector(params double[] współrzędne)
    {
        if (współrzędne == null || współrzędne.Length == 0)
            throw new ArgumentException("Vector musi mieć przynajmniej jedną współrzędną.");
        this.współrzędne = (double[])współrzędne.Clone();
    }

    public double Długość
    {
        get { return Math.Sqrt(IloczynSkalarny(this, this)); }
    }

    public byte Wymiar
    {
        get { return (byte)współrzędne.Length; }
    }

    public double this[byte index]
    {
        get
        {
            if (index >= Wymiar)
                throw new IndexOutOfRangeException("Indeks poza zakresem.");
            return współrzędne[index];
        }
        set
        {
            if (index >= Wymiar)
                throw new IndexOutOfRangeException("Indeks poza zakresem.");
            współrzędne[index] = value;
        }
    }

    public static double IloczynSkalarny(Vector V, Vector W)
    {
        if (V.Wymiar != W.Wymiar)
            return double.NaN;
        return V.współrzędne.Zip(W.współrzędne, (v, w) => v * w).Sum();
    }

    public static Vector Suma(params Vector[] Vectory)
    {
        if (Vectory == null || Vectory.Length == 0)
            throw new ArgumentException("Musi być przynajmniej jeden wektor.");
        byte wymiar = Vectory[0].Wymiar;
        if (Vectory.Any(Vector => Vector.Wymiar != wymiar))
            throw new ArgumentException("Wszystkie wektory muszą mieć ten sam wymiar.");
        double[] suma = new double[wymiar];
        foreach (var Vector in Vectory)
        {
            for (int i = 0; i < wymiar; i++)
            {
                suma[i] += Vector.współrzędne[i];
            }
        }
        return new Vector(suma);
    }

    public static Vector operator +(Vector V, Vector W)
    {
        return Suma(V, W);
    }

    public static Vector operator -(Vector V, Vector W)
    {
        if (V.Wymiar != W.Wymiar)
            throw new ArgumentException("Vectory muszą mieć ten sam wymiar.");
        double[] różnica = new double[V.Wymiar];
        for (int i = 0; i < V.Wymiar; i++)
        {
            różnica[i] = V.współrzędne[i] - W.współrzędne[i];
        }
        return new Vector(różnica);
    }

    public static Vector operator *(Vector V, double skalar)
    {
        double[] wynik = new double[V.Wymiar];
        for (int i = 0; i < V.Wymiar; i++)
        {
            wynik[i] = V.współrzędne[i] * skalar;
        }
        return new Vector(wynik);
    }

    public static Vector operator *(double skalar, Vector V)
    {
        return V * skalar;
    }

    public static Vector operator /(Vector V, double skalar)
    {
        if (skalar == 0)
            throw new DivideByZeroException("Skalar nie może być zerem.");
        double[] wynik = new double[V.Wymiar];
        for (int i = 0; i < V.Wymiar; i++)
        {
            wynik[i] = V.współrzędne[i] / skalar;
        }
        return new Vector(wynik);
    }
}