using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KModkit;
using System.Linq;

public class ChemistryModule : MonoBehaviour {

    public MeshRenderer VesselContents;
    public KMSelectable Switch;
    public Transform SolidDispenser, SolidExt, LiquidDispenser, LiquidExt, GasDispenser, GasExt, DispenserControls;
    public KMSelectable TypeUpButton, TypeDownButton, A0Button, A1Button, A2Button, A3Button, A4Button, A5Button, A6Button, A7Button, A8Button, A9Button;
    public TextMesh TypeText, AmountText;
    public GameObject TypeGroup, AmountGroup;
    public KMSelectable ConfirmButton, NextDispButton, PrevDispButton, ResetButton;
    public MeshRenderer PrevIndicator, NextIndicator;
    public Light PrevIndLight, NextIndLight;
    public Material SolidIndMat, LiquidIndMat, GasIndMat;
    public TextMesh ReadText;
    public Shader VCPowder, VCLitTrans, VCUnlitTrans;
    public Texture2D PowderTex;
    public KMAudio Audio;
    public KMBombInfo Info;
    public KMBombModule Module;

    #region Static definitions
    private static Compound[] solids = new Compound[] {
        new Compound("Potassium iodate", "Potassium iodate", "KIO₃", "KIO3", 100, 150, Compound.State.Solid, 214.1m),
        new Compound("Sodium dithionite", "Sodium dithionite", "Na₂S₂O₄", "Na2S2O4", 100, 150, Compound.State.Solid, 174.2m),
        new Compound("2,4,6-Trichlorophenol", "2,4,6-Trichlorophenol", "C₆H₂Cl₃OH", "C6H2Cl3OH", 80, 150, Compound.State.Solid, 197.53m, Compound.Render.Powder, new Color(1, 1, .8f)),
        new Compound("2-Aminobenzoic acid", "2-Aminobenzoic acid", "C₆H₄(NH₂)(COOH)", "C6H4(NH2)(COOH)", 90, 100, Compound.State.Solid, 137.07m, Compound.Render.Powder, new Color(1, 1, .7f)),
        new Compound("Propanedioic acid", "Propanedioic acid", "CH₂(COOH)₂", "CH2(COOH)2", 100, 150, Compound.State.Solid, 104.04m),
        new Compound("Manganese dichloride", "Manganese dichloride", "MnCl₂", "MnCl2", 80, 150, Compound.State.Solid, 125.9m, Compound.Render.Powder, new Color(1, .8f, .85f)),
        new Compound("2-Hydroxybenzoic acid", "2-Hydroxybenzoic acid", "C₆H₄(OH)COOH", "C6H4(OH)COOH", 80, 120, Compound.State.Solid, 138.06m),
        new Compound("Urania", "Urania", "U₃O₈", "U3O8", 150, 150, Compound.State.Solid, 842m, Compound.Render.Powder, new Color(.2f, .3f, 0)),
        new Compound("Benzene-3-nitro-\n1,2-dicarboxylic acid", "Benzene-3-nitro-1,2-dicarboxylic acid", "C₆H₃(COOH)₂NO₂", "C6H3(COOH)2NO2", 65, 100, Compound.State.Solid, 211.05m, Compound.Render.Powder, new Color(1, 1, .9f)),
        new Compound("Sodium nitrite", "Sodium nitrite", "NaNO₂", "NaNO2", 120, 150, Compound.State.Solid, 69m),
        new Compound("2-Benzofuran-1,3-dione", "2-Benzofuran-1,3-dione", "C₆H₄(CO)₂O", "C6H4(CO)2O", 80, 150, Compound.State.Solid, 148.04m),
        new Compound("Phenol", "Phenol", "C₆H₅OH", "C6H5OH", 150, 150, Compound.State.Solid, 94.06m)
    };
    private static Compound[] liquids = new Compound[] {
        new Compound("Toluene", "Toluene", "C₆H₅CH₃", "C6H5CH3", 150, 150, Compound.State.Liquid, 105.839m), //92.08m, 0.87d
        new Compound("Ethanol", "Ethanol", "C₂H₅OH", "C2H5OH", 150, 150, Compound.State.Liquid, 58.304m), //46.06m, 0.79d
        new Compound("Hydrogen peroxide", "Hydrogen peroxide", "H₂O₂", "H2O2", 100, 150, Compound.State.Liquid, 23.462m, Compound.Render.TransLit, new Color(.5f, .7f, 1, .3f)), //34.02m, 1.45d
        new Compound("Sulfuric acid", "Sulfuric acid", "H₂SO₄", "H2SO4", 120, 150, Compound.State.Liquid, 53.617m), //98.12m, 1.83d
        new Compound("Water", "Water", "H₂O", "H2O", 150, 150, Compound.State.Liquid, 18.02m), //18.02m, 1.00d
        new Compound("Triethylamine", "Triethylamine", "N(CH₂CH₃)₃", "N(CH2CH3)3", 120, 150, Compound.State.Liquid, 138.562m), //101.15m, 0.73d
        new Compound("Nitric acid", "Nitric acid", "HNO₃", "HNO3", 120, 150, Compound.State.Liquid, 41.728m), //63.01m, 1.51d
        new Compound("Dimethylaniline", "Dimethylaniline", "C₆H₅N(CH₃)₂", "C6H5N(CH3)2", 100, 120, Compound.State.Liquid, 126.156m), //121.11m, 0.96d
        new Compound("Hydrazine", "Hydrazine", "H₂NNH₂", "H2NNH2", 150, 150, Compound.State.Liquid, 31.412m), //32.04m, 1.02d
        new Compound("Acetic anhydride", "Acetic anhydride", "(CH₃CO)₂O", "(CH3CO)2O", 100, 150, Compound.State.Liquid, 94.5m), //102.06m, 1.08d
        new Compound("Oxalyl dichloride", "Oxalyl dichloride", "(COCl)₂", "(COCl)2", 100, 150, Compound.State.Liquid, 85.811m), //127m, 1.48d
        new Compound("Propane-1,2,3-triol", "Propane-1,2,3-triol", "C₃H₅(OH)₃", "C3H5(OH)3", 80, 150, Compound.State.Liquid, 73.079m) //92.08m, 1.26d
    };
    private static Compound[] gases = new Compound[] {
        new Compound("Hydrogen chloride", "Hydrogen chloride", "HCl", "HCl", 100, 150, Compound.State.Gas, 24.5m),
        new Compound("Ammonia", "Ammonia", "NH₃", "NH3", 150, 150, Compound.State.Gas, 24.5m),
        new Compound("Hydrogen", "Hydrogen", "H₂", "H2", 150, 150, Compound.State.Gas, 24.5m),
        new Compound("Hydrogen fluoride", "Hydrogen fluoride", "HF", "HF", 100, 150, Compound.State.Gas, 24.5m),
        new Compound("Fluorine", "Fluorine", "F₂", "F2", 150, 150, Compound.State.Gas, 24.5m)
    };
    //Briggs-Rauscher colors: (.9f, .9f, .9f, .1f) -> (1, .6f, 0, .4f) => (0, 0, .15f, 1) -> (.9f, .9f, .9f, .1f)
    private static Compound[] products = new Compound[] {
        new Compound("", "Luminol", "C₆H₃(CONH)₂(NH₂)", "C6H3(CONH)2(NH2)", 0, 0, Compound.State.Solid, 177.07m, Compound.Render.TransUnlit, new Color(0, .8f, 1, .7f)),
        new Compound("", "Nitroglycerin", "C₃H₅(ONO₂)₃", "C3H5(ONO2)3", 0, 0, Compound.State.Solid, 227.05m, Compound.Render.TransLit, new Color(.9f, .9f, .5f, .2f)),
        new Compound("", "Phenolphthalein", "C₆H₄(CO)C(C₆H₄OH)₂O", "C6H4(CO)C(C6H4OH)2O", 0, 0, Compound.State.Solid, 318.14m, Compound.Render.TransLit, new Color(1, 0, .6f, .5f)),
        new Compound("", "Methyl red", "C₆H₄(COOH)(N₂)C₆H₄N(CH₃)₂", "C6H4(COOH)(N2)C6H4N(CH3)2", 0, 0, Compound.State.Solid, 269.15m, Compound.Render.TransLit, new Color(1, 0, 0, .5f)),
        new Compound("", "Uranium hexafluoride", "UF₆", "UF6", 0, 0, Compound.State.Solid, 352m, Compound.Render.TransLit, new Color(.9f, .9f, .9f, .9f)),
        new Compound("", "Aspirin", "C₆H₄(CH₃CO)(COOH)O", "C6H4(CH3CO)(COOH)O", 0, 0, Compound.State.Solid, 180.08m, Compound.Render.TransLit, new Color(.6f, .6f, .9f, .2f)),
        new Compound("", "TCPO", "(COOC₆H₂Cl₃)₂", "(COOC6H2Cl3)2", 0, 0, Compound.State.Solid, 449.04m, Compound.Render.TransUnlit, new Color(0, 1, .4f, .7f)),
        new Compound("", "Briggs-Rauscher reaction", solids[0].Formula, solids[0].LogFormula, 0, 0, Compound.State.Solid, solids[0].UnitsPerMole, Compound.Render.TransLit, new Color(1, .6f, 0, .4f))
    };
    private static Reaction[] reactions = new Reaction[] {
        new Reaction(products[0], 1, liquids[11], 3),   //Luminol
        new Reaction(products[1], 1, null, 3),          //Nitroglycerin
        new Reaction(products[2], 1, liquids[4], 3),    //Phenolpthalein
        new Reaction(products[3], 1, liquids[1], 4),    //Methyl red
        new Reaction(products[4], 3, liquids[6], 5),    //3 UF6
        new Reaction(products[5], 1, liquids[3], 2),    //Aspirin
        new Reaction(products[6], 1, liquids[0], 3),    //TCPO
        new Reaction(products[7], 1, liquids[3], 4)       //Briggs-Rauscher reaction
    };
    private static bool reagentsDefined = false;
    private static Color[] readColors = {
        new Color(1, 0, 0),
        new Color(.2f, .2f, 1),
        new Color(0, 1, 0),
        new Color(1, 1, 0),
        new Color(0, 1, 1),
        new Color(1, .5f, 0),
        new Color(1, 0, 1),
        new Color(1, 1, 1)
    };
    private static string[] readColorLogs = {
        "red",
        "blue",
        "green",
        "yellow",
        "cyan",
        "orange",
        "magenta",
        "white"
    };
    private static int[] readColorValues = { 14, 22, 3, 48, 37, 19, 25, 50 };
    private static Color colorless = new Color(1, 1, 1, 0);
    #endregion
    private static int globalLogNum = 1;

