using Esercitazione03Aprile.Models;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace Esercitazione03Aprile.Repository
{
    public class ProdottoRepository : IRepository<Prodotto>
    {
        private static ProdottoRepository istanza;

        public static ProdottoRepository GetIstanza()
        {
            if (istanza == null)
                istanza = new ProdottoRepository();

            return istanza;
        }

        private ProdottoRepository() { }

        public bool Insert(Prodotto t)
        {
            bool res = false;

            using (var ctx = new Es03AprileContext())
            {
                try
                {
                    if (AggiornaQuantita(t))
                        return true;

                    ctx.Add(t);
                    ctx.SaveChanges();
                    res = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return res;
        }

        public bool Update(Prodotto t)
        {
            bool res = false;
            using (var ctx = new Es03AprileContext())
            {
                try
                {
                    Prodotto temp = ctx.Prodottos.FirstOrDefault(temp => temp.Codice == t.Codice);

                    t.ProdottoId=temp.ProdottoId;
                    t.Nome = t.Nome is not null ? t.Nome : temp.Nome;
                    t.Descrizione = t.Descrizione is not null ? t.Descrizione  : temp.Descrizione;
                    t.Prezzo = t.Prezzo == 0 ? t.Prezzo : temp.Prezzo;
                    t.Quantita = t.Quantita ==0 ? t.Quantita : temp.Quantita;
                    t.Categoria = t.Categoria is not null ? t.Categoria : temp.Categoria;
                    t.DataCreazione = t.DataCreazione is not null ? t.DataCreazione : temp.DataCreazione;

                    ctx.Entry(temp).CurrentValues.SetValues(t);
                    ctx.SaveChanges();

                    res = true;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return res;
        }

        //public bool DeleteById(int id)
        //{
        //    bool res = false;

        //    using (var ctx = new Es03AprileContext())
        //    {
        //        try
        //        {
        //            Prodotto p = ctx.Prodottos.First(p => p.ProdottoId == id);
        //            ctx.Prodottos.Remove(p);
        //            ctx.SaveChanges();
        //            res = true;
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //        }
        //    }
        //    return res;
        //}

        public bool DeleteByCodice(string codice)
        {
            bool res = false;

            using (var ctx = new Es03AprileContext())
            {
                try
                {
                    Prodotto p = ctx.Prodottos.First(p => p.Codice == codice);
                    ctx.Prodottos.Remove(p);
                    ctx.SaveChanges();
                    res = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return res;
        }

        public List<Prodotto> GetAll()
        {
            List<Prodotto> p = new List<Prodotto>();
            using (var ctx = new Es03AprileContext())
            {
                p = ctx.Prodottos.ToList();
            }
            return p;
        }

        public Prodotto GetById(int id)
        {
            Prodotto p;
            using (var ctx = new Es03AprileContext())
            {
                p = ctx.Prodottos.First(p => p.ProdottoId == id);
            }
            return p;
        }

        public Prodotto GetByCodice(string codice)
        {
            Prodotto p;

            using (var ctx = new Es03AprileContext())
            {

                p = ctx.Prodottos.First(p => p.Codice == codice);
            }

            return p;
        }

        public bool AggiornaQuantita(Prodotto t)
        {
            bool res = false;
            using (var ctx = new Es03AprileContext())
            {
                try
                {
                    List<Prodotto> prodottos = ctx.Prodottos.ToList();

                    foreach (Prodotto p in prodottos)
                    {
                        if (p.Categoria == t.Categoria && p.Nome == t.Nome)
                        {
                            t.ProdottoId = p.ProdottoId;
                            t.Codice = p.Codice;
                            t.Descrizione = p.Descrizione;
                            t.Prezzo = p.Prezzo;
                            t.Quantita = p.Quantita + 1;
                            t.DataCreazione = p.DataCreazione;

                            ctx.Entry(p).CurrentValues.SetValues(t);
                            ctx.SaveChanges();

                            res = true;
                        }
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return res;
        }

        public List<Prodotto> RicercaGenerica (string x)
        {
            List<Prodotto> ricerca = new List<Prodotto>();

            using (var ctx = new Es03AprileContext())
            {
                List<Prodotto> tutti = ctx.Prodottos.ToList();

                foreach(Prodotto p in tutti)
                {
                    if (p.Nome.Contains(x))
                        ricerca.Add(p);
                    else if (p.Descrizione.Contains(x))
                        ricerca.Add(p);
                    else if (p.Categoria.Contains(x))
                        ricerca.Add(p);
                    else if (p.Prezzo.ToString().Contains(x)) //non trovo la funzione isNan
                        ricerca.Add(p);
                    else if (p.Quantita.ToString().Contains(x)) //non trovo la funzione isNan
                        ricerca.Add(p);
                    else if (p.DataCreazione.ToString().Contains(x))
                        ricerca.Add(p);
                }
            }

            return ricerca;
        }

        public bool PiuQuantita (string codice)
        {
            bool res = false;

            using (var ctx = new Es03AprileContext())
            {
                try
                {
                    Prodotto p = ctx.Prodottos.First(p=> p.Codice == codice);
                    p.Quantita++;

                    ctx.Entry(p).Property(p=> p.Codice).IsModified= true;
                    ctx.SaveChanges();
                    res = true;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return res;
        }

        public bool MenoQuantita(string codice)
        {
            bool res = false;

            using (var ctx = new Es03AprileContext())
            {
                try
                {
                    Prodotto p = ctx.Prodottos.First(p => p.Codice == codice);
                    p.Quantita--;

                    ctx.Entry(p).Property(p => p.Codice).IsModified = true;
                    ctx.SaveChanges();
                    res = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return res;
        }

    }
}
