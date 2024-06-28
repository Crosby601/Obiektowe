using System;
using System.Collections.Generic;

public class Matrix<T> : IEquatable<Matrix<T>>
{
    private readonly T[,] tablica;

    public Matrix(int wiersze, int kolumny)
    {
        if (wiersze <= 0 || kolumny <= 0)
        {
            throw new ArgumentException("Wymiary macierzy muszą być większe od zera.");
        }

        tablica = new T[wiersze, kolumny];
    }

    public T this[int wiersz, int kolumna]
    {
        get { return tablica[wiersz, kolumna]; }
        set { tablica[wiersz, kolumna] = value; }
    }

    public int Wiersze => tablica.GetLength(0);
    public int Kolumny => tablica.GetLength(1);

    public static bool operator ==(Matrix<T> lewa, Matrix<T> prawa)
    {
        if (ReferenceEquals(lewa, prawa)) return true;
        if (ReferenceEquals(lewa, null)) return false;
        if (ReferenceEquals(prawa, null)) return false;

        if (lewa.Wiersze != prawa.Wiersze || lewa.Kolumny != prawa.Kolumny) return false;

        for (int i = 0; i < lewa.Wiersze; i++)
        {
            for (int j = 0; j < lewa.Kolumny; j++)
            {
                if (!EqualityComparer<T>.Default.Equals(lewa[i, j], prawa[i, j]))
                {
                    return false;
                }
            }
        }
        return true;
    }

    public static bool operator !=(Matrix<T> lewa, Matrix<T> prawa)
    {
        return !(lewa == prawa);
    }

    public bool Equals(Matrix<T> other)
    {
        return this == other;
    }

    public override bool Equals(object obj)
    {
        if (obj is Matrix<T> other)
        {
            return Equals(other);
        }
        return false;
    }

    public override int GetHashCode()
    {
        int hash = 17;
        foreach (var item in tablica)
        {
            hash = hash * 31 + (item == null ? 0 : item.GetHashCode());
        }
        return hash;
    }
}