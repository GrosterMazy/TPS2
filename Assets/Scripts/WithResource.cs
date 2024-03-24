using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WithResource : MonoBehaviour {
    // -1 = infinite resources
    public int amount;
    public int amountRemain;

    void Start() {
        this.amountRemain = this.amount;
    }

    void Update() {
        if (this.amountRemain == 0)
            Destroy(this.gameObject.GetComponent<WithResource>());
    }

    public int TakeResource(int amount) {
        if (amount >= this.amountRemain && this.amount != -1) {
            int tempAmount = this.amountRemain;
            this.amountRemain = 0;
            return tempAmount;
        }
        if (this.amount != -1)
            this.amountRemain -= amount;
        return amount;
    }
}
