using UnityEngine.UIElements;

public interface IDamageable : IBindable
{
    int Health { get; set; }
    float Cooldown { get; set; }

    void ApplyDamage(int damage);


}