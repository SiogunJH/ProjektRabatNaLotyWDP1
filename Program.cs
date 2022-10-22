namespace System
{
    class Program
    {
        static void Main()
        {
            //TESTFIELD

            //DATA AKTUALNA
            DateTime currentDate = DateTime.Now;

            //DATA URODZIN
            Console.Write("\nData urodzenia pasażera (YYYY-MM-DD): ");
            string input = Console.ReadLine();
            DateTime dateOfBirth = new DateTime(int.Parse(input.Split('-')[0]), int.Parse(input.Split('-')[1]), int.Parse(input.Split('-')[2]));
            if (dateOfBirth > currentDate)
            {
                Console.WriteLine("BŁĄD: Data urodzenia nie może być umiejscowiona w przyszłości");
                return;
            }

            //DATA LOTU
            Console.Write("Data odbycia się lotu (YYYY-MM-DD): ");
            input = Console.ReadLine();
            DateTime dateOfFlight = new DateTime(int.Parse(input.Split('-')[0]), int.Parse(input.Split('-')[1]), int.Parse(input.Split('-')[2]));
            if (dateOfFlight < currentDate)
            {
                Console.WriteLine("BŁĄD: Data lotu nie może być umiejscowiona w przeszłości");
                return;
            }

            //TYP LOTU
            Console.Write("Czy zarezerwowany lot jest lotem międzynarodowym (tak/nie): ");
            input = Console.ReadLine();
            bool isInternational = false;
            if (input.ToLower().StartsWith('y') || input.ToLower().StartsWith('t'))
                isInternational = true;

            //TYP KLIENTA
            bool isRegular = false;
            if (dateOfBirth.AddYears(18) <= currentDate)
            {
                Console.Write("Czy pasażer jest stałym klientem (tak/nie): ");
                input = Console.ReadLine();
                if (input.ToLower().StartsWith('y') || input.ToLower().StartsWith('t'))
                    isRegular = true;
            }

            //SEZONOWOŚĆ
            bool isSeasonal;
            if (
                (dateOfFlight.Month == 7 || dateOfFlight.Month == 8) ||
                (dateOfFlight > new DateTime(dateOfFlight.Year, 12, 20) || dateOfFlight < new DateTime(dateOfFlight.Year, 1, 10)) ||
                (dateOfFlight > new DateTime(dateOfFlight.Year, 3, 20) && dateOfFlight < new DateTime(dateOfFlight.Year, 4, 10))
                )
                isSeasonal = true;
            else
                isSeasonal = false;
            Console.WriteLine($"Czy lot jest w sezonie: {(isSeasonal ? 't' : 'n')}");

            //WYPRZEDZENIE
            bool isAhead;
            if (currentDate.AddMonths(5) > dateOfFlight)
                isAhead = false;
            else
                isAhead = true;
            Console.WriteLine($"Czy zarezerwowano z wyprzedzeniem: {(isAhead ? 't' : 'n')}\n");


            //DODATKOWE ZMIENNE
            int rabat = 0;
            int rabatMax = 30;

            //ROZLICZ RABATY
            if (dateOfBirth.AddYears(2) > currentDate) //niemowlęcia
            {
                Console.WriteLine("Niemowlę (max 80%)");
                rabatMax = 80;
                if (isInternational)
                {
                    Console.WriteLine("Niemowlę, Międzynarodowy Lot (+70%)");
                    rabat += 70;
                }
                else
                {
                    Console.WriteLine("Niemowlę, Lot Krajowy (+80%)");
                    rabat += 80;
                }
            }
            else if (isInternational && isSeasonal) //sezonowe międzynarodowe
            {
                Console.WriteLine("Międzynarodowy Lot Sezonalny (max 0%)");
                rabatMax = 0;
            }

            if (isInternational && !isSeasonal) //niesezonowe międzynarodowe
            {
                Console.WriteLine("Międzynarodowe Lot Niesezonalny (+15%)");
                rabat += 15;
            }

            if (dateOfBirth.AddYears(2) <= currentDate && dateOfBirth.AddYears(16) > currentDate) //dzieci
            {
                Console.WriteLine("Dziecko do lat 16 (+10%)");
                rabat += 10;
            }
            else if (isRegular) //stali klienci
            {
                Console.WriteLine("Stały Klient (+15%)");
                rabat += 15;
            }

            if (currentDate.AddMonths(5) < dateOfFlight) //wyprzedzenie
            {
                Console.WriteLine("Zakup z Wyprzedzeniem (+10%)");
                rabat += 10;
            }


            //GÓRNA GRANICA RABATU
            if (rabat > rabatMax)
                rabat = rabatMax;

            //PODSUMOWANIE
            Console.WriteLine($"\nNaliczony zostanie rabat w wysokości {rabat}%");
        }
    }
}