    private int logNum;
    private Compound.State dispMode = Compound.State.Solid, prevMode = Compound.State.Gas, nextMode = Compound.State.Liquid;
    private Animations anim;
    private int dispSwitchFollowup = 0;
    private int selectedIndex;
    private int selectedCompoundIndex;
    private Compound selectedCompound;
    private bool formulaShowSelected;
    private bool[] formulaShow = new bool[solids.Length + liquids.Length + gases.Length];
    private char readChar;
    private int readColor;
    private Reaction chosenReaction;
    private int productAmount;

    private bool amountMode = false;
    private decimal userInput = 0;
    private List<Compound> addedCompounds = new List<Compound>();
    private List<decimal> addedAmounts = new List<decimal>();
    private decimal solventAdded = 0m;
    private Compound[] visibleContent = new Compound[14];
    private int visSolidIndex = 0, visLiquidIndex = 0;
    private bool solved = false;

    #region Unity Base Methods
    // Use this for initialization
    void Start () {
        logNum = globalLogNum++;

        //Heater buttons
        Switch.OnInteract += delegate { SwitchPress(); return false; };

        //Dispenser buttons
        TypeDownButton.OnInteract += delegate { TypeButtonPress(false); return false; };
        TypeUpButton.OnInteract += delegate { TypeButtonPress(true); return false; };
        A0Button.OnInteract += delegate { AmountButtonPress(0); return false; };
        A1Button.OnInteract += delegate { AmountButtonPress(1); return false; };
        A2Button.OnInteract += delegate { AmountButtonPress(2); return false; };
        A3Button.OnInteract += delegate { AmountButtonPress(3); return false; };
        A4Button.OnInteract += delegate { AmountButtonPress(4); return false; };
        A5Button.OnInteract += delegate { AmountButtonPress(5); return false; };
        A6Button.OnInteract += delegate { AmountButtonPress(6); return false; };
        A7Button.OnInteract += delegate { AmountButtonPress(7); return false; };
        A8Button.OnInteract += delegate { AmountButtonPress(8); return false; };
        A9Button.OnInteract += delegate { AmountButtonPress(9); return false; };
        ConfirmButton.OnInteract += delegate { ConfirmButtonPress(); return false; };

        //Module buttons
        PrevDispButton.OnInteract += delegate { DispenserButtonPress(false); return false; };
        NextDispButton.OnInteract += delegate { DispenserButtonPress(true); return false; };
        ResetButton.OnInteract += delegate { ResetButtonPress(); return false; };

        Module.OnActivate += OnActivate;

        anim = new Animations(DispenserControls);

        if (!reagentsDefined) DefineReagents();
        Init();
    }
	
