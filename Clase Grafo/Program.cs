using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clase_Grafo
{
    public class CGrafoPonderado
    {
        public List<string> vertices;
        public int[,] matriz_ady;

        public int PosicionVert(string vertice)
        {
            int posicion = -1;
            for (int i = 0; i < vertices.Count; i++)
            {
                if (vertice == vertices[i])
                {
                    posicion = i;
                }
            }
            return posicion;
        }

        public void Inicializar()
        {
            for (int i = 0; i < vertices.Count; i++)
			{
                for (int j = 0; i < vertices.Count; i++)
                {
                    matriz_ady[i,j]=int.MaxValue;
                }	 
			}

        }
        public CGrafoPonderado()
        {
            vertices = new List<string>();
            matriz_ady = new int[vertices.Count, vertices.Count];
            Inicializar();
        }
        
        public bool EsVacio()
        {
            if(vertices.Count!=0)
            {
                return false;
            }
            return true;
        }

        public int NumeroDeVertices()
        {
            return vertices.Count;
        }

        public int NumeroDeArcos()
        {
            if (!EsVacio())
            {
                int cantArc = 0;
                for (int i = 0; i < matriz_ady.GetLength(0); i++)
                {
                    for (int j = 0; j < matriz_ady.GetLength(1); j++)
                    {
                        if (matriz_ady[i, j] != int.MaxValue)
                        {
                            cantArc++;
                        }
                    }
                }
                return cantArc;
            }
            return 0;            
        }

        public bool EstaElVertice(string vertice)
        {
            if (!EsVacio())
            {
                if (vertices.Contains(vertice))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public bool EstaElArco(int V1, int V2)
        {
            if (V1 >= 0 && V1 <= vertices.Count && V2 >= 0 && V2 <= vertices.Count)
            {
                if (matriz_ady[V1, V2] != int.MaxValue)
                {
                    return true;
                }
            }
            return false;
        }

        public void ModifVertice(string V,string P)
        {
            
            for (int i = 0; i <vertices.Count; i++)
			{
                if (V==vertices[i])
                {
                    vertices[i]=P;
                    int[,] aux = new int[vertices.Count + 1, vertices.Count + 1];
                    for (int j = 0; j < matriz_ady.GetLength(0); j++)
                    {
                        for (int k = 0; k < matriz_ady.GetLength(1); k++)
                        {
                            aux[j, k] = matriz_ady[j, k];
                        }
                    }
                    matriz_ady = new int[vertices.Count + 1, vertices.Count + 1];
                    Inicializar();
                    for (int k = 0; k < aux.GetLength(0); k++)
                    {
                        for (int j = 0; j < aux.GetLength(1); j++)
                        {
                            matriz_ady[k, j] = aux[k, j];
                        }
                    }
                }			 
			}
        }
        
        
      public List<string> bfs(string n)
      {
         Queue<string> q=new Queue<string>();
          
          List<string>visitados=new List<string>();
          q.Enqueue(n);
          while(q.Count!=0)
          {
              string p = q.Dequeue();
              visitados.Add(p);

              for (int i = 0; i < matriz_ady.GetLength(0); i++)
              {
                  if (!q.Contains(vertices[i])&&!visitados.Contains(vertices[i]) && matriz_ady[i, matriz_ady.GetLength(1)]!=int.MaxValue)
                  {
                      q.Enqueue(vertices[i]);
                  }
              }
          }
          return visitados;
      }
        public void InsertarVertice(string V)
        {
            vertices.Add(V);
            int[,] aux = new int[vertices.Count, vertices.Count];
            for (int i = 0; i < matriz_ady.GetLength(0); i++)
            {
                for (int j = 0; j < matriz_ady.GetLength(1); j++)
                {
                    aux[i, j] = matriz_ady[i, j];
                }
            }
            matriz_ady = new int[vertices.Count, vertices.Count];
           Inicializar();
            for (int i = 0; i < aux.GetLength(0); i++)
            {
                for (int j = 0; j < aux.GetLength(1); j++)
                {
                    matriz_ady[i, j] = aux[i, j];
                }
            }

        }

        public void InsertarArco(int V1, int V2, int valor)
        {
            if (V1 >= 0 && V1 <= vertices.Count && V2 >= 0 && V2 <= vertices.Count)
            {
                matriz_ady[V1, V2] = valor;
            }
            else
            {
                throw new Exception("Indice fuera del rango.");
            }
        }

        //Preguntar duda con respecto a la matriz de adyacencia
        public void EliminarVertice(string V)
        {
            vertices.Remove(V);
            int[,] aux = new int[vertices.Count - 1, vertices.Count - 1];
            for (int i = 0; i < matriz_ady.GetLength(0); i++)
            {
                for (int j = 0; j < matriz_ady.GetLength(1); j++)
                {
                    aux[i, j] = matriz_ady[i, j];
                }
            }
            matriz_ady = new int[vertices.Count - 1, vertices.Count - 1];
            for (int i = 0; i < aux.GetLength(0); i++)
            {
                for (int j = 0; j < aux.GetLength(1); j++)
                {
                    matriz_ady[i, j] = aux[i, j];
                }
            }
        }

        public void EliminarArco(int V1,int V2)
        {
            if (V1 >= 0 && V1 <= vertices.Count && V2 >= 0 && V2 <= vertices.Count)
            {
                matriz_ady[V1, V2] = int.MaxValue;
            }
            else
                throw new Exception("Indice fuera del rango.");
        }

        
        public List<string> Dkjistra(string verti)
        {
            List<string> predecesores = new List<string>(vertices.Count);
            List<int> caminoMin = new List<int>();
            List<string> visitados = new List<string>();
           
            for (int i = 0; i < predecesores.Count; i++)
            {
                predecesores.Add(verti);
            }
            visitados.Add(verti);
            int pos = PosicionVert(verti);
            for (int i = 0; i < (vertices.Count-1); i++)
            {
                caminoMin[i] = matriz_ady[pos, i];
            }
            int posmenor = -1;
            visitados.Add(vertices[PosicionVert(vertices[PosMenor(caminoMin)])]);
            while (visitados.Count!=vertices.Count)
            {
                posmenor = PosMenor(caminoMin);
                if (caminoMin[posmenor]>matriz_ady[])
                {
                    
                }
            }
        }

        public int PosMenor(List<int> a)
        {
            int posicion = -1;
            int r = int.MaxValue;
            for (int i = 0; i < a.Count; i++)
            {
                if (a[i]<r)
                {
                    posicion = i;
                }
            }
            return posicion;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            CGrafoPonderado grafito=new CGrafoPonderado();
            grafito.vertices = new List<string>() {"A", "B", "C", "D"};
            Console.WriteLine(grafito.PosicionVert("M"));
            //grafito.matriz_ady[1, 1] = 3;
            //Console.WriteLine(grafito.matriz_ady[0,0]);
            //Console.WriteLine(grafito.vertices.Count);
            //Console.WriteLine(grafito.bfs("A"));
        }
    }
}
