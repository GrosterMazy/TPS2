using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float speed;

    public float distance;
    private float _lifetime;

    protected virtual void DestroyBullet() {
        Destroy(this.gameObject);
    }

    protected virtual void Start() {
        this._lifetime = this.distance/this.speed;
        Invoke("DestroyBullet", this._lifetime);
    }

    protected virtual void FixedUpdate() {
        this.transform.position += this.transform.forward * this.speed * Time.fixedDeltaTime;
    }
}
