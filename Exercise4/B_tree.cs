using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exercise4
{
    public class B_tree
    {
        public string[] data;
        public int t;
        public List<List<List<int>>> tree = new List<List<List<int>>>();

        public B_tree()
        { }

        public void GetText(string[] data1)
        {
           
            data = new string[data1.Length];
            data = data1;

            Int32.TryParse(data[0], out t);
            t = 2 * t - 1;

            for (int i = 0; i < data.Length - 1; i++)
            {
                int value = 0;
                Int32.TryParse(data[i + 1], out value);
                 B_TREE_INSERT(value);
            }

        }

        public void NewParametarT(int t1)
        {
            t = t1;
            tree = new List<List<List<int>>>();

            for (int i = 0; i < data.Length - 1; i++)
            {
                int value = 0;
                Int32.TryParse(data[i + 1], out value);
                B_TREE_INSERT(value);
            }
        }

        public bool SearchValue(int value)
        {
            List<int> childPositionInRow = new List<int>();
            bool exists =  B_TREE_SEARCH(value, ref childPositionInRow);
            return exists;
        }

        public  void B_TREE_INSERT(int value)
        {
            List<int> childPositionInRow = new List<int>();
            bool exists = B_TREE_SEARCH(value, ref childPositionInRow);


            int x = childPositionInRow.Count;
            if (x == 0)
            {
                tree.Add(new List<List<int>>());
                tree[0].Add(new List<int>());
                tree[0][0].Add(value);
               return;
            }
            int y = childPositionInRow[x - 1];

            if (exists == false)
            {
                tree[x - 1][y].Add(value);
                tree[x - 1][y].Sort();
                int row = x - 1;
                while (tree[row][y].Count == t)
                {
                    int middle = tree[row][y][t / 2];
                    if (row == 0)
                    {
                        tree.Insert(0, new List<List<int>>());
                        tree[0].Add(new List<int>());
                        row = 1;
                    }
                    int y1 = childPositionInRow[row - 1];
                    tree[row - 1][y1].Add(middle);
                    tree[row - 1][y1].Sort();
                    List<int> firstHalf = new List<int>(), secondHalf = new List<int>();
                    for (int i = 0; i < t / 2; i++)
                    {
                        firstHalf.Add(tree[row][y][i]);
                    }
                    for (int i = t / 2 + 1; i < t; i++)
                    {
                        secondHalf.Add(tree[row][y][i]);
                    }
                    tree[row].RemoveAt(y);
                    tree[row].Insert(y, firstHalf);
                    tree[row].Insert(y + 1, secondHalf);
                    row--;
                    y = y1;
                }
            }

           return;
        }

        public bool B_TREE_SEARCH(int value, ref List<int> childPositionInRow)
        {
            int x = tree.Count;
            if (x == 0) return false;
            int y = tree[0].Count;

            int j = 0, k = 0, i = 0;

            while (i != x)
            {
                int z = tree[i][j].Count;
                while (value >= tree[i][j][k])
                {
                    if (value == tree[i][j][k]) return true;

                    k++;
                    if (k == z) break;
                }

                int position_y = 0;
                for (int j1 = 0; j1 < j; j1++)
                {
                    position_y += tree[i][j1].Count + 1;
                }
                if (x != 1) position_y += k;
                childPositionInRow.Add(j);
                j = position_y;
                i++;
                k = 0;
            }

            return false;
        }

        public string GetStringForGraphwiz()
        {
            int sum = 0;
            string output = "digraph {" + Environment.NewLine;
            for (int i = 0; i < tree.Count - 1; i++)
            {
                for (int j = 0; j < tree[i].Count; j++)
                {
                    string help = "\"";
                    for (int k = 0; k < tree[i][j].Count; k++)
                    {
                        help += tree[i][j][k].ToString() + " ";
                    }
                    help += "\"";

                    for (int jhelp = sum; jhelp < sum + tree[i][j].Count + 1; jhelp++)
                    {
                        string help2 = "\"";
                        for (int k = 0; k < tree[i + 1][jhelp].Count; k++)
                        {
                            help2 += tree[i + 1][jhelp][k].ToString() + " ";
                        }
                        help2 += "\"";
                        output += help + " -> " + help2 + Environment.NewLine;
                    }

                    sum += tree[i][j].Count + 1;
                }
                sum = 0;
            }

            output += "}";


            return output;
        }
    }
}