	// Update is called once per frame
	void Update () {
        anim.UpdateAnimation();

        if (anim.AnimationDone && dispSwitchFollowup > 0) {
            if (dispSwitchFollowup == 1) {
                dispMode = prevMode;
            } else {
                dispMode = nextMode;
            }
            DeployDispenser();

            amountMode = false;
            AmountGroup.SetActive(false);
            TypeGroup.SetActive(true);
            userInput = 0;

            dispSwitchFollowup = 0;
        }
    }
    #endregion

    void Init() {
        for (int i = 0; i < formulaShow.Length; i++) formulaShow[i] = Random.value < .5 ? true : false;
        AmountGroup.SetActive(false);

        readChar = (char)Random.Range(65, 91);
        readColor = Random.Range(0, 8);
        ReadText.text = "" + readChar;
        ReadText.color = readColors[readColor];
        DebugLog("Displayed letter is " + readColorLogs[readColor] + " " + readChar);

        int serialSum = KMBombInfoExtensions.GetSerialNumberNumbers(Info).Sum();
        int reactionNum = readChar - 65;
        if (reactionNum > serialSum) reactionNum -= serialSum;
        reactionNum %= 8;
        chosenReaction = reactions[reactionNum];

        productAmount = readColorValues[readColor];
        productAmount += KMBombInfoExtensions.GetOffIndicators(Info).Count();
        int portCount = KMBombInfoExtensions.GetPortCount(Info);
        if (portCount > 1) productAmount *= portCount;
        productAmount %= 50;
        if (productAmount == 0) productAmount = 50;
        productAmount *= 10;
        productAmount -= KMBombInfoExtensions.GetOnIndicators(Info).Count();
        if (productAmount <= 0) {
            DebugLog("That's a lot of lit indicators. I guess you deserve this.");
            SolveModule();
            return;
        }

        if (chosenReaction.Solvent == null) solventAdded = 100.000m;

        DebugLog("Reaction: " + chosenReaction.Product.GetLogString());
        DebugLog("Product amount: " + productAmount + "g");
        DebugLog("Solvent: " + (chosenReaction.Solvent == null ? "None" : chosenReaction.Solvent.GetLogString()));
        DebugLog("Required reagents:");
        foreach (Compound r in chosenReaction.Reagents) {
            DebugLog(" - " + chosenReaction.GetReagentAmount(r, productAmount) + r.GetUnit() + " of " + r.GetLogString());
        }
        DebugLog("----------------------------------");
    }

