using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        //aqui se agrega la direccion de la carpeta donde buscara los archivos y con ellos hara un array(arreglo)
        string[] archivos = Directory.GetFiles("D:\\Documentos 2\\UniKino\\Algebra");
        //hara saber al usuario la cantidad de archivos que contiene carpeta
        Console.WriteLine($"Número total de archivos: {archivos.Length}");


        Console.WriteLine("\nProceso Síncrono:");// primero se ejecutara el método síncrono
        var tiempoSincrono = ProcesoSincrono(archivos);

        Console.WriteLine("\nProceso Paralelo:");// despues se ejecutara el método paralelo
        var tiempoParalelo = ProcesoParalelo(archivos);

        CompararTiempos(tiempoSincrono, tiempoParalelo);// y por ultimo se compararán ambostiempos para saber que método fue más rapido de terminar la tarea

        Console.ReadLine();//a la espera de tocar una tecla para cerrar la ventana
    }

    //aqui se codea el proceso sincrono
    static double ProcesoSincrono(string[] archivos)
    {
        //se utiliza stopwatch para contar el tiempo de ejecicion con start y stop
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        //se captura la hora en tiempo real de cuando se inicia el programa sincrono
        Console.WriteLine($"Tiempo de inicio sincrono: {DateTime.Now:HH:mm:ss}");

        //se utiliza un foreach para aplicar la accion por cada uno de los archivos dentro de la carpeta
        foreach (var archivo in archivos)
        {
            // se pide la accion de leer el archivo, incluye descripcion de que archivo se lee y el tiempo que le toma leerlo
            LeerArchivo(archivo);
        }

        stopwatch.Stop();
        //se captura la hora en tiempo real de cuando se termina el programa sincrono
        Console.WriteLine($"Tiempo final sincrono: {DateTime.Now:HH:mm:ss}");
        double tiempoTotal = stopwatch.Elapsed.TotalSeconds;// se guarda en tiempoTotal el tiempo que duro la ejecucion 
        Console.WriteLine($"Tiempo total de procesamiento sincrono: {tiempoTotal} segundos");
        return tiempoTotal;
    }

    //aqui se codea el proceso paralelo
    static double ProcesoParalelo(string[] archivos)
    {
        //se utiliza stopwatch para contar el tiempo de ejecicion con start y stop
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        //se captura la hora en tiempo real de cuando se inicia el programa sincrono
        Console.WriteLine($"Tiempo de inicio paralelo: {DateTime.Now:HH:mm:ss}");

        //se utiliza un parallel foreach para aplicar la accion por cada uno de los archivos dentro de la carpeta en forma paralela
        Parallel.ForEach(archivos, archivo =>
        {
            // se pide la accion de leer el archivo
            LeerArchivo(archivo);
        });

        stopwatch.Stop();
        //se captura la hora en tiempo real de cuando se termina el programa sincrono
        Console.WriteLine($"Tiempo final paralelo: {DateTime.Now:HH:mm:ss}");
        double tiempoTotal = stopwatch.Elapsed.TotalSeconds;// se guarda en tiempoTotal el tiempo que duro la ejecucion 
        Console.WriteLine($"Tiempo total de procesamiento paralelo: {tiempoTotal} segundos");
        return tiempoTotal;
    }

    //aqui se codea la comparativa de tiempos entre ambos procesos
    static void CompararTiempos(double tiempoSincrono, double tiempoParalelo)
    {
        //si el proceso sincrono tardo menos tiempo, esto aparecerá en consola
        if (tiempoSincrono < tiempoParalelo)
        {
            Console.WriteLine("\n¡El proceso síncrono fue más rápido!");
            Console.WriteLine($"Con una diferencia de {tiempoParalelo - tiempoSincrono} segundos");
        }
        //si el proceso paralelo tardo menos tiempo, esto aparecerá en consola
        else if (tiempoSincrono > tiempoParalelo)
        {
            Console.WriteLine("\n¡El proceso paralelo fue más rápido!");
            Console.WriteLine($"La diferencia de tiempos fue de {tiempoSincrono - tiempoParalelo} segundos");
        }
        //si fue un empate, esto aparecerá en consola
        else
        {
            Console.WriteLine("\nQue sorpresa!, ambos procesos tomaron el mismo tiempo, pide un deseo que ha pasado un milagro :D.");
        }
    }


    //aqui se codea la accion de leer un archivo
    static void LeerArchivo(string archivo)
        
    {
        //se utiliza el stop watch para saber cuanto tardo en leer el archivo
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        //se utiliza un try catch por si surgen errores al realizar la lectura y de ser asi, que diga la razon del error
        try
        {
            string contenido = File.ReadAllText(archivo);
            Console.WriteLine($"Procesando archivo: {archivo}");
            stopwatch.Stop();
            double tiempoTotal = stopwatch.Elapsed.TotalSeconds;// se guarda en tiempoTotal el tiempo que duro la ejecucion 
            Console.WriteLine($"Tiempo total de lectura del archivo fue de {tiempoTotal} segundos");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al procesar el archivo {archivo}: {ex.Message}");
        }
        
    }
}
