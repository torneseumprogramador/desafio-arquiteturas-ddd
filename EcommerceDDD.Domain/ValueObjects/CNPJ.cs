using System;
using System.Text.RegularExpressions;

namespace EcommerceDDD.Domain.ValueObjects
{
    public class CNPJ
    {
        public string Value { get; private set; }

        public CNPJ(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("CNPJ não pode ser vazio", nameof(value));

            var cleanValue = RemoveFormatting(value);
            
            if (!IsValid(cleanValue))
                throw new ArgumentException("CNPJ inválido", nameof(value));

            Value = cleanValue;
        }

        private CNPJ() { } // Para EF Core

        public static bool IsValid(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                return false;

            cnpj = RemoveFormatting(cnpj);

            if (cnpj.Length != 14)
                return false;

            // Verifica se todos os dígitos são iguais
            if (cnpj.All(c => c == cnpj[0]))
                return false;

            // Calcula o primeiro dígito verificador
            int[] weights1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int sum = 0;
            for (int i = 0; i < 12; i++)
            {
                sum += int.Parse(cnpj[i].ToString()) * weights1[i];
            }

            int remainder = sum % 11;
            int digit1 = remainder < 2 ? 0 : 11 - remainder;

            if (int.Parse(cnpj[12].ToString()) != digit1)
                return false;

            // Calcula o segundo dígito verificador
            int[] weights2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            sum = 0;
            for (int i = 0; i < 13; i++)
            {
                sum += int.Parse(cnpj[i].ToString()) * weights2[i];
            }

            remainder = sum % 11;
            int digit2 = remainder < 2 ? 0 : 11 - remainder;

            return int.Parse(cnpj[13].ToString()) == digit2;
        }

        private static string RemoveFormatting(string cnpj)
        {
            return Regex.Replace(cnpj, @"[^\d]", "");
        }

        public string GetFormatted()
        {
            return $"{Value.Substring(0, 2)}.{Value.Substring(2, 3)}.{Value.Substring(5, 3)}/{Value.Substring(8, 4)}-{Value.Substring(12, 2)}";
        }

        public override string ToString()
        {
            return GetFormatted();
        }

        public override bool Equals(object obj)
        {
            if (obj is CNPJ other)
                return Value == other.Value;
            return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(CNPJ left, CNPJ right)
        {
            if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
                return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
                return false;
            return left.Equals(right);
        }

        public static bool operator !=(CNPJ left, CNPJ right)
        {
            return !(left == right);
        }
    }
} 