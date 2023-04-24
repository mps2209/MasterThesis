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
    string startingAxiom = "SAB";
    string initalAxiom = "SAB";

    string nextAxiom;
    public int previousSections;
    public int nextBranchSections;

    public int growthRateNominator;
    public int growthRateDenominator;
    public int branchOffRate;
    public int step = 0;
    BranchRenderer branchRenderer;
    // Start is called before the first frame update
    void Start()
    {
        branchRenderer = GameObject.Find("Tree").GetComponent<BranchRenderer>();
        axiom.text = startingAxiom;

        rules.Add(new Rule("AB", "AAB"));

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
            step++;


        }
        else
        {
            IterateAxiom();
            step++;

        }
    }
    public void StepTo(int toStep)
    {
        Debug.Log("Stepping" + toStep + " from " + step);
        toStep = step + toStep;

        if (toStep < step)
        {
            step = 0;
        }

        while (toStep > step)
        {
            StepForward();
        }

    }
    public void StepBackTo(int toStep)
    {
        step = 1;
        if (toStep == 1)
        {

            axiom.text = initalAxiom;

        }
        else
        {
            axiom.text = initalAxiom;
            while (step < toStep)
            {
                StepForward();
            }
        }



    }
    public void StepBack()
    {
        step--;

        if (step >= 1)
        {
            if (step == 1)
            {

                axiom.text = initalAxiom;

            }
            else
            {

                axiom.text = initalAxiom;
                int n = 1;
                while (n < step)
                {
                    StepForward();
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
    public void AddBranchRule(char previousBranch, char nextBranch, int previousSections, int nextBranchSections, int growthRateNominator, int growthRateDenominator, int branchOffRate)
    {
        this.previousSections = previousSections;
        this.nextBranchSections = nextBranchSections;
        this.growthRateDenominator = growthRateDenominator;
        this.growthRateNominator = growthRateNominator;
        this.branchOffRate = branchOffRate;
        //FirstBranch
        rules.Add(new Rule("S" + new string(previousBranch, previousSections + 1), "S" +
            new string(previousBranch, previousSections) + "[CB]" + previousBranch));
        //NextBranches
        rules.Add(new Rule("]" + new string(previousBranch, nextBranchSections + 1), "]" +
        new string(previousBranch, nextBranchSections) + "[CB]" + previousBranch));
        rules.Add(new Rule(nextBranch + "B", nextBranch + "BB"));
        rules.Add(new Rule(nextBranch + new string('B', growthRateDenominator),
            new string(nextBranch, growthRateNominator + 1) + "B"));
        rules.Add(new Rule(new string(nextBranch, branchOffRate + 1),
            new string(nextBranch, branchOffRate) + "[CB]" + nextBranch));


    }

    public void UpdateRules()
    {
        rules = new List<Rule>();
        rules.Add(new Rule("AB", "AAB"));

        AddBranchRule('A', 'C', previousSections, nextBranchSections, growthRateNominator, growthRateDenominator, branchOffRate);
        StepTo(-1);
        StepForward();
        branchRenderer.RenderTree();
    }
    public void UpdateRulesNoBranch()
    {
        rules = new List<Rule>();
        rules.Add(new Rule("AB", "AAB"));
        StepTo(-1);
        StepForward();
        branchRenderer.RenderTree();
    }
    public void IncreaseBranchOffRate()
    {
        if (branchOffRate > 1)
        {
            this.branchOffRate--;
            this.UpdateRules();
        }
    }
    public void DecreaseBranchOffRate()
    {


        this.branchOffRate++;
        this.UpdateRules();

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