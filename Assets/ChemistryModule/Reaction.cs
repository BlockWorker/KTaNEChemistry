using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Reaction {

    public Compound[] Reagents { get; private set; }
    public int[] ReagentAmounts { get; private set; }
    public Compound Product { get; private set; }
    public int ProductAmount { get; private set; }
    public Compound Solvent { get; private set; }

    private int addReagentIndex = 0;

    public Reaction(Compound _product, int _productAmount, Compound _solvent, int reagentCount) {
        Product = _product;
        ProductAmount = _productAmount;
        Solvent = _solvent;
        Reagents = new Compound[reagentCount];
        ReagentAmounts = new int[reagentCount];
    }

    public void AddReagent(Compound reagent, int amount) {
        if (addReagentIndex >= Reagents.Length) return;
        Reagents[addReagentIndex] = reagent;
        ReagentAmounts[addReagentIndex++] = amount;
    }

    public decimal GetReagentAmount(Compound c, int productMass) {
        decimal prodMols = Decimal.Round(productMass / (ProductAmount * Product.UnitsPerMole), 3, MidpointRounding.AwayFromZero);
        for (int i = 0; i < Reagents.Length; i++) {
            if (Reagents[i] == c) {
                return Decimal.Round(prodMols * ReagentAmounts[i] * Reagents[i].UnitsPerMole, 3, MidpointRounding.AwayFromZero);
            }
        }
        return 0m;
    }
}
