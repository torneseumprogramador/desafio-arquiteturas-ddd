using System;
using System.Text.RegularExpressions;

namespace EcommerceDDD.Domain.ValueObjects
{
    public class CPF
    {
        public string Value { get; private set; }

        public CPF(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("CPF não pode ser vazio", nameof(value));

            var cleanValue = RemoveFormatting(value);
            
            if (!IsValid(cleanValue))
                throw new ArgumentException("CPF inválido", nameof(value));

            Value = cleanValue;
        }

        private CPF() { } // Para EF Core

        public static bool IsValid(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            cpf = RemoveFormatting(cpf);

            if (cpf.Length != 11)
                return false;

            // Verifica se todos os dígitos são iguais
            if (cpf.All(c => c == cpf[0]))
                return false;

            // Calcula o primeiro dígito verificador
            int sum = 0;
            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(cpf[i].ToString()) * (10 - i);
            }

            int remainder = sum % 11;
            int digit1 = remainder < 2 ? 0 : 11 - remainder;

            if (int.Parse(cpf[9].ToString()) != digit1)
                return false;

            // Calcula o segundo dígito verificador
            sum = 0;
            for (int i = 0; i < 10; i++)
            {
                sum += int.Parse(cpf[i].ToString()) * (11 - i);
            }

            remainder = sum % 11;
            int digit2 = remainder < 2 ? 0 : 11 - remainder;

            return int.Parse(cpf[10].ToString()) == digit2;
        }

        private static string RemoveFormatting(string cpf)
        {
            return Regex.Replace(cpf, @"[^\d]", "");
        }

        public string GetFormatted()
        {
            return $"{Value.Substring(0, 3)}.{Value.Substring(3, 3)}.{Value.Substring(6, 3)}-{Value.Substring(9, 2)}";
        }

        public override string ToString()
        {
            return GetFormatted();
        }

        public override bool Equals(object obj)
        {
            if (obj is CPF other)
                return Value == other.Value;
            return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(CPF left, CPF right)
        {
            if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
                return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
                return false;
            return left.Equals(right);
        }

        public static bool operator !=(CPF left, CPF right)
        {
            return !(left == right);
        }
    }
} 