    void OnActivate() {
        DeployDispenser();
    }

    #region Helpers
    void DefineReagents() {
        //Luminol
        reactions[0].AddReagent(solids[8], 1);
        reactions[0].AddReagent(liquids[8], 1);
        reactions[0].AddReagent(solids[1], 1);
        //Nitroglycerin
        reactions[1].AddReagent(liquids[11], 1);
        reactions[1].AddReagent(liquids[6], 3);
        reactions[1].AddReagent(liquids[3], 3);
        //Phenolphthalein
        reactions[2].AddReagent(solids[10], 1);
        reactions[2].AddReagent(solids[11], 2);
        reactions[2].AddReagent(liquids[3], 1);
        //Methyl red
        reactions[3].AddReagent(solids[3], 1);
        reactions[3].AddReagent(gases[0], 1);
        reactions[3].AddReagent(solids[9], 1);
        reactions[3].AddReagent(liquids[7], 1);
        //3 UF6
        reactions[4].AddReagent(solids[7], 1);
        reactions[4].AddReagent(gases[1], 6);
        reactions[4].AddReagent(gases[2], 15);
        reactions[4].AddReagent(gases[3], 12);
        reactions[4].AddReagent(gases[4], 3);
        //Aspirin
        reactions[5].AddReagent(solids[6], 1);
        reactions[5].AddReagent(liquids[9], 1);
        //TCPO
        reactions[6].AddReagent(solids[2], 1);
        reactions[6].AddReagent(liquids[10], 1);
        reactions[6].AddReagent(liquids[5], 1);
        //Briggs-Rauscher reaction
        reactions[7].AddReagent(solids[0], 1);
        reactions[7].AddReagent(liquids[2], 2);
        reactions[7].AddReagent(solids[4], 1);
        reactions[7].AddReagent(solids[5], 1);

        reagentsDefined = true;
    }

