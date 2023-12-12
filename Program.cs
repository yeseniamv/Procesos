using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        //aqui se agrega la direccion de la carpeta donde buscara los archivos y con ellos hara un array(arreglo)
        string[] docs = Directory.GetFiles("D:\\Documentos 2\\UniKino\\Algebra");//ajusta la direccion de carpeta a una deseada
        //hara saber al usuario la cantidad de archivos que contiene carpeta
        Console.WriteLine($"Número total de archivos: {docs.Length}");


        Console.WriteLine("\nProceso Síncrono:");// primero se ejecutara el método síncrono
        var syncTime = SyncProcess(docs);

        Console.WriteLine("\nProceso Paralelo:");// despues se ejecutara el método paralelo
        var parallelTime = ParallelProcess(docs);

        CompareTimes(syncTime, parallelTime);// y por ultimo se compararán ambostiempos para saber que método fue más rapido de terminar la tarea

        Console.ReadLine();//a la espera de tocar una tecla para cerrar la ventana
    }

    //aqui se codea el proceso sincrono
    static double SyncProcess(string[] docs)
    {
        //se utiliza stopwatch para contar el tiempo de ejecicion con start y stop
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        //se captura la hora en tiempo real de cuando se inicia el programa sincrono
        Console.WriteLine($"Tiempo de inicio sincrono: {DateTime.Now:HH:mm:ss}");

        //se utiliza un foreach para aplicar la accion por cada uno de los archivos dentro de la carpeta
        foreach (var doc in docs)
        {
            // se pide la accion de leer el archivo, incluye descripcion de que archivo se lee y el tiempo que le toma leerlo
            ReadDoc(doc);
        }

        stopwatch.Stop();
        //se captura la hora en tiempo real de cuando se termina el programa sincrono
        Console.WriteLine($"Tiempo final sincrono: {DateTime.Now:HH:mm:ss}");
        double totalTime = stopwatch.Elapsed.TotalSeconds;// se guarda en tiempoTotal el tiempo que duro la ejecucion 
        Console.WriteLine($"Tiempo total de procesamiento sincrono: {totalTime} segundos");
        return totalTime;
    }

    //aqui se codea el proceso paralelo
    static double ParallelProcess(string[] docs)
    {
        //se utiliza stopwatch para contar el tiempo de ejecicion con start y stop
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        //se captura la hora en tiempo real de cuando se inicia el programa sincrono
        Console.WriteLine($"Tiempo de inicio paralelo: {DateTime.Now:HH:mm:ss}");

        //se utiliza un parallel foreach para aplicar la accion por cada uno de los archivos dentro de la carpeta en forma paralela
        Parallel.ForEach(docs, doc =>
        {
            // se pide la accion de leer el archivo
            ReadDoc(doc);
        });

        stopwatch.Stop();
        //se captura la hora en tiempo real de cuando se termina el programa sincrono
        Console.WriteLine($"Tiempo final paralelo: {DateTime.Now:HH:mm:ss}");
        double totalTime = stopwatch.Elapsed.TotalSeconds;// se guarda en tiempoTotal el tiempo que duro la ejecucion 
        Console.WriteLine($"Tiempo total de procesamiento paralelo: {totalTime} segundos");
        return totalTime;
    }

    //aqui se codea la comparativa de tiempos entre ambos procesos
    static void CompareTimes(double syncTime, double parallelTime)
    {
        //si el proceso sincrono tardo menos tiempo, esto aparecerá en consola
        if (syncTime < parallelTime)
        {
            Console.WriteLine("\n¡El proceso síncrono fue más rápido!");
            Console.WriteLine($"Con una diferencia de {parallelTime - syncTime} segundos");
        }
        //si el proceso paralelo tardo menos tiempo, esto aparecerá en consola
        else if (syncTime > parallelTime)
        {
            Console.WriteLine("\n¡El proceso paralelo fue más rápido!");
            Console.WriteLine($"La diferencia de tiempos fue de {syncTime - parallelTime} segundos");
        }
        //si fue un empate, esto aparecerá en consola
        else
        {
            Console.WriteLine("\nQue sorpresa!, ambos procesos tomaron el mismo tiempo, pide un deseo que ha pasado un milagro :D.");
        }
    }


    //aqui se codea la accion de leer un archivo
    static void ReadDoc(string doc)
        
    {
        //se utiliza el stop watch para saber cuanto tardo en leer el archivo
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        //se utiliza un try catch por si surgen errores al realizar la lectura 
        try
        {
            string content = File.ReadAllText(doc);
            Console.WriteLine($"Procesando archivo: {doc}");
            stopwatch.Stop();
            double totalTime = stopwatch.Elapsed.TotalSeconds;// se guarda en tiempoTotal el tiempo que duro la ejecucion 
            Console.WriteLine($"Tiempo total de lectura del archivo fue de {totalTime} segundos");
        }
        //si ocurre un error, describe la razon del error
        catch (Exception ex)
        {
            Console.WriteLine($"Error al procesar el archivo {doc}: {ex.Message}");
        }
        
    }
}
