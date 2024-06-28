using System;
using System.Collections.Generic;

public class Prostokat
{
    private double bokA;
    private double bokB;

    public Prostokat(double bokA, double bokB)
    {
        BokA = bokA;
        BokB = bokB;
    }

    public double BokA
    {
        get { return bokA; }
        set
        {
            if (!IsValidDimension(value))
                throw new ArgumentException("BokA musi być skończoną nieujemną liczbą.");
            bokA = value;
        }
    }

    public double BokB
    {
        get { return bokB; }
        set
        {
            if (!IsValidDimension(value))
                throw new ArgumentException("BokB musi być skończoną nieujemną liczbą.");
            bokB = value;
        }
    }

    private static readonly Dictionary<char, decimal> wysokośćArkusza0 = new Dictionary<char, decimal>
    {
        ['A'] = 1189m,
        ['B'] = 1414m,
        ['C'] = 1297m
    };

    private static readonly double pierwiastekZDwóch = Math.Sqrt(2);

    public static Prostokat ArkuszPapieru(string format)
    {
        if (string.IsNullOrWhiteSpace(format) || format.Length < 2)
            throw new ArgumentException("Format musi mieć przynajmniej 2 znaki.");

        char X = format[0];
        if (!wysokośćArkusza0.ContainsKey(X))
            throw new ArgumentException($"Nieznany format: {X}");

        if (!byte.TryParse(format.Substring(1), out byte i))
            throw new ArgumentException("Niepoprawny indeks w formacie.");

        decimal bazowaWysokość = wysokośćArkusza0[X];
        double bokA = (double)(bazowaWysokość / (decimal)Math.Pow(pierwiastekZDwóch, i));
        double bokB = bokA / pierwiastekZDwóch;

        return new Prostokat(bokA, bokB);
    }

    private static bool IsValidDimension(double value)
    {
        return !double.IsInfinity(value) && !double.IsNaN(value) && value >= 0;
    }
}