    void DeployDispenser() {
        switch (dispMode) {
            case Compound.State.Solid:
                SolidDispenser.gameObject.SetActive(true);
                LiquidDispenser.gameObject.SetActive(false);
                GasDispenser.gameObject.SetActive(false);
                anim.StartAnimation(Animations.Animation.SolidExtend, SolidDispenser, SolidExt);
                PrevIndicator.material = GasIndMat;
                NextIndicator.material = LiquidIndMat;
                PrevIndLight.color = new Color(1, 1, 0);
                NextIndLight.color = new Color(0, 0, 1);
                prevMode = Compound.State.Gas;
                nextMode = Compound.State.Liquid;
                selectedCompoundIndex = Random.Range(0, solids.Length);
                selectedCompound = solids[selectedCompoundIndex];
                formulaShowSelected = formulaShow[selectedCompoundIndex];
                break;
            case Compound.State.Liquid:
                SolidDispenser.gameObject.SetActive(false);
                LiquidDispenser.gameObject.SetActive(true);
                GasDispenser.gameObject.SetActive(false);
                anim.StartAnimation(Animations.Animation.LiquidExtend, LiquidDispenser, LiquidExt);
                PrevIndicator.material = SolidIndMat;
                NextIndicator.material = GasIndMat;
                PrevIndLight.color = new Color(1, 0, 0);
                NextIndLight.color = new Color(1, 1, 0);
                prevMode = Compound.State.Solid;
                nextMode = Compound.State.Gas;
                selectedCompoundIndex = Random.Range(0, liquids.Length);
                selectedCompound = liquids[selectedCompoundIndex];
                formulaShowSelected = formulaShow[solids.Length + selectedCompoundIndex];
                break;
            case Compound.State.Gas:
                SolidDispenser.gameObject.SetActive(false);
                LiquidDispenser.gameObject.SetActive(false);
                GasDispenser.gameObject.SetActive(true);
                anim.StartAnimation(Animations.Animation.GasExtend, GasDispenser, GasExt);
                PrevIndicator.material = LiquidIndMat;
                NextIndicator.material = SolidIndMat;
                PrevIndLight.color = new Color(0, 0, 1);
                NextIndLight.color = new Color(1, 0, 0);
                prevMode = Compound.State.Liquid;
                nextMode = Compound.State.Solid;
                selectedCompoundIndex = Random.Range(0, gases.Length);
                selectedCompound = gases[selectedCompoundIndex];
                formulaShowSelected = formulaShow[solids.Length + liquids.Length + selectedCompoundIndex];
                break;
        }

        selectedCompound.Display(TypeText, formulaShowSelected);
    }

