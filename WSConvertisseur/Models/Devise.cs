using System.ComponentModel.DataAnnotations;
using System.Data.Common;

namespace WSConvertisseur.Models
{
    public class Devise
    {
		private int id;

		public int Id
		{
			get { return id; }
			set { id = value; }
		}

		private string? nomDevise;
		[Required]
		public string? NomDevise
        {
			get { return nomDevise; }
			set { nomDevise = value; }
		}

		private double taux;

		public double Taux
		{
			get { return taux; }
			set { taux = value; }
		}


		public Devise()
		{

		}

        public Devise(int id, string? nomDevise, double taux)
        {
            this.Id = id;
            this.NomDevise = nomDevise;
            this.Taux = taux;
        }

        public override bool Equals(object? obj)
        {
            return obj is Devise devise &&
                   Id == devise.Id &&
                   NomDevise == devise.NomDevise &&
                   Taux == devise.Taux;
        }
    }
}
