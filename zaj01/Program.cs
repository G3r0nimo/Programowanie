using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace zaj01
{
    public class Node
    {
        public string Name { get; set; }

        public List<Node> Childrens { get; set; }

        public double Distance { get; set; }

        public Node(string name, double dist)
        {
            Name = name;
            Distance = dist;
            Childrens = new List<Node>();
        }

        public Node(string name)
        {
            Name = name;
            Distance = 0;
            Childrens = new List<Node>();
        }
    }
    public abstract class SearchMethod
    {
        protected List<Node> _openList;
        protected List<Node> _closedList;

        protected abstract void InsertChildrens(List<Node> childrens);

        public Node Search(Node startNode, string endNodeName)
        {
            var result = new Node("", 0);
            _openList = new List<Node>();
            _closedList = new List<Node>();

            _openList.Add(startNode);

            while (_openList.Any())  //.Any zwraca bool kiedy lista ma zawartość
            {
                var visited = _openList.FirstOrDefault();
                Console.WriteLine($"Visited node: {visited.Name}");
                if (visited.Name == endNodeName)
                {
                    result = visited;
                    break;
                }
                else
                {
                   InsertChildrens(visited.Childrens);

                }
                _openList.Remove(visited);
                _closedList.Add(visited);

                _openList = _openList.Where(x => !_closedList.Any(y => y.Name == x.Name)).ToList(); //wyjaśnić zasadę działaniew tego linq
            }
            return result;
        }
    }
    //nowa implementacja sortowanie do liścia

    class DFSMEthod : SearchMethod
    {
        protected override void InsertChildrens(List<Node> childrens)
        {
            _openList.InsertRange(0, childrens);
        }
    }

    //implementacja z wagą
    class MassSearch : SearchMethod
    {
        protected override void InsertChildrens(List<Node> childrens)
        {
            _openList.InsertRange(0, childrens);
        }
        public double SearchDistance()
        {
            double distance = 0;
            Node Last = _openList.FirstOrDefault();
            distance = Last.Distance;
            
            foreach (var x in _closedList)
            {
               // x.Childrens == Last.Name;
                foreach (var y in x.Childrens)
                {
                    if (y.Name == Last.Name)
                    {
                        distance += x.Distance;
                        Last == x;
                        
                    }
                    
                    
                }
            }
            return distance;

        }


    }



    class Program
    {
        static void Main(string[] args)
        {

            Node A = new Node("A");
            Node B = new Node("B");
            Node C = new Node("C");
            Node D = new Node("D");
            Node E = new Node("E");
            Node F = new Node("F");
            Node G = new Node("G");
            Node H = new Node("H");
            Node I = new Node("I");
            A.Childrens.Add(B);
            A.Childrens.Add(C);
            A.Childrens.Add(D);
            B.Childrens.Add(E);
            B.Childrens.Add(F);
            C.Childrens.Add(G);
            C.Childrens.Add(H);
            D.Childrens.Add(I);
            A.Distance = 0;
            B.Distance = 20;
            C.Distance = 30;
            D.Distance = 10;
            E.Distance = 10;
            F.Distance = 5;
            G.Distance = 4;
            H.Distance = 3;
            I.Distance = 2;
            DFSMEthod Wynik =new DFSMEthod();
            Wynik.Search(A, "F");

        }
    }
}
//augorytm zachłanny miejscowosci z realnymi odległościami 