    void UpdateAmountDisplay() {
        string s = userInput.ToString("000.000") + " ";
        switch (dispMode) {
            case Compound.State.Solid:
                s += "g";
                break;
            case Compound.State.Liquid:
                s += "ml";
                break;
            case Compound.State.Gas:
                s += "l";
                break;
        }
        AmountText.text = s;
    }

    void DebugLog(string message) {
        Debug.Log("[Chemical Reactions #" + logNum + "] " + message);
    }

    void UpdateVesselContents() {
        for (int i = 0; i < 14; i++) {
            if (visibleContent[i] == null) {
                VesselContents.materials[i].shader = VCUnlitTrans;
                VesselContents.materials[i].mainTexture = null;
                VesselContents.materials[i].color = colorless;
                if (i == 13) break;
                VesselContents.materials[i + 14].shader = VCUnlitTrans;
                VesselContents.materials[i + 14].mainTexture = null;
                VesselContents.materials[i + 14].color = colorless;
            } else {
                bool showInternal = i < 13;
                if (showInternal) {
                    if (visibleContent[i + 1] == null) showInternal = true;
                    else showInternal = (visibleContent[i].RenderType != visibleContent[i + 1].RenderType ||
                                         visibleContent[i].RenderColor != visibleContent[i + 1].RenderColor);
                }

                switch (visibleContent[i].RenderType) {
                    case Compound.Render.Powder:
                        VesselContents.materials[i].shader = VCPowder;
                        VesselContents.materials[i].mainTexture = PowderTex;
                        if (showInternal) {
                            VesselContents.materials[i + 14].shader = VCPowder;
                            VesselContents.materials[i + 14].mainTexture = PowderTex;
                        }
                        break;
                    case Compound.Render.TransLit:
                        VesselContents.materials[i].shader = VCLitTrans;
                        VesselContents.materials[i].mainTexture = null;
                        if (showInternal) {
                            VesselContents.materials[i + 14].shader = VCLitTrans;
                            VesselContents.materials[i + 14].mainTexture = null;
                        }
                        break;
                    case Compound.Render.TransUnlit:
                        VesselContents.materials[i].shader = VCUnlitTrans;
                        VesselContents.materials[i].mainTexture = null;
                        if (showInternal) {
                            VesselContents.materials[i + 14].shader = VCUnlitTrans;
                            VesselContents.materials[i + 14].mainTexture = null;
                        }
                        break;
                }
                VesselContents.materials[i].color = visibleContent[i].RenderColor;
                if (showInternal) VesselContents.materials[i + 14].color = visibleContent[i].RenderColor;
                else if (i < 13) {
                    VesselContents.materials[i + 14].shader = VCUnlitTrans;
                    VesselContents.materials[i + 14].mainTexture = null;
                    VesselContents.materials[i + 14].color = colorless;
                }
            }
        }
    }

