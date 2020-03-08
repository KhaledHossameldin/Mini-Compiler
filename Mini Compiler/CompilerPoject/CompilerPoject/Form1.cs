using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompilerPoject
{
    public partial class Form1 : Form
    {
        class MemoryNode
        {
            public string Type;
            public string Identifier;
            public string Variable;
            public string Value;
            public List<ValueNode> V;

            public MemoryNode()
            {
                Type = "";
                Identifier = "";
                Variable = "";
                Value = "";
                V = null;
            }
        }
        List<MemoryNode> Memory = new List<MemoryNode>();

        class ValueNode
        {
            public string Val;
            public char Op;
            public bool isDone;

            public ValueNode()
            {
                Val = "";
                Op = '\0';
                isDone = false;
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        string RemoveSpaces()
        {
            string Code;
            string Temp = textBoxCode.Text.Trim();

            Code = Temp.Replace(" ", "");

            return Code;
        }

        string IdentifyingCode(string Code)
        {
            string Temp = "";
            string Type = "";

            for (int i = 0; i < Code.Length; i++)
            {
                Temp += Code[i];
                if (Temp == "int" || Temp == "float" || Temp == "string" || Temp == "double" || Temp == "bool" || Temp == "char")
                {
                    Type = "Declaring Variable";
                    break;
                }

                if (Temp == "if(")
                {
                    Type = "If Condition";
                    break;
                }
            }
            if (Type == "")
            {
                Type = "Assigning Value";
            }

            return Type;
        }

        int CalculateValue(MemoryNode pnn)
        {
            int Total = 0;
            bool isNotFound = false;
            int PreviousNum = 0;
            int PreviousOp = '\0';
            List<int> ValueList = new List<int>();
            if (pnn.V != null)
            {
                for (int i = 0; i < pnn.V.Count; i++)
                {
                    if (i > 0 && !pnn.V[i].isDone)
                    {
                        if (int.TryParse(pnn.V[i-1].Val, out int n))
                        {
                            PreviousNum = n;
                        }
                        else
                        {
                            int NotFound = 0;
                            for (int j = 0; j < Memory.Count; j++)
                            {
                                if (Memory[j].Variable == pnn.V[i-1].Val)
                                {
                                    PreviousNum = Convert.ToInt32(Memory[j].Value);
                                    NotFound = 1;
                                    break;
                                }
                            }
                            if (NotFound != 0 && Memory.Count != 0)
                            {
                                isNotFound = true;
                            }
                        }
                        PreviousOp = pnn.V[i - 1].Op;


                        if (PreviousOp == '*')
                        {
                            if (int.TryParse(pnn.V[i].Val, out int num))
                            {
                                int Temp = PreviousNum * num;
                                ValueList.Add(Temp);
                                pnn.V[i].isDone = true;
                                pnn.V[i - 1].isDone = true;
                            }
                            else
                            {
                                int NotFound = 0;
                                for (int j = 0; j < Memory.Count; j++)
                                {
                                    if (Memory[j].Variable == pnn.V[i].Val)
                                    {
                                        NotFound = 1;
                                        int Temp = PreviousNum * Convert.ToInt32(Memory[j].Value);
                                        ValueList.Add(Temp);
                                        pnn.V[i].isDone = true;
                                        pnn.V[i - 1].isDone = true;
                                        break;
                                    }
                                }
                                if (NotFound != 1 && Memory.Count != 0)
                                {
                                    isNotFound = true;
                                }
                            }
                        }

                        if (PreviousOp == '/')
                        {
                            if (int.TryParse(pnn.V[i].Val, out int num))
                            {
                                int Temp = PreviousNum / num;
                                ValueList.Add(Temp);
                                pnn.V[i].isDone = true;
                                pnn.V[i - 1].isDone = true;
                            }
                            else
                            {
                                int NotFound = 0;
                                for (int j = 0; j < Memory.Count; j++)
                                {
                                    if (Memory[j].Variable == pnn.V[i].Val)
                                    {
                                        NotFound = 1;
                                        int Temp = PreviousNum * Convert.ToInt32(Memory[j].Value);
                                        ValueList.Add(Temp);
                                        pnn.V[i].isDone = true;
                                        pnn.V[i - 1].isDone = true;
                                        break;
                                    }
                                }
                                if (NotFound != 1 && Memory.Count != 0)
                                {
                                    isNotFound = true;
                                }
                            }
                        }
                    }
                }
                int PreviousNumber = 0;
                char PreviousOperation = '\0';
                int CalculateIndex = 0;
                for (int i = 0; i < pnn.V.Count; i++)
                {
                    if (!pnn.V[i].isDone)
                    {
                        if (Total == 0)
                        {
                            if (int.TryParse(pnn.V[i].Val, out int num))
                            {
                                Total += num;
                            }
                            else
                            {
                                int NotFound = 0;
                                for (int j = 0; j < Memory.Count; j++)
                                {
                                    if (Memory[j].Variable == pnn.V[i].Val)
                                    {
                                        Total += Convert.ToInt32(Memory[j].Value);
                                        NotFound = 1;
                                        break;
                                    }
                                }
                                if (NotFound != 1 && Memory.Count != 1)
                                {
                                    isNotFound = true;
                                }
                            }
                        }
                        else
                        {
                            if (PreviousOperation == '+')
                            {
                                if (int.TryParse(pnn.V[i].Val, out int num))
                                {
                                    Total += num;
                                }
                                else
                                {
                                    int NotFound = 0;
                                    for (int j = 0; j < Memory.Count; j++)
                                    {
                                        if (Memory[j].Variable == pnn.V[i].Val)
                                        {
                                            Total += Convert.ToInt32(Memory[j].Value);
                                            NotFound = 1;
                                            break;
                                        }
                                    }
                                    if (NotFound != 1 && Memory.Count != 0)
                                    {
                                        isNotFound = true;
                                    }
                                }
                            }
                            if (PreviousOperation == '-')
                            {
                                if (int.TryParse(pnn.V[i].Val, out int num))
                                {
                                    Total -= num;
                                }
                                else
                                {
                                    int NotFound = 0;
                                    for (int j = 0; j < Memory.Count; j++)
                                    {
                                        if (Memory[j].Variable == pnn.V[i].Val)
                                        {
                                            Total -= Convert.ToInt32(Memory[j].Value);
                                            NotFound = 1;
                                            break;
                                        }
                                    }
                                    if (NotFound != 1 && Memory.Count != 0)
                                    {
                                        isNotFound = true;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Total == 0)
                        {
                            Total += ValueList[CalculateIndex];
                            CalculateIndex++;
                        }
                        else
                        {
                            if (PreviousOperation == '+')
                            {
                                Total += ValueList[CalculateIndex];
                                CalculateIndex++;
                            }
                            if (PreviousOperation == '-')
                            {
                                Total -= ValueList[CalculateIndex];
                                CalculateIndex++;
                            }
                        }
                        i++;
                        PreviousOperation = pnn.V[i].Op;
                    }



                    if (int.TryParse(pnn.V[i].Val, out int n))
                    {
                        PreviousNumber = n;
                    }
                    else
                    {
                        int NotFound = 0;
                        for (int j = 0; j < Memory.Count; j++)
                        {
                            if (Memory[j].Variable == pnn.V[i].Val)
                            {
                                PreviousNumber = Convert.ToInt32(Memory[j].Value);
                                NotFound = 1;
                                break;
                            }
                        }
                        if (NotFound != 1 && Memory.Count != 0)
                        {
                            isNotFound = true;
                        }
                    }
                    PreviousOperation = pnn.V[i].Op;
                }
            }
            if (!isNotFound)
            {
                return Total;
            }
            else
            {
                return -1;
            }
        }

        void TokenizeDeclaring(MemoryNode pnn, string Code)
        {
            int ErrorShown = 0;

            if (Code[Code.Length-1] != ';')
            {
                MessageBox.Show("ERROR \n MISSING SEMICOLON");
            }
            else
            {
                bool isFound = false;
                string Temp = "";
                int Ct = 0;
                for (int i = 0; i < Code.Length-1; i++)
                {
                    Temp += Code[i];

                    if ((Temp == "int" || Temp == "float" || Temp == "string" || Temp == "double" || Temp == "bool" || Temp == "char") && Ct == 0)
                    {
                        pnn.Identifier = Temp;
                        Temp = "";
                        Ct++;
                        //label7.Text = "Identifier: " + pnn.Identifier;
                    }

                    if (Code[i+1] == '=' || Code[i+1] == ';')
                    {
                        if (char.IsDigit(Temp[0]))
                        {
                            MessageBox.Show("ERROR \n VARIABLE CAN'T BEGIN WITH A DIGIT");
                            isFound = true;
                            break;
                        }
                        else
                        {
                            if (Ct == 1)
                            {
                                if (Code[i+1] == ';')
                                {
                                    pnn.Value = "0";
                                }
                                for (int j = 0; j < Memory.Count; j++)
                                {
                                    if (Memory[j].Variable == Temp)
                                    {
                                        MessageBox.Show("ERROR \n THE VARIABLE IS ALREADY DECLARED");
                                        isFound = true;
                                        break;
                                    }
                                }
                                pnn.Variable = Temp;
                                Temp = "";
                                Ct++;
                                //label7.Text += "\n Variable: " + pnn.Variable;
                                if (pnn.Value == "0")
                                {
                                    //label7.Text += "\n Value: " + pnn.Value;
                                }
                            }
                        }
                    }

                    if (Temp != "" && Temp[0] == '=' && (Code[i+1] == ';') && Ct == 2)
                    {
                        pnn.V = new List<ValueNode>();
                        Temp = Temp.Remove(0, 1);
                        string Temp2 = "";
                        ValueNode pnn2 = new ValueNode();
                        for (int j = 0; j < Temp.Length; j++)
                        {
                            Temp2 += Temp[j];
                            if (Temp[j] == '+' || Temp[j] == '-' || Temp[j] == '/' || Temp[j] == '%' || Temp[j] == '*' || j == Temp.Length-1)
                            {
                                if (j != Temp.Length-1)
                                {
                                    pnn2.Op = Temp[j];
                                    Temp2 = Temp2.Remove(Temp2.Length - 1, 1);
                                }
                                pnn2.Val = Temp2;
                                pnn.V.Add(pnn2);
                                pnn2 = new ValueNode();
                                Temp2 = "";
                            }
                        }
                        string Total = CalculateValue(pnn).ToString();
                        if (Total != "-1" && Total != "")
                        {
                            pnn.Value = Total;
                            //label7.Text += "\n Value: " + pnn.Value;
                        }
                        else
                        {
                            MessageBox.Show("ERROR \n A VARIABLE IS NOT DECLARED");
                            ErrorShown = 1;
                        }
                    }
                }
                if (!isFound)
                {
                    if (pnn.Value != "-1" && pnn.Value != "")
                    {
                        Memory.Add(pnn);
                        ShowMemory();
                    }
                    else
                    {
                        if (ErrorShown != 1)
                        {
                            MessageBox.Show("ERROR \n A VARIABLE IS NOT DECLARED");
                        }
                    }
                }
            }
        }

        void TokenizeIf(MemoryNode pnn, string Code)
        {
            string Temp = "";
            int Ct = 0;
            int ctt = 0;
            int ctt2 = 0;
            int ctt3 = 0;
            int j = 0;
            int i = 0;
            int f = 0;
            int ctt4 = 0;
            int WrongVarFlag = 2;
            string temp2 = "";
            string temp3 = "";

            for (i = 0; i < Code.Length - 1; i++)
            {
                Temp += Code[i];

                if (Temp == "if(" && Ct == 0)
                {
                    pnn.Identifier = Temp;
                    Temp = "";
                    Ct++;
                    //label7.Text = "Identifier: " + pnn.Identifier;
                    Ct = 1;
                }
                for (int y = 0; y < Code.Length; y++)
                {
                    if (Code[y] == ')' && f == 0)
                    {
                        ctt4++;
                        f = 1;
                        // MessageBox.Show("" + ctt4);
                    }
                    if (Code[y] == '{' && f == 1)
                    {
                        ctt4++;
                        f = 2;
                        //MessageBox.Show("" + ctt4);

                    }
                    if (Code[y] == '}' && f == 2)
                    {
                        ctt4++;
                        f = 3;
                        //MessageBox.Show("" + ctt4);

                    }
                }

                if (ctt4 != 3)
                {
                    MessageBox.Show("Missing a bracket");
                    break;
                }
                if (Code[i + 1] == '=' || Code[i + 1] == '!' || Code[i + 1] == '<' || Code[i + 1] == '>')
                {

                    for (int c = 0; c < Memory.Count; c++)
                    {
                        if (Temp == Memory[c].Variable)
                        {
                            ctt3 = 1;
                            WrongVarFlag = 0;
                            //////////////////////////////////
                            Temp = "";

                            if (Code[i + 1] == '=' && Code[i + 2] == '=')
                            {
                                for (j = i + 3; Code[j] != ')'; j++)
                                {
                                    temp2 += Code[j];
                                }
                                if (char.IsDigit(temp2[0]))
                                {
                                    for (int b = 0; ; b++)
                                    {
                                        if (Convert.ToInt32(Memory[b].Value) == Convert.ToInt32(temp2))
                                        {
                                            MessageBox.Show("Condition Is True");
                                            for (int g = 0; Code[g] != '}'; g++)
                                            {
                                                if (Code[g] == '{')
                                                {
                                                    for (int e = g + 1; Code[e] != '}'; e++)
                                                    {
                                                        temp3 += Code[e];
                                                    }
                                                    //MessageBox.Show("" + temp3);

                                                    MemoryNode Statement = new MemoryNode();
                                                    TokenizeAssigning(Statement, temp3);

                                                    break;
                                                }

                                            }
                                            ctt = 1;
                                            break;
                                        }
                                        else
                                        {
                                            MessageBox.Show("Condition Is Wrong");
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    for (int m = 0; m < Memory.Count; m++)
                                    {
                                        if (Memory[m].Variable == temp2)
                                        {
                                            if (Convert.ToInt32(Memory[c].Value) == Convert.ToInt32(Memory[m].Value))
                                            {
                                                MessageBox.Show("Condition Is True");
                                                for (int g = 0; Code[g] != '}'; g++)
                                                {
                                                    if (Code[g] == '{')
                                                    {
                                                        for (int e = g + 1; Code[e] != '}'; e++)
                                                        {
                                                            temp3 += Code[e];
                                                        }
                                                        //MessageBox.Show("" + temp3);

                                                        MemoryNode Statement = new MemoryNode();
                                                        TokenizeAssigning(Statement, temp3);

                                                        break;
                                                    }

                                                }
                                                ctt = 1;
                                                break;
                                            }
                                            else
                                            {
                                                MessageBox.Show("Condition Is Wrong");
                                                break;
                                            }
                                        }
                                    }
                                }

                            }
                            ////////////////////less than or eq////////////////////////
                            if (Code[i + 1] == '<' && Code[i + 2] == '=')
                            {
                                for (j = i + 3; Code[j] != ')'; j++)
                                {
                                    temp2 += Code[j];
                                }
                                if (char.IsDigit(temp2[0]))
                                {
                                    for (int b = 0; ; b++)
                                    {
                                        if (Convert.ToInt32(Memory[b].Value) <= Convert.ToInt32(temp2))
                                        {
                                            MessageBox.Show("Condition Is True");
                                            for (int g = 0; Code[g] != '}'; g++)
                                            {
                                                if (Code[g] == '{')
                                                {
                                                    for (int e = g + 1; Code[e] != '}'; e++)
                                                    {
                                                        temp3 += Code[e];
                                                    }
                                                    //MessageBox.Show("" + temp3);

                                                    MemoryNode Statement = new MemoryNode();
                                                    TokenizeAssigning(Statement, temp3);

                                                    break;
                                                }

                                            }
                                            ctt = 1;
                                            break;
                                        }
                                        else
                                        {
                                            MessageBox.Show("Condition Is Wrong");
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    for (int m = 0; m < Memory.Count; m++)
                                    {
                                        if (Memory[m].Variable == temp2)
                                        {
                                            if (Convert.ToInt32(Memory[c].Value) <= Convert.ToInt32(Memory[m].Value))
                                            {
                                                MessageBox.Show("Condition Is True");
                                                for (int g = 0; Code[g] != '}'; g++)
                                                {
                                                    if (Code[g] == '{')
                                                    {
                                                        for (int e = g + 1; Code[e] != '}'; e++)
                                                        {
                                                            temp3 += Code[e];
                                                        }
                                                        //MessageBox.Show("" + temp3);

                                                        MemoryNode Statement = new MemoryNode();
                                                        TokenizeAssigning(Statement, temp3);

                                                        break;
                                                    }

                                                }
                                                ctt = 1;
                                                break;
                                            }
                                            else
                                            {
                                                MessageBox.Show("Condition Is Wrong");
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            ////////////////////greater than or eq//////////////////
                            if (Code[i + 1] == '>' && Code[i + 2] == '=')
                            {
                                for (j = i + 3; Code[j] != ')'; j++)
                                {
                                    temp2 += Code[j];
                                }
                                if (char.IsDigit(temp2[0]))
                                {


                                    for (int b = 0; ; b++)
                                    {
                                        if (Convert.ToInt32(Memory[b].Value) >= Convert.ToInt32(temp2))
                                        {
                                            MessageBox.Show("Condition Is True");
                                            for (int g = 0; Code[g] != '}'; g++)
                                            {
                                                if (Code[g] == '{')
                                                {
                                                    for (int e = g + 1; Code[e] != '}'; e++)
                                                    {
                                                        temp3 += Code[e];
                                                    }
                                                    //MessageBox.Show("" + temp3);

                                                    MemoryNode Statement = new MemoryNode();
                                                    TokenizeAssigning(Statement, temp3);

                                                    break;
                                                }

                                            }
                                            ctt = 1;
                                            break;
                                        }
                                        else
                                        {
                                            MessageBox.Show("Condition Is Wrong");
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    for (int m = 0; m < Memory.Count; m++)
                                    {
                                        if (Memory[m].Variable == temp2)
                                        {
                                            if (Convert.ToInt32(Memory[c].Value) >= Convert.ToInt32(Memory[m].Value))
                                            {
                                                MessageBox.Show("Condition Is True");
                                                for (int g = 0; Code[g] != '}'; g++)
                                                {
                                                    if (Code[g] == '{')
                                                    {
                                                        for (int e = g + 1; Code[e] != '}'; e++)
                                                        {
                                                            temp3 += Code[e];
                                                        }
                                                        //MessageBox.Show("" + temp3);

                                                        MemoryNode Statement = new MemoryNode();
                                                        TokenizeAssigning(Statement, temp3);

                                                        break;
                                                    }

                                                }
                                                ctt = 1;
                                                break;
                                            }
                                            else
                                            {
                                                MessageBox.Show("Condition Is Wrong");
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            ///////////////////////////////////
                            if (Code[i + 1] == '!' && Code[i + 2] == '=')
                            {
                                for (j = i + 3; Code[j] != ')'; j++)
                                {
                                    temp2 += Code[j];
                                }
                                if (char.IsDigit(temp2[0]))
                                {
                                    for (int b = 0; ; b++)
                                    {
                                        if (Convert.ToInt32(Memory[b].Value) != Convert.ToInt32(temp2))
                                        {
                                            MessageBox.Show("Condition Is True");
                                            for (int g = 0; Code[g] != '}'; g++)
                                            {
                                                if (Code[g] == '{')
                                                {
                                                    for (int e = g + 1; Code[e] != '}'; e++)
                                                    {
                                                        temp3 += Code[e];
                                                    }
                                                    //MessageBox.Show("" + temp3);

                                                    MemoryNode Statement = new MemoryNode();
                                                    TokenizeAssigning(Statement, temp3);

                                                    break;
                                                }

                                            }
                                            ctt = 1;
                                            break;
                                        }
                                        else
                                        {
                                            MessageBox.Show("Condition Is Wrong");
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    for (int m = 0; m < Memory.Count; m++)
                                    {
                                        if (Memory[m].Variable == temp2)
                                        {
                                            if (Convert.ToInt32(Memory[c].Value) != Convert.ToInt32(Memory[m].Value))
                                            {
                                                MessageBox.Show("Condition Is True");
                                                for (int g = 0; Code[g] != '}'; g++)
                                                {
                                                    if (Code[g] == '{')
                                                    {
                                                        for (int e = g + 1; Code[e] != '}'; e++)
                                                        {
                                                            temp3 += Code[e];
                                                        }
                                                        //MessageBox.Show("" + temp3);

                                                        MemoryNode Statement = new MemoryNode();
                                                        TokenizeAssigning(Statement, temp3);

                                                        break;
                                                    }

                                                }
                                                ctt = 1;
                                                break;
                                            }
                                            else
                                            {
                                                MessageBox.Show("Condition Is Wrong");
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            ////////////////////////////////////
                            if (Code[i + 1] == '<' && Code[i + 2] != '=')
                            {
                                for (j = i + 2; Code[j] != ')'; j++)
                                {
                                    temp2 += Code[j];
                                }
                                if (char.IsDigit(temp2[0]))
                                {


                                    for (int b = 0; ; b++)
                                    {
                                        if (Convert.ToInt32(Memory[b].Value) < Convert.ToInt32(temp2))
                                        {
                                            MessageBox.Show("Condition Is True");
                                            for (int g = 0; Code[g] != '}'; g++)
                                            {
                                                if (Code[g] == '{')
                                                {
                                                    for (int e = g + 1; Code[e] != '}'; e++)
                                                    {
                                                        temp3 += Code[e];
                                                    }
                                                    //MessageBox.Show("" + temp3);

                                                    MemoryNode Statement = new MemoryNode();
                                                    TokenizeAssigning(Statement, temp3);

                                                    break;
                                                }

                                            }
                                            ctt = 1;
                                            break;
                                        }
                                        else
                                        {
                                            MessageBox.Show("Condition Is Wrong");
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    for (int m = 0; m < Memory.Count; m++)
                                    {
                                        if (Memory[m].Variable == temp2)
                                        {
                                            if (Convert.ToInt32(Memory[c].Value) < Convert.ToInt32(Memory[m].Value))
                                            {
                                                MessageBox.Show("Condition Is True");
                                                for (int g = 0; Code[g] != '}'; g++)
                                                {
                                                    if (Code[g] == '{')
                                                    {
                                                        for (int e = g + 1; Code[e] != '}'; e++)
                                                        {
                                                            temp3 += Code[e];
                                                        }
                                                        //MessageBox.Show("" + temp3);

                                                        MemoryNode Statement = new MemoryNode();
                                                        TokenizeAssigning(Statement, temp3);

                                                        break;
                                                    }

                                                }
                                                ctt = 1;
                                                break;
                                            }
                                            else
                                            {
                                                MessageBox.Show("Condition Is Wrong");
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            //////////////////////////////////////
                            if (Code[i + 1] == '>' && Code[i + 2] != '=')
                            {
                                for (j = i + 3; Code[j] != ')'; j++)
                                {
                                    temp2 += Code[j];
                                }
                                if (char.IsDigit(temp2[0]))
                                {


                                    for (int b = 0; ; b++)
                                    {
                                        if (Convert.ToInt32(Memory[b].Value) > Convert.ToInt32(temp2))
                                        {
                                            MessageBox.Show("Condition Is True");
                                            for (int g = 0; Code[g] != '}'; g++)
                                            {
                                                if (Code[g] == '{')
                                                {
                                                    for (int e = g + 1; Code[e] != '}'; e++)
                                                    {
                                                        temp3 += Code[e];
                                                    }
                                                    //MessageBox.Show("" + temp3);

                                                    MemoryNode Statement = new MemoryNode();
                                                    TokenizeAssigning(Statement, temp3);

                                                    break;
                                                }

                                            }
                                            ctt = 1;
                                            break;
                                        }
                                        else
                                        {
                                            MessageBox.Show("Condition Is Wrong");
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    for (int m = 0; m < Memory.Count; m++)
                                    {
                                        if (Memory[m].Variable == temp2)
                                        {
                                            if (Convert.ToInt32(Memory[c].Value) > Convert.ToInt32(Memory[m].Value))
                                            {
                                                MessageBox.Show("Condition Is True");
                                                for (int g = 0; Code[g] != '}'; g++)
                                                {
                                                    if (Code[g] == '{')
                                                    {
                                                        for (int e = g + 1; Code[e] != '}'; e++)
                                                        {
                                                            temp3 += Code[e];
                                                        }
                                                        //MessageBox.Show("" + temp3);

                                                        MemoryNode Statement = new MemoryNode();
                                                        TokenizeAssigning(Statement, temp3);

                                                        break;
                                                    }

                                                }
                                                ctt = 1;
                                                break;
                                            }
                                            else
                                            {
                                                MessageBox.Show("Condition Is Wrong");
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            ///////////////////////////////////
                        }
                        else
                        {
                            WrongVarFlag = 1;
                            /*if(ctt3!=1)
                            MessageBox.Show("Variable Is Not Declared");
                            ctt3 = 1;
                            break;*/
                        }

                    }
                    if (WrongVarFlag == 1)
                    {
                        if (ctt3 != 1)
                            MessageBox.Show("Variable Is Not Declared");
                        ctt3 = 1;
                    }
                }
                ///

            }
        }

        void TokenizeAssigning(MemoryNode pnn, string Code)
        {
            if (Code[Code.Length - 1] != ';')
            {
                MessageBox.Show("ERROR \n MISSING SEMICOLON");
            }
            else
            {
                string Temp = "";
                int Ct = 0;
                bool isFound = false;
                int isFoundPosition = -1;
                for (int i = 0; i < Code.Length-1; i++)
                {
                    Temp += Code[i];
                    if (Code[i+1] == '=' || Code[i+1] == ';')
                    {
                        if (char.IsDigit(Temp[0]))
                        {
                            MessageBox.Show("ERROR IN VARIABLE");
                            break;
                        }
                        else
                        {
                            if (Ct == 0)
                            {
                                for (int j = 0; j < Memory.Count; j++)
                                {
                                    if (Memory[j].Variable == Temp)
                                    {
                                        isFound = true;
                                        isFoundPosition = j;
                                        break;
                                    }
                                }
                                Temp = "";
                                Ct++;
                            }
                        }
                    }

                    if (Temp != "" && Temp[0] == '=' && Code[i+1] == ';' && isFound && Ct == 1)
                    {
                        Temp = Temp.Remove(0, 1);
                        string Temp2 = "";
                        MemoryNode pnnMemory = Memory[isFoundPosition];
                        List<ValueNode> L = new List<ValueNode>();
                        ValueNode pnnValue = new ValueNode();
                        for (int j = 0; j < Temp.Length; j++)
                        {
                            Temp2 += Temp[j];
                            if (Temp[j] == '+' || Temp[j] == '-' || Temp[j] == '/' || Temp[j] == '%' || Temp[j] == '*' || j == Temp.Length - 1)
                            {
                                if (j != Temp.Length - 1)
                                {
                                    pnnValue.Op = Temp[j];
                                    Temp2 = Temp2.Remove(Temp2.Length - 1, 1);
                                }
                                pnnValue.Val = Temp2;
                                L.Add(pnnValue);
                                pnnValue = new ValueNode();
                                Temp2 = "";
                            }
                        }
                        pnnMemory.V = L;
                        string Total = CalculateValue(pnnMemory).ToString();
                        if (Total != "-1")
                        {
                            pnnMemory.Value = Total;
                            ShowMemory();
                        }
                        else
                        {
                            MessageBox.Show("ERROR \n A VARIABLE IS NOT DECLARED");
                        }
                    }
                    else
                    {
                        if (!isFound)
                        {
                            MessageBox.Show("ERROR \n A VARIABLE IS NOT DECLARED");
                            break;
                        }
                    }
                }
            }
        }

        void ShowMemory()
        {
            string Temp = "";
            for (int i = 0; i < Memory.Count; i++)
            {
                Temp += Memory[i].Variable + " = " + Memory[i].Value + "\n";
            }
            richTextBox1.Text = Temp;
        }

        private void buttonTokenize_Click(object sender, EventArgs e)
        {
            if (textBoxCode.Text != "")
            {
                MemoryNode pnn = new MemoryNode();
                string Code = RemoveSpaces();
                pnn.Type = IdentifyingCode(Code);

                if (pnn.Type == "Declaring Variable")
                {
                    TokenizeDeclaring(pnn, Code);
                }

                if (pnn.Type == "If Condition")
                {
                    TokenizeIf(pnn, Code);
                }

                if (pnn.Type == "Assigning Value")
                {
                    TokenizeAssigning(pnn, Code);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label7.Text = "";
        }

        private void buttonClearMemory_Click(object sender, EventArgs e)
        {
            Memory.Clear();
            ShowMemory();
            label7.Text = "";
        }
    }
}
