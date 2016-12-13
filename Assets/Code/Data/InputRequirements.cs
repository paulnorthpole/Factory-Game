using System;

[Serializable]
public struct InputRequirements {
    public string inputType;
    public int inputCountPerProduct;
    public int maxStorage;
    public int partsReceived;
}

public static class InputRequirementExtensions {
    public static int TotalReceived(this InputRequirements[] requirements) {
        var count = 0;
        for(int i = 0; i < requirements.Length; i++) {
            count += requirements[i].partsReceived;
        }
        return count;
    }

    public static bool Satisfied(this InputRequirements[] requirements) {
        if(requirements.Length == 0) {
            return true;
        }
        for(int i = 0; i < requirements.Length; i++) {
            if(requirements[i].partsReceived < requirements[i].inputCountPerProduct) {
                return false;
            }
        }
        return true;
    }

    public static void DeductOneProduct(this InputRequirements[] requirements) {
        for(int i = 0; i < requirements.Length; i++) {
            requirements[i].partsReceived -= requirements[i].inputCountPerProduct;
        }
    }

    public static bool ConsumeProduct(this InputRequirements[] requirements, Product product) {
        for(int i = 0; i < requirements.Length; i++) {
            if(requirements[i].inputType == product.typeName) {
                if(requirements[i].partsReceived < requirements[i].maxStorage) {
                    requirements[i].partsReceived++;
                    return true;
                }
            }
        }
        return false;
    }

    public static bool HasSpaceFor(this InputRequirements[] requirements, Product p) {
        for(int i = 0; i < requirements.Length; i++) {
            if(requirements[i].inputType == p.typeName) {
                return requirements[i].partsReceived < requirements[i].maxStorage;
            }
        }
        return false;
    }
}