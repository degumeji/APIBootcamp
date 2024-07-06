namespace ejemploEntity.Utilitarios
{
    public class ControlError
    {
        public void LogErrorMetodos(string error, string metodo)
        {
            var ruta = string.Empty;
            var archivo = string.Empty;
            DateTime Fecha = DateTime.Now;

            try
            {
                ruta = "C:\\ProyectoIntegrador\\logs";
                archivo = $"Log_{Fecha.ToString("dd-MM-yyyy")}.txt";

                if (!Directory.Exists(ruta))
                {
                    Directory.CreateDirectory(ruta);
                }

                //StreamWriter writ = new StreamWriter($"{ruta}\\{archivo}");
                //writ.WriteLine($"Se presento una novedad en el metodo: {metodo}, con el siguiente error: {error}");
                //writ.Close();
                var mensaje = $"\nSe presento una novedad en el metodo: {metodo}, con el siguiente error: {error}";
                //File.WriteAllText(Path.Combine(ruta,archivo), mensaje);

                File.AppendAllText(Path.Combine(ruta, archivo), mensaje);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error desde controlError: ", ex.Message);
            }
        }
    }
}
