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
    string initalAxiom = "AB";

    string nextAxiom;

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
            
            axiom.text = initalAxiom;
            rules.Add(new Rule("AB", "AAB"));
            step++;

        }
        else
        {
            step++;
            IterateAxiom();
        }
    }
    public void StepBack()
    {
        if (step > 1)
        {
            step--;
            if (step == 1)
            {

                axiom.text = initalAxiom;
                rules.Add(new Rule("AB", "AAB"));
             

            }
            else
            {

                axiom.text = initalAxiom;
                int n = 1;
                while (n < step)
                {
                    IterateAxiom();
                    n++;
                }
            }

        }

    }
    public int Step()
    {
        return step;
    }
    void IterateAxiom()
    {
        nextAxiom = "";
        startingAxiom = axiom.text;
        for (int position = 0; position < startingAxiom.Length; position++)
        {
            int selectorSize = startingAxiom.Length - position;
            Rule matchedRule = null;
            while (selectorSize > 0)
            {
                //Debug.Log("Looking for match at: " + position + " with length " + selectorSize);

                foreach (Rule rule in rules)
                {

                    if (startingAxiom.Substring(position, selectorSize) == rule.selector)
                    {
                        matchedRule = rule;
                        //Debug.Log("Found match at: " + position + " with length " + selectorSize);

                        //Debug.Log(startingAxiom + " =>" + nextAxiom + " + " + matchedRule.selector + "=>" + matchedRule.result);

                        nextAxiom += rule.result;
                        position += selectorSize - 1;
                        selectorSize = 0;

                        break;
                    }

                }

                selectorSize--;
            }
            if (matchedRule == null)
            {
                nextAxiom += startingAxiom.Substring(position, 1);

                //Debug.Log(startingAxiom + " =>" + nextAxiom);

            }

        }
        axiom.text = nextAxiom;
    }
    public void AddBranchRule(char previousBranch,char nextBranch, int previousSections, int growthRatenominator,int growthRateDenominator)
    {
        rules.Add(new Rule(new string(previousBranch, previousSections + 1),
            new string(previousBranch, previousSections) + "[CB]"+ previousBranch));
        rules.Add(new Rule(nextBranch+"B", nextBranch+ "BB"));
        rules.Add(new Rule(nextBranch + new string('B', growthRateDenominator),
            new string(nextBranch, growthRatenominator+1) +"B"));



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