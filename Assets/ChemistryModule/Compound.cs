using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compound {

    public string Name { get; private set; }
    public string SingleLineName { get; private set; }
    public string Formula { get; private set; }
    public string LogFormula { get; private set; }
    public int NameTextSize { get; private set; }
    public int FormulaTextSize { get; private set; }
    public State DefState { get; private set; }
    public decimal UnitsPerMole { get; private set; }
    public Render RenderType { get; private set; }
    public Color RenderColor { get; private set; }

    public Compound(string name, string singleLineName, string formula, string logFormula, int textSizeName, int textSizeFormula, State defState, decimal unitsPerMole, Render renderType, Color renderColor) {
        Name = name;
        SingleLineName = singleLineName;
        Formula = formula;
        LogFormula = logFormula;
        NameTextSize = textSizeName;
        FormulaTextSize = textSizeFormula;
        DefState = defState;
        UnitsPerMole = unitsPerMole;
        RenderType = renderType;
        RenderColor = renderColor;
    }

    public Compound(string name, string singleLineName, string formula, string logFormula, int textSizeName, int textSizeFormula, State defState, decimal unitsPerMole) {
        Name = name;
        SingleLineName = singleLineName;
        Formula = formula;
        LogFormula = logFormula;
        NameTextSize = textSizeName;
        FormulaTextSize = textSizeFormula;
        DefState = defState;
        UnitsPerMole = unitsPerMole;
        RenderType = defState == State.Solid ? Render.Powder : defState == State.Liquid ? Render.TransLit : Render.None;
        RenderColor = defState == State.Solid ? new Color(1, 1, 1) : defState == State.Liquid ? new Color(1, 1, 1, .2f) : new Color(1, 1, 1, 0);
    }

    public string GetLogString() { return SingleLineName + " [" + LogFormula + "]"; }

    public void Display(TextMesh display, bool showFormula) {
        display.text = showFormula ? Formula : Name;
        display.fontSize = showFormula ? FormulaTextSize : NameTextSize;
    }

    public string GetUnit() {
        switch (DefState) {
            case State.Solid: return "g";
            case State.Liquid: return "ml";
            case State.Gas: return "l";
        }
        return "g"; //fallback
    }

    public enum State {
        Solid,
        Liquid,
        Gas
    }

    public enum Render {
        Powder,
        TransLit,
        TransUnlit,
        None
    }

}
