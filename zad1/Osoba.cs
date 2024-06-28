using System;

public class Osoba
{
    private string imie;
    private string nazwisko;

    public Osoba(string pelneImieNazwisko)
    {
        PelneImieNazwisko = pelneImieNazwisko;
    }
    public string Imie
    {
        get { return imie; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Imie nie moze byc puste");
            imie = value;
        }
    }

    public string Nazwisko
    {
        get { return nazwisko; }
        set { nazwisko = value; }
    }

    public DateTime? DataUrodzenia { get; set; } = null;
    public DateTime? DataSmierci { get; set; } = null;

    public string PelneImieNazwisko
    {
        get { return $"{Imie} {Nazwisko}".Trim(); }
        set
        {
            var czesci = value.Split(' ');
            Imie = czesci[0];
            Nazwisko = czesci.Length > 1 ? czesci[^1] : string.Empty;
        }
    }

    public TimeSpan? Wiek
    {
        get
        {
            if (DataUrodzenia == null)
                return null;

            DateTime dataKoncowa = DataSmierci ?? DateTime.Now;
            return dataKoncowa - DataUrodzenia.Value;
        }
    }
}