    void ResetModule() {
        amountMode = false;
        AmountGroup.SetActive(false);
        TypeGroup.SetActive(true);
        userInput = 0;

        addedCompounds.Clear();
        addedAmounts.Clear();
        solventAdded = chosenReaction.Solvent == null ? 100.000m : 0m;
        for (int i = 0; i < 14; i++) visibleContent[i] = null;
        visSolidIndex = 0;
        visLiquidIndex = 0;
        UpdateVesselContents();
    }

    void SolveModule() {
        amountMode = false;
        AmountGroup.SetActive(false);
        TypeGroup.SetActive(true);
        userInput = 0;

        addedCompounds.Clear();
        addedAmounts.Clear();
        solventAdded = 0;

        visibleContent = new Compound[14];
        visSolidIndex = 0;
        for (int i = 0; i < visLiquidIndex; i++) {
            visibleContent[i] = chosenReaction.Product;
        }
        visLiquidIndex = 0;
        UpdateVesselContents();

        TypeText.text = "Successful reaction.";
        TypeText.fontSize = 90;

        DebugLog("Reaction correctly started. Module solved.");
        Module.HandlePass();
        solved = true;
    }
    #endregion

    #region Interaction Handlers
    void SwitchPress() {
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.BigButtonPress, transform);
        Switch.AddInteractionPunch();

        if (solved) return;

