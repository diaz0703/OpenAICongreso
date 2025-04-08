using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatCommun
{
    internal class Tools
    {
        public static List<hotelModel> RecogeArch()
        {
            string filePath = "HotelData.txt"; // Cambia esto por la ruta de tu archivo
            List<hotelModel> personas = new List<hotelModel>();
            int _conteo = 0;
            bool _conencabezado = true;
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (_conencabezado && _conteo == 0)
                        {
                            _conencabezado = false;
                            continue;
                        }
                        _conteo++;

                        string[] parts = line.Split('\t');
                        if (parts.Length == 11) // Asegúrate de que haya 11 partes
                        {
                            hotelModel persona = new hotelModel
                            {
                                HotelId = parts[0].ToString(),
                                HotelName = parts[1],
                                Description = parts[2].ToString(),
                                DescriptionFr = parts[3].ToString(),
                                Category = parts[4].ToString(),
                                Tags = parts[5].ToString().Split(" "),
                                ParkingIncluded = parts[6].ToString() == "1",
                                Rating = parts[10] == "" ? null : double.Parse(parts[9])
                            };
                            personas.Add(persona);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al leer el archivo: " + e.Message);
            }
            return personas;
        }
    }
}
