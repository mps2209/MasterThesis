using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MVCLSystem : MonoBehaviour
{
    public Text axiom;
    public Text rulesText;
    public Text alphabetText;
    public List<Rule> rules = new List<Rule>();
    string startingAxiom = "AB";
    int step = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StepForward()
    {
        if (step == 0)
        {
            axiom.text = startingAxiom;
            rules.Add(new Rule("AB", "AAB"));
        }
        else
        {
            step++;
            IterateAxiom();
        }
    }
    public void StepBack()
    {
        if (step < 1)
        {
            step--;
            axiom.text = startingAxiom;
        }
        int n = 0;
        while (n < step)
        {
            IterateAxiom();
            n++;
        }
    }
    public int Step()
    {
        return step;
    }
    void IterateAxiom()
    {
        string newAxiom = axiom.text;
        string currentAxiom = axiom.text;
        for (int position = 0; position < currentAxiom.Length; position++)
        {
            int selectorSize = currentAxiom.Length - position;
            Rule matchedRule = null;
            while (selectorSize > 0)
            {
                //Debug.Log("Looking for match at: " + position + " with length " + selectorSize);

                foreach (Rule rule in rules)
                {

                    if (currentAxiom.Substring(position, selectorSize) == rule.selector)
                    {
                        matchedRule = rule;
                        newAxiom += rule.result;
                        position += selectorSize - 1;
                        selectorSize = 0;

                        break;
                    }

                }

                selectorSize--;
            }
            if (matchedRule == null)
            {
                newAxiom += startingAxiom.Substring(position, 1);

                //Debug.Log(startingAxiom + " =>" + nextAxiom);

            }

        }
        axiom.text = newAxiom;

    }
}

[Serializable]
public class Rule : System.Object
{
    public string selector;
    public string result;
    public Rule(string selector, string result)
    {
        this.selector = selector;
        this.result = result;
    }
}