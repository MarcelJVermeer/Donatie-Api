using System.ComponentModel.DataAnnotations;
using Donatie.Applicatie.DTOs;

namespace Donatie.API.Repositories
{
    public class DonatieRepository
    {
        List<DonatieEntity> donations;
        public DonatieRepository()
        {
            donations = new List<DonatieEntity>();
            donations.Add(new DonatieEntity()
            {
                ID = 1,
                Afzender = "Jan de Vries",
                Bedrag = 65,
                Datum = DateTime.Now.AddDays(-10)
            });
            donations.Add(new DonatieEntity()
            {
                ID = 2,
                Afzender = "Gerrit Jansen",
                Bedrag = 75,
                Datum = DateTime.Now.AddDays(-9)
            });
            donations.Add(new DonatieEntity()
            {
                ID = 3,
                Afzender = "Piet de Vries",
                Bedrag = 80,
                Datum = DateTime.Now.AddDays(-5)
            });
            donations.Add(new DonatieEntity()
            {
                ID = 4,
                Afzender = "Kees Pietersen",
                Bedrag = 85,
                Datum = DateTime.Now.AddDays(-2)
            });
        }

        /// <summary>
        /// Geeft alle donaties die bewaard zijn
        /// </summary>
        /// <returns>Lijst van Donaties(Bedrag en Afzender)</returns>
        public IEnumerable<DonatieItem> GeefAlleDonaties()
        {
            return donations.Select(donation =>
            new DonatieItem()
            {
                ID = donation.ID,
                Bedrag = donation.Bedrag,
                Afzender = donation.Afzender
            });
        }

        /// <summary>
        /// Zoek naar alle donaties van de betreffende afzender. Er wordt gezocht met lowercase en in gedeelte van een afzender
        /// </summary>
        /// <param name="afzender">Gedeelte van de afzendernaam</param>
        /// <returns>Lijst van Donaties(Bedrag en Afzender)</returns>
        public IEnumerable<DonatieItem> ZoekDonaties(string afzender)
        {
            return donations.Where(n => n.Afzender.ToLower().Contains(afzender.ToLower())).Select(donation =>
            new DonatieItem()
            {
                ID = donation.ID,
                Bedrag = donation.Bedrag,
                Afzender = donation.Afzender
            });
        }

        /// <summary>
        /// Geeft aan de hand van een Id de netreffende donatie terug.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>Geeft null als de Donatie niet gevonden is</returns>
        public GeefDonatie? GeefDonatie(int ID)
        {
            var donatie = donations.SingleOrDefault(n => n.ID == ID);
            if (donatie == null)
                return null;
            return new GeefDonatie()
            {
                ID = ID,
                Afzender = donatie.Afzender,
                Bedrag = donatie.Bedrag,
                Datum = donatie.Datum
            };

        }

        /// <summary>
        /// Maakt een nieuwe donatie en genereert een nieuwe id
        /// </summary>
        /// <param name="donatie">De te bewaren gegevens</param>
        /// <returns>De gegenereerde id</returns>
        public int CreateDonatie(MaakDonatie donatie)
        {
            int nieuwId = donations.Max(n => n.ID) + 1;
            donations.Add(new DonatieEntity()
            {
                ID = nieuwId,
                Afzender = donatie.Afzender,
                Bedrag = donatie.Bedrag,
                Datum = DateTime.Now
            });
            return nieuwId;
        }

        /// <summary>
        /// Update een donatie mbv de id's
        /// </summary>
        /// <param name="ID">ID uit de route</param>
        /// <param name="donatie">De gewijzigde gegevens</param>
        /// <exception cref="ValidationException">Geeft een exceptie wanneer de id's niet gelijk zijn</exception>
        /// <exception cref="Exception">Geeft een exceptie wanneer de donatie niet gevonden is</exception>
        public void UpdateDonatie(int ID, UpdateDonatie donatie)
        {
            if (ID != donatie.ID)
                throw new ValidationException("Ids zijn niet gelijk");
            var donatieEntity = donations.SingleOrDefault(n => n.ID == ID);
            if (donatieEntity == null)
                throw new Exception("Geen Donatie gevonden met gegeven ID");

            donatieEntity.Datum = donatie.Datum;
            donatieEntity.Afzender = donatie.Afzender;
            donatieEntity.Bedrag = donatie.Bedrag;
        }

        /// <summary>
        /// Verwijderd de donatie mbv een id
        /// </summary>
        /// <param name="ID"></param>
        /// <exception cref="Exception">Deze fout wordt gegeven als de donatie niet gevonden is</exception>
        public void DeleteDonatie(int ID)
        {
            var donatie = donations.Find(n => n.ID == ID);
            if (donatie == null)
                throw new Exception("Geen donatie gevonden");

            donations.Remove(donatie);
        }
    }

    class DonatieEntity
    {
        public int ID { get; set; }
        public double Bedrag { get; set; }
        public string Afzender { get; set; }
        public DateTime Datum { get; set; }
    }

}