        foreach (Compound r in chosenReaction.Reagents) {
            if (solventAdded < 100.000m) { //Missing solvent, nothing inputted.
                Module.HandleStrike();
                DebugLog("Not enough solvent was added (" + solventAdded + chosenReaction.Solvent.GetUnit() + "), strike. Vessel contents reset.");
                ResetModule();
                return;
            } if (!addedCompounds.Contains(r)) { //Missing an ingredient.
                Module.HandleStrike();
                DebugLog("Missing reagent " + r.GetLogString() + ". Strike, vessel contents reset.");
                ResetModule();
                return;
            } else if (addedAmounts[addedCompounds.IndexOf(r)] != chosenReaction.GetReagentAmount(r, productAmount)) { //Incorrect amount.
                Module.HandleStrike();
                DebugLog("Incorrect amount " + addedAmounts[addedCompounds.IndexOf(r)] + r.GetUnit() + " for reagent " + r.GetLogString() + ". Strike, vessel contents reset.");
                ResetModule();
                return;
            }
        }
        SolveModule();
        Switch.transform.Rotate(-24, 0, 0, Space.Self);
    }

    void TypeButtonPress(bool up) {
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.BigButtonPress, transform);

        if (solved) return;

        Compound[] options;
        switch (dispMode) {
            case Compound.State.Solid:
                options = solids;
                break;
            case Compound.State.Liquid:
                options = liquids;
                break;
            case Compound.State.Gas:
                options = gases;
                break;
            default:
                options = solids; //failsafe def
                break;
        }

        selectedCompoundIndex += up ? 1 : -1;
        if (selectedCompoundIndex < 0) selectedCompoundIndex += options.Length;
        if (selectedCompoundIndex >= options.Length) selectedCompoundIndex = 0;
        selectedCompound = options[selectedCompoundIndex];

        switch (dispMode) {
            case Compound.State.Solid:
                formulaShowSelected = formulaShow[selectedCompoundIndex];
                break;
            case Compound.State.Liquid:
                formulaShowSelected = formulaShow[selectedCompoundIndex + solids.Length];
                break;
            case Compound.State.Gas:
                formulaShowSelected = formulaShow[selectedCompoundIndex + solids.Length + liquids.Length];
                break;
        }

        selectedCompound.Display(TypeText, formulaShowSelected);
    }

    void AmountButtonPress(int value) {
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.BigButtonPress, transform);
        userInput *= 10m;
        userInput += value / 1000m;
        userInput = decimal.Round(userInput % 1000m, 3);
        UpdateAmountDisplay();
    }

    void ConfirmButtonPress() {
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.BigButtonPress, transform);

        if (solved) return;

        if (amountMode) {
            amountMode = false;
            AmountGroup.SetActive(false);
            TypeGroup.SetActive(true);
            
            if (userInput > 0) {
                ConfirmButton.AddInteractionPunch(Mathf.Min((float)userInput / 2f, 2f));
                if (chosenReaction.Solvent == selectedCompound) {
                    solventAdded += userInput;
                    DebugLog("Added " + userInput + selectedCompound.GetUnit() + " of solvent. Total solvent amount is now " +
                                solventAdded + selectedCompound.GetUnit() + ".");
                    int slvLevel = (int)(7 * solventAdded / 100);
                    while (slvLevel > visLiquidIndex) {
                        visibleContent[visLiquidIndex++] = chosenReaction.Solvent;
                    }
                    UpdateVesselContents();
                } else if (solventAdded != 100.000m) {
                    Module.HandleStrike();
                    DebugLog("Tried to add " + selectedCompound.GetLogString() + " before adding exactly 100ml of the correct solvent (added solvent amount is " +
                                solventAdded + chosenReaction.Solvent.GetUnit() + "). Strike.");
                } else if (!chosenReaction.Reagents.Contains(selectedCompound)) { //if added compound isn't part of this reaction: strike
                    Module.HandleStrike();
                    DebugLog("Tried to add " + selectedCompound.GetLogString() + ", which is not part of the reaction. Strike.");
                } else if (addedCompounds.Contains(selectedCompound)) { //if compound was already added before: just increase added amount
                    addedAmounts[addedCompounds.IndexOf(selectedCompound)] += userInput;
                    DebugLog("Added " + userInput + selectedCompound.GetUnit() + " of " + selectedCompound.GetLogString() +
                             ". Total amount is now " + addedAmounts[addedCompounds.IndexOf(selectedCompound)] + selectedCompound.GetUnit() + ".");
                } else { //add new compound
                    addedCompounds.Add(selectedCompound);
                    addedAmounts.Add(userInput);
                    DebugLog("Added " + userInput + selectedCompound.GetUnit() + " of " + selectedCompound.GetLogString() + ".");
                    if (selectedCompound.DefState == Compound.State.Solid) {
                        visibleContent[visSolidIndex++] = selectedCompound;
                        if (visSolidIndex < 4) visibleContent[visSolidIndex++] = selectedCompound;
                    } else if (selectedCompound.DefState == Compound.State.Liquid) {
                        visibleContent[visLiquidIndex++] = selectedCompound;
                        if (visLiquidIndex < 4) visibleContent[visLiquidIndex++] = selectedCompound;
                    }
                    UpdateVesselContents();
                }
            }

            userInput = 0;
        } else {
            amountMode = true;
            TypeGroup.SetActive(false);
            AmountGroup.SetActive(true);
            UpdateAmountDisplay();
        }
    }

    void DispenserButtonPress(bool next) {
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.BigButtonPress, transform);
        if (!anim.AnimationDone) return;

        switch (dispMode) {
            case Compound.State.Solid:
                anim.StartAnimation(Animations.Animation.SolidRetract, SolidDispenser, SolidExt);
                break;
            case Compound.State.Liquid:
                anim.StartAnimation(Animations.Animation.LiquidRetract, LiquidDispenser, LiquidExt);
                break;
            case Compound.State.Gas:
                anim.StartAnimation(Animations.Animation.GasRetract, GasDispenser, GasExt);
                break;
        }

        dispSwitchFollowup = next ? 2 : 1;
    }

    void ResetButtonPress() {
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        ResetButton.AddInteractionPunch();

        if (solved) return;

        ResetModule();
        DebugLog("Reset button pressed, vessel contents cleared.");
    }
    #endregion
}
