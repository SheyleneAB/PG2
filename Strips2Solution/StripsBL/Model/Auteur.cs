﻿using StripsBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripsBL.Model
{
    public class Auteur
    {
        private string naam;
        private string? email;
        public Auteur() { }

        public string Naam
        {
            get { return naam; }
            set { if (string.IsNullOrWhiteSpace(value)) throw new DomeinException("SetNaam"); naam = value; }
        }
        public string? Email
        {
            get { return email; }
            set {  email = value; }
        }
        public int? Id;

        public Auteur( string naam)
        {
            Naam = naam;
        }
        public Auteur( int? id, string naam, string? email)
        {
            Id = id;
            Naam = naam;
            Email = email;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Auteur)
            {
                Auteur compAuteur=(Auteur)obj;
                if (Id.HasValue && compAuteur.Id.HasValue)
                {
                    if (Id==compAuteur.Id) return true; else return false;
                }
                else
                {
                    return naam == compAuteur.Naam && email == compAuteur.Email;
                }
            }
            else return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(naam, email, Id);
        }
    }
}
