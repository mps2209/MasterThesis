using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public string[] selectors;
    public string[] results;
    List<Rule> rules = new List<Rule>();
    List<LSystemModel> systems = new List<LSystemModel>();

    public string startingAxiom;
    public Text axiom;
    public Text rulesText;
    public Text alphabetText;
    string initialAxiom;

    string alphabet = "";
    public string nextAxiom;
    int n = 0;
    void Start()
    {
        InitRules();
        InitTexts();
        initialAxiom = axiom.text;
    }

    void InitRules()
    {
        int i = 0;
        foreach (var item in selectors)
        {
            foreach (var letter in selectors[i])
            {
                if (!alphabet.Contains(letter.ToString()))
                {
                    alphabet += "\n" + letter;
                }
            }
            foreach (var letter in results[i])
            {
                if (!alphabet.Contains(letter.ToString()))
                {
                    alphabet += "\n" + letter;
                }
            }

            rules.Add(new Rule(selectors[i], results[i]));
            rulesText.text += $"{selectors[i]} => {results[i]} \n ";
            i++;
        }
    }
    void InitTexts()
    {


        alphabetText.text += alphabet;
        axiom.text = startingAxiom;
    }
    // Update is called once per frame
    void Update()
    {

    }


    public void StepForward()
    {
        if (n == 0)
        {
            InitRules();
            InitTexts();
        }
        n++;
        IterateAxiom();
    }
    public void StepBack()
    {
        int step = n - 1;
        n = 0;
        startingAxiom = initialAxiom;
        if (step != 0)
        {
            while (n < step)
            {
                StepForward();

            }
        }
        else
        {
            axiom.text = startingAxiom;
        }


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

    public void AddStep(string letter)
    {
        if (startingAxiom.Contains(letter))
        {
            StepForward();
        }
        else if (startingAxiom == "")
        {
            startingAxiom += letter + "B";
            rules.Add(new Rule(letter + "B", letter + letter + "B"));
            UpdateRules();
            InitTexts();
        }
    }
    void UpdateRules()
    {
        this.selectors = new string[rules.Count];
        this.results = new string[rules.Count];
        int i = 0;
        foreach(Rule rule in rules)
        {
            selectors[i] = rule.selector;
            results[i] = rule.result;
            rulesText.text += $"{rule.selector} => {rule.result} \n ";
            foreach (var letter in rule.selector)
            {
                if (!alphabet.Contains(letter.ToString()))
                {
                    alphabet += "\n" + letter;
                }
            }
            foreach (var letter in rule.result)
            {
                if (!alphabet.Contains(letter.ToString()))
                {
                    alphabet += "\n" + letter;
                }
            }
            i++;
        }

    }
    public void ApplyRules()
    {
    }
    public void ApplyRule(Rule rule)
    {

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
[Serializable]
public class LSystemModel : System.Object
{
    public string id;
    Rule[] rules;
    public string startingAxiom;

    public LSystemModel(List<Rule> rules)
    {
        this.rules = rules.ToArray();
        Guid g = Guid.NewGuid();
        this.id = g.ToString();
    }
}
